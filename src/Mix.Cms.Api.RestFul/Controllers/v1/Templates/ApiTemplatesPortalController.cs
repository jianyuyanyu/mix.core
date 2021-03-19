﻿// Licensed to the Mixcore Foundation under one or more agreements.
// The Mixcore Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mix.Cms.Lib.Constants;
using Mix.Cms.Lib.Controllers;
using Mix.Cms.Lib.Enums;
using Mix.Cms.Lib.Models.Cms;
using Mix.Cms.Lib.ViewModels.MixTemplates;
using Mix.Domain.Core.ViewModels;
using Mix.Domain.Data.Repository;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mix.Cms.Api.RestFul.Controllers.v1
{
    [Produces("application/json")]
    [Route("api/v1/rest/template/portal")]
    public class ApiTemplateController :
        BaseAuthorizedRestApiController<MixCmsContext, MixTemplate, UpdateViewModel, ReadViewModel, DeleteViewModel>
    {
        public ApiTemplateController(DefaultRepository<MixCmsContext, MixTemplate, ReadViewModel> repo, DefaultRepository<MixCmsContext, MixTemplate, UpdateViewModel> updRepo, DefaultRepository<MixCmsContext, MixTemplate, DeleteViewModel> delRepo) : base(repo, updRepo, delRepo)
        {
        }

        [HttpGet]
        public override async Task<ActionResult<PaginationModel<ReadViewModel>>> Get()
        {
            bool isStatus = Enum.TryParse(Request.Query[MixRequestQueryKeywords.Status], out MixContentStatus status);
            bool isFromDate = DateTime.TryParse(Request.Query[MixRequestQueryKeywords.FromDate], out DateTime fromDate);
            bool isToDate = DateTime.TryParse(Request.Query[MixRequestQueryKeywords.ToDate], out DateTime toDate);
            string keyword = Request.Query[MixRequestQueryKeywords.Keyword];
            bool isTheme = int.TryParse(Request.Query["themeId"], out int themeId);
            string folderType = Request.Query["folderType"];
            Expression<Func<MixTemplate, bool>> predicate = model =>
                (!isStatus || model.Status == status)
                && (!isTheme || model.ThemeId == themeId)
                && (!isFromDate || model.CreatedDateTime >= fromDate)
                && (!isToDate || model.CreatedDateTime <= toDate)
                && (string.IsNullOrEmpty(folderType) || model.FolderType == folderType)
                && (string.IsNullOrEmpty(keyword)
                 || EF.Functions.Like(model.FileName, $"%{keyword}%")
                 || EF.Functions.Like(model.Content, $"%{keyword}%")
                 );
            var getData = await base.GetListAsync<ReadViewModel>(predicate);
            if (getData.IsSucceed)
            {
                return getData.Data;
            }
            else
            {
                return BadRequest(getData.Errors);
            }
        }

        
        [HttpGet("copy/{id}")]
        public async Task<ActionResult<UpdateViewModel>> Copy(string id)
        {
            var getData = await GetSingleAsync<UpdateViewModel>(id);
            if (getData.IsSucceed)
            {
                var copyResult = await getData.Data.CopyAsync();
                if (copyResult.IsSucceed)
                {
                    return Ok(copyResult.Data);
                }
                else
                {
                    return BadRequest(copyResult.Errors);
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
}
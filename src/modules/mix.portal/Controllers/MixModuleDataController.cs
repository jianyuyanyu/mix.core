﻿using Microsoft.AspNetCore.Mvc;

namespace Mix.Portal.Controllers
{
    [Route("api/v2/rest/mix-portal/mix-module-data")]
    [ApiController]
    public class MixModuleDataController
        : MixRestApiControllerBase<MixModuleDataViewModel, MixCmsContext, MixModuleData, int>
    {
        public MixModuleDataController(
            IConfiguration configuration,
            MixService mixService,
            TranslatorService translator,
            EntityRepository<MixCmsContext, MixCulture, int> cultureRepository,
            MixIdentityService mixIdentityService,
            MixCmsContext context,
            MixCacheService cacheService,
            IQueueService<MessageQueueModel> queueService)
            : base(configuration, mixService, translator, cultureRepository, mixIdentityService, context, cacheService, queueService)
        {

        }

        #region Overrides


        protected override SearchQueryModel<MixModuleData, int> BuildSearchRequest(SearchRequestDto req)
        {
            var request = new SearchModuleDataDto(req, Request);
            var searchRequest = base.BuildSearchRequest(request);

            searchRequest.Predicate = searchRequest.Predicate.AndAlsoIf(
                request.ModuleContentId.HasValue,
                m => m.ModuleContentId == request.ModuleContentId);

            return searchRequest;
        }
        #endregion

        [HttpGet]
        [Route("init-form/{moduleId}")]
        public async Task<ActionResult<MixModuleDataViewModel>> InitByIdAsync(int moduleId)
        {
            var getModule = await MixModuleContentViewModel.GetRepository(_uow).GetSingleAsync(m => m.Id == moduleId).ConfigureAwait(false);
            if (getModule != null)
            {
                var moduleData = new MixModuleDataViewModel()
                {
                    ModuleContentId = getModule.Id,
                    SimpleDataColumns =  getModule.SimpleDataColumns
                };
                await moduleData.ExpandView(_cacheService);
                return Ok(moduleData);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

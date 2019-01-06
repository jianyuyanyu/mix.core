﻿using Microsoft.EntityFrameworkCore.Storage;
using Mix.Cms.Lib.Models.Cms;
using Mix.Cms.Lib.Services;
using Mix.Common.Helper;
using Mix.Domain.Core.ViewModels;
using Mix.Domain.Data.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using static Mix.Cms.Lib.MixEnums;

namespace Mix.Cms.Lib.ViewModels.MixModules
{
    public class ReadMvcViewModel
        : ViewModelBase<MixCmsContext, MixModule, ReadMvcViewModel>
    {
        #region Properties

        #region Models

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }

        [JsonProperty("template")]
        public string Template { get; set; }

        [JsonProperty("formTemplate")]
        public string FormTemplate { get; set; }

        [JsonProperty("edmTemplate")]
        public string EdmTemplate { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("fields")]
        public string Fields { get; set; }

        [JsonProperty("type")]
        public MixModuleType Type { get; set; }
        [JsonProperty("status")]
        public MixContentStatus Status { get; set; }

        [JsonProperty("lastModified")]
        public DateTime? LastModified { get; set; }

        [JsonProperty("modifiedBy")]
        public string ModifiedBy { get; set; }

        [JsonProperty("pageSize")]
        public int? PageSize { get; set; }
        #endregion Models

        #region Views
        [JsonProperty("domain")]
        public string Domain { get { return MixService.GetConfig<string>("Domain"); } }

        [JsonProperty("detailsUrl")]
        public string DetailsUrl { get; set; }
        [JsonProperty("imageUrl")]
        public string ImageUrl
        {
            get
            {
                if (!string.IsNullOrEmpty(Image) && (Image.IndexOf("http") == -1) && Image[0] != '/')
                {
                    return CommonHelper.GetFullPath(new string[] {
                    Domain,  Image
                });
                }
                else
                {
                    return Image;
                }
            }
        }
        [JsonProperty("thumbnailUrl")]
        public string ThumbnailUrl
        {
            get
            {
                if (Thumbnail != null && Thumbnail.IndexOf("http") == -1 && Thumbnail[0] != '/')
                {
                    return CommonHelper.GetFullPath(new string[] {
                    Domain,  Thumbnail
                });
                }
                else
                {
                    return string.IsNullOrEmpty(Thumbnail) ? ImageUrl : Thumbnail;
                }
            }
        }

        [JsonProperty("columns")]
        public List<ModuleFieldViewModel> Columns
        {
            get { return Fields == null ? null : JsonConvert.DeserializeObject<List<ModuleFieldViewModel>>(Fields); }
            set { Fields = JsonConvert.SerializeObject(value); }
        }

        [JsonProperty("view")]
        public MixTemplates.ReadViewModel View { get; set; }
        [JsonProperty("formView")]
        public MixTemplates.ReadViewModel FormView { get; set; }

        [JsonProperty("edmView")]
        public MixTemplates.ReadViewModel EdmView { get; set; }
        [JsonProperty("data")]
        public PaginationModel<ViewModels.MixModuleDatas.ReadViewModel> Data { get; set; } = new PaginationModel<ViewModels.MixModuleDatas.ReadViewModel>();

        [JsonProperty("articles")]
        public PaginationModel<MixModuleArticles.ReadViewModel> Articles { get; set; } = new PaginationModel<MixModuleArticles.ReadViewModel>();

        [JsonProperty("products")]
        public PaginationModel<MixModuleProducts.ReadViewModel> Products { get; set; } = new PaginationModel<MixModuleProducts.ReadViewModel>();

        public string TemplatePath
        {
            get
            {
                return CommonHelper.GetFullPath(new string[]
                {
                    ""
                    , MixConstants.Folder.TemplatesFolder
                    , MixService.GetConfig<string>(MixConstants.ConfigurationKeyword.ThemeFolder, Specificulture) ?? "Default"
                    , Template
                });
            }
        }

        #endregion Views

        public int? ArticleId { get; set; }
        public int? CategoryId { get; set; }

        #endregion Properties

        #region Contructors

        public ReadMvcViewModel() : base()
        {
        }

        public ReadMvcViewModel(MixModule model, MixCmsContext _context = null, IDbContextTransaction _transaction = null) : base(model, _context, _transaction)
        {
        }

        #endregion Contructors

        #region Overrides

        public override void ExpandView(MixCmsContext _context = null, IDbContextTransaction _transaction = null)
        {
            this.View = MixTemplates.ReadViewModel.GetTemplateByPath(Template, Specificulture, _context, _transaction).Data;
            this.FormView = MixTemplates.ReadViewModel.GetTemplateByPath(FormTemplate, Specificulture, _context, _transaction).Data;
            this.EdmView = MixTemplates.ReadViewModel.GetTemplateByPath(EdmTemplate, Specificulture, _context, _transaction).Data;
            // call load data from controller for padding parameter (articleId, productId, ...)
        }

        #endregion Overrides

        #region Expand

        public static RepositoryResponse<ReadMvcViewModel> GetBy(
            Expression<Func<MixModule, bool>> predicate, int? articleId = null, int? productid = null, int categoryId = 0
             , MixCmsContext _context = null, IDbContextTransaction _transaction = null)
        {
            var result = Repository.GetSingleModel(predicate, _context, _transaction);
            if (result.IsSucceed)
            {
                result.Data.ArticleId = articleId;
                result.Data.CategoryId = categoryId;
                result.Data.LoadData();
            }
            return result;
        }

        public void LoadData(int? articleId = null, int? productId = null, int? categoryId = null
            , int? pageSize = null, int? pageIndex = 0
            , MixCmsContext _context = null, IDbContextTransaction _transaction = null)
        {
            UnitOfWorkHelper<MixCmsContext>.InitTransaction(_context, _transaction, out MixCmsContext context, out IDbContextTransaction transaction, out bool isRoot);
            try
            {
                pageSize = pageSize > 0 ? PageSize : PageSize;
                pageIndex = pageIndex ?? 0;
                Expression<Func<MixModuleData, bool>> dataExp = null;
                Expression<Func<MixModuleArticle, bool>> articleExp = null;
                Expression<Func<MixModuleProduct, bool>> productExp = null;
                switch (Type)
                {
                    case MixModuleType.Content:
                    case MixModuleType.Data:
                        dataExp = m => m.ModuleId == Id && m.Specificulture == Specificulture;
                        //articleExp = n => n.ModuleId == Id && n.Specificulture == Specificulture;
                        //productExp = m => m.ModuleId == Id && m.Specificulture == Specificulture;
                        break;

                    case MixModuleType.SubPage:
                        dataExp = m => m.ModuleId == Id && m.Specificulture == Specificulture && (m.CategoryId == categoryId);
                        articleExp = n => n.ModuleId == Id && n.Specificulture == Specificulture;
                        productExp = m => m.ModuleId == Id && m.Specificulture == Specificulture;
                        break;

                    case MixModuleType.SubArticle:
                        dataExp = m => m.ModuleId == Id && m.Specificulture == Specificulture && (m.ArticleId == articleId);
                        break;
                    case MixModuleType.SubProduct:
                        dataExp = m => m.ModuleId == Id && m.Specificulture == Specificulture && (m.ProductId == productId);
                        break;
                    case MixModuleType.ListArticle:
                        articleExp = n => n.ModuleId == Id && n.Specificulture == Specificulture;
                        break;
                    case MixModuleType.ListProduct:
                        productExp = n => n.ModuleId == Id && n.Specificulture == Specificulture;
                        break;
                    default:
                        dataExp = m => m.ModuleId == Id && m.Specificulture == Specificulture;
                        articleExp = n => n.ModuleId == Id && n.Specificulture == Specificulture;
                        productExp = m => m.ModuleId == Id && m.Specificulture == Specificulture;
                        break;
                }

                if (dataExp != null)
                {
                    var getDataResult = MixModuleDatas.ReadViewModel.Repository
                    .GetModelListBy(
                        dataExp
                        , MixService.GetConfig<string>(MixConstants.ConfigurationKeyword.OrderBy), 0
                        , pageSize, pageIndex
                        , _context: context, _transaction: transaction);
                    if (getDataResult.IsSucceed)
                    {
                        getDataResult.Data.JsonItems = new List<JObject>();
                        getDataResult.Data.Items.ForEach(d => getDataResult.Data.JsonItems.Add(d.JItem));
                        Data = getDataResult.Data;
                    }
                }
                if (articleExp != null)
                {
                    var getArticles = MixModuleArticles.ReadViewModel.Repository
                    .GetModelListBy(articleExp
                    , MixService.GetConfig<string>(MixConstants.ConfigurationKeyword.OrderBy), 0
                    , pageSize, pageIndex
                    , _context: context, _transaction: transaction);
                    if (getArticles.IsSucceed)
                    {
                        Articles = getArticles.Data;
                    }
                }
                if (productExp != null)
                {
                    var getProducts = MixModuleProducts.ReadViewModel.Repository
                    .GetModelListBy(productExp
                    , MixService.GetConfig<string>(MixConstants.ConfigurationKeyword.OrderBy), 0
                    , PageSize, pageIndex
                    , _context: context, _transaction: transaction);
                    if (getProducts.IsSucceed)
                    {
                        Products = getProducts.Data;
                    }
                }
            }
            catch (Exception ex)
            {
                UnitOfWorkHelper<MixCmsContext>.HandleException<PaginationModel<ReadMvcViewModel>>(ex, isRoot, transaction);
            }
            finally
            {
                if (isRoot)
                {
                    //if current Context is Root
                    context.Dispose();
                }
            }
        }

        #endregion Expand
    }
}

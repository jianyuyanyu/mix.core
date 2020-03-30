﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Mix.Cms.Lib.Models.Cms;
using Mix.Cms.Lib.Services;
using Mix.Common.Helper;
using Mix.Domain.Core.ViewModels;
using Mix.Domain.Data.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Mix.Cms.Lib.MixEnums;

namespace Mix.Cms.Lib.ViewModels.MixPages
{
    public class UpdateViewModel
       : ViewModelBase<MixCmsContext, MixPage, UpdateViewModel>
    {
        #region Properties

        #region Models

        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("specificulture")]
        public string Specificulture { get; set; }
        [JsonProperty("priority")]
        public int Priority { get; set; }
        [JsonProperty("cultures")]
        public List<Domain.Core.Models.SupportedCulture> Cultures { get; set; }

        [JsonProperty("template")]
        public string Template { get; set; }

        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("cssClass")]
        public string CssClass { get; set; }

        [JsonProperty("layout")]
        public string Layout { get; set; }

        [Required]
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("excerpt")]
        public string Excerpt { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("seoName")]
        public string SeoName { get; set; }

        [JsonProperty("seoTitle")]
        public string SeoTitle { get; set; }

        [JsonProperty("seoDescription")]
        public string SeoDescription { get; set; }

        [JsonProperty("seoKeywords")]
        public string SeoKeywords { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("views")]
        public int? Views { get; set; }

        [JsonProperty("type")]
        public MixPageType Type { get; set; }

        [JsonProperty("status")]
        public MixEnums.PageStatus Status { get; set; }

        [JsonProperty("createdDateTime")]
        public DateTime CreatedDateTime { get; set; }

        [JsonProperty("updatedDateTime")]
        public DateTime? UpdatedDateTime { get; set; }

        [JsonProperty("createdBy")]
        public string CreatedBy { get; set; }

        [JsonProperty("tags")]
        public string Tags { get; set; }

        [JsonProperty("staticUrl")]
        public string StaticUrl { get; set; }

        [JsonProperty("level")]
        public int? Level { get; set; }

        [JsonProperty("lastModified")]
        public DateTime? LastModified { get; set; }

        [JsonProperty("modifiedBy")]
        public string ModifiedBy { get; set; }

        [JsonProperty("pageSize")]
        public int? PageSize { get; set; }

        #endregion Models

        #region Views

        [JsonProperty("detailsUrl")]
        public string DetailsUrl { get; set; }

        [JsonProperty("moduleNavs")]
        public List<MixPageModules.ReadMvcViewModel> ModuleNavs { get; set; } // Parent to Modules

        [JsonProperty("parentNavs")]
        public List<MixPagePages.ReadViewModel> ParentNavs { get; set; } // Parent to  Parent

        [JsonProperty("childNavs")]
        public List<MixPagePages.ReadViewModel> ChildNavs { get; set; } // Parent to  Parent

        [JsonProperty("listTag")]
        public JArray ListTag { get; set; } = new JArray();

        [JsonProperty("imageFileStream")]
        public FileStreamViewModel ImageFileStream { get; set; }

        [JsonProperty("domain")]
        public string Domain { get { return MixService.GetConfig<string>("Domain"); } }

        [JsonProperty("imageUrl")]
        public string ImageUrl {
            get {
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
        public string ThumbnailUrl {
            get {
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

        #region Template

        [JsonProperty("view")]
        public MixTemplates.UpdateViewModel View { get; set; }

        [JsonProperty("templates")]
        public List<MixTemplates.UpdateViewModel> Templates { get; set; }

        [JsonProperty("master")]
        public MixTemplates.UpdateViewModel Master { get; set; }

        [JsonProperty("masters")]
        public List<MixTemplates.UpdateViewModel> Masters { get; set; }

        [JsonIgnore]
        public int ActivedTheme {
            get {
                return MixService.GetConfig<int>(MixConstants.ConfigurationKeyword.ThemeId, Specificulture);
            }
        }

        [JsonIgnore]
        public string TemplateFolderType {
            get {
                return MixEnums.EnumTemplateFolder.Pages.ToString();
            }
        }

        [JsonProperty("templateFolder")]
        public string TemplateFolder {
            get {
                return CommonHelper.GetFullPath(new string[]
                {
                    MixConstants.Folder.TemplatesFolder
                    , MixService.GetConfig<string>(MixConstants.ConfigurationKeyword.ThemeName, Specificulture)
                    , TemplateFolderType
                }
            );
            }
        }

        #endregion Template

        [JsonProperty("urlAliases")]
        public List<MixUrlAliases.UpdateViewModel> UrlAliases { get; set; }

        [JsonProperty("attributes")]
        public MixAttributeSets.UpdateViewModel Attributes { get; set; }

        [JsonProperty("attributeData")]
        public MixRelatedAttributeDatas.UpdateViewModel AttributeData { get; set; }

        [JsonProperty("sysCategories")]
        public List<MixRelatedAttributeDatas.UpdateViewModel> SysCategories { get; set; }

        [JsonProperty("sysTags")]
        public List<MixRelatedAttributeDatas.UpdateViewModel> SysTags { get; set; }

        #endregion Views

        #endregion Properties

        #region Contructors

        public UpdateViewModel() : base()
        {
        }

        public UpdateViewModel(MixPage model, MixCmsContext _context = null, IDbContextTransaction _transaction = null) : base(model, _context, _transaction)
        {
        }

        #endregion Contructors

        #region Overrides

        public override MixPage ParseModel(MixCmsContext _context = null, IDbContextTransaction _transaction = null)
        {
            GenerateSEO();

            //var navParent = ParentNavs?.FirstOrDefault(p => p.IsActived);

            //if (navParent != null)
            //{
            //    Level = 1; //Repository.GetSingleModel(c => c.Id == navParent.ParentId, _context, _transaction).Data.Level + 1;
            //}
            //else
            //{
            //    Level = 0;
            //}

            Template = View != null ? $"{View.FolderType}/{View.FileName}{View.Extension}" : Template;
            Layout = Master != null ? $"{Master.FolderType}/{Master.FileName}{Master.Extension}" : null;
            if (Id == 0)
            {
                Id = Repository.Max(c => c.Id, _context, _transaction).Data + 1;
                CreatedDateTime = DateTime.UtcNow;
            }
            LastModified = DateTime.UtcNow;
            if (!string.IsNullOrEmpty(Image) && Image[0] == '/') { Image = Image.Substring(1); }
            if (!string.IsNullOrEmpty(Thumbnail) && Thumbnail[0] == '/') { Thumbnail = Thumbnail.Substring(1); }
            return base.ParseModel(_context, _transaction);
        }

        public override void ExpandView(MixCmsContext _context = null, IDbContextTransaction _transaction = null)
        {
            Cultures = Helper.LoadCultures(Id, Specificulture, _context, _transaction);
            if (!string.IsNullOrEmpty(this.Tags))
            {
                ListTag = JArray.Parse(this.Tags);
            }

            LoadAttributes(_context, _transaction);
            // Load page views
            this.Templates = MixTemplates.UpdateViewModel.Repository.GetModelListBy(
                t => t.Theme.Id == ActivedTheme && t.FolderType == this.TemplateFolderType, _context, _transaction).Data;
            var templateName = Template?.Substring(Template.LastIndexOf('/') + 1) ?? MixConstants.DefaultTemplate.Page;
            this.View = Templates.FirstOrDefault(t => !string.IsNullOrEmpty(templateName) && templateName.Equals($"{t.FileName}{t.Extension}"));
            if (this.View == null)
            {
                this.View = Templates.FirstOrDefault(t => MixConstants.DefaultTemplate.Module.Equals($"{t.FileName}{t.Extension}"));
            }
            this.Template = $"{View?.FileFolder}/{View?.FileName}{View.Extension}";
            // Load Attributes
            // Load master views
            this.Masters = MixTemplates.UpdateViewModel.Repository.GetModelListBy(
                t => t.Theme.Id == ActivedTheme && t.FolderType == MixEnums.EnumTemplateFolder.Masters.ToString(), _context, _transaction).Data;
            var masterName = Layout?.Substring(Layout.LastIndexOf('/') + 1) ?? MixConstants.DefaultTemplate.Master;
            this.Master = Masters.FirstOrDefault(t => !string.IsNullOrEmpty(masterName) && masterName.Equals($"{t.FileName}{t.Extension}"));
            if (this.Master == null)
            {
                this.Master = Masters.FirstOrDefault(t => MixConstants.DefaultTemplate.Master.Equals($"{t.FileName}{t.Extension}"));
            }
            this.Layout = $"{Master?.FileFolder}/{Master?.FileName}{Master?.Extension}";

            this.ModuleNavs = GetModuleNavs(_context, _transaction);
            //this.ParentNavs = GetParentNavs(_context, _transaction);
            //this.ChildNavs = GetChildNavs(_context, _transaction);
            this.UrlAliases = GetAliases(_context, _transaction);
        }

        #region Sync

        public override RepositoryResponse<bool> SaveSubModels(MixPage parent, MixCmsContext _context = null, IDbContextTransaction _transaction = null)
        {
            var result = new RepositoryResponse<bool> { IsSucceed = true };
            var saveTemplate = View.SaveModel(true, _context, _transaction);
            ViewModelHelper.HandleResult(saveTemplate, ref result);

            if (result.IsSucceed && Master != null)
            {
                var saveLayout = Master.SaveModel(true, _context, _transaction);
                ViewModelHelper.HandleResult(saveLayout, ref result);
            }
            if (result.IsSucceed && UrlAliases != null)
            {
                foreach (var item in UrlAliases)
                {
                    if (result.IsSucceed)
                    {
                        item.SourceId = parent.Id.ToString();
                        item.Type = UrlAliasType.Page;
                        item.Specificulture = Specificulture;
                        var saveResult = item.SaveModel(false, _context, _transaction);
                        ViewModelHelper.HandleResult(saveResult, ref result);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (result.IsSucceed)
            {
                foreach (var item in ModuleNavs)
                {
                    item.PageId = parent.Id;

                    if (item.IsActived)
                    {
                        var saveResult = item.SaveModel(false, _context, _transaction);
                        ViewModelHelper.HandleResult(saveResult, ref result);
                    }
                    else
                    {
                        var saveResult = item.RemoveModel(false, _context, _transaction);
                        ViewModelHelper.HandleResult(saveResult, ref result);
                    }
                }
            }
            //if (result.IsSucceed)
            //{
            //    foreach (var item in ParentNavs)
            //    {
            //        item.Id = parent.Id;
            //        if (item.IsActived)
            //        {
            //            var saveResult = item.SaveModel(false, _context, _transaction);
            //            ViewModelHelper.HandleResult(saveResult, ref result);
            //        }
            //        else
            //        {
            //            var saveResult = item.RemoveModel(false, _context, _transaction);
            //            ViewModelHelper.HandleResult(saveResult, ref result);
            //        }
            //    }
            //}

            //if (result.IsSucceed)
            //{
            //    foreach (var item in ChildNavs)
            //    {
            //        item.ParentId = parent.Id;
            //        if (item.IsActived)
            //        {
            //            var saveResult = item.SaveModel(false, _context, _transaction);
            //            ViewModelHelper.HandleResult(saveResult, ref result);
            //        }
            //        else
            //        {
            //            var saveResult = item.RemoveModel(false, _context, _transaction);
            //            ViewModelHelper.HandleResult(saveResult, ref result);
            //        }
            //    }
            //}
            return result;
        }

        #endregion Sync

        #region Async

        public override async Task<RepositoryResponse<bool>> SaveSubModelsAsync(MixPage parent, MixCmsContext _context = null, IDbContextTransaction _transaction = null)
        {
            var result = new RepositoryResponse<bool> { IsSucceed = true };
            var saveTemplate = await View.SaveModelAsync(true, _context, _transaction);
            ViewModelHelper.HandleResult(saveTemplate, ref result);

            if (result.IsSucceed && Master != null)
            {
                var saveLayout = Master.SaveModel(true, _context, _transaction);
                ViewModelHelper.HandleResult(saveLayout, ref result);
            }
            if (result.IsSucceed && UrlAliases != null)
            {
                foreach (var item in UrlAliases)
                {
                    if (result.IsSucceed)
                    {
                        item.SourceId = parent.Id.ToString();
                        item.Type = UrlAliasType.Page;
                        item.Specificulture = Specificulture;
                        var saveResult = await item.SaveModelAsync(false, _context, _transaction);
                        ViewModelHelper.HandleResult(saveResult, ref result);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (result.IsSucceed)
            {
                foreach (var item in ModuleNavs)
                {
                    item.PageId = parent.Id;

                    if (item.IsActived)
                    {
                        var saveResult = await item.SaveModelAsync(false, _context, _transaction);
                        ViewModelHelper.HandleResult(saveResult, ref result);
                    }
                    else
                    {
                        var saveResult = await item.RemoveModelAsync(false, _context, _transaction);
                        ViewModelHelper.HandleResult(saveResult, ref result);
                    }
                }
            }

            //if (result.IsSucceed)
            //{
            //    foreach (var item in ParentNavs)
            //    {
            //        item.Id = parent.Id;
            //        if (item.IsActived)
            //        {
            //            var saveResult = await item.SaveModelAsync(false, _context, _transaction);
            //            ViewModelHelper.HandleResult(saveResult, ref result);
            //        }
            //        else
            //        {
            //            var saveResult = await item.RemoveModelAsync(false, _context, _transaction);
            //            ViewModelHelper.HandleResult(saveResult, ref result);
            //        }
            //    }
            //}

            if (result.IsSucceed)
            {
                // Save Attributes
                result = await SaveAttributeAsync(parent.Id, _context, _transaction);
            }
            //if (result.IsSucceed)
            //{
            //    foreach (var item in ChildNavs)
            //    {
            //        item.ParentId = parent.Id;
            //        if (item.IsActived)
            //        {
            //            var saveResult = await item.SaveModelAsync(false, _context, _transaction);
            //            ViewModelHelper.HandleResult(saveResult, ref result);
            //        }
            //        else
            //        {
            //            var saveResult = await item.RemoveModelAsync(false, _context, _transaction);
            //            ViewModelHelper.HandleResult(saveResult, ref result);
            //        }
            //    }
            //}
            return result;
        }

        private async Task<RepositoryResponse<bool>> SaveAttributeAsync(int parentId, MixCmsContext context, IDbContextTransaction transaction)
        {
            var result = new RepositoryResponse<bool>() { IsSucceed = true };
            AttributeData.ParentId = parentId.ToString();
            AttributeData.ParentType = (int)MixEnums.MixAttributeSetDataType.Page;
            var saveData = await AttributeData.Data.SaveModelAsync(true, context, transaction);
            ViewModelHelper.HandleResult(saveData, ref result);
            if (result.IsSucceed)
            {
                AttributeData.Id = saveData.Data.Id;
                var saveRelated = await AttributeData.SaveModelAsync(true, context, transaction);
                ViewModelHelper.HandleResult(saveRelated, ref result);
            }
            foreach (var item in SysCategories)
            {
                if (result.IsSucceed)
                {
                    item.ParentId = parentId.ToString();
                    item.ParentType = (int)MixEnums.MixAttributeSetDataType.Page;
                    item.Specificulture = Specificulture;
                    var saveResult = await item.SaveModelAsync(false, context, transaction);
                    ViewModelHelper.HandleResult(saveResult, ref result);
                }
            }

            foreach (var item in SysTags)
            {
                if (result.IsSucceed)
                {
                    item.ParentId = parentId.ToString();
                    item.ParentType = (int)MixEnums.MixAttributeSetDataType.Page;
                    item.Specificulture = Specificulture;
                    var saveResult = await item.SaveModelAsync(false, context, transaction);
                    ViewModelHelper.HandleResult(saveResult, ref result);
                }
            }
            return result;
        }

        #endregion Async

        #endregion Overrides

        #region Expands

        private void LoadAttributes(MixCmsContext _context, IDbContextTransaction _transaction)
        {
            var getAttrs = MixAttributeSets.UpdateViewModel.Repository.GetSingleModel(m => m.Name == MixConstants.AttributeSetName.ADDITIONAL_FIELD_PAGE
                , _context, _transaction);
            if (getAttrs.IsSucceed)
            {
                Attributes = getAttrs.Data;
                AttributeData = MixRelatedAttributeDatas.UpdateViewModel.Repository.GetFirstModel(
                    a => a.ParentId == Id.ToString() && a.Specificulture == Specificulture && a.AttributeSetId == Attributes.Id
                        , _context, _transaction).Data;
                if (AttributeData == null)
                {
                    AttributeData = new MixRelatedAttributeDatas.UpdateViewModel(
                        new MixRelatedAttributeData()
                        {
                            Specificulture = Specificulture,
                            ParentType = (int)MixEnums.MixAttributeSetDataType.Page,
                            ParentId = Id.ToString(),
                            AttributeSetId = Attributes.Id,
                            AttributeSetName = Attributes.Name
                        }
                        )
                    {
                        Data = new MixAttributeSetDatas.UpdateViewModel(
                    new MixAttributeSetData()
                    {
                        Specificulture = Specificulture,
                        AttributeSetId = Attributes.Id,
                        AttributeSetName = Attributes.Name
                    }
                    )
                    };
                }
                foreach (var field in Attributes.Fields.OrderBy(f => f.Priority))
                {
                    var val = AttributeData.Data.Values.FirstOrDefault(v => v.AttributeFieldId == field.Id);
                    if (val == null)
                    {
                        val = new MixAttributeSetValues.UpdateViewModel(
                            new MixAttributeSetValue() { AttributeFieldId = field.Id }
                            , _context, _transaction)
                        {
                            Field = field,
                            AttributeFieldName = field.Name,
                            Priority = field.Priority
                        };
                        AttributeData.Data.Values.Add(val);
                    }
                    val.Priority = field.Priority;
                    val.Field = field;
                }
                var getCategories = MixRelatedAttributeDatas.UpdateViewModel.Repository.GetModelListBy(m => m.Specificulture == Specificulture
                && m.ParentId == Id.ToString() && m.ParentType == (int)MixEnums.MixAttributeSetDataType.Page
                && m.AttributeSetName == MixConstants.AttributeSetName.SYSTEM_CATEGORY, _context, _transaction);
                if (getCategories.IsSucceed)
                {
                    SysCategories = getCategories.Data;
                }

                var getTags = MixRelatedAttributeDatas.UpdateViewModel.Repository.GetModelListBy(m => m.Specificulture == Specificulture
                    && m.ParentId == Id.ToString() && m.ParentType == (int)MixEnums.MixAttributeSetDataType.Page
                    && m.AttributeSetName == MixConstants.AttributeSetName.SYSTEM_TAG, _context, _transaction);
                if (getTags.IsSucceed)
                {
                    SysTags = getTags.Data;
                }
            }
        }

        private void GenerateSEO()
        {
            if (string.IsNullOrEmpty(this.SeoName))
            {
                this.SeoName = SeoHelper.GetSEOString(this.Title);
            }
            int i = 1;
            string name = SeoName;
            while (Repository.CheckIsExists(a => a.SeoName == name && a.Specificulture == Specificulture && a.Id != Id))
            {
                name = SeoName + "_" + i;
                i++;
            }
            SeoName = name;

            if (string.IsNullOrEmpty(this.SeoTitle))
            {
                this.SeoTitle = SeoHelper.GetSEOString(this.Title);
            }

            if (string.IsNullOrEmpty(this.SeoDescription))
            {
                this.SeoDescription = SeoHelper.GetSEOString(this.Title);
            }

            if (string.IsNullOrEmpty(this.SeoKeywords))
            {
                this.SeoKeywords = SeoHelper.GetSEOString(this.Title);
            }
        }

        public List<MixUrlAliases.UpdateViewModel> GetAliases(MixCmsContext context, IDbContextTransaction transaction)
        {
            var result = MixUrlAliases.UpdateViewModel.Repository.GetModelListBy(p => p.Specificulture == Specificulture
                        && p.SourceId == Id.ToString() && p.Type == (int)MixEnums.UrlAliasType.Page, context, transaction);
            if (result.IsSucceed && result.Data != null)
            {
                return result.Data;
            }
            else
            {
                return new List<MixUrlAliases.UpdateViewModel>();
            }
        }

        public List<MixPageModules.ReadMvcViewModel> GetModuleNavs(MixCmsContext context, IDbContextTransaction transaction)
        {
            var query = context.MixModule
                .Include(cp => cp.MixPageModule)
                .Where(module => module.Specificulture == Specificulture)
                .Select(module => new MixPageModules.ReadMvcViewModel()
                {
                    PageId = Id,
                    ModuleId = module.Id,
                    Specificulture = Specificulture,
                    Description = module.Title,
                    Image = module.Image
                });

            var result = query.ToList();
            result.ForEach(nav =>
            {
                var currentNav = context.MixPageModule.FirstOrDefault(
                        m => m.ModuleId == nav.ModuleId && m.PageId == Id && m.Specificulture == Specificulture);
                nav.Priority = currentNav?.Priority ?? 0;
                nav.IsActived = currentNav != null;
            });
            return result.OrderBy(m => m.Priority).ToList();
        }

        public List<MixPagePages.ReadViewModel> GetParentNavs(MixCmsContext context, IDbContextTransaction transaction)
        {
            var query = context.MixPage
                .Include(cp => cp.MixPagePageMixPage)
                .Where(Category => Category.Specificulture == Specificulture && Category.Id != Id)
                .Select(Category =>
                    new MixPagePages.ReadViewModel()
                    {
                        Id = Id,
                        ParentId = Category.Id,
                        Specificulture = Specificulture,
                        Description = Category.Title,
                    }
                );

            var result = query.ToList();
            result.ForEach(nav =>
            {
                nav.IsActived = context.MixPagePage.Any(
                        m => m.ParentId == nav.ParentId && m.Id == Id && m.Specificulture == Specificulture);
            });
            return result.OrderBy(m => m.Priority).ToList();
        }

        public List<MixPagePages.ReadViewModel> GetChildNavs(MixCmsContext context, IDbContextTransaction transaction)
        {
            var query = context.MixPage
                .Include(cp => cp.MixPagePageMixPage)
                .Where(Category => Category.Specificulture == Specificulture && Category.Id != Id)
                .Select(Category =>
                new MixPagePages.ReadViewModel(
                      new MixPagePage()
                      {
                          Id = Category.Id,
                          ParentId = Id,
                          Specificulture = Specificulture,
                          Description = Category.Title,
                      }, context, transaction));

            var result = query.ToList();
            result.ForEach(nav =>
            {
                var currentNav = context.MixPagePage.FirstOrDefault(
                        m => m.ParentId == Id && m.Id == nav.Id && m.Specificulture == Specificulture);
                nav.Priority = currentNav?.Priority ?? 0;
                nav.IsActived = currentNav != null;
            });
            return result.OrderBy(m => m.Priority).ToList();
        }

        //public override List<Task> GenerateRelatedData(MixCmsContext context, IDbContextTransaction transaction)
        //{
        //    var tasks = new List<Task>();
        //    var relatedPages = context.MixPage.Where(m => m.MixPagePageMixPage
        //         .Any(d => d.Specificulture == Specificulture && (d.Id == Id || d.ParentId == Id)));
        //    foreach (var item in relatedPages)
        //    {
        //        tasks.Add(Task.Run(() =>
        //        {
        //            var data = new ReadViewModel(item, context, transaction);
        //            data.RemoveCache(item, context, transaction);
        //        }));
        //    }

        //    return tasks;
        //}

        #endregion Expands
    }
}
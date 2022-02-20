﻿namespace Mix.Portal.Domain.ViewModels
{
    [GenerateRestApiController]
    public class MixModuleContentViewModel
        : ExtraColumnMultilanguageSEOContentViewModelBase<MixCmsContext, MixModuleContent, int, MixModuleContentViewModel>
    {
        #region Contructors

        public MixModuleContentViewModel()
        {
        }

        public MixModuleContentViewModel(MixModuleContent entity,
            UnitOfWorkInfo uowInfo = null) : base(entity, uowInfo)
        {
        }

        public MixModuleContentViewModel(UnitOfWorkInfo unitOfWorkInfo) : base(unitOfWorkInfo)
        {
        }
        #endregion

        #region Properties

        public string SystemName { get; set; }
        public string ClassName { get; set; }
        public int? PageSize { get; set; }
        public MixModuleType? Type { get; set; }
        public string SimpleDataColumns { get; set; }

        public List<ModuleColumnModel> Columns { get; set; }

        #endregion

        public override Task<MixModuleContent> ParseEntity()
        {
            var arrField = Columns != null ? JArray.Parse(
               JsonConvert.SerializeObject(Columns.OrderBy(c => c.Priority).Where(
                   c => !string.IsNullOrEmpty(c.SystemName)))) : new JArray();
            SimpleDataColumns = arrField.ToString(Formatting.None);
            return base.ParseEntity();
        }

        public override Task ExpandView()
        {
            if (!string.IsNullOrEmpty(SimpleDataColumns))
            {
                JArray arrField = JArray.Parse(SimpleDataColumns);
                Columns = arrField.ToObject<List<ModuleColumnModel>>();
            }
            else
            {
                Columns = new List<ModuleColumnModel>();
            }

            return base.ExpandView();
        }

        public override async Task<int> CreateParentAsync()
        {
            MixModuleViewModel parent = new(UowInfo)
            {
                DisplayName = Title,
                SystemName = SystemName,
                Description = Excerpt
            };
            return await parent.SaveAsync();
        }

        protected override async Task DeleteHandlerAsync()
        {
            if (Repository.GetListQuery(m => m.ParentId == ParentId).Count() == 1)
            {
                var mdlRepo = MixModuleViewModel.GetRepository(UowInfo);

                await Repository.DeleteAsync(Id);
                await mdlRepo.DeleteAsync(ParentId);
            }
            else
            {
                await base.DeleteHandlerAsync();
            }
        }
    }
}

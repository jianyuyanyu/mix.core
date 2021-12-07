﻿namespace Mixcore.Domain.ViewModels
{
    public class ModuleDataViewModel
        : MultilanguageSEOContentViewModelBase
            <MixCmsContext, MixModuleData, int, ModuleDataViewModel>
    {
        #region Contructors

        public ModuleDataViewModel()
        {
        }

        public ModuleDataViewModel(MixModuleData entity,
            MixCacheService cacheService = null,
            UnitOfWorkInfo uowInfo = null) : base(entity, cacheService, uowInfo)
        {
        }

        public ModuleDataViewModel(UnitOfWorkInfo unitOfWorkInfo) : base(unitOfWorkInfo)
        {
        }
        #endregion

        #region Properties
        public string Value { get; set; }

        public int ModuleContentId { get; set; }

        public JObject Data { get; set; } = new JObject();
        #endregion

        #region Overrides

        public override Task ExpandView(MixCacheService cacheService = null)
        {
            if (!string.IsNullOrEmpty(Value))
            {
                Data = JObject.Parse(Value);
            }
            return base.ExpandView(cacheService);
        }

        #endregion

        #region Helper
        public string Property(string name)
        {
            return Data.Property(name)?.Value<string>();
        }
        #endregion
    }
}

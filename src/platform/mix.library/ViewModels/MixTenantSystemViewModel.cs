﻿using Mix.Lib.Models.Configurations;

namespace Mix.Lib.ViewModels
{
    [GeneratePublisher]
    public class MixTenantSystemViewModel
        : ViewModelBase<MixCmsContext, MixTenant, int, MixTenantSystemViewModel>
    {
        #region Properties

        public string PrimaryDomain { get; set; }
        public string SystemName { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }

        public List<MixDomainViewModel> Domains { get; set; } = new();

        public TenantConfigurationModel Configurations { get; set; }
        #endregion

        #region Constructors

        public MixTenantSystemViewModel()
        {
        }

        public MixTenantSystemViewModel(MixTenant entity,

            UnitOfWorkInfo uowInfo = null) : base(entity, uowInfo)
        {
        }

        public MixTenantSystemViewModel(UnitOfWorkInfo unitOfWorkInfo) : base(unitOfWorkInfo)
        {
        }

        #endregion

        #region Overrides

        public override async Task ExpandView()
        {
            Domains = await MixDomainViewModel.GetRepository(UowInfo).GetAllAsync(m => m.MixTenantId == Id);
            var srv = new TenantConfigService(SystemName);
            Configurations = srv.AppSettings;
        }


        #endregion

        #region Methods
        #endregion
    }
}

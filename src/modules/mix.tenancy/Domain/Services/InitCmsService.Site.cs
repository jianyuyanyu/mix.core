﻿using Mix.Shared.Enums;
using Mix.Tenancy.Domain.Dtos;
using Mix.Tenancy.Domain.ViewModels.Init;

namespace Mix.Tenancy.Domain.Services
{
    public partial class InitCmsService
    {
        public async Task InitDbContext(InitCmsDto model)
        {
            _databaseService.InitMixCmsContext(
                model.ConnectionString,
                model.DatabaseProvider,
                model.Culture.Specificulture);

            await _databaseService.InitMixCmsContextAsync();
        }

        public async Task InitTenantAsync(InitCmsDto model)
        {
            InitTenantViewModel vm = new(_context, model);
            await vm.SaveAsync();
            GlobalConfigService.Instance.AppSettings.DefaultCulture = model.Culture.Specificulture;
            GlobalConfigService.Instance.AppSettings.InitStatus = InitStep.InitTenant;
            GlobalConfigService.Instance.SaveSettings();
        }
    }
}

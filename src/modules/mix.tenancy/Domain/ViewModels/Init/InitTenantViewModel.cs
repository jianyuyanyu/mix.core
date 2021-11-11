using Mix.Database.Entities.Cms;
using Mix.Heart.Enums;
using Mix.Heart.ViewModel;
using Mix.Tenancy.Domain.Dtos;
using System;
using System.Threading.Tasks;
using Mix.Heart.Extensions;
using Mix.Heart.UnitOfWork;
using Mix.Heart.Services;

namespace Mix.Tenancy.Domain.ViewModels.Init
{
    public class InitTenantViewModel : ViewModelBase<MixCmsContext, MixTenant, int, InitTenantViewModel>
    {
        public string SystemName { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }

        public InitCultureViewModel Culture { get; set; }

        public InitTenantViewModel() : base()
        {
        }

        public InitTenantViewModel(MixTenant entity,
            MixCacheService cacheService = null,
            UnitOfWorkInfo uowInfo = null)
            : base(entity, cacheService, uowInfo)
        {
        }

        public InitTenantViewModel(UnitOfWorkInfo unitOfWorkInfo) : base(unitOfWorkInfo)
        {
        }

        public InitTenantViewModel(MixCmsContext context) : base(context)
        {
        }

        public void InitSiteData(InitCmsDto model)
        {
            DisplayName = model.SiteName;
            SystemName = model.SiteName.ToSEOString('_');
            Description = model.SiteName;
            Status = MixContentStatus.Published;
            CreatedDateTime = DateTime.UtcNow;

            Culture = new InitCultureViewModel()
            {
                Title = model.Culture.FullName,
                Specificulture = model.Culture.Specificulture,
                Alias = model.Culture.Alias,
                Icon = model.Culture.Icon,
                DisplayName = model.Culture.FullName,
                SystemName = model.Culture.Specificulture,
                Description = model.SiteName,
                Status = MixContentStatus.Published,
                CreatedDateTime = DateTime.UtcNow,
            };
        }

        protected override async Task<MixTenant> SaveHandlerAsync()
        {
            var entity = await base.SaveHandlerAsync();
            return entity;
        }

        protected override async Task SaveEntityRelationshipAsync(MixTenant parent)
        {
            Culture.MixTenantId = parent.Id;

            // Save and subscribe result for current consumer
            // Or can use this instead of _consumer to listen result in this viewmodel 
            // Then override ConsumeAsync to handle result
            await Culture.SaveAsync(UowInfo, _consumer);
        }

        public override void InitDefaultValues(string language = null, int? cultureId = null)
        {
            CreatedDateTime = DateTime.UtcNow;
            Status = MixContentStatus.Published;
        }
    }
}

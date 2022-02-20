﻿namespace Mix.Portal.Domain.ViewModels
{
    [GenerateRestApiController(Route = "api/v2/rest/portal/mix-configuration", Name = "Mix Configuration")]
    public class MixConfigurationViewModel
        : SiteDataWithContentViewModelBase
            <MixCmsContext, MixConfiguration, int, MixConfigurationViewModel,
            MixConfigurationContent, MixConfigurationContentViewModel>
    {
        private Repository<MixCmsContext, MixConfigurationContent, int, MixConfigurationContentViewModel> _contentQueryRepository;

        #region Contructores

        public MixConfigurationViewModel()
        {

        }

        public MixConfigurationViewModel(UnitOfWorkInfo unitOfWorkInfo) : base(unitOfWorkInfo)
        {
        }

        public MixConfigurationViewModel(MixConfiguration entity,

            UnitOfWorkInfo uowInfo = null) : base(entity, uowInfo)
        {
        }

        #endregion

        #region Properties

        public string SystemName { get; set; }

        #endregion

        #region Overrides

        public override async Task ExpandView()
        {
            _contentQueryRepository = MixConfigurationContentViewModel.GetRepository(UowInfo);

            Contents = await _contentQueryRepository.GetListAsync(
                        m => m.ParentId == Id);
        }

        protected override async Task SaveEntityRelationshipAsync(MixConfiguration parentEntity)
        {
            if (Contents != null)
            {
                foreach (var item in Contents)
                {
                    item.SetUowInfo(UowInfo);
                    item.ParentId = parentEntity.Id;
                    await item.SaveAsync();
                }
            }
        }

        #endregion
    }
}

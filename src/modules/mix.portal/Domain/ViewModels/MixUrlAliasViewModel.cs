﻿namespace Mix.Portal.Domain.ViewModels
{
    [GenerateRestApiController]
    public class MixUrlAliasViewModel
        : SiteDataViewModelBase<MixCmsContext, MixUrlAlias, int, MixUrlAliasViewModel>
    {
        #region Properties
        public int? SourceContentId { get; set; }

        public Guid? SourceContentGuidId { get; set; }

        public string Alias { get; set; }

        public MixUrlAliasType Type { get; set; }
        #endregion

        #region Contructors

        public MixUrlAliasViewModel()
        {

        }

        public MixUrlAliasViewModel(UnitOfWorkInfo unitOfWorkInfo) : base(unitOfWorkInfo)
        {
        }

        public MixUrlAliasViewModel(MixUrlAlias entity,
            MixCacheService cacheService = null,
            UnitOfWorkInfo uowInfo = null)
            : base(entity, cacheService, uowInfo)
        {
        }

        #endregion

        #region Overrides

        #endregion
    }
}

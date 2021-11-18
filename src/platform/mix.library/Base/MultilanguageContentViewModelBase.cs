﻿using Microsoft.EntityFrameworkCore;
using Mix.Heart.Entities;

namespace Mix.Lib.Base
{
    public abstract class MultilanguageContentViewModelBase<TDbContext, TEntity, TPrimaryKey, TView> 
        : ViewModelBase<TDbContext, TEntity, TPrimaryKey, TView>
         where TDbContext : DbContext
         where TPrimaryKey : IComparable
        where TEntity : class, IEntity<TPrimaryKey>
        where TView : ViewModelBase<TDbContext, TEntity, TPrimaryKey, TView>
    {
        #region Contructors

        public MultilanguageContentViewModelBase()
        {

        }

        protected MultilanguageContentViewModelBase(UnitOfWorkInfo unitOfWorkInfo) : base(unitOfWorkInfo)
        {
        }

        protected MultilanguageContentViewModelBase(TEntity entity,
            MixCacheService cacheService = null,
            UnitOfWorkInfo uowInfo = null) : base(entity, cacheService, uowInfo)
        {
        }

        #endregion

        #region Properties

        public string Specificulture { get; set; }

        public TPrimaryKey ParentId { get; set; }
        public int MixCultureId { get; set; }

        #endregion

        #region Overrides

        public override void InitDefaultValues(string language = null, int? cultureId = null)
        {
            base.InitDefaultValues(language, cultureId);
            Specificulture ??= language;
            MixCultureId = cultureId ?? 1;
        }

        #endregion

    }
}

﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mix.Lib.Services;

namespace Mix.Lib.Controllers
{
    public class MixAutoGenerateQueryApiController<TView, TDbContext, TEntity, TPrimaryKey>
        : MixQueryApiControllerBase<TView, TDbContext, TEntity, TPrimaryKey>
        where TPrimaryKey : IComparable
        where TDbContext : DbContext
        where TEntity : EntityBase<TPrimaryKey>
        where TView : ViewModelBase<TDbContext, TEntity, TPrimaryKey, TView>
    {
        public MixAutoGenerateQueryApiController(
            IConfiguration configuration,
            MixService mixService,
            TranslatorService translator,
            EntityRepository<MixCmsContext, MixCulture, int> cultureRepository,
            MixIdentityService mixIdentityService,
            TDbContext context,
            MixCacheService cacheService,
            IQueueService<MessageQueueModel> queueService)
            : base(configuration, mixService, translator, cultureRepository, mixIdentityService, context, queueService)
        {
        }
    }
}

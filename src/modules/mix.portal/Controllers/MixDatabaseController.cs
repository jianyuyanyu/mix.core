﻿using Microsoft.AspNetCore.Mvc;

namespace Mix.Portal.Controllers
{
    [Route("api/v2/rest/mix-portal/mix-database")]
    [ApiController]
    public class MixDatabaseController
        : MixRestApiControllerBase<MixDatabaseViewModel, MixCmsContext, MixDatabase, int>
    {
        public MixDatabaseController(
            IConfiguration configuration,
            MixService mixService,
            TranslatorService translator,
            EntityRepository<MixCmsContext, MixCulture, int> cultureRepository,
            MixIdentityService mixIdentityService,
            MixCmsContext context,
            MixCacheService cacheService,
            IQueueService<MessageQueueModel> queueService)
            : base(configuration, mixService, translator, cultureRepository, mixIdentityService, context, queueService)
        {

        }

        #region Overrides

        protected override Task<ActionResult> DeleteHandler(MixDatabaseViewModel data)
        {
            if (data.Type == MixDatabaseType.System)
            {
                throw new MixException($"Cannot Delete System Database: {data.SystemName}");
            }
            return base.DeleteHandler(data);
        }
        #endregion


    }
}

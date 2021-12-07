﻿using Microsoft.EntityFrameworkCore;
using Mix.Heart.Repository;
using Mix.Lib.Services;
using Microsoft.Extensions.Configuration;
using Mix.Database.Entities.Base;
using Mix.Lib.Dtos;
using Mix.Lib.Models.Common;
using Mix.Heart.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Mix.Lib.Base
{
    public class MixAssociationApiControllerBase<TView, TDbContext, TEntity>
        : MixRestApiControllerBase<TView, TDbContext, TEntity, int>
        where TDbContext : DbContext
        where TEntity : AssociationBase<int>
        where TView : ViewModelBase<TDbContext, TEntity, int, TView>
    {
        public MixAssociationApiControllerBase(
            IConfiguration configuration,
            MixService mixService,
            TranslatorService translator,
            EntityRepository<MixCmsContext, MixCulture, int> cultureRepository,
            MixIdentityService mixIdentityService,
            TDbContext context,
            MixCacheService cacheService,
            IQueueService<MessageQueueModel> queueService)
            : base(configuration, mixService, translator, cultureRepository, mixIdentityService, context, cacheService, queueService)
        {
        }
        protected override SearchQueryModel<TEntity, int> BuildSearchRequest(SearchRequestDto req)
        {
            var request = new SearchAssociationDto(req, Request);
            var searchRequest = base.BuildSearchRequest(request);

            searchRequest.Predicate = searchRequest.Predicate.AndAlsoIf(
                request.LeftId.HasValue,
                m => m.LeftId == request.LeftId.Value);

            searchRequest.Predicate = searchRequest.Predicate.AndAlsoIf(
                request.RightId.HasValue,
                m => m.RightId == request.RightId.Value);

            return searchRequest;
        }


    }
}

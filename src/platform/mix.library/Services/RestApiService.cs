﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mix.Lib.Interfaces;
using Mix.Lib.Models.Common;
using Mix.Mq.Lib.Models;
using Mix.SignalR.Enums;
using Mix.SignalR.Interfaces;
using Mix.SignalR.Models;
using Newtonsoft.Json;

namespace Mix.Lib.Services
{
    public class RestApiService<TView, TDbContext, TEntity, TPrimaryKey> : TenantServiceBase, IRestApiService<TView, TDbContext, TEntity, TPrimaryKey>
        where TPrimaryKey : IComparable
        where TDbContext : DbContext
        where TEntity : class, IEntity<TPrimaryKey>
        where TView : SimpleViewModelBase<TDbContext, TEntity, TPrimaryKey, TView>
    {
        protected readonly MixIdentityService MixIdentityService;
        protected readonly IMemoryQueueService<MessageQueueModel> QueueService;
        protected readonly IPortalHubClientService PortalHub;
        protected UnitOfWorkInfo Uow;
        protected UnitOfWorkInfo CacheUow;
        public Repository<TDbContext, TEntity, TPrimaryKey, TView> Repository { get; set; }
        protected readonly TDbContext Context;

        public RestApiService(
            IHttpContextAccessor httpContextAccessor,
            MixIdentityService identityService,
            UnitOfWorkInfo<TDbContext> uow,
            IMemoryQueueService<MessageQueueModel> queueService,
            MixCacheService cacheService,
            IPortalHubClientService portalHub,
            IMixTenantService mixTenantService)
            : base(httpContextAccessor, cacheService, mixTenantService)
        {
            MixIdentityService = identityService;
            Uow = uow;
            QueueService = queueService;
            Context = (TDbContext)uow.ActiveDbContext;
            Repository ??= SimpleViewModelBase<TDbContext, TEntity, TPrimaryKey, TView>.GetRepository(Uow, cacheService);
            Repository.CacheService = cacheService;
            PortalHub = portalHub;
        }

        #region Command Handlers

        public virtual async Task<TPrimaryKey> CreateHandlerAsync(
            TView data, 
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (data == null)
            {
                throw new MixException(MixErrorStatus.Badrequest, "Null Object");
            }

            if (ReflectionHelper.HasProperty(typeof(TView), MixRequestQueryKeywords.TenantId))
            {
                ReflectionHelper.SetPropertyValue(data, new EntityPropertyModel()
                {
                    PropertyName = MixRequestQueryKeywords.TenantId,
                    PropertyValue = CurrentTenant?.Id
                });
            }

            data.SetUowInfo(Uow, CacheService);
            var id = await data.SaveAsync(cancellationToken);
            await PortalHub.SendMessageAsync(new SignalRMessageModel()
            {
                Action = MessageAction.NewQueueMessage,
                Title = MixQueueTopics.MixViewModelChanged,
                Message = ViewModelAction.Create.ToString(),
                Data = ReflectionHelper.ParseObject(data).ToString(),
                Type = MessageType.Success,
            });

            QueueService.PushMemoryQueue(CurrentTenant.Id, MixQueueTopics.MixViewModelChanged, MixRestAction.Post.ToString(), data);
            return id;
        }

        public virtual async Task UpdateHandler(
            TPrimaryKey id, 
            TView data, 
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var currentId = ReflectionHelper.GetPropertyValue(data, "Id").ToString();
            if (id.ToString() != currentId)
            {
                throw new MixException(MixErrorStatus.Badrequest, "Invalid Id");
            }
            data.SetUowInfo(Uow, CacheService);
            await data.SaveAsync(cancellationToken);
            await CacheService.RemoveCacheAsync(id, Repository.CacheFolder, cancellationToken);
            await PortalHub.SendMessageAsync(new SignalRMessageModel()
            {
                Action = MessageAction.NewQueueMessage,
                Title = MixQueueTopics.MixViewModelChanged,
                Message = ViewModelAction.Update.ToString(),
                Data = ReflectionHelper.ParseObject(data).ToString(),
                Type = MessageType.Success,
            });
            QueueService.PushMemoryQueue(CurrentTenant.Id, MixQueueTopics.MixViewModelChanged, MixRestAction.Put.ToString(), data);
        }

        public virtual async Task DeleteHandler(TView data, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            data.SetUowInfo(Uow, CacheService);
            await data.DeleteAsync(cancellationToken);
            await CacheService.RemoveCacheAsync(data.Id.ToString(), Repository.CacheFolder, cancellationToken);
            await PortalHub.SendMessageAsync(new SignalRMessageModel()
            {
                Action = MessageAction.NewQueueMessage,
                Title = MixQueueTopics.MixViewModelChanged,
                Message = ViewModelAction.Delete.ToString(),
                Data = ReflectionHelper.ParseObject(data).ToString(),
                Type = MessageType.Success,
            });
            QueueService.PushMemoryQueue(CurrentTenant.Id, MixQueueTopics.MixViewModelChanged, MixRestAction.Delete.ToString(), data);
        }

        public virtual async Task PatchHandler(
            TPrimaryKey id, 
            TView data, 
            IEnumerable<EntityPropertyModel> properties, 
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            data.SetUowInfo(Uow, CacheService);
            await data.SaveFieldsAsync(properties, cancellationToken);
            await CacheService.RemoveCacheAsync(id.ToString(), Repository.CacheFolder, cancellationToken);
            QueueService.PushMemoryQueue(CurrentTenant.Id, MixQueueTopics.MixViewModelChanged, MixRestAction.Patch.ToString(), data);
        }

        public virtual async Task SaveManyHandler(List<TView> data, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            foreach (var item in data)
            {
                item.SetUowInfo(Uow, CacheService);
                await item.SaveAsync(cancellationToken);
            }
        }

        #endregion

        #region Query Handlers
        public virtual async Task<PagingResponseModel<TView>> SearchHandler(
            SearchRequestDto request,
            SearchQueryModel<TEntity, TPrimaryKey> searchQuery,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await Repository.GetPagingAsync(searchQuery.Predicate, searchQuery.PagingData, cancellationToken);
        }

        public virtual PagingResponseModel<TView> ParseSearchResult(
            SearchRequestDto request,
            PagingResponseModel<TView> result,
            JsonSerializer serializer = null)
        {
            if (!string.IsNullOrEmpty(request.Columns))
            {
                Repository.SetSelectedMembers(request.Columns.Split(',', StringSplitOptions.TrimEntries));
            }

            if (string.IsNullOrEmpty(request.Columns) && serializer is null)
            {
                return result;
            }

            List<TView> objects = [];
            foreach (var item in result.Items)
            {
                objects.Add(ReflectionHelper.GetMembers(item, Repository.SelectedMembers).ToObject<TView>());
            }

            return new PagingResponseModel<TView>()
            {
                Items = objects,
                PagingData = result.PagingData
            };
        }

        #endregion

        #region Helpers

        public virtual SearchQueryModel<TEntity, TPrimaryKey> BuildSearchRequest(SearchRequestDto request)
        {
            if (!request.PageSize.HasValue)
            {
                request.PageSize = CurrentTenant.Configurations.MaxPageSize;
            }

            return new SearchQueryModel<TEntity, TPrimaryKey>(HttpContextAccessor.HttpContext!.Request, request, CurrentTenant.Id);
        }

        public virtual async Task<TView> GetById(TPrimaryKey id)
        {
            return await Repository.GetSingleAsync(id);
        }

        #endregion
    }
}

﻿using Microsoft.EntityFrameworkCore;
using Mix.Constant.Enums;
using Mix.Database.Entities.Account;
using Mix.Identity.Models.AccountViewModels;
using Mix.Identity.Models.ManageViewModels;
using Mix.Lib.Models.Common;
using Mix.Lib.Services;
using Mix.RepoDb.Repositories;
using RepoDb;
using RepoDb.Enumerations;
using System.Text.Json.Serialization;

namespace Mix.Lib.ViewModels
{
    public class MixUserViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? LockoutEnd { get; set; }
        public bool IsActived { get; set; }
        public System.DateTime? LastModified { get; set; }
        public string ModifiedBy { get; set; }

        public string PhoneNumber { get; set; }

        public string Avatar { get; set; }
        public string NickName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime? DOB { get; set; }

        public FileModel MediaFile { get; set; } = new();

        public JObject UserData { get; set; }

        public List<AspNetUserRoles> Roles { get; set; }

        public List<string> Endpoints { get; set; } = new();

        #region Change Password

        public ResetPasswordViewModel ResetPassword { get; set; }

        public bool IsChangePassword { get; set; }

        public ChangePasswordViewModel ChangePassword { get; set; }
        [JsonIgnore]
        private UnitOfWorkInfo _cmsUow { get; }

        #endregion Change Password


        public MixUserViewModel(MixUser user, UnitOfWorkInfo uow)
        {
            ReflectionHelper.Mapping(user, this);
            _cmsUow = uow;
        }


        public async Task<AdditionalDataContentViewModel> CreatetUserData(int tenantId, JObject userData)
        {
            var data = new AdditionalDataContentViewModel(_cmsUow)
            {
                MixTenantId = tenantId,
                Data = userData,
                GuidParentId = Id,
                MixDatabaseName = MixDatabaseNames.SYSTEM_USER_DATA,
                ParentType = MixDatabaseParentType.User
            };
            await data.SaveAsync();
            return data;
        }

        public async Task LoadUserDataAsync(int tenantId, MixRepoDbRepository repoDbRepository)
        {
            if (!GlobalConfigService.Instance.IsInit)
            {
                try
                {
                    MixDatabaseViewModel database = await MixDatabaseViewModel.GetRepository(_cmsUow)
                        .GetSingleAsync(m => m.SystemName == MixDatabaseNames.SYSTEM_USER_DATA);
                    repoDbRepository.Init(MixDatabaseNames.SYSTEM_USER_DATA);
                    var data = await repoDbRepository.GetSingleByParentAsync(MixContentType.User, Id);
                    if (data != null)
                    {

                        UserData = data != null ? JObject.FromObject(data) : null;
                        foreach (var relation in database.Relationships)
                        {
                            var associations = await MixDatabaseAssociationViewModel.GetRepository(_cmsUow)
                                        .GetAllAsync(m => m.ParentDatabaseName == MixDatabaseNames.SYSTEM_USER_DATA
                                                        && m.ChildDatabaseName == relation.DestinateDatabaseName
                                                        && m.ParentId == UserData.Value<int>("id"));
                            if (associations.Count > 0)
                            {
                                var ids = associations.Select(m => m.ChildId).ToList();

                                repoDbRepository.Init(relation.DestinateDatabaseName);
                                var nestedData = await repoDbRepository.GetListByAsync(new List<QueryField> {
                                    new("id", ids.First())
                                });
                                if (nestedData != null)
                                {
                                    UserData.Add(new JProperty(relation.DisplayName, JArray.FromObject(nestedData)));
                                }
                            }
                        }
                    }
                    using var context = new MixCmsAccountContext();
                    var roles = from ur in context.AspNetUserRoles
                                join r in context.MixRoles
                                on ur.RoleId equals r.Id
                                where ur.UserId == Id && ur.MixTenantId == tenantId
                                select ur;
                    Roles = await roles.ToListAsync();


                }
                catch (Exception ex)
                {
                    MixService.LogException(ex);
                }
            }
        }

        //public async Task LoadUserEndpointsAsync(int tenantId, MixRepoDbRepository repoDbRepository)
        //{
        //    repoDbRepository.Init(MixDatabaseNames.SYSTEM_ENDPOINT);
        //    List<JObject> endpoints = new();
        //    foreach (var role in Roles)
        //    {
        //        var temp = await repoDbRepository.get

        //            GetByAllParent<MixDataContentViewModel>(
        //            new SearchDataContentModel(tenantId)
        //            {
        //                MixDatabaseName = MixDatabaseNames.SYSTEM_ENDPOINT,
        //                GuidParentId = role.RoleId
        //            });
        //        endpoints.AddRange(temp.Select(m => m.Data));
        //    }

        //    if (UserData != null && UserData.Data.ContainsKey("endpoints"))
        //    {
        //        var userEndpoints = UserData.Data.Value<JArray>("endpoints");
        //        endpoints.AddRange(userEndpoints.ToObject<List<JObject>>());
        //    }

        //    Endpoints = endpoints.Select(e =>
        //    {
        //        string method = e.Value<string>("method")?.ToLower();
        //        string path = e.Value<string>("path")?.ToLower();
        //        return $"{method} - {path}";
        //    }).Distinct().ToList();
        //}

    }
}

﻿namespace Mix.Portal.Domain.ViewModels
{
    public class MixDataContentViewModel 
        : HaveParentContentViewModelBase<MixCmsContext, MixDataContent, Guid, MixDataContentViewModel>
    {
        #region Contructors

        public MixDataContentViewModel()
        {
        }

        public MixDataContentViewModel(MixDataContent entity) : base(entity)
        {
        }

        public MixDataContentViewModel(UnitOfWorkInfo unitOfWorkInfo = null) : base(unitOfWorkInfo)
        {
        }

        public MixDataContentViewModel(string lang, int cultureId, string databaseName, JObject data)
        {
            Specificulture = lang;
            MixCultureId = cultureId;
            MixDatabaseName = databaseName;
            Data = data;
        }

        public MixDataContentViewModel(MixDataContent entity,
            MixCacheService cacheService = null,
            UnitOfWorkInfo uowInfo = null) : base(entity, cacheService, uowInfo)
        {
        }
        #endregion

        #region Properties
        public int MixDatabaseId { get; set; }
        public string MixDatabaseName { get; set; }
        public List<MixDatabaseColumnViewModel> Columns { get; set; }
        public List<MixDataContentValueViewModel> Values { get; set; }
        public JObject Data { get; set; }

        public List<MixDataContentViewModel> ChildData { get; set; } = new();
        public List<MixDataContentAssociationViewModel> RelatedData { get; set; } = new();

        public Guid? GuidParentId { get; set; }
        public int? IntParentId { get; set; }
        public MixDatabaseParentType ParentType { get; set; }
        #endregion

        #region Overrides

        public override async Task ExpandView(MixCacheService cacheService = null)
        {
            using var colRepo = MixDatabaseColumnViewModel.GetRepository(UowInfo);
            using var valRepo = MixDataContentValueViewModel.GetRepository(UowInfo);

            Columns ??= await colRepo.GetListAsync(m => m.MixDatabaseName == MixDatabaseName, cacheService, UowInfo);
            Values ??= await valRepo.GetListAsync(m => m.ParentId == Id, cacheService, UowInfo);

            if (Data == null)
            {
                Data = MixDataHelper.ParseData(Id, UowInfo);
            }

            await Data.LoadAllReferenceDataAsync(Id, MixDatabaseName, UowInfo);
        }

        public override async Task<MixDataContent> ParseEntity(MixCacheService cacheService = null)
        {
            using var colRepo = MixDatabaseColumnViewModel.GetRepository(UowInfo);
            using var valRepo = MixDataContentValueViewModel.GetRepository(UowInfo);

            if (IsDefaultId(Id))
            {
                Id = Guid.NewGuid();
                CreatedDateTime = DateTime.UtcNow;
            }

            if (string.IsNullOrEmpty(MixDatabaseName))
            {
                MixDatabaseName = Context.MixDatabase.First(m => m.Id == MixDatabaseId)?.SystemName;
            }
            if (MixDatabaseId == 0)
            {
                MixDatabaseId = Context.MixDatabase.First(m => m.SystemName == MixDatabaseName)?.Id ?? 0;
            }

            Columns ??= await colRepo.GetListAsync(m => m.MixDatabaseName == MixDatabaseName, cacheService, UowInfo);
            Values ??= await valRepo.GetListAsync(m => m.ParentId == Id, cacheService, UowInfo);

            await ParseObjectToValues(cacheService);

            Title = Id.ToString();
            Data = MixDataHelper.ParseData(Id, UowInfo);
            Content = Data.ToString(Newtonsoft.Json.Formatting.None);
            return await base.ParseEntity();
        }

        protected override async Task<MixDataContent> SaveHandlerAsync()
        {
            var result = await base.SaveHandlerAsync();

            var assoRepo = MixDataContentAssociationViewModel.GetRepository(UowInfo);

            if (!MixCmsHelper.IsDefaultId(GuidParentId) || !MixCmsHelper.IsDefaultId(IntParentId))
            {
                var getNav = await assoRepo.CheckIsExistsAsync(
                    m => m.DataContentId == Id
                    && (m.GuidParentId == GuidParentId || m.IntParentId == IntParentId)
                    && m.ParentType == ParentType
                    && m.Specificulture == Specificulture);
                if (!getNav)
                {
                    var nav = new MixDataContentAssociationViewModel(UowInfo)
                    {
                        DataContentId = Id,
                        Specificulture = Specificulture,
                        MixDatabaseId = MixDatabaseId,
                        MixDatabaseName = MixDatabaseName,
                        ParentType = ParentType,
                        GuidParentId = GuidParentId,
                        IntParentId = IntParentId,
                        Status = MixContentStatus.Published
                    };
                    var saveResult = await nav.SaveAsync();
                }
            }
            Data = MixDataHelper.ParseData(Id, UowInfo);
            return result;
        }
        protected override async Task SaveEntityRelationshipAsync(MixDataContent parentEntity)
        {
            if (Values != null)
            {
                foreach (var item in Values)
                {
                    item.SetUowInfo(UowInfo);
                    item.ParentId = parentEntity.Id;
                    item.Specificulture = Specificulture;
                    item.ParentId = parentEntity.Id;
                    item.MixDatabaseName = parentEntity.MixDatabaseName;
                    await item.SaveAsync(UowInfo);
                }
            }
        }

        protected override async Task DeleteHandlerAsync()
        {
            if (Repository.GetListQuery(m => m.ParentId == ParentId).Count() == 1)
            {
                var dataRepo = MixDataViewModel.GetRepository(UowInfo);

                await Repository.DeleteAsync(Id);
                await dataRepo.DeleteAsync(m => m.Id == ParentId);
            }
            else
            {
                await base.DeleteHandlerAsync();
            }
        }
        #endregion

        #region Helper

        public void ToModelValue(MixDataContentValueViewModel item,
           JToken property)
        {
            if (property == null)
            {
                return;
            }

            if (item.Column.ColumnConfigurations.IsEncrypt)
            {
                var obj = property.Value<JObject>();
                item.StringValue = obj.ToString(Formatting.None);
                item.EncryptValue = obj["data"]?.ToString();
                item.EncryptKey = obj["key"]?.ToString();
            }
            else
            {
                switch (item.Column.DataType)
                {
                    case MixDataType.DateTime:
                        item.DateTimeValue = property.Value<DateTime?>();
                        item.StringValue = property.Value<string>();
                        break;

                    case MixDataType.Date:
                        item.DateTimeValue = property.Value<DateTime?>();
                        item.StringValue = property.Value<string>();
                        break;

                    case MixDataType.Time:
                        item.DateTimeValue = property.Value<DateTime?>();
                        item.StringValue = property.Value<string>();
                        break;

                    case MixDataType.Double:
                        item.DoubleValue = property.Value<double?>();
                        item.StringValue = property.Value<string>();
                        break;

                    case MixDataType.Boolean:
                        item.BooleanValue = property.Value<bool?>();
                        item.StringValue = property.Value<string>()?.ToLower();
                        break;

                    case MixDataType.Integer:
                        item.IntegerValue = property.Value<int?>();
                        item.StringValue = property.Value<string>();
                        break;

                    case MixDataType.Reference:
                        item.StringValue = property.Value<string>();
                        break;

                    case MixDataType.Upload:
                        string mediaData = property.Value<string>();
                        item.StringValue = mediaData;
                        break;

                    case MixDataType.Custom:
                    case MixDataType.Duration:
                    case MixDataType.PhoneNumber:
                    case MixDataType.Text:
                    case MixDataType.Html:
                    case MixDataType.MultilineText:
                    case MixDataType.EmailAddress:
                    case MixDataType.Password:
                    case MixDataType.Url:
                    case MixDataType.ImageUrl:
                    case MixDataType.CreditCard:
                    case MixDataType.PostalCode:
                    case MixDataType.Color:
                    case MixDataType.Icon:
                    case MixDataType.VideoYoutube:
                    case MixDataType.TuiEditor:
                    default:
                        item.StringValue = property.Value<string>();
                        break;
                }
            }
        }


        private async Task ParseObjectToValues(MixCacheService cacheService = null)
        {
            Data ??= new JObject();
            foreach (var col in Columns.OrderBy(f => f.Priority))
            {
                var val = await GetFieldValue(col, cacheService);
                val.DataType = col.DataType;
                val.MixDatabaseColumnId = col.Id;
                val.MixDatabaseName = col.MixDatabaseName;
                val.MixDatabaseId = col.MixDatabaseId;
                if (Data[val.MixDatabaseColumnName] != null)
                {
                    if (val.Column.DataType == MixDataType.Reference)
                    {
                        var arr = Data[val.MixDatabaseColumnName].Value<JArray>();
                        val.IntegerValue = val.Column.ReferenceId;
                        val.StringValue = val.Column.ReferenceId.ToString();
                        if (arr != null)
                        {
                            foreach (JObject objData in arr)
                            {
                                Guid id = objData["id"]?.Value<Guid>() ?? Guid.Empty;
                                // if have id => update data, else add new
                                if (id != Guid.Empty)
                                {
                                    var data = await Repository.GetSingleAsync(m => m.Id == id);
                                    data.Data = objData;
                                    ChildData.Add(data);
                                }
                                else
                                {
                                    ChildData.Add(new MixDataContentViewModel()
                                    {
                                        Specificulture = Specificulture,
                                        MixDatabaseId = col.ReferenceId.Value,
                                        Data = objData["obj"].Value<JObject>()
                                    });
                                }
                            }
                        }
                    }
                    else
                    {
                        ToModelValue(val, Data[val.MixDatabaseColumnName]);
                    }
                }
            }
        }

        private async Task<MixDataContentValueViewModel> GetFieldValue(
            MixDatabaseColumnViewModel field, 
            MixCacheService cacheService = null)
        {
            var val = Values.FirstOrDefault(v => v.MixDatabaseColumnId == field.Id);
            if (val == null)
            {
                val = new MixDataContentValueViewModel(UowInfo)
                {
                    MixDatabaseColumnId = field.Id,
                    MixDatabaseColumnName = field.SystemName,
                    StringValue = field.DefaultValue,
                    Priority = field.Priority,
                    Column = field,
                    ParentId = Id,
                    CreatedDateTime = DateTime.UtcNow,
                    CreatedBy = CreatedBy
                };
                await val.ExpandView(cacheService);
                Values.Add(val);
            }
            val.Status = Status;
            val.LastModified = DateTime.UtcNow;
            val.Priority = field.Priority;
            val.MixDatabaseName = MixDatabaseName;
            return val;
        }

        public bool HasValue(string fieldName)
        {
            return Data != null && Data.Value<string>(fieldName) != null;
        }

        public T Property<T>(string fieldName)
        {
            return Data.Property(fieldName).Value<T>();
        }

        public override async Task<Guid> CreateParentAsync()
        {
            MixDataViewModel parent = new(UowInfo)
            {
                Id = Guid.NewGuid(),
                CreatedDateTime = DateTime.UtcNow,
                MixTenantId = 1,
                MixDatabaseId = MixDatabaseId,
                MixDatabaseName = MixDatabaseName,
                CreatedBy = CreatedBy,
                DisplayName = Title,
                Description = Excerpt
            };
            return await parent.SaveAsync();
        }

        #endregion
    }
}

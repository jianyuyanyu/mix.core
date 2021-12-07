﻿using Mix.Shared.Enums;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace Mix.Lib.ViewModels
{
    public class MixDataContentValueViewModel
        : MultilanguageContentViewModelBase<MixCmsContext, MixDataContentValue, Guid, MixDataContentValueViewModel>
    {
        #region Properties

        public string MixDatabaseColumnName { get; set; }
        public string MixDatabaseName { get; set; }
        public MixDataType DataType { get; set; }
        public bool? BooleanValue { get; set; }
        public DateTime? DateTimeValue { get; set; }
        public double? DoubleValue { get; set; }
        public int? IntegerValue { get; set; }
        public string StringValue { get; set; }
        public string EncryptValue { get; set; }
        public string EncryptKey { get; set; }
        public MixEncryptType EncryptType { get; set; }

        public Guid MixDataContentId { get; set; }
        public int MixDatabaseColumnId { get; set; }

        public MixDatabaseColumnViewModel Column { get; set; }

        #endregion

        #region Contructors

        public MixDataContentValueViewModel()
        {

        }

        public MixDataContentValueViewModel(UnitOfWorkInfo unitOfWorkInfo) : base(unitOfWorkInfo)
        {
        }

        public MixDataContentValueViewModel(MixDataContentValue entity,
            MixCacheService cacheService = null,
            UnitOfWorkInfo uowInfo = null) : base(entity, cacheService, uowInfo)
        {
        }

        #endregion

        #region Overrides

        public override Task<MixDataContentValue> ParseEntity(MixCacheService cacheService = null)
        {
            Priority = Column?.Priority ?? Priority;
            DataType = Column?.DataType ?? DataType;

            MixDatabaseColumnName = Column?.SystemName;
            MixDatabaseColumnId = Column?.Id ?? 0;
            return base.ParseEntity(cacheService);
        }

        public override async Task ExpandView(MixCacheService cacheService = null)
        {
            using var colRepo = MixDatabaseColumnViewModel.GetRepository(UowInfo);
            Column = await colRepo.GetSingleAsync(MixDatabaseColumnId, cacheService);
            if (MixDatabaseColumnId > 0)
            {
                Column ??= await colRepo.GetSingleAsync(MixDatabaseColumnId);
                MixDatabaseName = Column.MixDatabaseName;
            }
            else // additional field for page / post / module => id = 0
            {
                Column = new()
                {
                    DataType = DataType,
                    DisplayName = MixDatabaseColumnName,
                    SystemName = MixDatabaseColumnName,
                    Priority = Priority
                };
            }
        }

        #endregion

        #region Expands

        public JProperty ToJProperty()
        {
            switch (DataType)
            {
                case MixDataType.DateTime:
                    return new JProperty(MixDatabaseColumnName, DateTimeValue);

                case MixDataType.Date:
                    if (!DateTimeValue.HasValue)
                    {
                        if (DateTime.TryParseExact(
                            StringValue,
                            "MM/dd/yyyy HH:mm:ss",
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.RoundtripKind,
                            out DateTime date))
                        {
                            DateTimeValue = date;
                        }
                    }
                    return (new JProperty(MixDatabaseColumnName, DateTimeValue));

                case MixDataType.Time:
                    return (new JProperty(MixDatabaseColumnName, DateTimeValue));

                case MixDataType.Double:
                    return (new JProperty(MixDatabaseColumnName, DoubleValue ?? 0));

                case MixDataType.Boolean:
                    return (new JProperty(MixDatabaseColumnName, BooleanValue));

                case MixDataType.Integer:
                    return (new JProperty(MixDatabaseColumnName, IntegerValue ?? 0));

                case MixDataType.Reference:
                    return (new JProperty(MixDatabaseColumnName, new JArray()));

                case MixDataType.Upload:
                    string url = !string.IsNullOrEmpty(StringValue)
                   ? !StringValue.Contains(GlobalConfigService.Instance.AppSettings.Domain)
                        ? $"{GlobalConfigService.Instance.AppSettings.Domain.TrimEnd('/')}/{StringValue.TrimStart('/')}"
                        : StringValue
                   : null;
                    return (new JProperty(MixDatabaseColumnName, url));
                case MixDataType.Tag:
                    try
                    {
                        return (new JProperty(MixDatabaseColumnName, JArray.Parse(StringValue)));
                    }
                    catch
                    {
                        return (new JProperty(MixDatabaseColumnName, new JArray()));
                    }
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
                    return (new JProperty(MixDatabaseColumnName, StringValue));
            }
        }

        #endregion
    }
}

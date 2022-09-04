﻿using System.ComponentModel.DataAnnotations;

namespace Mix.Lib.ViewModels
{
    public class MixDatabaseRelationshipViewModel
        : ViewModelBase<MixCmsContext, MixDatabaseRelationship, int, MixDatabaseRelationshipViewModel>
    {
        #region Properties
        public int ParentId { get; set; }
        public int ChildId { get; set; }
        public string DisplayName { get; set; }
        public string SourceDatabaseName { get; set; }
        public string DestinateDatabaseName { get; set; }
        public MixDatabaseRelationshipType Type { get; set; }
        #endregion

        #region Constructors

        public MixDatabaseRelationshipViewModel()
        {

        }

        public MixDatabaseRelationshipViewModel(UnitOfWorkInfo unitOfWorkInfo) : base(unitOfWorkInfo)
        {
        }

        public MixDatabaseRelationshipViewModel(MixDatabaseRelationship entity, UnitOfWorkInfo uowInfo = null)
            : base(entity, uowInfo)
        {
        }

        #endregion

        #region Overrides
        public override async Task Validate()
        {
            await base.Validate();

            if (Repository.Table.Any(m => !m.Id.Equals(Id) && m.ParentId.Equals(ParentId) && m.ChildId.Equals(ChildId)))
            {
                IsValid = false;
                Errors.Add(new ValidationResult("Entity Existed"));
            }
            if (MixHelper.IsDefaultId(ParentId))
            {
                IsValid = false;
                Errors.Add(new("Parent Id cannot be null"));
            }
            if (MixHelper.IsDefaultId(ChildId))
            {
                IsValid = false;
                Errors.Add(new("Child Id cannot be null"));
            }
            if (ParentId == 0 || ChildId == 0)
            {
                IsValid = false;
                Errors.Add(new($"Ivalid relationship: parent Id = {ParentId} - child Id = {ChildId} - Type = {Type}"));
            }

        }

        protected override async Task DeleteHandlerAsync()
        {
            var leftDb = Context.MixDatabase.Find(ParentId);
            string leftColName = $"{leftDb.SystemName}Id";
            var rightDb = Context.MixDatabase.Find(ChildId);
            string rightColName = $"{rightDb.SystemName}Id";
            await MixDatabaseColumnViewModel.GetRepository(UowInfo).DeleteManyAsync(
                m => (m.MixDatabaseId == ParentId && m.SystemName == rightColName)
                    || (m.MixDatabaseId == ChildId && m.SystemName == leftColName));
            await base.DeleteHandlerAsync();
        }

        #endregion
    }
}

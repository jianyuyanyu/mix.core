﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mix.Database.Entities.Cms;
using Mix.Database.EntityConfigurations.POSTGRES.Base;

namespace Mix.Database.EntityConfigurations.POSTGRES
{
    public class MixPagePostAssociationConfiguration : PostgresAssociationBaseConfiguration<MixPagePostAssociation, int>
    {
        public override void Configure(EntityTypeBuilder<MixPagePostAssociation> builder)
        {
            base.Configure(builder);
        }
    }
}

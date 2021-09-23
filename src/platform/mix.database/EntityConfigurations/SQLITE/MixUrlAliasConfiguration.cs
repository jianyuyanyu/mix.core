﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mix.Database.Entities.Cms;
using Mix.Database.EntityConfigurations.SQLITE.Base;

namespace Mix.Database.EntityConfigurations.SQLITE
{
    public class MixUrlAliasConfiguration : SqliteTenantEntityBaseConfiguration<MixUrlAlias, int>
    {
        public override void Configure(EntityTypeBuilder<MixUrlAlias> builder)
        {
            base.Configure(builder);
        }
    }
}

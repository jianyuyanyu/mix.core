﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mix.Database.Entities.Cms;
using Mix.Database.EntityConfigurations.POSTGRES.Base;

namespace Mix.Database.EntityConfigurations.POSTGRES
{
    public class MixPostConfiguration : PostgresEntityBaseConfiguration<MixPost, int>
    {
        public override void Configure(EntityTypeBuilder<MixPost> builder)
        {
            base.Configure(builder);
        }
    }
}

﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mix.Database.Entities.Cms;
using Mix.Database.EntityConfigurations.MYSQL.Base;

namespace Mix.Database.EntityConfigurations.MYSQL
{
    public class MixPostConfiguration : MySqlEntityBaseConfiguration<MixPost, int>
    {
        public override void Configure(EntityTypeBuilder<MixPost> builder)
        {
            base.Configure(builder);
        }
    }
}

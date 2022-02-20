﻿using Microsoft.EntityFrameworkCore;
using Mix.Database.Entities.Cms;
using Mix.Database.EntityConfigurations.MYSQL;
using Mix.Database.Services;

namespace Mix.Database.Entities.v2
{
    public class MySqlMixCmsContext : MixCmsContext
    {
        public MySqlMixCmsContext(MixDatabaseService databaseService)
            : base(databaseService)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(
                this.GetType().Assembly,
                m => m.Namespace == typeof(MySqlDatabaseConstants).Namespace);
        }
    }
}

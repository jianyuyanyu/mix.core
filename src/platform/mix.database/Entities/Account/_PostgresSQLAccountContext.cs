﻿// Licensed to the Mixcore Foundation under one or more agreements.
// The Mixcore Foundation licenses this file to you under the MIT.
// See the LICENSE file in the project root for more information.

using Mix.Database.Services;

namespace Mix.Database.Entities.Account
{
    public partial class PostgresSQLAccountContext : MixCmsAccountContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext" /> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public PostgresSQLAccountContext(MixDatabaseService databaseService)
                    : base(databaseService)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(
                this.GetType().Assembly,
                m => m.Namespace == $"Mix.Database.EntityConfigurations.POSTGRES.Account");
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
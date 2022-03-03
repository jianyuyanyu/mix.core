﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mix.Database.EntityConfigurations
{
    public static class PropertuBuilderExtensions
    {
        public static PropertyBuilder<TPrimaryKey> HasDefaultValueIf<TPrimaryKey>(this PropertyBuilder<TPrimaryKey> builder, bool condition, string method)
        {
            if (condition)
            {
                builder.HasDefaultValueSql(method);
            }
            return builder;
        }
        public static PropertyBuilder<TPrimaryKey> UseIncreaseValueIf<TPrimaryKey>(this PropertyBuilder<TPrimaryKey> builder, bool condition)
        {
            if (condition)
            {
                builder.UseIdentityByDefaultColumn();
            }
            return builder;
        }
    }
}

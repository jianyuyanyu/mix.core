﻿namespace Mix.Database.EntityConfigurations
{
    public static class PropertyBuilderExtensions
    {
        public static PropertyBuilder<TPrimaryKey> UseDefaultGUIDIf<TPrimaryKey>(this PropertyBuilder<TPrimaryKey> builder, bool condition, string method)
        {
            if (condition)
            {
                builder
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql(method);
            }
            return builder;
        }
        public static PropertyBuilder<TPrimaryKey> UseIncreaseValueIf<TPrimaryKey>(this PropertyBuilder<TPrimaryKey> builder, bool condition)
        {
            if (condition)
            {
                builder.UseIdentityByDefaultColumn()
                    .ValueGeneratedOnAdd();
            }
            return builder;
        }
    }
}

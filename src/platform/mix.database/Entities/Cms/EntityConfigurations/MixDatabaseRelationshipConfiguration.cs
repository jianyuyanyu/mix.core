﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mix.Database.EntityConfigurations.Base;
using Mix.Database.Services.MixGlobalSettings;

namespace Mix.Database.Entities.Cms.EntityConfigurations
{
    public class MixDatabaseRelationshipConfiguration : EntityBaseConfiguration<MixDatabaseRelationship, int>

    {
        public MixDatabaseRelationshipConfiguration(DatabaseService databaseService) : base(databaseService)
        {
        }

        public override void Configure(EntityTypeBuilder<MixDatabaseRelationship> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.ParentId)
               .IsRequired()
               .HasColumnName("parent_id");
            
            builder.Property(e => e.ChildId)
               .IsRequired()
               .HasColumnName("child_id");

            builder.Property(e => e.SourceDatabaseName)
               .IsRequired()
               .HasColumnName("source_database_name")
               .HasColumnType($"{Config.String}{Config.SmallLength}")
               .HasCharSet(Config.CharSet);

            builder.Property(e => e.DisplayName)
               .IsRequired()
               .HasColumnName("display_name")
               .HasColumnType($"{Config.String}{Config.SmallLength}")
               .HasCharSet(Config.CharSet);

            builder.Property(e => e.DestinateDatabaseName)
               .IsRequired()
               .HasColumnName("destinate_database_name")
               .HasColumnType($"{Config.String}{Config.SmallLength}")
               .HasCharSet(Config.CharSet);

            builder.HasOne(e => e.SourceDatabase)
                .WithMany(e => e.SourceRelationships)
                .HasForeignKey(e => e.ParentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.DestinateDatabase)
                .WithMany(e => e.DestinateRelationships)
                .HasForeignKey(e => e.ChildId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(e => e.Type)
               .IsRequired()
               .HasColumnName("type")
               .HasConversion(new EnumToStringConverter<MixDatabaseRelationshipType>())
               .HasColumnType($"{Config.NString}{Config.SmallLength}")
               .HasCharSet(Config.CharSet);
        }
    }
}

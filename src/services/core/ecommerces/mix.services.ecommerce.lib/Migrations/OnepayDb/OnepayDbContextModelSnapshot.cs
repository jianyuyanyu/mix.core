﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mix.Services.Ecommerce.Lib.Entities.Onepay;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Mix.Services.Ecommerce.Lib.Migrations
{
    [DbContext(typeof(OnepayDbContext))]
    partial class OnepayDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Mix.Services.Ecommerce.Lib.Entities.Onepay.OnepayTransactionRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AgainLink")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("varchar(250)");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("OnepayStatus")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    MySqlPropertyBuilderExtensions.HasCharSet(b.Property<string>("OnepayStatus"), "utf8");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    MySqlPropertyBuilderExtensions.HasCharSet(b.Property<string>("Status"), "utf8");

                    b.Property<string>("Title")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("vpc_AccessCode")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("vpc_Amount")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("vpc_Command")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("vpc_Currency")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("vpc_Customer_Email")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("vpc_Customer_Id")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("vpc_Customer_Phone")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("vpc_Locale")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("vpc_MerchTxnRef")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("vpc_Merchant")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("vpc_OrderInfo")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("vpc_ReturnURL")
                        .HasColumnType("varchar(4000)");

                    b.Property<string>("vpc_SecureHash")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("vpc_TicketNo")
                        .HasColumnType("varchar(250)");

                    b.Property<int>("vpc_Version")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK_OnepayTransactionRequest");

                    b.ToTable("OnepayTransactionRequest", (string)null);
                });

            modelBuilder.Entity("Mix.Services.Ecommerce.Lib.Entities.Onepay.OnepayTransactionResponse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("varchar(250)");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("OnepayStatus")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    MySqlPropertyBuilderExtensions.HasCharSet(b.Property<string>("OnepayStatus"), "utf8");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    MySqlPropertyBuilderExtensions.HasCharSet(b.Property<string>("Status"), "utf8");

                    b.Property<string>("vpc_AdditionData")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("vpc_Amount")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("vpc_Command")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("vpc_CurrencyCode")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("vpc_Locale")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("vpc_MerchTxnRef")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("vpc_Merchant")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("vpc_Message")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("vpc_OrderInfo")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("vpc_SecureHash")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("vpc_TransactionNo")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("vpc_TxnResponseCode")
                        .HasColumnType("varchar(250)");

                    b.HasKey("Id")
                        .HasName("PK_OnepayTransactionResponse");

                    b.ToTable("OnepayTransactionResponse", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}

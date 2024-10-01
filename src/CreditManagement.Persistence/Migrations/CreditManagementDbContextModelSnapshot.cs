﻿// <auto-generated />
using System;
using CreditManagement.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CreditManagement.Persistence.Migrations
{
    [DbContext(typeof(CreditManagementDbContext))]
    partial class CreditManagementDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("credit_management")
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CreditManagement.Domain.Accounts.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("AccountHolder")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("account_holder");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("balance");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_on_utc");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("deleted");

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("deleted_on_utc");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("modified_on_utc");

                    b.HasKey("Id")
                        .HasName("pk_account");

                    b.ToTable("account", "credit_management");
                });

            modelBuilder.Entity("CreditManagement.Domain.Accounts.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uuid")
                        .HasColumnName("account_id");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("amount");

                    b.Property<int>("Category")
                        .HasColumnType("integer")
                        .HasColumnName("category");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_on_utc");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(500)")
                        .HasColumnName("description");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("modified_on_utc");

                    b.HasKey("Id")
                        .HasName("pk_transaction");

                    b.HasIndex("AccountId")
                        .HasDatabaseName("ix_transaction_account_id");

                    b.HasIndex("Category")
                        .HasDatabaseName("ix_transaction_category");

                    b.ToTable("transaction", "credit_management");
                });

            modelBuilder.Entity("CreditManagement.Persistence.Configurations.EnumLookup<CreditManagement.Domain.Accounts.TransactionCategory>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("Value")
                        .HasColumnType("integer")
                        .HasColumnName("value");

                    b.HasKey("Id")
                        .HasName("pk_enum_lookup_transaction_category");

                    b.HasAlternateKey("Value")
                        .HasName("ak_enum_lookup_transaction_category_value");

                    b.ToTable("enum_lookup_transaction_category", "credit_management");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Income",
                            Value = 1
                        },
                        new
                        {
                            Id = 2,
                            Name = "Expense",
                            Value = 2
                        },
                        new
                        {
                            Id = 3,
                            Name = "InternalTransfer",
                            Value = 3
                        },
                        new
                        {
                            Id = 4,
                            Name = "Other",
                            Value = 4
                        });
                });

            modelBuilder.Entity("CreditManagement.Domain.Accounts.Transaction", b =>
                {
                    b.HasOne("CreditManagement.Domain.Accounts.Account", null)
                        .WithMany("Transactions")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_transaction_account_account_id");

                    b.HasOne("CreditManagement.Persistence.Configurations.EnumLookup<CreditManagement.Domain.Accounts.TransactionCategory>", null)
                        .WithMany()
                        .HasForeignKey("Category")
                        .HasPrincipalKey("Value")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_transaction_enum_lookup_transaction_category_category");
                });

            modelBuilder.Entity("CreditManagement.Domain.Accounts.Account", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}

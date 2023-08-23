﻿// <auto-generated />
using System;
using BankManagment_Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BankManagment_Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BankManagment_Domain.Entity.AccountType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AccountTypes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8627701d-72fe-4ae4-b14a-b8a79a6b5979"),
                            Name = "Liability"
                        },
                        new
                        {
                            Id = new Guid("13f7a5d2-7281-49af-a8c0-ac48d578cdb9"),
                            Name = "Asset"
                        });
                });

            modelBuilder.Entity("BankManagment_Domain.Entity.BankAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("AccountTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ClosingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OpeningDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TotalBalance")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("AccountTypeId");

                    b.ToTable("BankAccounts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c65b3dea-b80e-407c-b2f5-4afea82fe21d"),
                            AccountNumber = "15176549",
                            AccountTypeId = new Guid("7d9fc653-a3b3-46c6-893b-e95563a15e88"),
                            FirstName = "Akash",
                            LastName = "Rana",
                            OpeningDate = new DateTime(2023, 8, 24, 0, 28, 9, 673, DateTimeKind.Local).AddTicks(1539),
                            TotalBalance = 1000m
                        },
                        new
                        {
                            Id = new Guid("edd6b8ee-17c3-422e-a514-3efb6228ff54"),
                            AccountNumber = "53179841",
                            AccountTypeId = new Guid("7d9fc653-a3b3-46c6-893b-e95563a15e88"),
                            FirstName = "Akash",
                            LastName = "Rana",
                            OpeningDate = new DateTime(2023, 8, 23, 0, 28, 9, 673, DateTimeKind.Local).AddTicks(1559),
                            TotalBalance = 1000m
                        },
                        new
                        {
                            Id = new Guid("d5ed1e68-552a-4020-81d8-be2fc09b75e4"),
                            AccountNumber = "51588042",
                            AccountTypeId = new Guid("7d9fc653-a3b3-46c6-893b-e95563a15e88"),
                            FirstName = "Akash",
                            LastName = "Rana",
                            OpeningDate = new DateTime(2023, 8, 22, 0, 28, 9, 673, DateTimeKind.Local).AddTicks(1563),
                            TotalBalance = 1000m
                        },
                        new
                        {
                            Id = new Guid("828080d8-801b-4783-9966-929ebdee22d5"),
                            AccountNumber = "96290323",
                            AccountTypeId = new Guid("7d9fc653-a3b3-46c6-893b-e95563a15e88"),
                            FirstName = "Akash",
                            LastName = "Rana",
                            OpeningDate = new DateTime(2023, 8, 21, 0, 28, 9, 673, DateTimeKind.Local).AddTicks(1570),
                            TotalBalance = 1000m
                        },
                        new
                        {
                            Id = new Guid("f5dcc32f-ae0f-4657-9044-5fa7d2ecb4bb"),
                            AccountNumber = "98674631",
                            AccountTypeId = new Guid("7d9fc653-a3b3-46c6-893b-e95563a15e88"),
                            FirstName = "Akash",
                            LastName = "Rana",
                            OpeningDate = new DateTime(2023, 8, 20, 0, 28, 9, 673, DateTimeKind.Local).AddTicks(1573),
                            TotalBalance = 1000m
                        });
                });

            modelBuilder.Entity("BankManagment_Domain.Entity.BankTransaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("BankAccountID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PaymentMethodID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TransactionPersonFirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TransactionPersonLastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TransactionPersonMiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TransactionType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BankAccountID");

                    b.HasIndex("PaymentMethodID");

                    b.ToTable("BankTransactions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2e0deea8-5154-45fe-945c-a46899a6eea0"),
                            Amount = 279.671857525747000m,
                            BankAccountID = new Guid("adcfa62b-9c94-434f-bceb-d3d629eae6fb"),
                            Category = "Bank Interest",
                            PaymentMethodID = new Guid("adcfa62b-9c94-434f-bceb-d3d629eae6fb"),
                            TransactionDate = new DateTime(2023, 8, 24, 0, 28, 9, 673, DateTimeKind.Local).AddTicks(1697),
                            TransactionPersonFirstName = "Akash",
                            TransactionPersonLastName = "Rana",
                            TransactionType = "Credit"
                        },
                        new
                        {
                            Id = new Guid("87f41ddc-1e35-4d74-929e-459e5f349d0c"),
                            Amount = 394.888107542132000m,
                            BankAccountID = new Guid("adcfa62b-9c94-434f-bceb-d3d629eae6fb"),
                            Category = "Bank Interest",
                            PaymentMethodID = new Guid("adcfa62b-9c94-434f-bceb-d3d629eae6fb"),
                            TransactionDate = new DateTime(2023, 8, 23, 0, 28, 9, 673, DateTimeKind.Local).AddTicks(1701),
                            TransactionPersonFirstName = "Akash",
                            TransactionPersonLastName = "Rana",
                            TransactionType = "Debit"
                        },
                        new
                        {
                            Id = new Guid("10fe9da3-466f-45b1-ae5a-1514a2feaeb4"),
                            Amount = 973.793510990241000m,
                            BankAccountID = new Guid("adcfa62b-9c94-434f-bceb-d3d629eae6fb"),
                            Category = "Opening Balance",
                            PaymentMethodID = new Guid("adcfa62b-9c94-434f-bceb-d3d629eae6fb"),
                            TransactionDate = new DateTime(2023, 8, 22, 0, 28, 9, 673, DateTimeKind.Local).AddTicks(1706),
                            TransactionPersonFirstName = "Akash",
                            TransactionPersonLastName = "Rana",
                            TransactionType = "Credit"
                        },
                        new
                        {
                            Id = new Guid("06504d5d-b272-449a-bc4d-439cf08d2856"),
                            Amount = 754.592598353568000m,
                            BankAccountID = new Guid("adcfa62b-9c94-434f-bceb-d3d629eae6fb"),
                            Category = "Opening Balance",
                            PaymentMethodID = new Guid("adcfa62b-9c94-434f-bceb-d3d629eae6fb"),
                            TransactionDate = new DateTime(2023, 8, 21, 0, 28, 9, 673, DateTimeKind.Local).AddTicks(1767),
                            TransactionPersonFirstName = "Akash",
                            TransactionPersonLastName = "Rana",
                            TransactionType = "Debit"
                        },
                        new
                        {
                            Id = new Guid("08d944eb-1662-4c69-8ec8-764ceee7f7e2"),
                            Amount = 129.784390990282000m,
                            BankAccountID = new Guid("adcfa62b-9c94-434f-bceb-d3d629eae6fb"),
                            Category = "Normal Transactions",
                            PaymentMethodID = new Guid("adcfa62b-9c94-434f-bceb-d3d629eae6fb"),
                            TransactionDate = new DateTime(2023, 8, 20, 0, 28, 9, 673, DateTimeKind.Local).AddTicks(1771),
                            TransactionPersonFirstName = "Akash",
                            TransactionPersonLastName = "Rana",
                            TransactionType = "Credit"
                        });
                });

            modelBuilder.Entity("BankManagment_Domain.Entity.PaymentMethod", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PaymentMethods");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0262c056-69b3-4a98-8806-5f7e22cb917d"),
                            Name = "Cash"
                        },
                        new
                        {
                            Id = new Guid("89f8fab8-358f-4d24-956f-fdf9dba1c4da"),
                            Name = "Cheque"
                        },
                        new
                        {
                            Id = new Guid("9bbfba47-bdc0-44c4-aacb-08e70aa4898f"),
                            Name = "NEFT"
                        },
                        new
                        {
                            Id = new Guid("fb2f3d21-b413-4df2-9160-b829158d9422"),
                            Name = "RTGS"
                        },
                        new
                        {
                            Id = new Guid("5ad96931-5355-470b-aa0b-089e3402235a"),
                            Name = "Other"
                        });
                });

            modelBuilder.Entity("BankManagment_Domain.Entity.BankAccount", b =>
                {
                    b.HasOne("BankManagment_Domain.Entity.AccountType", "AccountType")
                        .WithMany()
                        .HasForeignKey("AccountTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccountType");
                });

            modelBuilder.Entity("BankManagment_Domain.Entity.BankTransaction", b =>
                {
                    b.HasOne("BankManagment_Domain.Entity.BankAccount", "BankAccount")
                        .WithMany()
                        .HasForeignKey("BankAccountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BankManagment_Domain.Entity.PaymentMethod", "PaymentMethod")
                        .WithMany()
                        .HasForeignKey("PaymentMethodID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BankAccount");

                    b.Navigation("PaymentMethod");
                });
#pragma warning restore 612, 618
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankManagment_Migration.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpeningDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClosingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccountTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccounts_AccountTypes_AccountTypeId",
                        column: x => x.AccountTypeId,
                        principalTable: "AccountTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionPersonFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionPersonMiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionPersonLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentMethodID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankAccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankTransactions_BankAccounts_BankAccountID",
                        column: x => x.BankAccountID,
                        principalTable: "BankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BankTransactions_PaymentMethods_PaymentMethodID",
                        column: x => x.PaymentMethodID,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AccountTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("507cbf03-829b-488b-97b1-f237ea31a43f"), "Liability" },
                    { new Guid("79605657-382b-4be2-9d70-687d9ab23123"), "Asset" }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0bf38f97-3634-424a-ada8-5e94acbb27bc"), "Cheque" },
                    { new Guid("480ee408-ddcb-4c46-87a3-be057376723e"), "Cash" },
                    { new Guid("933e1a20-e086-4c4b-910f-a8fc17f6751b"), "RTGS" },
                    { new Guid("cf9b49e9-6372-4989-a732-bfa6ac5a3c03"), "NEFT" },
                    { new Guid("f4cffe7d-869c-44c9-86c5-0aec1ad30ef1"), "Other" }
                });

            migrationBuilder.InsertData(
                table: "BankAccounts",
                columns: new[] { "Id", "AccountNumber", "AccountTypeId", "ClosingDate", "FirstName", "LastName", "MiddleName", "OpeningDate", "TotalBalance" },
                values: new object[,]
                {
                    { new Guid("010315fd-a567-4abd-a4b0-f73431454f4f"), "93300357", new Guid("79605657-382b-4be2-9d70-687d9ab23123"), null, "Akash", "Rana", null, new DateTime(2023, 10, 31, 11, 26, 17, 605, DateTimeKind.Local).AddTicks(6481), 1000m },
                    { new Guid("1d94c74c-9794-40ca-8a43-6a465c6c16ff"), "98151379", new Guid("79605657-382b-4be2-9d70-687d9ab23123"), null, "Akash", "Rana", null, new DateTime(2023, 11, 3, 11, 26, 17, 605, DateTimeKind.Local).AddTicks(6445), 1000m },
                    { new Guid("5d096aa8-d3f8-40a0-9fdf-9bfc0be93d5f"), "51863866", new Guid("79605657-382b-4be2-9d70-687d9ab23123"), null, "Akash", "Rana", null, new DateTime(2023, 10, 30, 11, 26, 17, 605, DateTimeKind.Local).AddTicks(6488), 1000m },
                    { new Guid("7027972d-4e89-4feb-8d4e-56b24dce32e2"), "20011388", new Guid("507cbf03-829b-488b-97b1-f237ea31a43f"), null, "Akash", "Rana", null, new DateTime(2023, 11, 2, 11, 26, 17, 605, DateTimeKind.Local).AddTicks(6473), 1000m },
                    { new Guid("e6cc0556-98fb-460e-a11d-0ec930fa5835"), "81906010", new Guid("507cbf03-829b-488b-97b1-f237ea31a43f"), null, "Akash", "Rana", null, new DateTime(2023, 11, 1, 11, 26, 17, 605, DateTimeKind.Local).AddTicks(6477), 1000m }
                });

            migrationBuilder.InsertData(
                table: "BankTransactions",
                columns: new[] { "Id", "Amount", "BankAccountID", "Category", "PaymentMethodID", "TransactionDate", "TransactionPersonFirstName", "TransactionPersonLastName", "TransactionPersonMiddleName", "TransactionType" },
                values: new object[,]
                {
                    { new Guid("3918570a-fd49-42e7-a754-426de2a76afc"), 843.717140581565000m, new Guid("1d94c74c-9794-40ca-8a43-6a465c6c16ff"), "Bank Charges", new Guid("480ee408-ddcb-4c46-87a3-be057376723e"), new DateTime(2023, 11, 3, 11, 26, 17, 605, DateTimeKind.Local).AddTicks(6665), "Akash", "Rana", null, "Credit" },
                    { new Guid("43606249-3139-45d0-b3d0-3f48dc0cb514"), 921.384731920803000m, new Guid("1d94c74c-9794-40ca-8a43-6a465c6c16ff"), "Bank Interest", new Guid("f4cffe7d-869c-44c9-86c5-0aec1ad30ef1"), new DateTime(2023, 10, 30, 11, 26, 17, 605, DateTimeKind.Local).AddTicks(6684), "Akash", "Rana", null, "Credit" },
                    { new Guid("7a1d7970-2cb4-4e41-81fe-d359df2b4c02"), 34.8647367220243000m, new Guid("1d94c74c-9794-40ca-8a43-6a465c6c16ff"), "Bank Interest", new Guid("cf9b49e9-6372-4989-a732-bfa6ac5a3c03"), new DateTime(2023, 11, 1, 11, 26, 17, 605, DateTimeKind.Local).AddTicks(6674), "Akash", "Rana", null, "Credit" },
                    { new Guid("9482873a-02ee-4407-b3e6-e71e916c712a"), 433.23440167249000m, new Guid("5d096aa8-d3f8-40a0-9fdf-9bfc0be93d5f"), "Opening Balance", new Guid("0bf38f97-3634-424a-ada8-5e94acbb27bc"), new DateTime(2023, 11, 2, 11, 26, 17, 605, DateTimeKind.Local).AddTicks(6671), "Akash", "Rana", null, "Debit" },
                    { new Guid("b3283467-4c39-4b6c-af4f-f0aacbfef3d6"), 841.705131144781000m, new Guid("010315fd-a567-4abd-a4b0-f73431454f4f"), "Normal Transactions", new Guid("f4cffe7d-869c-44c9-86c5-0aec1ad30ef1"), new DateTime(2023, 10, 31, 11, 26, 17, 605, DateTimeKind.Local).AddTicks(6681), "Akash", "Rana", null, "Debit" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_AccountTypeId",
                table: "BankAccounts",
                column: "AccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BankTransactions_BankAccountID",
                table: "BankTransactions",
                column: "BankAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_BankTransactions_PaymentMethodID",
                table: "BankTransactions",
                column: "PaymentMethodID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankTransactions");

            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "AccountTypes");
        }
    }
}

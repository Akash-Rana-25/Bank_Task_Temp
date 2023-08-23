using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankManagment_Infrastructure.Migrations
{
    public partial class one : Migration
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
                    { new Guid("2ef49c40-ce42-47a8-a983-2535003a9023"), "Asset" },
                    { new Guid("6275087a-891f-4d17-bab3-556ca853c7d2"), "Liability" }
                });

            migrationBuilder.InsertData(
                table: "BankAccounts",
                columns: new[] { "Id", "AccountNumber", "AccountTypeId", "ClosingDate", "FirstName", "LastName", "MiddleName", "OpeningDate", "TotalBalance" },
                values: new object[,]
                {
                    { new Guid("2b0af3c3-10b6-46bc-8291-4fef421c4ba1"), "28950592", new Guid("df5f2fd9-fb28-4ab0-9582-519ef71566c3"), null, "Akash", "Rana", null, new DateTime(2023, 8, 22, 0, 19, 47, 980, DateTimeKind.Local).AddTicks(5678), 1000m },
                    { new Guid("4d1ba01b-e860-4f9c-ab1e-83584e144124"), "80349014", new Guid("df5f2fd9-fb28-4ab0-9582-519ef71566c3"), null, "Akash", "Rana", null, new DateTime(2023, 8, 24, 0, 19, 47, 980, DateTimeKind.Local).AddTicks(5650), 1000m },
                    { new Guid("4deea147-0299-4caa-81be-a2260c74ee38"), "75104870", new Guid("df5f2fd9-fb28-4ab0-9582-519ef71566c3"), null, "Akash", "Rana", null, new DateTime(2023, 8, 20, 0, 19, 47, 980, DateTimeKind.Local).AddTicks(5689), 1000m },
                    { new Guid("51536717-9ccd-4a98-a0b2-e5281123c8a1"), "47227226", new Guid("df5f2fd9-fb28-4ab0-9582-519ef71566c3"), null, "Akash", "Rana", null, new DateTime(2023, 8, 21, 0, 19, 47, 980, DateTimeKind.Local).AddTicks(5683), 1000m },
                    { new Guid("86ea06a2-d037-4fff-ba01-b795bd72d214"), "53999320", new Guid("df5f2fd9-fb28-4ab0-9582-519ef71566c3"), null, "Akash", "Rana", null, new DateTime(2023, 8, 23, 0, 19, 47, 980, DateTimeKind.Local).AddTicks(5673), 1000m }
                });

            migrationBuilder.InsertData(
                table: "BankTransactions",
                columns: new[] { "Id", "Amount", "BankAccountID", "Category", "PaymentMethodID", "TransactionDate", "TransactionPersonFirstName", "TransactionPersonLastName", "TransactionPersonMiddleName", "TransactionType" },
                values: new object[,]
                {
                    { new Guid("6a0d5711-5c27-4a71-8ecb-6db310393bf5"), 280.375245429027000m, new Guid("00000000-0000-0000-0000-000000000000"), "Normal Transactions", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2023, 8, 20, 0, 19, 47, 980, DateTimeKind.Local).AddTicks(5840), "Akash", "Rana", null, "Credit" },
                    { new Guid("7e047a47-1e7f-4284-9d02-93c76e249b05"), 948.896981609242000m, new Guid("00000000-0000-0000-0000-000000000000"), "Normal Transactions", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2023, 8, 24, 0, 19, 47, 980, DateTimeKind.Local).AddTicks(5823), "Akash", "Rana", null, "Credit" },
                    { new Guid("b86bf147-9f53-401a-adc0-5c4a674e5e20"), 383.865094655055000m, new Guid("00000000-0000-0000-0000-000000000000"), "Bank Interest", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2023, 8, 22, 0, 19, 47, 980, DateTimeKind.Local).AddTicks(5831), "Akash", "Rana", null, "Credit" },
                    { new Guid("c573bde5-752a-4b7f-8080-1fa1f37e8fa0"), 501.880672867526000m, new Guid("00000000-0000-0000-0000-000000000000"), "Bank Charges", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2023, 8, 21, 0, 19, 47, 980, DateTimeKind.Local).AddTicks(5837), "Akash", "Rana", null, "Debit" },
                    { new Guid("d364d88e-1f53-48b1-b895-035a5570be7d"), 231.35737198547000m, new Guid("00000000-0000-0000-0000-000000000000"), "Bank Charges", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2023, 8, 23, 0, 19, 47, 980, DateTimeKind.Local).AddTicks(5828), "Akash", "Rana", null, "Debit" }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2b0b7594-004c-46ec-a650-f8bacaf42f3a"), "Other" },
                    { new Guid("3f65991b-f79d-4040-a87f-22577f58db32"), "Cheque" },
                    { new Guid("7d86873e-dfb0-432e-8042-7c73b0a3ceb4"), "RTGS" },
                    { new Guid("94ff73c0-e71e-4bb1-9c24-4b9532109987"), "NEFT" },
                    { new Guid("d201d6f8-2ba5-4807-b8a8-c50b2e08c45c"), "Cash" }
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

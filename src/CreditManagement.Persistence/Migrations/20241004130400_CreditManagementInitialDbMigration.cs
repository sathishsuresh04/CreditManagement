using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CreditManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreditManagementInitialDbMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "credit_management");

            migrationBuilder.CreateTable(
                name: "account",
                schema: "credit_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    account_number = table.Column<string>(type: "varchar(50)", nullable: false),
                    account_holder = table.Column<string>(type: "varchar(50)", nullable: false),
                    balance = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_on_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted_on_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_account", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "enum_lookup_transaction_category",
                schema: "credit_management",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    value = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_enum_lookup_transaction_category", x => x.id);
                    table.UniqueConstraint("ak_enum_lookup_transaction_category_value", x => x.value);
                });

            migrationBuilder.CreateTable(
                name: "transaction",
                schema: "credit_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    account_id = table.Column<Guid>(type: "uuid", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    description = table.Column<string>(type: "varchar(500)", nullable: true),
                    amount = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    is_anomalous = table.Column<bool>(type: "boolean", nullable: false),
                    category = table.Column<int>(type: "integer", nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_on_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transaction", x => x.id);
                    table.ForeignKey(
                        name: "fk_transaction_account_account_id",
                        column: x => x.account_id,
                        principalSchema: "credit_management",
                        principalTable: "account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_transaction_enum_lookup_transaction_category_category",
                        column: x => x.category,
                        principalSchema: "credit_management",
                        principalTable: "enum_lookup_transaction_category",
                        principalColumn: "value",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "credit_management",
                table: "enum_lookup_transaction_category",
                columns: new[] { "id", "description", "name", "value" },
                values: new object[,]
                {
                    { 1, null, "Income", 1 },
                    { 2, null, "Expense", 2 },
                    { 3, null, "InternalTransfer", 3 },
                    { 4, null, "Other", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "ix_account_account_number",
                schema: "credit_management",
                table: "account",
                column: "account_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_transaction_account_id",
                schema: "credit_management",
                table: "transaction",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "ix_transaction_category",
                schema: "credit_management",
                table: "transaction",
                column: "category");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transaction",
                schema: "credit_management");

            migrationBuilder.DropTable(
                name: "account",
                schema: "credit_management");

            migrationBuilder.DropTable(
                name: "enum_lookup_transaction_category",
                schema: "credit_management");
        }
    }
}

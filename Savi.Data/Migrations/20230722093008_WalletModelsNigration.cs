using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Savi.Data.Migrations
{
    public partial class WalletModelsNigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "WalletFundings",
                newName: "TransactionType");

            migrationBuilder.AddColumn<decimal>(
                name: "Cummulative",
                table: "WalletFundings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cummulative",
                table: "WalletFundings");

            migrationBuilder.RenameColumn(
                name: "TransactionType",
                table: "WalletFundings",
                newName: "Type");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Savi.Data.Migrations
{
    public partial class targetFundingUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SetTargetFundings_SetTargetId",
                table: "SetTargetFundings");

            migrationBuilder.AddColumn<string>(
                name: "SetTargetFundingId",
                table: "SetTargets",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SetTargetFundings_SetTargetId",
                table: "SetTargetFundings",
                column: "SetTargetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SetTargetFundings_SetTargetId",
                table: "SetTargetFundings");

            migrationBuilder.DropColumn(
                name: "SetTargetFundingId",
                table: "SetTargets");

            migrationBuilder.CreateIndex(
                name: "IX_SetTargetFundings_SetTargetId",
                table: "SetTargetFundings",
                column: "SetTargetId",
                unique: true);
        }
    }
}

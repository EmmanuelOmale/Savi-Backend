using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Savi.Data.Migrations
{
    public partial class updateentities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "SetTargets",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SetTargets_UserId",
                table: "SetTargets",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SetTargets_AspNetUsers_UserId",
                table: "SetTargets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SetTargets_AspNetUsers_UserId",
                table: "SetTargets");

            migrationBuilder.DropIndex(
                name: "IX_SetTargets_UserId",
                table: "SetTargets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SetTargets");
        }
    }
}

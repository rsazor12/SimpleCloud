using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleCloud_Monolithic.Infrastructure.Migrations
{
    public partial class AddClientId2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MLServices_ClientId",
                table: "MLServices");

            migrationBuilder.CreateIndex(
                name: "IX_MLServices_ClientId",
                table: "MLServices",
                column: "ClientId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MLServices_ClientId",
                table: "MLServices");

            migrationBuilder.CreateIndex(
                name: "IX_MLServices_ClientId",
                table: "MLServices",
                column: "ClientId");
        }
    }
}

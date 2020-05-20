using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleCloud_Monolithic.Infrastructure.Migrations
{
    public partial class AddClientProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Clients",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Clients");
        }
    }
}

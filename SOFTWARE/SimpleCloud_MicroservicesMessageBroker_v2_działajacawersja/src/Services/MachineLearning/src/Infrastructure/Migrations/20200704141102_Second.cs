using Microsoft.EntityFrameworkCore.Migrations;

namespace MachineLearning_SimpleCloud_MicroservicesHttp.Infrastructure.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Clients");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

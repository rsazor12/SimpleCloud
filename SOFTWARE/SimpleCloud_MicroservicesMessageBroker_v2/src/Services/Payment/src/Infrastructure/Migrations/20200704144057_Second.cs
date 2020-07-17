using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MachineLearning_SimpleCloud_MicroservicesHttp.Infrastructure.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PaymentId",
                table: "ServiceTasks",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTasks_PaymentId",
                table: "ServiceTasks",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceTasks_Payment_PaymentId",
                table: "ServiceTasks",
                column: "PaymentId",
                principalTable: "Payment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceTasks_Payment_PaymentId",
                table: "ServiceTasks");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_ServiceTasks_PaymentId",
                table: "ServiceTasks");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "ServiceTasks");
        }
    }
}

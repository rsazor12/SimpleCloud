using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MachineLearning_SimpleCloud_MicroservicesHttp.Infrastructure.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceTasks_Clients_ClientId",
                table: "ServiceTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceTasks_Payment_PaymentId",
                table: "ServiceTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceTasks",
                table: "ServiceTasks");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Payment");

            migrationBuilder.RenameTable(
                name: "ServiceTasks",
                newName: "ClientTasks");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceTasks_PaymentId",
                table: "ClientTasks",
                newName: "IX_ClientTasks_PaymentId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceTasks_ClientId",
                table: "ClientTasks",
                newName: "IX_ClientTasks_ClientId");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Payment",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientTasks",
                table: "ClientTasks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientTasks_Clients_ClientId",
                table: "ClientTasks",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientTasks_Payment_PaymentId",
                table: "ClientTasks",
                column: "PaymentId",
                principalTable: "Payment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientTasks_Clients_ClientId",
                table: "ClientTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientTasks_Payment_PaymentId",
                table: "ClientTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientTasks",
                table: "ClientTasks");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Payment");

            migrationBuilder.RenameTable(
                name: "ClientTasks",
                newName: "ServiceTasks");

            migrationBuilder.RenameIndex(
                name: "IX_ClientTasks_PaymentId",
                table: "ServiceTasks",
                newName: "IX_ServiceTasks_PaymentId");

            migrationBuilder.RenameIndex(
                name: "IX_ClientTasks_ClientId",
                table: "ServiceTasks",
                newName: "IX_ServiceTasks_ClientId");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Payment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceTasks",
                table: "ServiceTasks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceTasks_Clients_ClientId",
                table: "ServiceTasks",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceTasks_Payment_PaymentId",
                table: "ServiceTasks",
                column: "PaymentId",
                principalTable: "Payment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleCloud_Monolithic.Infrastructure.Migrations
{
    public partial class AddClientId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MLServices_Clients_ClientId",
                table: "MLServices");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClientId",
                table: "MLServices",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MLServices_Clients_ClientId",
                table: "MLServices",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MLServices_Clients_ClientId",
                table: "MLServices");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClientId",
                table: "MLServices",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_MLServices_Clients_ClientId",
                table: "MLServices",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleCloud_Monolithic.Infrastructure.Migrations
{
    public partial class AddServiceTaskId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceTasks_ServiceDetails_ServiceDetailsId",
                table: "ServiceTasks");

            migrationBuilder.AlterColumn<Guid>(
                name: "ServiceDetailsId",
                table: "ServiceTasks",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceTasks_ServiceDetails_ServiceDetailsId",
                table: "ServiceTasks",
                column: "ServiceDetailsId",
                principalTable: "ServiceDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceTasks_ServiceDetails_ServiceDetailsId",
                table: "ServiceTasks");

            migrationBuilder.AlterColumn<Guid>(
                name: "ServiceDetailsId",
                table: "ServiceTasks",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceTasks_ServiceDetails_ServiceDetailsId",
                table: "ServiceTasks",
                column: "ServiceDetailsId",
                principalTable: "ServiceDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

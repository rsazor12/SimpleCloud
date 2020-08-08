using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MachineLearning_SimpleCloud_Broker.Infrastructure.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TrainDataPath = table.Column<string>(nullable: true),
                    TestDataPath = table.Column<string>(nullable: true),
                    ModelPath = table.Column<string>(nullable: true),
                    Epochs = table.Column<int>(nullable: false),
                    BatchSize = table.Column<int>(nullable: false),
                    LearningRate = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MLServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ServiceName = table.Column<string>(nullable: true),
                    ClientId = table.Column<Guid>(nullable: false),
                    ServiceDetailsId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MLServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MLServices_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MLServices_ServiceDetails_ServiceDetailsId",
                        column: x => x.ServiceDetailsId,
                        principalTable: "ServiceDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    ServiceDetailsId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceTasks_ServiceDetails_ServiceDetailsId",
                        column: x => x.ServiceDetailsId,
                        principalTable: "ServiceDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MLServices_ClientId",
                table: "MLServices",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MLServices_ServiceDetailsId",
                table: "MLServices",
                column: "ServiceDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTasks_ServiceDetailsId",
                table: "ServiceTasks",
                column: "ServiceDetailsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "MLServices");

            migrationBuilder.DropTable(
                name: "ServiceTasks");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "ServiceDetails");
        }
    }
}

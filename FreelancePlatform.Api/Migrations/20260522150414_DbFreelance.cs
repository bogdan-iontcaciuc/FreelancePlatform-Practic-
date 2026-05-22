using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreelancePlatform.Api.Migrations
{
    /// <inheritdoc />
    public partial class DbFreelance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeComplet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parola = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Anunturi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titlu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descriere = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    TipAnunt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Categorie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tehnologii = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PretSauBuget = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataPublicarii = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UtilizatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anunturi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Anunturi_Users_UtilizatorId",
                        column: x => x.UtilizatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnuntId = table.Column<int>(type: "int", nullable: false),
                    FreelancerId = table.Column<int>(type: "int", nullable: false),
                    MesajAplicare = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataAplicarii = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Applications_Anunturi_AnuntId",
                        column: x => x.AnuntId,
                        principalTable: "Anunturi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Applications_Users_FreelancerId",
                        column: x => x.FreelancerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnuntId = table.Column<int>(type: "int", nullable: false),
                    BuyerId = table.Column<int>(type: "int", nullable: false),
                    FreelancerId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Anunturi_AnuntId",
                        column: x => x.AnuntId,
                        principalTable: "Anunturi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Users_FreelancerId",
                        column: x => x.FreelancerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Continut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpeditorId = table.Column<int>(type: "int", nullable: false),
                    DestinatarId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    EsteLivrare = table.Column<bool>(type: "bit", nullable: false),
                    FisierUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataTrimiterii = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AnuntId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Anunturi_AnuntId",
                        column: x => x.AnuntId,
                        principalTable: "Anunturi",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Messages_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Messages_Users_DestinatarId",
                        column: x => x.DestinatarId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_Users_ExpeditorId",
                        column: x => x.ExpeditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Anunturi_UtilizatorId",
                table: "Anunturi",
                column: "UtilizatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_AnuntId",
                table: "Applications",
                column: "AnuntId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_FreelancerId",
                table: "Applications",
                column: "FreelancerId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_AnuntId",
                table: "Messages",
                column: "AnuntId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_DestinatarId",
                table: "Messages",
                column: "DestinatarId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ExpeditorId",
                table: "Messages",
                column: "ExpeditorId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_OrderId",
                table: "Messages",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AnuntId",
                table: "Orders",
                column: "AnuntId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BuyerId",
                table: "Orders",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_FreelancerId",
                table: "Orders",
                column: "FreelancerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Anunturi");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

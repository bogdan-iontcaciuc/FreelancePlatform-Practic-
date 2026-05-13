using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreelancePlatform.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddAnuntTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_Anunturi_UtilizatorId",
                table: "Anunturi",
                column: "UtilizatorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Anunturi");
        }
    }
}

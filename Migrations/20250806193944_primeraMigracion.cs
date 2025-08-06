using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Temporada2025.Backend.Migrations
{
    /// <inheritdoc />
    public partial class primeraMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblJugador",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApellidoPaterno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Posición = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dorsal = table.Column<int>(type: "int", nullable: false),
                    FechaNacimiento = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblJugador", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblEstadistica",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaJornada = table.Column<DateOnly>(type: "date", nullable: false),
                    PartidosJugados = table.Column<int>(type: "int", nullable: false),
                    Goles = table.Column<int>(type: "int", nullable: false),
                    Asistencias = table.Column<int>(type: "int", nullable: false),
                    JugadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Puntaje = table.Column<double>(type: "float", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblEstadistica", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblEstadistica_TblJugador_JugadorId",
                        column: x => x.JugadorId,
                        principalTable: "TblJugador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblEstadistica_JugadorId",
                table: "TblEstadistica",
                column: "JugadorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblEstadistica");

            migrationBuilder.DropTable(
                name: "TblJugador");
        }
    }
}

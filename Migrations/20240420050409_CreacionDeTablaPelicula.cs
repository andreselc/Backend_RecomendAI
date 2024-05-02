using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IARecommendAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreacionDeTablaPelicula : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pelicula",
                columns: table => new
                {
                    Idpelicula = table.Column<int>(name: "Id_pelicula", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulooriginal = table.Column<string>(name: "Titulo_original", type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fechaestreno = table.Column<DateTime>(name: "Fecha_estreno", type: "datetime2", nullable: false),
                    Cartelpath = table.Column<string>(name: "Cartel_path", type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pelicula", x => x.Idpelicula);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pelicula");
        }
    }
}

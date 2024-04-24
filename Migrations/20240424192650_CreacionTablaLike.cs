using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IARecommendAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreacionTablaLike : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Like",
                columns: table => new
                {
                    Idpelicula = table.Column<int>(name: "Id_pelicula", type: "int", nullable: false),
                    Idusuario = table.Column<string>(name: "Id_usuario", type: "nvarchar(450)", nullable: false),
                    IdLike = table.Column<int>(name: "Id_Like", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Like", x => new { x.Idpelicula, x.Idusuario });
                    table.ForeignKey(
                        name: "FK_Like_AspNetUsers_Id_usuario",
                        column: x => x.Idusuario,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Like_Pelicula_Id_pelicula",
                        column: x => x.Idpelicula,
                        principalTable: "Pelicula",
                        principalColumn: "Id_pelicula",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Like_Id_usuario",
                table: "Like",
                column: "Id_usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Like");
        }
    }
}

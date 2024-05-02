using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IARecommendAPI.Migrations
{
    /// <inheritdoc />
    public partial class ModificacionEnUsuarioyPelicula : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Like",
                table: "Like");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Like",
                table: "Like",
                columns: new[] { "Id_pelicula", "Id_usuario", "Id_Like" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Like",
                table: "Like");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Like",
                table: "Like",
                columns: new[] { "Id_pelicula", "Id_usuario" });
        }
    }
}

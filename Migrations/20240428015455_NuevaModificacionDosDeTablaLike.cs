using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IARecommendAPI.Migrations
{
    /// <inheritdoc />
    public partial class NuevaModificacionDosDeTablaLike : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "prueba",
                table: "Like");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "prueba",
                table: "Like",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

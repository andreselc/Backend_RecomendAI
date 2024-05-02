using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IARecommendAPI.Migrations
{
    /// <inheritdoc />
    public partial class NuevaModificacionDeTablaLike : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "prueba",
                table: "Like",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "prueba",
                table: "Like");
        }
    }
}

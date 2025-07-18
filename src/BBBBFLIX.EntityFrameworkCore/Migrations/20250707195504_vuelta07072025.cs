using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBBBFLIX.Migrations
{
    /// <inheritdoc />
    public partial class vuelta07072025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "AppNotificacion",
                newName: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "AppNotificacion",
                newName: "UsuarioId");
        }
    }
}

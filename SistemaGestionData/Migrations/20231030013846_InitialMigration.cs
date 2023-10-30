using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaGestionData.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Usuarios_idUsuario",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_idUsuario",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "idUsuario",
                table: "Productos");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_UsuarioId",
                table: "Productos",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Usuarios_UsuarioId",
                table: "Productos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Usuarios_UsuarioId",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_UsuarioId",
                table: "Productos");

            migrationBuilder.AddColumn<int>(
                name: "idUsuario",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_idUsuario",
                table: "Productos",
                column: "idUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Usuarios_idUsuario",
                table: "Productos",
                column: "idUsuario",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

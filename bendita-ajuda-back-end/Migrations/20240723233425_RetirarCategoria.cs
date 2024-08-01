using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bendita_ajuda_back_end.Migrations
{
    /// <inheritdoc />
    public partial class RetirarCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicos_Categorias_CategoriaId",
                table: "Servicos");

            migrationBuilder.DropIndex(
                name: "IX_Servicos_CategoriaId",
                table: "Servicos");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Servicos");

            migrationBuilder.AddColumn<int>(
                name: "Email",
                table: "Prestadores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Telefone",
                table: "Prestadores",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Prestadores");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Prestadores");

            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Servicos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Servicos_CategoriaId",
                table: "Servicos",
                column: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicos_Categorias_CategoriaId",
                table: "Servicos",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

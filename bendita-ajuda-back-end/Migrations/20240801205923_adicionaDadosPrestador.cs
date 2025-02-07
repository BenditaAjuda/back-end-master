using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bendita_ajuda_back_end.Migrations
{
    /// <inheritdoc />
    public partial class adicionaDadosPrestador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Endereco",
                table: "Prestadores",
                newName: "Logradouro");

            migrationBuilder.AddColumn<string>(
                name: "Bairro",
                table: "Prestadores",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Cep",
                table: "Prestadores",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "Prestadores",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Prestadores",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bairro",
                table: "Prestadores");

            migrationBuilder.DropColumn(
                name: "Cep",
                table: "Prestadores");

            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "Prestadores");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Prestadores");

            migrationBuilder.RenameColumn(
                name: "Logradouro",
                table: "Prestadores",
                newName: "Endereco");
        }
    }
}

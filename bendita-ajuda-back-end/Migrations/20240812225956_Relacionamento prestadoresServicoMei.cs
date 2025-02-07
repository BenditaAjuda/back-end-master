using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bendita_ajuda_back_end.Migrations
{
    /// <inheritdoc />
    public partial class RelacionamentoprestadoresServicoMei : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Telefone",
                table: "Prestadores",
                newName: "TelefoneFixo");

            migrationBuilder.AddColumn<string>(
                name: "Complemento",
                table: "Prestadores",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TelefoneCelular",
                table: "Prestadores",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PrestadorServicosMei",
                columns: table => new
                {
                    PrestadoresPrestadorId = table.Column<int>(type: "int", nullable: false),
                    ServicosMeiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrestadorServicosMei", x => new { x.PrestadoresPrestadorId, x.ServicosMeiId });
                    table.ForeignKey(
                        name: "FK_PrestadorServicosMei_Prestadores_PrestadoresPrestadorId",
                        column: x => x.PrestadoresPrestadorId,
                        principalTable: "Prestadores",
                        principalColumn: "PrestadorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrestadorServicosMei_ServicosMei_ServicosMeiId",
                        column: x => x.ServicosMeiId,
                        principalTable: "ServicosMei",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PrestadorServicosMei_ServicosMeiId",
                table: "PrestadorServicosMei",
                column: "ServicosMeiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrestadorServicosMei");

            migrationBuilder.DropColumn(
                name: "Complemento",
                table: "Prestadores");

            migrationBuilder.DropColumn(
                name: "TelefoneCelular",
                table: "Prestadores");

            migrationBuilder.RenameColumn(
                name: "TelefoneFixo",
                table: "Prestadores",
                newName: "Telefone");
        }
    }
}

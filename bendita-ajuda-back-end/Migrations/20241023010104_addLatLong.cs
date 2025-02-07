using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bendita_ajuda_back_end.Migrations
{
    /// <inheritdoc />
    public partial class addLatLong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Lat",
                table: "Prestadores",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Long",
                table: "Prestadores",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lat",
                table: "Prestadores");

            migrationBuilder.DropColumn(
                name: "Long",
                table: "Prestadores");
        }
    }
}

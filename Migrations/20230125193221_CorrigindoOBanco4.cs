using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace techtestpaymentapi.Migrations
{
    /// <inheritdoc />
    public partial class CorrigindoOBanco4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Vendas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Vendas",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

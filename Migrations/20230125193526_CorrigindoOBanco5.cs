using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace techtestpaymentapi.Migrations
{
    /// <inheritdoc />
    public partial class CorrigindoOBanco5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Vendas",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Vendas");
        }
    }
}

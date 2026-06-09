using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoRaizes.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarCanalAoPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Canal",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Canal",
                table: "Pedidos");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CantinaBariri143.Data.Migrations
{
    /// <inheritdoc />
    public partial class Vendas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vendas",
                columns: table => new
                {
                    VendasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PedidosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataVenda = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendas", x => x.VendasId);
                    table.ForeignKey(
                        name: "FK_Vendas_Clientes_ClientesId",
                        column: x => x.ClientesId,
                        principalTable: "Clientes",
                        principalColumn: "ClientesId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vendas_Pedidos_PedidosId",
                        column: x => x.PedidosId,
                        principalTable: "Pedidos",
                        principalColumn: "PedidosId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_ClientesId",
                table: "Vendas",
                column: "ClientesId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_PedidosId",
                table: "Vendas",
                column: "PedidosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vendas");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CantinaBariri143.Data.Migrations
{
    /// <inheritdoc />
    public partial class Pedidos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    PedidosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlimentosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Qtd = table.Column<int>(type: "int", nullable: false),
                    DataPedido = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.PedidosId);
                    table.ForeignKey(
                        name: "FK_Pedidos_Alimentos_AlimentosId",
                        column: x => x.AlimentosId,
                        principalTable: "Alimentos",
                        principalColumn: "AlimentosId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_AlimentosId",
                table: "Pedidos",
                column: "AlimentosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pedidos");
        }
    }
}

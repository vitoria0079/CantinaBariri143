using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CantinaBariri143.Data.Migrations
{
    /// <inheritdoc />
    public partial class Compras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Compras",
                columns: table => new
                {
                    ComprasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FornecedoresId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlimentosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrecoUnitario = table.Column<double>(type: "float", nullable: false),
                    DataCompra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QtdUnitaria = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compras", x => x.ComprasId);
                    table.ForeignKey(
                        name: "FK_Compras_Alimentos_AlimentosId",
                        column: x => x.AlimentosId,
                        principalTable: "Alimentos",
                        principalColumn: "AlimentosId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Compras_Fornecedores_FornecedoresId",
                        column: x => x.FornecedoresId,
                        principalTable: "Fornecedores",
                        principalColumn: "FornecedoresId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compras_AlimentosId",
                table: "Compras",
                column: "AlimentosId");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_FornecedoresId",
                table: "Compras",
                column: "FornecedoresId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Compras");
        }
    }
}

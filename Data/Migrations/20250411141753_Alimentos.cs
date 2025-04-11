using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CantinaBariri143.Data.Migrations
{
    /// <inheritdoc />
    public partial class Alimentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alimentos",
                columns: table => new
                {
                    AlimentosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrecoUnitario = table.Column<double>(type: "float", nullable: false),
                    Validade = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QtdEstoque = table.Column<int>(type: "int", nullable: false),
                    Restricoes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alimentos", x => x.AlimentosId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alimentos");
        }
    }
}

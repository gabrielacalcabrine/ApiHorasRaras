using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loucademy.HorasRaro.Repository.Migrations
{
    public partial class AlteracaoEntityUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataFIm",
                table: "Projetos",
                newName: "DataFim");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataFim",
                table: "Projetos",
                newName: "DataFIm");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loucademy.HorasRaro.Repository.Migrations
{
    public partial class MudancaProjetoUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProjetosUsuarios");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProjetosUsuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

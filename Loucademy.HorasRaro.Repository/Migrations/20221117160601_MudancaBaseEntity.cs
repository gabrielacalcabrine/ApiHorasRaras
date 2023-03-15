using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loucademy.HorasRaro.Repository.Migrations
{
    public partial class MudancaBaseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "usuarioAlteracao",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "usuarioCriacao",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "usuarioAlteracao",
                table: "ProjetosUsuarios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "usuarioCriacao",
                table: "ProjetosUsuarios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "usuarioAlteracao",
                table: "Projetos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "usuarioCriacao",
                table: "Projetos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "usuarioAlteracao",
                table: "CodigosConfirmacao",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "usuarioCriacao",
                table: "CodigosConfirmacao",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "usuarioAlteracao",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "usuarioCriacao",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "usuarioAlteracao",
                table: "ProjetosUsuarios");

            migrationBuilder.DropColumn(
                name: "usuarioCriacao",
                table: "ProjetosUsuarios");

            migrationBuilder.DropColumn(
                name: "usuarioAlteracao",
                table: "Projetos");

            migrationBuilder.DropColumn(
                name: "usuarioCriacao",
                table: "Projetos");

            migrationBuilder.DropColumn(
                name: "usuarioAlteracao",
                table: "CodigosConfirmacao");

            migrationBuilder.DropColumn(
                name: "usuarioCriacao",
                table: "CodigosConfirmacao");
        }
    }
}

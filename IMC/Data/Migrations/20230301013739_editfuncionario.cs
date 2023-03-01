using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMC.Data.Migrations
{
    public partial class editfuncionario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Categoria",
                table: "Funcionarios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdCidade",
                table: "Funcionarios",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categoria",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "IdCidade",
                table: "Funcionarios");
        }
    }
}

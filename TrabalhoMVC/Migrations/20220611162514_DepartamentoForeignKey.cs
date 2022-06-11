using Microsoft.EntityFrameworkCore.Migrations;

namespace TrabalhoMVC.Migrations
{
    public partial class DepartamentoForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendedores_Departamento_DepartamentoId",
                table: "Vendedores");

            migrationBuilder.AlterColumn<int>(
                name: "DepartamentoId",
                table: "Vendedores",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendedores_Departamento_DepartamentoId",
                table: "Vendedores",
                column: "DepartamentoId",
                principalTable: "Departamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendedores_Departamento_DepartamentoId",
                table: "Vendedores");

            migrationBuilder.AlterColumn<int>(
                name: "DepartamentoId",
                table: "Vendedores",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Vendedores_Departamento_DepartamentoId",
                table: "Vendedores",
                column: "DepartamentoId",
                principalTable: "Departamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

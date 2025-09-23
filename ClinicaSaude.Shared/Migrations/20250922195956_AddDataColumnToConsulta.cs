using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicaSaude.Shared.Migrations
{
    public partial class AddDataColumnToConsulta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Adiciona a coluna Data na tabela Consultas
            migrationBuilder.AddColumn<DateTime>(
                name: "Data",
                table: "Consultas",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.Now); // ou DateTime.MinValue se preferir
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove a coluna caso seja necessário reverter
            migrationBuilder.DropColumn(
                name: "Data",
                table: "Consultas");
        }
    }
}

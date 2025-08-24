using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Dema.Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Demandas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: true),
                    DataDeFechamento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataDeConclusao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataDeEntrega = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Atraso = table.Column<string>(type: "text", nullable: true),
                    TipoCliente = table.Column<string>(type: "text", nullable: true),
                    ClientNR = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    IDResponsavel = table.Column<int>(type: "integer", nullable: false),
                    IDOrcamento = table.Column<int>(type: "integer", nullable: false),
                    IDServico = table.Column<int>(type: "integer", nullable: false),
                    Custo = table.Column<decimal>(type: "numeric", nullable: false),
                    Val_MaoDeObra = table.Column<string>(type: "text", nullable: true),
                    Val_Servico = table.Column<string>(type: "text", nullable: true),
                    Desconto = table.Column<string>(type: "text", nullable: true),
                    NumServico = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Demandas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Demandas");
        }
    }
}

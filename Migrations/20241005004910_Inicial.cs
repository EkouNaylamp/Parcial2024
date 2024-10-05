using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Parcial2024.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Remesas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Remitente = table.Column<string>(type: "text", nullable: true),
                    Destinatario = table.Column<string>(type: "text", nullable: true),
                    PaisOrigen = table.Column<string>(type: "text", nullable: true),
                    PaisDestino = table.Column<string>(type: "text", nullable: true),
                    MontoEnviado = table.Column<decimal>(type: "numeric", nullable: false),
                    MonedaEnviada = table.Column<string>(type: "text", nullable: true),
                    TasaCambio = table.Column<decimal>(type: "numeric", nullable: false),
                    MontoFinal = table.Column<decimal>(type: "numeric", nullable: false),
                    EstadoTransaccion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Remesas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Remesas");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class Inicio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PromocionEstados",
                columns: table => new
                {
                    PromocionEstadoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromocionEstados", x => x.PromocionEstadoId);
                });

            migrationBuilder.CreateTable(
                name: "Promociones",
                columns: table => new
                {
                    PromocionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoGenerado = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PromocionEstadoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promociones", x => x.PromocionId);
                    table.ForeignKey(
                        name: "FK_Promociones_PromocionEstados_PromocionEstadoId",
                        column: x => x.PromocionEstadoId,
                        principalTable: "PromocionEstados",
                        principalColumn: "PromocionEstadoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PromocionEstados",
                columns: new[] { "PromocionEstadoId", "Descripcion" },
                values: new object[] { 1, "Generado " });

            migrationBuilder.InsertData(
                table: "PromocionEstados",
                columns: new[] { "PromocionEstadoId", "Descripcion" },
                values: new object[] { 2, "Canjeado" });

            migrationBuilder.CreateIndex(
                name: "IX_Promociones_PromocionEstadoId",
                table: "Promociones",
                column: "PromocionEstadoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Promociones");

            migrationBuilder.DropTable(
                name: "PromocionEstados");
        }
    }
}

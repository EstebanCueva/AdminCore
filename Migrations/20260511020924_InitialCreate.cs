using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriasGasto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasGasto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Ruc = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubCategoriasGasto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CategoriaGastoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategoriasGasto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategoriasGasto_CategoriasGasto_CategoriaGastoId",
                        column: x => x.CategoriaGastoId,
                        principalTable: "CategoriasGasto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AreasEmpresa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmpresaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreasEmpresa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AreasEmpresa_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Ruc = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoProveedor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    EmpresaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proveedores_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PresupuestosArea",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    AreaEmpresaId = table.Column<int>(type: "int", nullable: false),
                    Mes = table.Column<int>(type: "int", nullable: false),
                    Anio = table.Column<int>(type: "int", nullable: false),
                    MontoAsignado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PresupuestosArea", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PresupuestosArea_AreasEmpresa_AreaEmpresaId",
                        column: x => x.AreaEmpresaId,
                        principalTable: "AreasEmpresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PresupuestosArea_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AreasEmpresa_EmpresaId",
                table: "AreasEmpresa",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_Ruc",
                table: "Empresas",
                column: "Ruc",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PresupuestosArea_AreaEmpresaId",
                table: "PresupuestosArea",
                column: "AreaEmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_PresupuestosArea_EmpresaId_AreaEmpresaId_Mes_Anio",
                table: "PresupuestosArea",
                columns: new[] { "EmpresaId", "AreaEmpresaId", "Mes", "Anio" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Proveedores_EmpresaId_Ruc",
                table: "Proveedores",
                columns: new[] { "EmpresaId", "Ruc" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoriasGasto_CategoriaGastoId",
                table: "SubCategoriasGasto",
                column: "CategoriaGastoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PresupuestosArea");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "SubCategoriasGasto");

            migrationBuilder.DropTable(
                name: "AreasEmpresa");

            migrationBuilder.DropTable(
                name: "CategoriasGasto");

            migrationBuilder.DropTable(
                name: "Empresas");
        }
    }
}

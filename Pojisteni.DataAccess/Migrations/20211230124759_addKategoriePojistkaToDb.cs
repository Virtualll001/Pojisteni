using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pojisteni.DataAccess.Migrations
{
    public partial class addKategoriePojistkaToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategories",
                columns: table => new
                {
                    KategorieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Typ = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategories", x => x.KategorieId);
                });

            migrationBuilder.CreateTable(
                name: "Pojistky",
                columns: table => new
                {
                    PojisteniId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Podminky = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pojistky", x => x.PojisteniId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kategories");

            migrationBuilder.DropTable(
                name: "Pojistky");
        }
    }
}

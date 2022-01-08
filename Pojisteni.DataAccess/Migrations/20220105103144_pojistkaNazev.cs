using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pojisteni.DataAccess.Migrations
{
    public partial class pojistkaNazev : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nazev",
                table: "Pojistky",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nazev",
                table: "Pojistky");
        }
    }
}

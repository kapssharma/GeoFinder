using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoFinder.Data.Migrations
{
    public partial class tokenName_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TokenName",
                table: "Tokens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TokenName",
                table: "Tokens");
        }
    }
}

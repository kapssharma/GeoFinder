using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoFinder.Data.Migrations
{
    public partial class _08Feb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "SearchLog",
                newName: "CreatedDate");

            migrationBuilder.AlterColumn<string>(
                name: "Longitude",
                table: "SearchLog",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Latitude",
                table: "SearchLog",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Osm_Id",
                table: "SearchLog",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Osm_Type",
                table: "SearchLog",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Place_Id",
                table: "SearchLog",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SearchType",
                table: "SearchLog",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SignUp",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignUp", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SignUp");

            migrationBuilder.DropColumn(
                name: "Osm_Id",
                table: "SearchLog");

            migrationBuilder.DropColumn(
                name: "Osm_Type",
                table: "SearchLog");

            migrationBuilder.DropColumn(
                name: "Place_Id",
                table: "SearchLog");

            migrationBuilder.DropColumn(
                name: "SearchType",
                table: "SearchLog");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "SearchLog",
                newName: "CreatedOn");

            migrationBuilder.AlterColumn<double>(
                name: "Longitude",
                table: "SearchLog",
                type: "float",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Latitude",
                table: "SearchLog",
                type: "float",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}

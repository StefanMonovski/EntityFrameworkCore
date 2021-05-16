using Microsoft.EntityFrameworkCore.Migrations;

namespace _01.RealEstates.Data.Migrations
{
    public partial class AddBuildingAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "PropertyTypes",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "BuildingTypes",
                newName: "Type");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Buildings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Buildings");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "PropertyTypes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "BuildingTypes",
                newName: "Name");
        }
    }
}

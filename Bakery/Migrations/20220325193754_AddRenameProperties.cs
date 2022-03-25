using Microsoft.EntityFrameworkCore.Migrations;

namespace Bakery.Migrations
{
    public partial class AddRenameProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TreatName",
                table: "Treats",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "NationalityOfTreat",
                table: "Treats",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "FlavorName",
                table: "Flavors",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "CountryOfOrigin",
                table: "Treats",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryOfOrigin",
                table: "Treats");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Treats",
                newName: "TreatName");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Treats",
                newName: "NationalityOfTreat");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Flavors",
                newName: "FlavorName");
        }
    }
}

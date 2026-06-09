using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeloceCRM.Data.Migrations
{
    /// <inheritdoc />
    public partial class addCompanyEmpolyees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Employees",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FoundedYear",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Employees",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "FoundedYear",
                table: "Companies");
        }
    }
}

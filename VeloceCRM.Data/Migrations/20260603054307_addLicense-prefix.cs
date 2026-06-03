using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeloceCRM.Data.Migrations
{
    /// <inheritdoc />
    public partial class addLicenseprefix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Prefix",
                table: "Licenses",
                type: "varchar(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prefix",
                table: "Licenses");
        }
    }
}

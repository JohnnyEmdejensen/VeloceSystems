using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeloceCRM.Data.Migrations
{
    /// <inheritdoc />
    public partial class addPersonTitleImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Persons",
                type: "longblob",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TitleId",
                table: "Persons",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "TitleId",
                table: "Persons");
        }
    }
}

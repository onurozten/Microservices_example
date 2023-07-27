using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Microservices.PhoneBook.Migrations
{
    /// <inheritdoc />
    public partial class contactInfoAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContactContent",
                table: "ContactInfos",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactContent",
                table: "ContactInfos");
        }
    }
}

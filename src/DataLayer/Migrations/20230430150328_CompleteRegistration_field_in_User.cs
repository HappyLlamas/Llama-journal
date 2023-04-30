using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LlamaJournal.Migrations
{
    /// <inheritdoc />
    public partial class CompleteRegistration_field_in_User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CompleteRegistration",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompleteRegistration",
                table: "Users");
        }
    }
}

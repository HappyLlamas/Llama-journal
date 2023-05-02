using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LlamaJournal.Migrations
{
    /// <inheritdoc />
    public partial class update_user_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameAndOrganization",
                table: "Groups");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameAndOrganization",
                table: "Groups",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}

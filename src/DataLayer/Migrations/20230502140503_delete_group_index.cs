using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LlamaJournal.Migrations
{
    /// <inheritdoc />
    public partial class delete_group_index : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Groups_Name",
                table: "Groups");

            migrationBuilder.AddColumn<string>(
                name: "NameAndOrganization",
                table: "Groups",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameAndOrganization",
                table: "Groups");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Name",
                table: "Groups",
                column: "Name",
                unique: true);
        }
    }
}

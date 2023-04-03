using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LlamaJournal.Migrations
{
/// <inheritdoc />
public partial class added_missing_m2m : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropForeignKey(
		    name: "FK_Users_Disciplines_DisciplineId",
		    table: "Users");

		migrationBuilder.DropIndex(
		    name: "IX_Users_DisciplineId",
		    table: "Users");

		migrationBuilder.DropColumn(
		    name: "DisciplineId",
		    table: "Users");

		migrationBuilder.CreateTable(
		    name: "DisciplineUser",
		columns: table => new {
			TeacherDisciplinesId = table.Column<long>(type: "bigint", nullable: false),
			TeachersId = table.Column<string>(type: "character varying(50)", nullable: false)
		},
		constraints: table => {
			table.PrimaryKey("PK_DisciplineUser", x => new {
				x.TeacherDisciplinesId, x.TeachersId
			});
			table.ForeignKey(
			    name: "FK_DisciplineUser_Disciplines_TeacherDisciplinesId",
			    column: x => x.TeacherDisciplinesId,
			    principalTable: "Disciplines",
			    principalColumn: "Id",
			    onDelete: ReferentialAction.Cascade);
			table.ForeignKey(
			    name: "FK_DisciplineUser_Users_TeachersId",
			    column: x => x.TeachersId,
			    principalTable: "Users",
			    principalColumn: "Id",
			    onDelete: ReferentialAction.Cascade);
		});

		migrationBuilder.CreateIndex(
		    name: "IX_DisciplineUser_TeachersId",
		    table: "DisciplineUser",
		    column: "TeachersId");
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
		    name: "DisciplineUser");

		migrationBuilder.AddColumn<long>(
		    name: "DisciplineId",
		    table: "Users",
		    type: "bigint",
		    nullable: true);

		migrationBuilder.CreateIndex(
		    name: "IX_Users_DisciplineId",
		    table: "Users",
		    column: "DisciplineId");

		migrationBuilder.AddForeignKey(
		    name: "FK_Users_Disciplines_DisciplineId",
		    table: "Users",
		    column: "DisciplineId",
		    principalTable: "Disciplines",
		    principalColumn: "Id");
	}
}
}

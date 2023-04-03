using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LlamaJournal.Migrations
{
/// <inheritdoc />
public partial class init_models : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable(
		    name: "Disciplines",
		columns: table => new {
			Id = table.Column<long>(type: "bigint", maxLength: 12, nullable: false)
			     .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
			     Name = table.Column<string>(type: "text", nullable: false),
			     Description = table.Column<string>(type: "text", nullable: true)
		},
		constraints: table => {
			table.PrimaryKey("PK_Disciplines", x => x.Id);
		});

		migrationBuilder.CreateTable(
		    name: "Organizations",
		columns: table => new {
			Id = table.Column<long>(type: "bigint", maxLength: 12, nullable: false)
			     .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
			     Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false)
		},
		constraints: table => {
			table.PrimaryKey("PK_Organizations", x => x.Id);
		});

		migrationBuilder.CreateTable(
		    name: "Groups",
		columns: table => new {
			Id = table.Column<long>(type: "bigint", maxLength: 12, nullable: false)
			     .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
			     Name = table.Column<string>(type: "text", nullable: false),
			     OrganizationId = table.Column<long>(type: "bigint", nullable: false)
		},
		constraints: table => {
			table.PrimaryKey("PK_Groups", x => x.Id);
			table.ForeignKey(
			    name: "FK_Groups_Organizations_OrganizationId",
			    column: x => x.OrganizationId,
			    principalTable: "Organizations",
			    principalColumn: "Id",
			    onDelete: ReferentialAction.Cascade);
		});

		migrationBuilder.CreateTable(
		    name: "DisciplineGroup",
		columns: table => new {
			DisciplinesId = table.Column<long>(type: "bigint", nullable: false),
			GroupsId = table.Column<long>(type: "bigint", nullable: false)
		},
		constraints: table => {
			table.PrimaryKey("PK_DisciplineGroup", x => new {
				x.DisciplinesId, x.GroupsId
			});
			table.ForeignKey(
			    name: "FK_DisciplineGroup_Disciplines_DisciplinesId",
			    column: x => x.DisciplinesId,
			    principalTable: "Disciplines",
			    principalColumn: "Id",
			    onDelete: ReferentialAction.Cascade);
			table.ForeignKey(
			    name: "FK_DisciplineGroup_Groups_GroupsId",
			    column: x => x.GroupsId,
			    principalTable: "Groups",
			    principalColumn: "Id",
			    onDelete: ReferentialAction.Cascade);
		});

		migrationBuilder.CreateTable(
		    name: "Users",
		columns: table => new {
			Id = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
			Email = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
			FullName = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
			Role = table.Column<int>(type: "integer", nullable: false),
			GroupId = table.Column<long>(type: "bigint", nullable: false),
			DisciplineId = table.Column<long>(type: "bigint", nullable: true)
		},
		constraints: table => {
			table.PrimaryKey("PK_Users", x => x.Id);
			table.ForeignKey(
			    name: "FK_Users_Disciplines_DisciplineId",
			    column: x => x.DisciplineId,
			    principalTable: "Disciplines",
			    principalColumn: "Id");
			table.ForeignKey(
			    name: "FK_Users_Groups_GroupId",
			    column: x => x.GroupId,
			    principalTable: "Groups",
			    principalColumn: "Id",
			    onDelete: ReferentialAction.Cascade);
		});

		migrationBuilder.CreateTable(
		    name: "Attendances",
		columns: table => new {
			Id = table.Column<int>(type: "integer", nullable: false)
			     .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
			     Date = table.Column<DateTime>(type: "Date", nullable: false),
			     UserId = table.Column<string>(type: "character varying(50)", nullable: false),
			     DisciplineId = table.Column<long>(type: "bigint", nullable: false)
		},
		constraints: table => {
			table.PrimaryKey("PK_Attendances", x => x.Id);
			table.ForeignKey(
			    name: "FK_Attendances_Disciplines_DisciplineId",
			    column: x => x.DisciplineId,
			    principalTable: "Disciplines",
			    principalColumn: "Id",
			    onDelete: ReferentialAction.Cascade);
			table.ForeignKey(
			    name: "FK_Attendances_Users_UserId",
			    column: x => x.UserId,
			    principalTable: "Users",
			    principalColumn: "Id",
			    onDelete: ReferentialAction.Cascade);
		});

		migrationBuilder.CreateIndex(
		    name: "IX_Attendances_DisciplineId",
		    table: "Attendances",
		    column: "DisciplineId");

		migrationBuilder.CreateIndex(
		    name: "IX_Attendances_UserId",
		    table: "Attendances",
		    column: "UserId");

		migrationBuilder.CreateIndex(
		    name: "IX_DisciplineGroup_GroupsId",
		    table: "DisciplineGroup",
		    column: "GroupsId");

		migrationBuilder.CreateIndex(
		    name: "IX_Groups_Name",
		    table: "Groups",
		    column: "Name",
		    unique: true);

		migrationBuilder.CreateIndex(
		    name: "IX_Groups_OrganizationId",
		    table: "Groups",
		    column: "OrganizationId");

		migrationBuilder.CreateIndex(
		    name: "IX_Organizations_Name",
		    table: "Organizations",
		    column: "Name",
		    unique: true);

		migrationBuilder.CreateIndex(
		    name: "IX_Users_DisciplineId",
		    table: "Users",
		    column: "DisciplineId");

		migrationBuilder.CreateIndex(
		    name: "IX_Users_Email",
		    table: "Users",
		    column: "Email",
		    unique: true);

		migrationBuilder.CreateIndex(
		    name: "IX_Users_GroupId",
		    table: "Users",
		    column: "GroupId");
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
		    name: "Attendances");

		migrationBuilder.DropTable(
		    name: "DisciplineGroup");

		migrationBuilder.DropTable(
		    name: "Users");

		migrationBuilder.DropTable(
		    name: "Disciplines");

		migrationBuilder.DropTable(
		    name: "Groups");

		migrationBuilder.DropTable(
		    name: "Organizations");
	}
}
}

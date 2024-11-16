#nullable disable
#pragma warning disable MA0048, AV1706

namespace NKZSoft.Template.Persistence.PostgreSQL.Migrations;
using Microsoft.EntityFrameworkCore.Migrations;

public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "ToDoLists",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Title = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                CreatedBy = table.Column<string>(type: "text", nullable: false),
                Modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                ModifiedBy = table.Column<string>(type: "text", nullable: true),
            },
            constraints: table => table.PrimaryKey("PK_ToDoLists", x => x.Id));

        migrationBuilder.CreateTable(
            name: "ToDoItems",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Title = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                Note = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                ToDoListId = table.Column<Guid>(type: "uuid", nullable: true),
                Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                CreatedBy = table.Column<string>(type: "text", nullable: false),
                Modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                ModifiedBy = table.Column<string>(type: "text", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ToDoItems", x => x.Id);
                table.ForeignKey(
                    name: "FK_ToDoItems_ToDoLists_ToDoListId",
                    column: x => x.ToDoListId,
                    principalTable: "ToDoLists",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_ToDoItems_ToDoListId",
            table: "ToDoItems",
            column: "ToDoListId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "ToDoItems");

        migrationBuilder.DropTable(
            name: "ToDoLists");
    }
}

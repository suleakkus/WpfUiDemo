using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.DatabaseModule.Migrations
{
    /// <inheritdoc />
    public partial class _0001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todo_Lists_TodoListId",
                table: "Todo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Todo",
                table: "Todo");

            migrationBuilder.RenameTable(
                name: "Todo",
                newName: "Todos");

            migrationBuilder.RenameIndex(
                name: "IX_Todo_TodoListId",
                table: "Todos",
                newName: "IX_Todos_TodoListId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Todos",
                table: "Todos",
                column: "TodoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_Lists_TodoListId",
                table: "Todos",
                column: "TodoListId",
                principalTable: "Lists",
                principalColumn: "TodoListId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todos_Lists_TodoListId",
                table: "Todos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Todos",
                table: "Todos");

            migrationBuilder.RenameTable(
                name: "Todos",
                newName: "Todo");

            migrationBuilder.RenameIndex(
                name: "IX_Todos_TodoListId",
                table: "Todo",
                newName: "IX_Todo_TodoListId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Todo",
                table: "Todo",
                column: "TodoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Todo_Lists_TodoListId",
                table: "Todo",
                column: "TodoListId",
                principalTable: "Lists",
                principalColumn: "TodoListId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

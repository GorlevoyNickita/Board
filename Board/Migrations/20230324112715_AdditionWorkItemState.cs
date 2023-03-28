using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Board.Migrations
{
    /// <inheritdoc />
    public partial class AdditionWorkItemState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "WorkItemStates",
                column: "Value",
                value: "On Hold");

            migrationBuilder.InsertData(
                table: "WorkItemStates",
                column: "Value",
                value: "Rejected");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WorkItemStates",
                keyColumn : "Value",
                keyValue: "On Hold");

            migrationBuilder.DeleteData(
                table: "WorkItemStates",
                keyColumn: "Value",
                keyValue: "Rejected");
        }
    }
}

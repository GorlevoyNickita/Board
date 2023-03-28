using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Board.Migrations
{
    /// <inheritdoc />
    public partial class UserToComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Comments");

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorID",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AuthorID",
                table: "Comments",
                column: "AuthorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_AuthorID",
                table: "Comments",
                column: "AuthorID",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_AuthorID",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_AuthorID",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "AuthorID",
                table: "Comments");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

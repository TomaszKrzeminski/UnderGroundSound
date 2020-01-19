using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace UndergroundSound.Migrations
{
    public partial class Entityappuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_AspNetUsers_AppUserId",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_AppUserId",
                table: "Votes");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Votes",
                newName: "UserId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Votes",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Votes",
                newName: "AppUserId");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Votes",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Votes_AppUserId",
                table: "Votes",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_AspNetUsers_AppUserId",
                table: "Votes",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

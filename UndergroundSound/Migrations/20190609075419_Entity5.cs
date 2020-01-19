using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace UndergroundSound.Migrations
{
    public partial class Entity5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SongName",
                table: "Songs",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MusicGenreName",
                table: "MusicGenres",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MusicGenreDescription",
                table: "MusicGenres",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BandsRecordName",
                table: "BandsRecords",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "History",
                table: "Bands",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FormedPlace",
                table: "Bands",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BandName",
                table: "Bands",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SongName",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "BandsRecordName",
                table: "BandsRecords");

            migrationBuilder.DropColumn(
                name: "BandName",
                table: "Bands");

            migrationBuilder.AlterColumn<string>(
                name: "MusicGenreName",
                table: "MusicGenres",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "MusicGenreDescription",
                table: "MusicGenres",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "History",
                table: "Bands",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "FormedPlace",
                table: "Bands",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}

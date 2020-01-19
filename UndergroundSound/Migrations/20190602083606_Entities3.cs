using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace UndergroundSound.Migrations
{
    public partial class Entities3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Bands_BandId",
                table: "Vote");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_BandsRecords_BandsRecordId",
                table: "Vote");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Songs_SongId",
                table: "Vote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vote",
                table: "Vote");

            migrationBuilder.RenameTable(
                name: "Vote",
                newName: "Votes");

            migrationBuilder.RenameIndex(
                name: "IX_Vote_SongId",
                table: "Votes",
                newName: "IX_Votes_SongId");

            migrationBuilder.RenameIndex(
                name: "IX_Vote_BandsRecordId",
                table: "Votes",
                newName: "IX_Votes_BandsRecordId");

            migrationBuilder.RenameIndex(
                name: "IX_Vote_BandId",
                table: "Votes",
                newName: "IX_Votes_BandId");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Votes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Songs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SongText",
                table: "Songs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MusicGenreDescription",
                table: "MusicGenres",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseOfTheCd",
                table: "BandsRecords",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FormedDate",
                table: "Bands",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FormedPlace",
                table: "Bands",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "History",
                table: "Bands",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Votes",
                table: "Votes",
                column: "VoteId");

            migrationBuilder.CreateTable(
                name: "Awards",
                columns: table => new
                {
                    AwardId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AwardDescription = table.Column<string>(nullable: true),
                    AwardName = table.Column<string>(nullable: true),
                    BandId = table.Column<int>(nullable: true),
                    BandsRecordId = table.Column<int>(nullable: true),
                    SongId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Awards", x => x.AwardId);
                    table.ForeignKey(
                        name: "FK_Awards_Bands_BandId",
                        column: x => x.BandId,
                        principalTable: "Bands",
                        principalColumn: "BandId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Awards_BandsRecords_BandsRecordId",
                        column: x => x.BandsRecordId,
                        principalTable: "BandsRecords",
                        principalColumn: "BandsRecordId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Awards_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "SongId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MusicVideos",
                columns: table => new
                {
                    MusicVideoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MusicVideoPath = table.Column<string>(nullable: true),
                    SongId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicVideos", x => x.MusicVideoId);
                    table.ForeignKey(
                        name: "FK_MusicVideos_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "SongId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    PictureId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BandId = table.Column<int>(nullable: true),
                    BandsRecordId = table.Column<int>(nullable: true),
                    MusicGenreId = table.Column<int>(nullable: true),
                    PicturePath = table.Column<string>(nullable: true),
                    SongId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.PictureId);
                    table.ForeignKey(
                        name: "FK_Pictures_Bands_BandId",
                        column: x => x.BandId,
                        principalTable: "Bands",
                        principalColumn: "BandId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pictures_BandsRecords_BandsRecordId",
                        column: x => x.BandsRecordId,
                        principalTable: "BandsRecords",
                        principalColumn: "BandsRecordId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pictures_MusicGenres_MusicGenreId",
                        column: x => x.MusicGenreId,
                        principalTable: "MusicGenres",
                        principalColumn: "MusicGenreId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pictures_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "SongId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Votes_AppUserId",
                table: "Votes",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Awards_BandId",
                table: "Awards",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_Awards_BandsRecordId",
                table: "Awards",
                column: "BandsRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Awards_SongId",
                table: "Awards",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicVideos_SongId",
                table: "MusicVideos",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_BandId",
                table: "Pictures",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_BandsRecordId",
                table: "Pictures",
                column: "BandsRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_MusicGenreId",
                table: "Pictures",
                column: "MusicGenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_SongId",
                table: "Pictures",
                column: "SongId");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_AspNetUsers_AppUserId",
                table: "Votes",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Bands_BandId",
                table: "Votes",
                column: "BandId",
                principalTable: "Bands",
                principalColumn: "BandId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_BandsRecords_BandsRecordId",
                table: "Votes",
                column: "BandsRecordId",
                principalTable: "BandsRecords",
                principalColumn: "BandsRecordId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Songs_SongId",
                table: "Votes",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "SongId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_AspNetUsers_AppUserId",
                table: "Votes");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Bands_BandId",
                table: "Votes");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_BandsRecords_BandsRecordId",
                table: "Votes");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Songs_SongId",
                table: "Votes");

            migrationBuilder.DropTable(
                name: "Awards");

            migrationBuilder.DropTable(
                name: "MusicVideos");

            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Votes",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_AppUserId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "SongText",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "MusicGenreDescription",
                table: "MusicGenres");

            migrationBuilder.DropColumn(
                name: "ReleaseOfTheCd",
                table: "BandsRecords");

            migrationBuilder.DropColumn(
                name: "FormedDate",
                table: "Bands");

            migrationBuilder.DropColumn(
                name: "FormedPlace",
                table: "Bands");

            migrationBuilder.DropColumn(
                name: "History",
                table: "Bands");

            migrationBuilder.RenameTable(
                name: "Votes",
                newName: "Vote");

            migrationBuilder.RenameIndex(
                name: "IX_Votes_SongId",
                table: "Vote",
                newName: "IX_Vote_SongId");

            migrationBuilder.RenameIndex(
                name: "IX_Votes_BandsRecordId",
                table: "Vote",
                newName: "IX_Vote_BandsRecordId");

            migrationBuilder.RenameIndex(
                name: "IX_Votes_BandId",
                table: "Vote",
                newName: "IX_Vote_BandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vote",
                table: "Vote",
                column: "VoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Bands_BandId",
                table: "Vote",
                column: "BandId",
                principalTable: "Bands",
                principalColumn: "BandId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_BandsRecords_BandsRecordId",
                table: "Vote",
                column: "BandsRecordId",
                principalTable: "BandsRecords",
                principalColumn: "BandsRecordId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Songs_SongId",
                table: "Vote",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "SongId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

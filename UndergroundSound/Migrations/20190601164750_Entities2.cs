using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace UndergroundSound.Migrations
{
    public partial class Entities2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BandsRecordId",
                table: "Songs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BandId",
                table: "BandsRecords",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MusicGenreId",
                table: "Bands",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BandId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Vote",
                columns: table => new
                {
                    VoteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BandId = table.Column<int>(nullable: true),
                    BandsRecordId = table.Column<int>(nullable: true),
                    SongId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vote", x => x.VoteId);
                    table.ForeignKey(
                        name: "FK_Vote_Bands_BandId",
                        column: x => x.BandId,
                        principalTable: "Bands",
                        principalColumn: "BandId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vote_BandsRecords_BandsRecordId",
                        column: x => x.BandsRecordId,
                        principalTable: "BandsRecords",
                        principalColumn: "BandsRecordId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vote_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "SongId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Songs_BandsRecordId",
                table: "Songs",
                column: "BandsRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_BandsRecords_BandId",
                table: "BandsRecords",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_Bands_MusicGenreId",
                table: "Bands",
                column: "MusicGenreId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BandId",
                table: "AspNetUsers",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_Vote_BandId",
                table: "Vote",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_Vote_BandsRecordId",
                table: "Vote",
                column: "BandsRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Vote_SongId",
                table: "Vote",
                column: "SongId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Bands_BandId",
                table: "AspNetUsers",
                column: "BandId",
                principalTable: "Bands",
                principalColumn: "BandId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bands_MusicGenres_MusicGenreId",
                table: "Bands",
                column: "MusicGenreId",
                principalTable: "MusicGenres",
                principalColumn: "MusicGenreId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BandsRecords_Bands_BandId",
                table: "BandsRecords",
                column: "BandId",
                principalTable: "Bands",
                principalColumn: "BandId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_BandsRecords_BandsRecordId",
                table: "Songs",
                column: "BandsRecordId",
                principalTable: "BandsRecords",
                principalColumn: "BandsRecordId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Bands_BandId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Bands_MusicGenres_MusicGenreId",
                table: "Bands");

            migrationBuilder.DropForeignKey(
                name: "FK_BandsRecords_Bands_BandId",
                table: "BandsRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_Songs_BandsRecords_BandsRecordId",
                table: "Songs");

            migrationBuilder.DropTable(
                name: "Vote");

            migrationBuilder.DropIndex(
                name: "IX_Songs_BandsRecordId",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_BandsRecords_BandId",
                table: "BandsRecords");

            migrationBuilder.DropIndex(
                name: "IX_Bands_MusicGenreId",
                table: "Bands");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_BandId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BandsRecordId",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "BandId",
                table: "BandsRecords");

            migrationBuilder.DropColumn(
                name: "MusicGenreId",
                table: "Bands");

            migrationBuilder.DropColumn(
                name: "BandId",
                table: "AspNetUsers");
        }
    }
}

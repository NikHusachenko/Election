using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Election.Api.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ElectionEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectionEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CandidateEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ElectionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateEntity_ElectionEntity_ElectionId",
                        column: x => x.ElectionId,
                        principalTable: "ElectionEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VoteEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VotedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CandidateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoteEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoteEntity_CandidateEntity_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "CandidateEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ElectionEntity",
                columns: new[] { "Id", "EndedOn", "Name", "StartedOn" },
                values: new object[] { 1, new DateTime(2025, 11, 5, 13, 15, 45, 781, DateTimeKind.Local).AddTicks(6167), "Best technology company 2025", new DateTime(2025, 10, 29, 13, 15, 45, 779, DateTimeKind.Local).AddTicks(2304) });

            migrationBuilder.InsertData(
                table: "CandidateEntity",
                columns: new[] { "Id", "ElectionId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "NVIDIA" },
                    { 2, 1, "SpaceX" },
                    { 3, 1, "OpenAI" },
                    { 4, 1, "Intel" },
                    { 5, 1, "Xiaomi" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidateEntity_ElectionId",
                table: "CandidateEntity",
                column: "ElectionId");

            migrationBuilder.CreateIndex(
                name: "IX_VoteEntity_CandidateId",
                table: "VoteEntity",
                column: "CandidateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VoteEntity");

            migrationBuilder.DropTable(
                name: "CandidateEntity");

            migrationBuilder.DropTable(
                name: "ElectionEntity");
        }
    }
}

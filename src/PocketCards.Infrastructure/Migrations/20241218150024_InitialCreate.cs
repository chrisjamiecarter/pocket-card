using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PocketCards.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PocketPack",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PocketPack", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PocketCard",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageFilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rarity = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    HitPoints = table.Column<int>(type: "int", nullable: true),
                    Stage = table.Column<int>(type: "int", nullable: true),
                    PackPoints = table.Column<int>(type: "int", nullable: true),
                    IsCollected = table.Column<bool>(type: "bit", nullable: false),
                    PocketPackId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PocketCard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PocketCard_PocketPack_PocketPackId",
                        column: x => x.PocketPackId,
                        principalTable: "PocketPack",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PocketCard_Number",
                table: "PocketCard",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PocketCard_PocketPackId",
                table: "PocketCard",
                column: "PocketPackId");

            migrationBuilder.CreateIndex(
                name: "IX_PocketPack_Name",
                table: "PocketPack",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PocketCard");

            migrationBuilder.DropTable(
                name: "PocketPack");
        }
    }
}

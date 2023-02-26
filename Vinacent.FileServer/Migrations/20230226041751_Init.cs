using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vinacent.FileServer.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RootProjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RootProjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RootProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdaterId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtensionJsonData = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileItems_FileItems_FileItemId",
                        column: x => x.FileItemId,
                        principalTable: "FileItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FileItems_RootProjects_RootProjectId",
                        column: x => x.RootProjectId,
                        principalTable: "RootProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileItems_FileItemId",
                table: "FileItems",
                column: "FileItemId");

            migrationBuilder.CreateIndex(
                name: "IX_FileItems_RootProjectId",
                table: "FileItems",
                column: "RootProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileItems");

            migrationBuilder.DropTable(
                name: "RootProjects");
        }
    }
}

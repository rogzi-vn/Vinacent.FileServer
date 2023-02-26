using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vinacent.FileServer.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FileItems",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhysicalServerPath",
                table: "FileItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "FileItemLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrowserInfo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileItemLogs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileItemLogs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FileItems");

            migrationBuilder.DropColumn(
                name: "PhysicalServerPath",
                table: "FileItems");
        }
    }
}

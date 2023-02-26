using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vinacent.FileServer.Migrations
{
    /// <inheritdoc />
    public partial class Init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "RootProjects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationTime",
                table: "RootProjects",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "FileItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationTime",
                table: "FileItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "FileItemId",
                table: "FileItemLogs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "FileItemLogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "RootProjects");

            migrationBuilder.DropColumn(
                name: "ModificationTime",
                table: "RootProjects");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "FileItems");

            migrationBuilder.DropColumn(
                name: "ModificationTime",
                table: "FileItems");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "FileItemLogs");

            migrationBuilder.AlterColumn<Guid>(
                name: "FileItemId",
                table: "FileItemLogs",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}

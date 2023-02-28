using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rich.BookMark.Migrations
{
    /// <inheritdoc />
    public partial class crawlerremovemenuId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MenuGuid",
                table: "CrawlerDetails");

            migrationBuilder.AlterColumn<string>(
                name: "ParentId",
                table: "CrawlerMenus",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "MenuId",
                table: "CrawlerMenus",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ParentId",
                table: "CrawlerMenus",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MenuId",
                table: "CrawlerMenus",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MenuGuid",
                table: "CrawlerDetails",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}

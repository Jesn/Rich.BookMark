using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rich.BookMark.Migrations
{
    /// <inheritdoc />
    public partial class crawler : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CrawlerDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SpiderSource = table.Column<int>(type: "integer", nullable: false),
                    DataId = table.Column<string>(type: "text", nullable: true),
                    MenuGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    MenuId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    UrlSource = table.Column<string>(name: "Url_Source", type: "text", nullable: true),
                    UrlSpider = table.Column<string>(name: "Url_Spider", type: "text", nullable: true),
                    Icon = table.Column<string>(type: "text", nullable: true),
                    favicon = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrawlerDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CrawlerMenus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MenuId = table.Column<int>(type: "integer", nullable: false),
                    ParentId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Icon = table.Column<string>(type: "text", nullable: true),
                    Sort = table.Column<int>(type: "integer", nullable: false),
                    SpiderSource = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrawlerMenus", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CrawlerDetails");

            migrationBuilder.DropTable(
                name: "CrawlerMenus");
        }
    }
}

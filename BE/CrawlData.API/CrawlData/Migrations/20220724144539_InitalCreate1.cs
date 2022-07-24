using Microsoft.EntityFrameworkCore.Migrations;

namespace CrawlData.Migrations
{
    public partial class InitalCreate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Pages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Pages",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyThueTro.Migrations
{
    /// <inheritdoc />
    public partial class themLyDo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LyDo",
                table: "lichXemPhongs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LyDo",
                table: "lichXemPhongs");
        }
    }
}

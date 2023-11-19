using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyThueTro.Migrations
{
    /// <inheritdoc />
    public partial class AddPublicIDImg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "giaTK",
                table: "loaiTaiKhoans",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "publicId",
                table: "ImagesPhongTro",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "publicId",
                table: "ImagesPhongTro");

            migrationBuilder.AlterColumn<double>(
                name: "giaTK",
                table: "loaiTaiKhoans",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}

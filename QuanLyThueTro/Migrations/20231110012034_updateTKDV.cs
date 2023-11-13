using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyThueTro.Migrations
{
    /// <inheritdoc />
    public partial class updateTKDV : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "soLuongDangTai",
                table: "loaiTaiKhoans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "trangThaiSuDung",
                table: "loaiTaiKhoans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "trangThaiSuDung",
                table: "dichVuDangTins",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "viTriHienThi",
                table: "dichVuDangTins",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "soLuongDangTai",
                table: "loaiTaiKhoans");

            migrationBuilder.DropColumn(
                name: "trangThaiSuDung",
                table: "loaiTaiKhoans");

            migrationBuilder.DropColumn(
                name: "trangThaiSuDung",
                table: "dichVuDangTins");

            migrationBuilder.DropColumn(
                name: "viTriHienThi",
                table: "dichVuDangTins");
        }
    }
}

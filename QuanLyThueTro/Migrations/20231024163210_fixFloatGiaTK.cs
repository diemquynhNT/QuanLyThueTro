using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyThueTro.Migrations
{
    /// <inheritdoc />
    public partial class fixFloatGiaTK : Migration
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

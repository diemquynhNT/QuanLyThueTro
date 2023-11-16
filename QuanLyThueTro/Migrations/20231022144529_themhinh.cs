﻿ using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyThueTro.Migrations
{
    /// <inheritdoc />
    public partial class themhinh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "hinhAnh",
                table: "users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hinhAnh",
                table: "users");
        }
    }
}

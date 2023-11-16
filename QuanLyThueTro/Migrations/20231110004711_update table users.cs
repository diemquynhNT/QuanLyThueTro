using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyThueTro.Migrations
{
    /// <inheritdoc />
    public partial class updatetableusers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "idUser",
                table: "tinDangs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tinDangs_idUser",
                table: "tinDangs",
                column: "idUser");

            migrationBuilder.AddForeignKey(
                name: "FK_tinDangs_users_idUser",
                table: "tinDangs",
                column: "idUser",
                principalTable: "users",
                principalColumn: "idUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tinDangs_users_idUser",
                table: "tinDangs");

            migrationBuilder.DropIndex(
                name: "IX_tinDangs_idUser",
                table: "tinDangs");

            migrationBuilder.DropColumn(
                name: "idUser",
                table: "tinDangs");
        }
    }
}

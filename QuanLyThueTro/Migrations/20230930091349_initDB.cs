using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyThueTro.Migrations
{
    /// <inheritdoc />
    public partial class initDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "chucVus",
                columns: table => new
                {
                    idChucVu = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    tenChucVu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chucVus", x => x.idChucVu);
                });

            migrationBuilder.CreateTable(
                name: "dichVuDangTins",
                columns: table => new
                {
                    idDichVu = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    hanDangTin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    giaCa = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dichVuDangTins", x => x.idDichVu);
                });

            migrationBuilder.CreateTable(
                name: "loaiTaiKhoans",
                columns: table => new
                {
                    idLoaiTK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    tenLoaiTK = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    giaTK = table.Column<float>(type: "real", nullable: false),
                    hanSuDung = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loaiTaiKhoans", x => x.idLoaiTK);
                });

            migrationBuilder.CreateTable(
                name: "thanhPhos",
                columns: table => new
                {
                    idThanhPho = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    tenThanhPho = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_thanhPhos", x => x.idThanhPho);
                });

            migrationBuilder.CreateTable(
                name: "thongTinCongTy",
                columns: table => new
                {
                    tenCongTy = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    hinhAnh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    moTa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_thongTinCongTy", x => x.tenCongTy);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    idUser = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    hoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    emailUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sdtUsers = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ngayThamGia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    userName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    passwordUser = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    gioiTinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idChucVu = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    idLoaiTK = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.idUser);
                    table.ForeignKey(
                        name: "FK_users_chucVus_idChucVu",
                        column: x => x.idChucVu,
                        principalTable: "chucVus",
                        principalColumn: "idChucVu");
                    table.ForeignKey(
                        name: "FK_users_loaiTaiKhoans_idLoaiTK",
                        column: x => x.idLoaiTK,
                        principalTable: "loaiTaiKhoans",
                        principalColumn: "idLoaiTK");
                });

            migrationBuilder.CreateTable(
                name: "khuVucs",
                columns: table => new
                {
                    idKhuVuc = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    tenKhuVuc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    moTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idThanhPho = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_khuVucs", x => x.idKhuVuc);
                    table.ForeignKey(
                        name: "FK_khuVucs_thanhPhos_idThanhPho",
                        column: x => x.idThanhPho,
                        principalTable: "thanhPhos",
                        principalColumn: "idThanhPho");
                });

            migrationBuilder.CreateTable(
                name: "giaoDiches",
                columns: table => new
                {
                    idGiaoDich = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    loaiDichVu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    tongTien = table.Column<float>(type: "real", nullable: false),
                    ngayGiaoDich = table.Column<DateTime>(type: "datetime2", nullable: false),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idUser = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_giaoDiches", x => x.idGiaoDich);
                    table.ForeignKey(
                        name: "FK_giaoDiches_users_idUser",
                        column: x => x.idUser,
                        principalTable: "users",
                        principalColumn: "idUser");
                });

            migrationBuilder.CreateTable(
                name: "tinDangs",
                columns: table => new
                {
                    idTinDang = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    tieuDe = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ngayBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    trangThaiTinDang = table.Column<bool>(type: "bit", nullable: false),
                    sdtNguoiLienHe = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    nguoiLienHe = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    doiTuongChoThue = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    soLuongPhong = table.Column<int>(type: "int", nullable: false),
                    ngayTaoTin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    idKhuVuc = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    idDichVu = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tinDangs", x => x.idTinDang);
                    table.ForeignKey(
                        name: "FK_tinDangs_dichVuDangTins_idDichVu",
                        column: x => x.idDichVu,
                        principalTable: "dichVuDangTins",
                        principalColumn: "idDichVu");
                    table.ForeignKey(
                        name: "FK_tinDangs_khuVucs_idKhuVuc",
                        column: x => x.idKhuVuc,
                        principalTable: "khuVucs",
                        principalColumn: "idKhuVuc");
                });

            migrationBuilder.CreateTable(
                name: "ImagesPhongTro",
                columns: table => new
                {
                    idImage = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nameImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idTinDang = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagesPhongTro", x => x.idImage);
                    table.ForeignKey(
                        name: "FK_ImagesPhongTro_tinDangs_idTinDang",
                        column: x => x.idTinDang,
                        principalTable: "tinDangs",
                        principalColumn: "idTinDang",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "lichXemPhongs",
                columns: table => new
                {
                    idLichXem = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ngayXem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    nguoiXem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sdtNguoiXem = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    trangThai = table.Column<bool>(type: "bit", nullable: false),
                    idTinDang = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lichXemPhongs", x => x.idLichXem);
                    table.ForeignKey(
                        name: "FK_lichXemPhongs_tinDangs_idTinDang",
                        column: x => x.idTinDang,
                        principalTable: "tinDangs",
                        principalColumn: "idTinDang");
                });

            migrationBuilder.CreateTable(
                name: "phongTros",
                columns: table => new
                {
                    idTinDang = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    diaChi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    giaPhong = table.Column<float>(type: "real", nullable: false),
                    dienTich = table.Column<double>(type: "float", nullable: false),
                    moTa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phongTros", x => x.idTinDang);
                    table.ForeignKey(
                        name: "FK_phongTros_tinDangs_idTinDang",
                        column: x => x.idTinDang,
                        principalTable: "tinDangs",
                        principalColumn: "idTinDang",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tinYeuThiches",
                columns: table => new
                {
                    idTinDang = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    idUser = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tinYeuThiches", x => new { x.idTinDang, x.idUser });
                    table.ForeignKey(
                        name: "FK_tinyeuthich_tindang",
                        column: x => x.idTinDang,
                        principalTable: "tinDangs",
                        principalColumn: "idTinDang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tinyeuthich_users",
                        column: x => x.idUser,
                        principalTable: "users",
                        principalColumn: "idUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "review",
                columns: table => new
                {
                    idUser = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    idPhongTro = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    noiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngayReview = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_review", x => new { x.idUser, x.idPhongTro });
                    table.ForeignKey(
                        name: "FK_review_tindang",
                        column: x => x.idPhongTro,
                        principalTable: "phongTros",
                        principalColumn: "idTinDang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_review_users",
                        column: x => x.idUser,
                        principalTable: "users",
                        principalColumn: "idUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_giaoDiches_idUser",
                table: "giaoDiches",
                column: "idUser");

            migrationBuilder.CreateIndex(
                name: "IX_ImagesPhongTro_idTinDang",
                table: "ImagesPhongTro",
                column: "idTinDang");

            migrationBuilder.CreateIndex(
                name: "IX_khuVucs_idThanhPho",
                table: "khuVucs",
                column: "idThanhPho");

            migrationBuilder.CreateIndex(
                name: "IX_lichXemPhongs_idTinDang",
                table: "lichXemPhongs",
                column: "idTinDang");

            migrationBuilder.CreateIndex(
                name: "IX_review_idPhongTro",
                table: "review",
                column: "idPhongTro");

            migrationBuilder.CreateIndex(
                name: "IX_tinDangs_idDichVu",
                table: "tinDangs",
                column: "idDichVu");

            migrationBuilder.CreateIndex(
                name: "IX_tinDangs_idKhuVuc",
                table: "tinDangs",
                column: "idKhuVuc");

            migrationBuilder.CreateIndex(
                name: "IX_tinYeuThiches_idUser",
                table: "tinYeuThiches",
                column: "idUser");

            migrationBuilder.CreateIndex(
                name: "IX_users_idChucVu",
                table: "users",
                column: "idChucVu");

            migrationBuilder.CreateIndex(
                name: "IX_users_idLoaiTK",
                table: "users",
                column: "idLoaiTK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "giaoDiches");

            migrationBuilder.DropTable(
                name: "ImagesPhongTro");

            migrationBuilder.DropTable(
                name: "lichXemPhongs");

            migrationBuilder.DropTable(
                name: "review");

            migrationBuilder.DropTable(
                name: "thongTinCongTy");

            migrationBuilder.DropTable(
                name: "tinYeuThiches");

            migrationBuilder.DropTable(
                name: "phongTros");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "tinDangs");

            migrationBuilder.DropTable(
                name: "chucVus");

            migrationBuilder.DropTable(
                name: "loaiTaiKhoans");

            migrationBuilder.DropTable(
                name: "dichVuDangTins");

            migrationBuilder.DropTable(
                name: "khuVucs");

            migrationBuilder.DropTable(
                name: "thanhPhos");
        }
    }
}

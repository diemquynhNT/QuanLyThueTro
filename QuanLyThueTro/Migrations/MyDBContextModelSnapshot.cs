﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuanLyThueTro.Data;

#nullable disable

namespace QuanLyThueTro.Migrations
{
    [DbContext(typeof(MyDBContext))]
    partial class MyDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("QuanLyThueTro.Model.ChucVu", b =>
                {
                    b.Property<string>("idChucVu")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("tenChucVu")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("idChucVu");

                    b.ToTable("chucVus");
                });

            modelBuilder.Entity("QuanLyThueTro.Model.DichVuDangTin", b =>
                {
                    b.Property<string>("idDichVu")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("giaCa")
                        .HasColumnType("real");

                    b.Property<DateTime>("hanDangTin")
                        .HasColumnType("datetime2");

                    b.HasKey("idDichVu");

                    b.ToTable("dichVuDangTins");
                });

            modelBuilder.Entity("QuanLyThueTro.Model.GiaoDich", b =>
                {
                    b.Property<string>("idGiaoDich")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("idUser")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("loaiDichVu")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("ngayGiaoDich")
                        .HasColumnType("datetime2");

                    b.Property<string>("note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("tongTien")
                        .HasColumnType("real");

                    b.HasKey("idGiaoDich");

                    b.HasIndex("idUser");

                    b.ToTable("giaoDiches");
                });

            modelBuilder.Entity("QuanLyThueTro.Model.Images", b =>
                {
                    b.Property<int>("idImage")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idImage"));

                    b.Property<string>("idTinDang")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("nameImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idImage");

                    b.HasIndex("idTinDang");

                    b.ToTable("ImagesPhongTro");
                });

            modelBuilder.Entity("QuanLyThueTro.Model.KhuVuc", b =>
                {
                    b.Property<string>("idKhuVuc")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("idThanhPho")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("moTa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tenKhuVuc")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("idKhuVuc");

                    b.HasIndex("idThanhPho");

                    b.ToTable("khuVucs");
                });

            modelBuilder.Entity("QuanLyThueTro.Model.LichXemPhong", b =>
                {
                    b.Property<string>("idLichXem")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("idTinDang")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("ngayXem")
                        .HasColumnType("datetime2");

                    b.Property<string>("nguoiXem")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("sdtNguoiXem")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<bool>("trangThai")
                        .HasColumnType("bit");

                    b.HasKey("idLichXem");

                    b.HasIndex("idTinDang");

                    b.ToTable("lichXemPhongs");
                });

            modelBuilder.Entity("QuanLyThueTro.Model.LoaiTaiKhoan", b =>
                {
                    b.Property<string>("idLoaiTK")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("giaTK")
                        .HasColumnType("real");

                    b.Property<DateTime>("hanSuDung")
                        .HasColumnType("datetime2");

                    b.Property<string>("tenLoaiTK")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("idLoaiTK");

                    b.ToTable("loaiTaiKhoans");
                });

            modelBuilder.Entity("QuanLyThueTro.Model.PhongTro", b =>
                {
                    b.Property<string>("idTinDang")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("diaChi")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<double>("dienTich")
                        .HasColumnType("float");

                    b.Property<float>("giaPhong")
                        .HasColumnType("real");

                    b.Property<string>("moTa")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idTinDang");

                    b.ToTable("phongTros");
                });

            modelBuilder.Entity("QuanLyThueTro.Model.Review", b =>
                {
                    b.Property<string>("idUser")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("idPhongTro")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("ngayReview")
                        .HasColumnType("datetime2");

                    b.Property<string>("noiDung")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idUser", "idPhongTro");

                    b.HasIndex("idPhongTro");

                    b.ToTable("review");
                });

            modelBuilder.Entity("QuanLyThueTro.Model.ThanhPho", b =>
                {
                    b.Property<string>("idThanhPho")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("tenThanhPho")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("idThanhPho");

                    b.ToTable("thanhPhos");
                });

            modelBuilder.Entity("QuanLyThueTro.Model.ThongTinCongTy", b =>
                {
                    b.Property<string>("tenCongTy")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("hinhAnh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("moTa")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("tenCongTy");

                    b.ToTable("thongTinCongTy");
                });

            modelBuilder.Entity("QuanLyThueTro.Model.TinDang", b =>
                {
                    b.Property<string>("idTinDang")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("doiTuongChoThue")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("idDichVu")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("idKhuVuc")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("ngayBatDau")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ngayKetThuc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ngayTaoTin")
                        .HasColumnType("datetime2");

                    b.Property<string>("nguoiLienHe")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("sdtNguoiLienHe")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("soLuongPhong")
                        .HasColumnType("int");

                    b.Property<string>("tieuDe")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("trangThaiTinDang")
                        .HasColumnType("bit");

                    b.HasKey("idTinDang");

                    b.HasIndex("idDichVu");

                    b.HasIndex("idKhuVuc");

                    b.ToTable("tinDangs");
                });

            modelBuilder.Entity("QuanLyThueTro.Model.TinYeuThich", b =>
                {
                    b.Property<string>("idTinDang")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("idUser")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("idTinDang", "idUser");

                    b.HasIndex("idUser");

                    b.ToTable("tinYeuThiches");
                });

            modelBuilder.Entity("QuanLyThueTro.Model.Users", b =>
                {
                    b.Property<string>("idUser")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("emailUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("gioiTinh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("hoTen")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("idChucVu")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("idLoaiTK")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("ngayThamGia")
                        .HasColumnType("datetime2");

                    b.Property<string>("passwordUser")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("sdtUsers")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("userName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("idUser");

                    b.HasIndex("idChucVu");

                    b.HasIndex("idLoaiTK");

                    b.ToTable("users");
                });

            modelBuilder.Entity("QuanLyThueTro.Model.GiaoDich", b =>
                {
                    b.HasOne("QuanLyThueTro.Model.Users", "users")
                        .WithMany("GiaoDiches")
                        .HasForeignKey("idUser");

                    b.Navigation("users");
                });

            modelBuilder.Entity("QuanLyThueTro.Model.Images", b =>
                {
                    b.HasOne("QuanLyThueTro.Model.TinDang", "tinDangs")
                        .WithMany("Images")
                        .HasForeignKey("idTinDang")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("tinDangs");
                });

            modelBuilder.Entity("QuanLyThueTro.Model.KhuVuc", b =>
                {
                    b.HasOne("QuanLyThueTro.Model.ThanhPho", "thanhPho")
                        .WithMany("KhuVucs")
                        .HasForeignKey("idThanhPho");

                    b.Navigation("thanhPho");
                });

            modelBuilder.Entity("QuanLyThueTro.Model.LichXemPhong", b =>
                {
                    b.HasOne("QuanLyThueTro.Model.TinDang", "tinDangs")
                        .WithMany("lichXemPhongs")
                        .HasForeignKey("idTinDang");

                    b.Navigation("tinDangs");
                });

            modelBuilder.Entity("QuanLyThueTro.Model.PhongTro", b =>
                {
                    b.HasOne("QuanLyThueTro.Model.TinDang", "tinDangs")
                        .WithMany()
                        .HasForeignKey("idTinDang")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("tinDangs");
                });

            modelBuilder.Entity("QuanLyThueTro.Model.Review", b =>
                {
                    b.HasOne("QuanLyThueTro.Model.PhongTro", "phongTros")
                        .WithMany("reviews")
                        .HasForeignKey("idPhongTro")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_review_tindang");

                    b.HasOne("QuanLyThueTro.Model.Users", "users")
                        .WithMany("reviews")
                        .HasForeignKey("idUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_review_users");

                    b.Navigation("phongTros");

                    b.Navigation("users");
                });

            modelBuilder.Entity("QuanLyThueTro.Model.TinDang", b =>
                {
                    b.HasOne("QuanLyThueTro.Model.DichVuDangTin", "dichVuDangTin")
                        .WithMany("TinDangs")
                        .HasForeignKey("idDichVu");

                    b.HasOne("QuanLyThueTro.Model.KhuVuc", "khuVucs")
                        .WithMany("TinDangs")
                        .HasForeignKey("idKhuVuc");

                    b.Navigation("dichVuDangTin");

                    b.Navigation("khuVucs");
                });

            modelBuilder.Entity("QuanLyThueTro.Model.TinYeuThich", b =>
                {
                    b.HasOne("QuanLyThueTro.Model.TinDang", "tinDangs")
                        .WithMany("tinYeuThiches")
                        .HasForeignKey("idTinDang")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_tinyeuthich_tindang");

                    b.HasOne("QuanLyThueTro.Model.Users", "users")
                        .WithMany("tinYeuThiches")
                        .HasForeignKey("idUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_tinyeuthich_users");

                    b.Navigation("tinDangs");

                    b.Navigation("users");
                });

            modelBuilder.Entity("QuanLyThueTro.Model.Users", b =>
                {
                    b.HasOne("QuanLyThueTro.Model.ChucVu", "chucVus")
                        .WithMany("users")
                        .HasForeignKey("idChucVu");

                    b.HasOne("QuanLyThueTro.Model.LoaiTaiKhoan", "loaiTaiKhoans")
                        .WithMany("users")
                        .HasForeignKey("idLoaiTK");

                    b.Navigation("chucVus");

                    b.Navigation("loaiTaiKhoans");
                });

            modelBuilder.Entity("QuanLyThueTro.Model.ChucVu", b =>
                {
                    b.Navigation("users");
                });

            modelBuilder.Entity("QuanLyThueTro.Model.DichVuDangTin", b =>
                {
                    b.Navigation("TinDangs");
                });

            modelBuilder.Entity("QuanLyThueTro.Model.KhuVuc", b =>
                {
                    b.Navigation("TinDangs");
                });

            modelBuilder.Entity("QuanLyThueTro.Model.LoaiTaiKhoan", b =>
                {
                    b.Navigation("users");
                });

            modelBuilder.Entity("QuanLyThueTro.Model.PhongTro", b =>
                {
                    b.Navigation("reviews");
                });

            modelBuilder.Entity("QuanLyThueTro.Model.ThanhPho", b =>
                {
                    b.Navigation("KhuVucs");
                });

            modelBuilder.Entity("QuanLyThueTro.Model.TinDang", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("lichXemPhongs");

                    b.Navigation("tinYeuThiches");
                });

            modelBuilder.Entity("QuanLyThueTro.Model.Users", b =>
                {
                    b.Navigation("GiaoDiches");

                    b.Navigation("reviews");

                    b.Navigation("tinYeuThiches");
                });
#pragma warning restore 612, 618
        }
    }
}

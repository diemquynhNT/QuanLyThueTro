using Microsoft.EntityFrameworkCore;
using QuanLyThueTro.Model;

namespace QuanLyThueTro.Data
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options) { }

        #region
        public DbSet<ThanhPho> thanhPhos { get; set; }
        public DbSet<KhuVuc> khuVucs { get; set; }
        public DbSet<TinDang> tinDangs { get; set; }
        public DbSet<GoiTinDichVu> dichVuDangTins { get; set; }
        public DbSet<LichXemPhong> lichXemPhongs { get; set; }
        public DbSet<PhongTro> phongTros { get; set; }
        public DbSet<Users> users { get; set; }
        public DbSet<GiaoDich> giaoDiches { get; set; }
        public DbSet<ChucVu> chucVus { get; set; }
        public DbSet<LoaiTaiKhoan> loaiTaiKhoans { get; set; }
        public DbSet<TinYeuThich> tinYeuThiches { get; set; }
        public DbSet<Review> review { get; set; }
        public DbSet<ThongTinCongTy> thongTinCongTy { get; set; }
        public DbSet<Images> ImagesPhongTro { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<TinYeuThich>(ct =>
            {
                ct.HasKey(t => new { t.idTinDang, t.idUser });

                ct.HasOne(e => e.users).
                   WithMany(e => e.tinYeuThiches)
                  .HasConstraintName("FK_tinyeuthich_users")
                  .HasForeignKey(e => e.idUser);

                ct.HasOne(e => e.tinDangs).
                WithMany(e => e.tinYeuThiches)
               .HasConstraintName("FK_tinyeuthich_tindang")
               .HasForeignKey(e => e.idTinDang);
            });

            modelBuilder.Entity<Review>(ct =>
            {
                ct.HasKey(t => new { t.idUser, t.idPhongTro });

                ct.HasOne(e => e.users).
                   WithMany(e => e.reviews)
                  .HasConstraintName("FK_review_users")
                  .HasForeignKey(e => e.idUser);

                ct.HasOne(e => e.phongTros).
                WithMany(e => e.reviews)
               .HasConstraintName("FK_review_tindang")
               .HasForeignKey(e => e.idPhongTro);
            });

            //Has Trigger
            modelBuilder.Entity<TinDang>(entry =>
            {
                entry.ToTable(tb => tb.HasTrigger("tr_ThemTinSLPhong"));
            });
            modelBuilder.Entity<LichXemPhong>(entry =>
            {
                entry.ToTable(tb => tb.HasTrigger("tr_ChuThueHuyLichXem"));
            });
        }
    }
}

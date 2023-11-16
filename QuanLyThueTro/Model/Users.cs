using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThueTro.Model
{
    public class Users
    {
        [Key]
        public string idUser { get; set; }
        [Required]
        [MaxLength(100)]
        public string hoTen { get; set; }

        [EmailAddress()]
        public string emailUser { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 8)]
        public string sdtUsers { get; set; }
        [Required]
        public DateTime ngayThamGia { get; set; }
        [Required]
        [MaxLength(100)]
        public string userName { get; set; }
        [Required]
        [MaxLength(100)]
        public string passwordUser { get; set; }
        [Required]
        public string gioiTinh { get; set; }

        public string? hinhAnh { get; set; }

        public bool trangThai { get; set; }


        public virtual ICollection<Review> reviews { get; set; }
        public virtual ICollection<GiaoDich> GiaoDiches { get; set; }
        public virtual ICollection<TinYeuThich> tinYeuThiches { get; set; }
        public virtual ICollection<TinDang> tinDangs { get; set; }
        public string? idChucVu { get; set; }
        [ForeignKey("idChucVu")]
        public ChucVu chucVus { get; set; }

        public string? idLoaiTK { get; set; }
        [ForeignKey("idLoaiTK")]
        public LoaiTaiKhoan loaiTaiKhoans { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace QuanLyThueTro.Dto
{
    public class TinDangPhongTroWithoutId
    {
        public string tieuDe { get; set; }
        public string loaiTin { get; set; }

        //public DateTime? ngayBatDau { get; set; }

        //public DateTime? ngayKetThuc { get; set; }

        //[Required]
        //public bool trangThaiTinDang { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 8)]
        public string sdtNguoiLienHe { get; set; }
        [MaxLength(100)]
        public string nguoiLienHe { get; set; }

        [Required]
        [MaxLength(100)]
        public string doiTuongChoThue { get; set; }
        [Required]
        public int soLuongPhong { get; set; }

        //public DateTime ngayTaoTin { get; set; }

        //public string? idKhuVuc { get; set; }

        //public string? idDichVu { get; set; }

        public string diaChi { get; set; } //phongtro
        [Required]
        public float giaPhong { get; set; } //phongtro
        [Required]
        public double dienTich { get; set; } //phongtro
        public string? moTa { get; set; } //phongtro
        public float tienDien { get; set; } //phongtro
        public float tienNuoc { get; set; } //phongtro
        public float tienDichVu { get; set; } //phongtro
        public string idUser { get; set; }
        public string? idDichVu { get; set; }

    }
}

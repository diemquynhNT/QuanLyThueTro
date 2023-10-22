using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Client_QuanLythueTro.Models
{
    public class TinDang
    {
        [Key]
        public string idTinDang { get; set; }
        [Required]
        [MaxLength(200)]
        public string tieuDe { get; set; }

        public DateTime? ngayBatDau { get; set; }

        public DateTime? ngayKetThuc { get; set; }
        [Required]
        public bool trangThaiTinDang { get; set; }
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
        [Required]
        public DateTime ngayTaoTin { get; set; }

        public string? idKhuVuc { get; set; }

        public string? idDichVu { get; set; }
    }
}

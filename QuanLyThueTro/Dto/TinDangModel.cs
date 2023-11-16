using QuanLyThueTro.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyThueTro.Dto
{
    public class TinDangModel
    {
        [Required]
        [MaxLength(200)]
        public string tieuDe { get; set; }
        [Required]
        [MaxLength(200)]
        public string loaiTin { get; set; }
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

        public string? idKhuVuc { get; set; }

        [Required]
        [MaxLength(100)]
        public string diaChi { get; set; }
        [Required]
        public float giaPhong { get; set; }
        [Required]
        public double dienTich { get; set; }
        public string? moTa { get; set; }
        public float tienDien { get; set; }
        public float tienNuoc { get; set; }
        public float tienDichVu { get; set; }
        public string? idUser { get; set; }
        //public List<IFormFile> listimg { get; set; }


    }
}

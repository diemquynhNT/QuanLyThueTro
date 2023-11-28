using QuanLyThueTro.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace QuanLyThueTro.Dto
{
    public class TinDangVM
    {
        [Key]
        public string idTinDang { get; set; }
        [Required]
        [MaxLength(200)]
        public string tieuDe { get; set; }
        [Required]
        [MaxLength(200)]
        public string loaiTin { get; set; }

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
        [Required]
        [MaxLength(100)]
        public string diaChi { get; set; }
        [Required]
        public float giaPhong { get; set; }
        [Required]
        public double dienTich { get; set; }
        public string? moTa { get; set; }
        public float tienNha { get; set; }
        public float tienDien { get; set; }
        public float tienNuoc { get; set; }
        public float tienDichVu { get; set; }
        public string? idUser { get; set; }
       
    }
}

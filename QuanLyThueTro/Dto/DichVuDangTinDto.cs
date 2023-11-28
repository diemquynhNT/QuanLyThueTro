using System.ComponentModel.DataAnnotations;

namespace QuanLyThueTro.Dto
{
    public class DichVuDangTinDto
    {
        [Key]
        public string idDichVu { get; set; }
        [Required]
        [MaxLength(100)]
        public string loaiDichVu { get; set; }
        [Required]
        public float giaCa { get; set; }
        [Required]
        public int hanDung { get; set; }
        public bool trangThaiSuDung { get; set; }
        public int viTriHienThi { get; set; }
    }
}

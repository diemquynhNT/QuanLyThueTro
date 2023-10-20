using System.ComponentModel.DataAnnotations;

namespace QuanLyThueTro.Dto
{
    public class DichVuDangTinDto
    {
        [Required]
        [MaxLength(100)]
        public string loaiDichVu { get; set; }
        [Required]
        public DateTime hanDangTin { get; set; }
        [Required]
        public float giaCa { get; set; }
    }
}

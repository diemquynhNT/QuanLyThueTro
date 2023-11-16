using System.ComponentModel.DataAnnotations;

namespace QuanLyThueTro.Dto
{
    public class DichVuDangTinDto
    {

        [Required]
        [MaxLength(100)]
        public string loaiDichVu { get; set; }
        [Required]
        public float giaCa { get; set; }
        [Required]
        public int hanDung { get; set; }

        public int viTriHienThi { get; set; }
    }
}

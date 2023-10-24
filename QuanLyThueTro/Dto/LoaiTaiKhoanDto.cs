using System.ComponentModel.DataAnnotations;

namespace QuanLyThueTro.Dto
{
    public class LoaiTaiKhoanDto
    {
        [Required]
        [MaxLength(100)]
        public string tenLoaiTK { get; set; }
        [Required]
        public float giaTK { get; set; }
        [Required]
        public int hanSuDung { get; set; }
    }
}

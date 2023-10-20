using System.ComponentModel.DataAnnotations;

namespace QuanLyThueTro.Dto
{
    public class ChucVuDto
    {
        [Required]
        [MaxLength(100)]
        public string tenChucVu { get; set; }
    }
}

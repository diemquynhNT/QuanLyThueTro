using System.ComponentModel.DataAnnotations;

namespace QuanLyThueTro.Dto
{
    public class ChucVuDto
    {
        [Key]
        public string idChucVu { get; set; }
        [Required]
        [MaxLength(100)]
        public string tenChucVu { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace QuanLyThueTro.Model
{
    public class ChucVu
    {
        [Key]
        public string idChucVu { get; set; }
        [Required]
        [MaxLength(100)]
        public string tenChucVu { get; set; }

        public virtual ICollection<Users> users { get; set; }

    }
}

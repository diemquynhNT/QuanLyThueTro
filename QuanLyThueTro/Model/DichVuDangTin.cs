using System.ComponentModel.DataAnnotations;

namespace QuanLyThueTro.Model
{
    public class DichVuDangTin
    {
        [Key]
        public string idDichVu { get; set; }
        [Required]
        [MaxLength(100)]
        public string loaiDichVu { get;}
        [Required]
        public DateTime hanDangTin { get; set; }
        [Required]
        public float giaCa { get; set; }

        public virtual ICollection<TinDang> TinDangs { get; set; }

    }
}

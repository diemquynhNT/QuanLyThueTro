using System.ComponentModel.DataAnnotations;

namespace QuanLyThueTro.Model
{
    public class DichVuDangTin
    {
        [Key]
        public string idDichVu { get; set; }
        [Required]
        [MaxLength(100)]
        public string loaiDichVu { get; set; }
        [Required]
        public float giaCa { get; set; }

        [Required]
        public int hanDangTin { get; set; }

        public virtual ICollection<TinDang> TinDangs { get; set; }

    }
}

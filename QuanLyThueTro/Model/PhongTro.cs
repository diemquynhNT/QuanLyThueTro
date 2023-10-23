using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThueTro.Model
{
    public class PhongTro
    {
        [Key]
        public string idTinDang { get; set; }
        [ForeignKey("idTinDang")]
        public TinDang tinDangs { get; set; }

        [Required]
        [MaxLength(100)]
        public string diaChi { get; set; }
        [Required]
        public float giaPhong { get; set; }
        [Required]
        public double dienTich { get; set; }
        public string? moTa { get; set; }
        public float tienDien { get; set; }
        public float tienNuoc { get; set; }
        public float tienDichVu { get; set; }

        public virtual ICollection<Review> reviews { get; set; }



    }
}

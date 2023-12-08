using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThueTro.Model
{
    public class GiaoDich
    {
        [Key]
        public string idGiaoDich { get; set; }
        [Required]
        [MaxLength(100)]
        public string loaiDichVu { get; set; }
        [Required]
        public float tongTien { get; set; }
        [Required]
        public DateTime ngayGiaoDich { get; set; }  
        public string? note { get; set; }

        public string? idUser { get; set; }
        [ForeignKey("idUser")]
        public Users? users { get; set; }
    }
}

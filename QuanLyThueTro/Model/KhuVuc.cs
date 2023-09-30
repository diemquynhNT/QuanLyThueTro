using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace QuanLyThueTro.Model
{
    public class KhuVuc
    {
        [Key]
        public string idKhuVuc { get; set; }
        [Required]
        [MaxLength(100)]
        public string tenKhuVuc { get; set; }

        public string? moTa { get; set; }

        public string? idThanhPho { get; set; }
        [ForeignKey("idThanhPho")]
        public ThanhPho thanhPho { get; set; }

        public virtual ICollection<TinDang> TinDangs { get; set; }





    }
}

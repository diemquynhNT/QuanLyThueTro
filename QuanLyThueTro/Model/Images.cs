using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThueTro.Model
{
    public class Images
    {
        [Key]
        public int idImage { get; set; }
        [Required]
        public string nameImage { get; set; }
        public string publicId { get; set; }
        public string idTinDang { get; set; }
        [ForeignKey("idTinDang")]
        public TinDang tinDangs { get; set; }

      
    }
}

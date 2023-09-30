using System.ComponentModel.DataAnnotations;

namespace QuanLyThueTro.Model
{
    public class ThanhPho
    {
        [Key]
        public string idThanhPho { get; set; }
        [Required]
        [MaxLength(100)]
        public string tenThanhPho { get; set; }

        public virtual ICollection<KhuVuc> KhuVucs { get; set; }

    }
}

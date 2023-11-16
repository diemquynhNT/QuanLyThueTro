using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Client_QuanLythueTro.Models
{
    public class Khuvucs
    {
        [Key]
        public string idKhuVuc { get; set; }
        [Required]
        [MaxLength(100)]
        public string tenKhuVuc { get; set; }

        public string? moTa { get; set; }

        public string? idThanhPho { get; set; }
    }
}

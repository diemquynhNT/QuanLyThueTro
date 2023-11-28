using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client_QuanLythueTro.Models
{
    public class images
    {
        [Key]
        public int idImage { get; set; }
        [Required]
        public string nameImage { get; set; }

        public string idTinDang { get; set; }

    }
}

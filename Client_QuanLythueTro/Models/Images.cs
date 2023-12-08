using System.ComponentModel.DataAnnotations;

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

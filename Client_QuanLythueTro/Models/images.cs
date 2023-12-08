using System.ComponentModel.DataAnnotations;

namespace Client_QuanLythueTro.Models
{
    public class Images
    {
        [Key]
        public int idImage { get; set; }
        public string nameImage { get; set; }
        public string publicId { get; set; }
        public string idTinDang { get; set; }


    }
}

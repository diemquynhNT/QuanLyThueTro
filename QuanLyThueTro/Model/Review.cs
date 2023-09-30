using System.ComponentModel.DataAnnotations;

namespace QuanLyThueTro.Model
{
    public class Review
    {
        
        public string idUser { get; set; }
        public string idPhongTro { get; set; }
        public Users users { get; set; }
        public PhongTro phongTros { get; set; }
        [Required]
        public string noiDung { get; set; }
        [Required]
        public DateTime ngayReview { get; set; }


    }
}

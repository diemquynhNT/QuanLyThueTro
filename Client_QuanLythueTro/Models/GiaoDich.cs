using System.ComponentModel.DataAnnotations;

namespace Client_QuanLythueTro.Models
{
    public class GiaoDich
    {
        public string idGiaoDich { get; set; }
        public string loaiDichVu { get; set; }
        public float tongTien { get; set; }
        public DateTime ngayGiaoDich { get; set; }
        public string note { get; set; }
        public string idUser { get; set; }
    }
}

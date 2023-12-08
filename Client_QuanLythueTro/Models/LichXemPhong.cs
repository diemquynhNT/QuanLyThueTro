using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client_QuanLythueTro.Models
{
    public class LichXemPhong
    {
        public string idLichXem { get; set; }
        public DateTime ngayXem { get; set; }
        public string nguoiXem { get; set; }
        public string sdtNguoiXem { get; set; }
        public bool trangThai { get; set; }
        public string? LyDo { get; set; }
        public string? idTinDang { get; set; }


    }
}

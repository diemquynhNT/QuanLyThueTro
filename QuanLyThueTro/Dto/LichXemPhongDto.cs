using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThueTro.Dto
{
    public class LichXemPhongDto
    {
        [Key]
        public string idLichXem { get; set; }
        [Required]
        public DateTime ngayXem { get; set; }
        [Required]
        public string nguoiXem { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 8)]
        public string sdtNguoiXem { get; set; }
        public bool trangThai { get; set; }

        public string? idTinDang { get; set; }


    }
}

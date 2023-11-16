using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace QuanLyThueTro.Model
{
    public class TinDang
    {
        [Key]
        public string idTinDang { get; set; }
        [Required]
        [MaxLength(200)]
        public string tieuDe { get; set; }
        [Required]
        [MaxLength(200)]
        public string loaiTin { get; set; }


        public DateTime? ngayBatDau { get; set; }
      
        public DateTime? ngayKetThuc { get; set; }

        [Required]
        public bool trangThaiTinDang { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 8)]
        public string sdtNguoiLienHe { get; set; } 
        [MaxLength(100)]
        public string nguoiLienHe { get; set; }
      
        [Required]
        [MaxLength(100)]
        public string doiTuongChoThue { get; set; }
        [Required]
        public int soLuongPhong { get; set; }
        [Required]
        public DateTime ngayTaoTin { get; set; }

        public string? idKhuVuc { get; set; }
        [ForeignKey("idKhuVuc")]
        public KhuVuc khuVucs { get; set; }
        public int luotTruyCap { get; set; }
        public string? idDichVu { get; set; }
        [ForeignKey("idDichVu")]
        public GoiTinDichVu dichVuDangTin { get; set; }
        public string? idUser { get; set; }
        [ForeignKey("idUser")]
        public Users users { get; set; }

        public virtual ICollection<LichXemPhong> lichXemPhongs { get; set; }
        public virtual ICollection<TinYeuThich> tinYeuThiches { get; set; }
        public virtual ICollection<Images> Images { get; set; }

        public TinDang()
        {
            lichXemPhongs = new HashSet<LichXemPhong>(); 
            tinYeuThiches = new HashSet<TinYeuThich>(); 
            Images = new HashSet<Images>(); 

        }



    }
}

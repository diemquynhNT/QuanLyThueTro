using QuanLyThueTro.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyThueTro.Dto
{
    public class TinDangPhongTroVM
    {

        public string idTinDang { get; set; }
        public string tieuDe { get; set; }
        public string loaiTin { get; set; }

        public DateTime? ngayBatDau { get; set; }

        public DateTime? ngayKetThuc { get; set; }

        //[Required]
        //public bool trangThaiTinDang { get; set; }
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
        
        //public DateTime ngayTaoTin { get; set; }

        //public string? idKhuVuc { get; set; }

        //public string? idDichVu { get; set; }

        public string diaChi { get; set; } //phongtro
        [Required]
        public float giaPhong { get; set; } //phongtro
        [Required]
        public double dienTich { get; set; } //phongtro
        public string? moTa { get; set; } //phongtro
        public float tienDien { get; set; } //phongtro
        public float tienNuoc { get; set; } //phongtro
        public float tienDichVu { get; set; } //phongtro
        public int luotTruyCap { get; set; }
        public string idUser { get; set; }
        public bool trangThaiTinDang { get; set; }
        public string? idDichVu { get; set; }
        public TinDangPhongTroVM(string idTinDang, string tieuDe, string loaiTin, DateTime? ngayBatDau, DateTime? ngayKetThuc, string sdtNguoiLienHe, string nguoiLienHe, string doiTuongChoThue, int soLuongPhong, string diaChi, float giaPhong, double dienTich, string? moTa, float tienDien, float tienNuoc, float tienDichVu, int luotTruyCap, string idUser, bool trangThaiTinDang, string? idDichVu)
        {
            this.idTinDang = idTinDang;
            this.tieuDe = tieuDe;
            this.loaiTin = loaiTin;
            this.sdtNguoiLienHe = sdtNguoiLienHe;
            this.nguoiLienHe = nguoiLienHe;
            this.doiTuongChoThue = doiTuongChoThue;
            this.soLuongPhong = soLuongPhong;
            this.diaChi = diaChi;
            this.giaPhong = giaPhong;
            this.dienTich = dienTich;
            this.moTa = moTa;
            this.tienDien = tienDien;
            this.tienNuoc = tienNuoc;
            this.tienDichVu = tienDichVu;
            this.ngayBatDau = ngayBatDau;
            this.ngayKetThuc = ngayKetThuc;
            this.luotTruyCap = luotTruyCap;
            this.idUser = idUser;
            this.trangThaiTinDang = trangThaiTinDang;
            this.idDichVu = idDichVu;
        }

        public TinDangPhongTroVM()
        {
        }
    }
}

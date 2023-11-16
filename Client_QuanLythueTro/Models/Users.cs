using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Client_QuanLythueTro.Models
{
    public class Users
    {
        [Key]
        public string idUser { get; set; }
        [Required]
        [MaxLength(100)]
        public string hoTen { get; set; }

        [EmailAddress()]
        public string emailUser { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 8)]
        public string sdtUsers { get; set; }
        [Required]
        public DateTime ngayThamGia { get; set; }
        [Required]
        [MaxLength(100)]
        public string userName { get; set; }
        [Required]
        [MaxLength(100)]
        public string passwordUser { get; set; }
        [Required]
        public string gioiTinh { get; set; }

        public string? hinhAnh { get; set; }

        public bool trangThai { get; set; }
        public string? idChucVu { get; set; }

        public string? idLoaiTK { get; set; }
        public IFormFile Avatar { get; set; }
    }
}

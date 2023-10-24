﻿using System.ComponentModel.DataAnnotations;

namespace QuanLyThueTro.Model
{
    public class LoaiTaiKhoan
    {
        [Key]
        public string idLoaiTK { get; set; }
        [Required]
        [MaxLength(100)]
        public string tenLoaiTK { get; set; }
        [Required]
        public double giaTK { get; set; }
        [Required]
        public int hanDung { get; set; }

        public virtual ICollection<Users> users { get; set; }

    }
}

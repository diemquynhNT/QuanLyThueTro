﻿using System.ComponentModel.DataAnnotations;

namespace Client_QuanLythueTro.Models
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
    }
}
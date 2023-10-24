﻿using System.ComponentModel.DataAnnotations;

namespace Client_QuanLythueTro.Models
{
    public class DichVuDangTin
    {
        [Key]
        public string idDichVu { get; set; }
        [Required]
        [MaxLength(100)]
        public string loaiDichVu { get; set; }
        [Required]
        public float giaCa { get; set; }

        [Required]
        public int hanDung { get; set; }
    }
}

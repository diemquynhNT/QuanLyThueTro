﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThueTro.Model
{
    public class Images
    {
        [Key]
        public int idImage { get; set; }
        public string nameImage { get; set; }

        public string idTinDang { get; set; }

      
    }
}

using System.ComponentModel.DataAnnotations;

namespace Client_QuanLythueTro.Models
{
    public class ThongTinCongTy
    {
        [Key]
        public string tenCongTy { get; set; }
        public string hinhAnh { get; set; }
        public string? moTa { get; set; }

    }

}


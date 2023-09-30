namespace QuanLyThueTro.Model
{
    public class TinYeuThich
    {
        public string idTinDang { get; set; }
        public string idUser { get; set; }

        public Users users { get; set; }
        public TinDang tinDangs { get; set; }
 
    }
}

using QuanLyThueTro.Dto;
using QuanLyThueTro.Model;

namespace QuanLyThueTro.Services
{
    public interface ITinDang
    {
        public List<TinDang> GetAll();
        public List<PhongTro> GetAllPhong();
        public TinDang GetTinDangById(string idTin);
        public PhongTro GetPhongById(string idTin);
        public Task<TinDang> AddTinDang(TinDang tin,PhongTro phong);
        public Task<TinDang> UpdateTinDang(TinDang tin,PhongTro phong);
        public Task<bool> DeleteTinDang(string idTinDang);
        public bool IsValidTinDang(string idTinDang);

      
    }
}

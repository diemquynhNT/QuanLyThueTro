using QuanLyThueTro.Dto;
using QuanLyThueTro.Model;

namespace QuanLyThueTro.Services
{
    public interface ITinDang
    {
        public List<TinDang> GetAll();
        public Task<TinDang> GetTinDangById(string idTin);
        public Task<TinDang> AddTinDang(TinDang tin);
        public Task<TinDang> UpdateTinDang(TinDang tin);
        public Task<bool> DeleteTinDang(string idTinDang);
        public bool IsValidTinDang(string idTinDang);

      
    }
}

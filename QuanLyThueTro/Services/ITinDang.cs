using QuanLyThueTro.Dto;
using QuanLyThueTro.Model;

namespace QuanLyThueTro.Services
{
    public interface ITinDang
    {
        public List<TinDang> GetAll();
        public List<TinDang> GetTinDangByIdUser(string id);
        public List<TinDang> GetAllByIdThanhPho(string idTP);
        public List<PhongTro> GetAllPhong();
        public List<Images> GetAllImagesById(string idTin);
        public TinDang GetTinDangById(string idTin);
        public PhongTro GetPhongById(string idTin);
        public Task<TinDang> AddTinDang(TinDang tin,PhongTro phong);
        public Task<TinDang> UpdateTinDang(TinDang tin,PhongTro phong);
        public void DeleteTinDang(string idTinDang);
        public bool IsValidTinDang(string idTinDang);
        public void DuyetTin(TinDang tin);
        public List<TinDang> Filter(int thang, bool trangThai);
        public Task AddHinhanh(string tinId, IFormFile file);
        public Task ThemHinh(Images hinh);
        public Task<Images> GetImage(string idTin);
        public void HuyDuyetTin(TinDang tin);
        public List<TinDangVM> BinarySearchByDienTich(List<TinDangVM> listPhong, double minDT, double maxDT);
        public List<TinDangVM> BinarySearchByPrice(List<TinDangVM> listPhong, double minGia, double maxGia);




    }
}

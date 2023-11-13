using QuanLyThueTro.Model;

namespace QuanLyThueTro.Services
{
    public interface IExtensionService
    {
        public void AutoPK_ChucVu(ChucVu chucVu);
        bool IsNotExistNameChucVu(ChucVu chucVu);
        public void AutoPK_LoaiTK(LoaiTaiKhoan loaiTaiKhoan);
        bool IsNotExistNameLoaiTK(LoaiTaiKhoan loaiTaiKhoan);
        public void AutoPK_DichVu(GoiTinDichVu dichVu);
        bool IsNotExistNameDichVu(GoiTinDichVu dichVu);
        public string AutoPK_Common();
        public int AutoPK_IntCommon();
        public void GoUpLuotTruyCap(TinDang tinDang);
    }
}

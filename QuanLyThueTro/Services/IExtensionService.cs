using QuanLyThueTro.Model;

namespace QuanLyThueTro.Services
{
    public interface IExtensionService
    {
        public void AutoPK_ChucVu(ChucVu chucVu);
        bool IsNotExistNameChucVu(ChucVu chucVu);
    }
}

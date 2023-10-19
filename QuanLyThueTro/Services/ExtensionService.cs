using Microsoft.EntityFrameworkCore;
using QuanLyThueTro.Data;
using QuanLyThueTro.Model;

namespace QuanLyThueTro.Services
{
    public class ExtensionService : IExtensionService
    {
        private readonly MyDBContext _dbContext;

        public ExtensionService(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AutoPK_ChucVu(ChucVu chucVu)
        {
            Random rnd = new Random();
            string num = rnd.Next(1000, 10000000).ToString();
            chucVu.idChucVu = "#RO" + num;
        }

        public bool IsNotExistNameChucVu(ChucVu chucVu)
        {
            var existing = _dbContext.chucVus.FirstOrDefault(x => x.tenChucVu == chucVu.tenChucVu);

            if (existing == null)
            {
                return true;
            }
            if (existing.idChucVu != chucVu.idChucVu && _dbContext.chucVus.Any(x => x.tenChucVu == chucVu.tenChucVu))
            {
                return false;
            }
            //else if(_dbContext.chucVus.Any(u => u.tenChucVu == chucVu.tenChucVu))
            //    return false;
            _dbContext.chucVus.Remove(existing);
            return true;
        }
    }
}

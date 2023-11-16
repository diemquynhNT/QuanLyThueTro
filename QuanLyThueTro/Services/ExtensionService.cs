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
            chucVu.idChucVu = "RO" + num;
        }

        public string AutoPK_Common()
        {
            Random rnd = new Random();
            string stRnd = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string pK = new string(Enumerable.Repeat(stRnd, 12).Select(s => s[rnd.Next(s.Length)]).ToArray());
            return pK;
        }

        public int AutoPK_IntCommon()
        {
            Random rnd = new Random();
            string stRnd = "0123456789";
            string pK = new string(Enumerable.Repeat(stRnd, 6).Select(s => s[rnd.Next(s.Length)]).ToArray());
            int pkN = (int)(uint)long.Parse(pK);
            return pkN;
        }

        public void AutoPK_DichVu(GoiTinDichVu dichVu)
        {
            Random rnd = new Random();
            string num = rnd.Next(1000, 10000000).ToString();
            dichVu.idDichVu = "DV" + num;
        }

        public void AutoPK_LoaiTK(LoaiTaiKhoan loaiTaiKhoan)
        {
            Random rnd = new Random();
            string num = rnd.Next(1000, 10000000).ToString();
            loaiTaiKhoan.idLoaiTK = "TAC" + num;
        }


        #region Check Exist Object's name 
        #endregion

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
            _dbContext.chucVus.Remove(existing);
            return true;
        }

        public bool IsNotExistNameDichVu(GoiTinDichVu dichVu)
        {
            var existing = _dbContext.dichVuDangTins.FirstOrDefault(x => x.loaiDichVu == dichVu.loaiDichVu);

            if (existing == null)
            {
                return true;
            }
            if (existing.idDichVu != dichVu.idDichVu && _dbContext.dichVuDangTins.Any(x => x.loaiDichVu == dichVu.loaiDichVu))
            {
                return false;
            }
            _dbContext.dichVuDangTins.Remove(existing);
            return true;
        }

        public bool IsNotExistNameLoaiTK(LoaiTaiKhoan loaiTaiKhoan)
        {
            var existing = _dbContext.loaiTaiKhoans.FirstOrDefault(x => x.tenLoaiTK == loaiTaiKhoan.tenLoaiTK);

            if (existing == null)
            {
                return true;
            }
            if (existing.idLoaiTK != loaiTaiKhoan.idLoaiTK && _dbContext.loaiTaiKhoans.Any(x => x.tenLoaiTK == loaiTaiKhoan.tenLoaiTK))
            {
                return false;
            }
            _dbContext.loaiTaiKhoans.Remove(existing);
            return true;
        }

        public void GoUpLuotTruyCap(TinDang tinDang)
        {
            tinDang.luotTruyCap+=1;
            _dbContext.Entry(tinDang).State = EntityState.Modified;
            try
            {
                _dbContext.SaveChangesAsync();
                
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}

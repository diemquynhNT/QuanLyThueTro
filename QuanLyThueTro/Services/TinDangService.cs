using Microsoft.Extensions.Options;
using QuanLyThueTro.Data;
using QuanLyThueTro.Model;

namespace QuanLyThueTro.Services
{
    public class TinDangService : ITinDang
    {
        private MyDBContext _context;
        private GenerateAlphanumericId getId;

        public TinDangService(MyDBContext context)
        {
            _context = context;
            getId = new GenerateAlphanumericId();
        }
        public async Task<TinDang> AddTinDang(TinDang tin)
        {
            try
            {
                tin.idTinDang = getId.GenerateId(6);
                tin.trangThaiTinDang = false;
                tin.ngayTaoTin = DateTime.Now;
                tin.idDichVu = null;
                _context.Add(tin);
                await _context.SaveChangesAsync();
                return tin;
            }
            catch (Exception ex)
            {
                // Logging or handling exception here
                throw; // Rethrow the exception to maintain the error flow
            }
        }

        public async Task<bool> DeleteTinDang(string idTinDang)
        {
            var tin = _context.tinDangs.SingleOrDefault(t => t.idTinDang == idTinDang);
            if (tin == null)
                return false;
            _context.Remove(tin);
            _context.SaveChanges();
            return true;
        }

        public List<TinDang> GetAll()
        {
            return _context.tinDangs.ToList();
        }

        public async Task<TinDang> GetTinDangById(string idTin)
        {
            return _context.tinDangs.Where(t => t.idTinDang == idTin).FirstOrDefault();
        }

        public bool IsValidTinDang(string idTinDang)
        {
            throw new NotImplementedException();
        }

        public async Task<TinDang> UpdateTinDang(TinDang tin)
        {
            _context.tinDangs.Update(tin);
            _context.SaveChanges();
            return tin;
        }
    }
}

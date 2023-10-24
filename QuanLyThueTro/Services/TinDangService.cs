using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<TinDang> AddTinDang(TinDang tin,PhongTro phong)
        {
            try
            {
                tin.idTinDang = getId.GenerateId(6);
                tin.trangThaiTinDang = false;
                tin.ngayTaoTin = DateTime.Now;
                tin.ngayBatDau = DateTime.Now;
                tin.idDichVu = null;
                phong.idTinDang = tin.idTinDang;
                _context.tinDangs.Add(tin);
                _context.phongTros.Add(phong);
                await _context.SaveChangesAsync();
                return tin;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

       

        public async Task<bool> DeleteTinDang(string idTinDang)
        {
            var tin = _context.tinDangs.SingleOrDefault(t => t.idTinDang == idTinDang);
            var phong = _context.phongTros.SingleOrDefault(t => t.idTinDang == idTinDang);
            if (tin == null)
                return false;
            _context.Remove(phong);
            _context.Remove(tin);
            _context.SaveChanges();
            return true;
        }

        public PhongTro GetPhongById(string idTin)
        {
            return _context.phongTros.Where(t => t.idTinDang == idTin).FirstOrDefault();
        }

        public List<TinDang> GetAll()
        {
            return _context.tinDangs.ToList();
        }

        public List<PhongTro> GetAllPhong()
        {
            return _context.phongTros.ToList();
        }

        public TinDang GetTinDangById(string idTin)
        {
            return _context.tinDangs.Where(t => t.idTinDang == idTin).FirstOrDefault();
        }

        public bool IsValidTinDang(string idTinDang)
        {
            throw new NotImplementedException();
        }

        public async Task<TinDang> UpdateTinDang(TinDang tin,PhongTro phong)
        {
            _context.tinDangs.Update(tin);
            _context.phongTros.Update(phong);
            _context.Entry(tin).State = EntityState.Modified;
            _context.Entry(phong).State = EntityState.Modified;
            _context.SaveChanges();
            return tin;
        }

        public void DuyetTin(TinDang tin,bool status)
        {
            tin.trangThaiTinDang = status;
            if(status)
            {
                tin.ngayBatDau = DateTime.Today;
                tin.ngayKetThuc = DateTime.Today.AddDays(30);
            }    
            _context.Entry(tin).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public List<TinDang> Filter(int thang, bool trangThai)
        {
            if (thang == 0)
                return _context.tinDangs.Where(t=>t.trangThaiTinDang==trangThai).ToList();
            else
                return _context.tinDangs.Where(t =>t.ngayTaoTin.Month == thang && t.trangThaiTinDang == trangThai).ToList();

        }
        public async Task<Images> AddHinhanh(string tinId, IFormFile file)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            var hinhanh = new Images
            {
                nameImage = fileName,
                idTinDang = tinId,
                idImage = Guid.NewGuid().GetHashCode(),

            };
            _context.ImagesPhongTro.Add(hinhanh);
            await _context.SaveChangesAsync();

            return hinhanh;
        }

    }
}

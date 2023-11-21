
﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using QuanLyThueTro.Data;
using QuanLyThueTro.Dto;
using QuanLyThueTro.Model;

namespace QuanLyThueTro.Services
{
    public class TinDangService : ITinDang
    {
        private MyDBContext _context;
        private GenerateAlphanumericId getId;
        public static IWebHostEnvironment _webHostEnvironment;

        public TinDangService(MyDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            getId = new GenerateAlphanumericId();
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<TinDang> AddTinDang(TinDang tin,PhongTro phong)
        {
            try
            {
                tin.idTinDang ="T"+ getId.GenerateId(6);
                tin.trangThaiTinDang = false;
                tin.ngayTaoTin = DateTime.Now;
                tin.ngayBatDau = null;
                tin.idDichVu = "NODV";
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
        public async Task AddHinhanh(string tinId, IFormFile file)
        {
            Images hinh = new Images();
            if (file.Length > 0)
            {
                
                hinh.idTinDang = tinId;
                string path = _webHostEnvironment.WebRootPath + "\\img\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);

                }
                using (FileStream fileStream = System.IO.File.Create(path + file.FileName))
                {
                    file.CopyTo(fileStream);
                    fileStream.Flush();
                    hinh.nameImage = file.FileName;

                }
            }
            _context.ImagesPhongTro.Add(hinh);
            _context.SaveChanges();
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

        public void DuyetTin(TinDang tin)
        {
            tin.trangThaiTinDang = true;
            tin.ngayBatDau = DateTime.Today;
            tin.ngayKetThuc = DateTime.Today.AddDays(30);
            _context.SaveChanges();
        }
        public void HuyDuyetTin(TinDang tin)
        {
            tin.trangThaiTinDang = false;
            tin.ngayBatDau = null;
            tin.ngayKetThuc = null;
            _context.SaveChanges();
        }

        public List<TinDang> Filter(int thang, bool trangThai)
        {
            if (thang == 0)
                return _context.tinDangs.Where(t=>t.trangThaiTinDang==trangThai).ToList();
            else
                return _context.tinDangs.Where(t =>t.ngayTaoTin.Month == thang && t.trangThaiTinDang == trangThai).ToList();

        }
       
        public Task<Images> GetImage(string idTin)
        {
            //List<Images> list=_context.ImagesPhongTro.Where(t=>t.idTinDang==idTin).ToList();
            // foreach(Images img in list)
            // {
            //     string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");
            //     var filePath = Path.Combine(path,img.nameImage );
            //     if (System.IO.File.Exists(filePath))
            //     {
            //         var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            //         return fileStream;
            //     }
            //     return null; ;
            // }    
            return null;
            
        }

        public List<TinDang> GetTinDangByIdUser(string id)
        {
            return _context.tinDangs.Where(t=>t.idUser== id).ToList();
        }
    }
}

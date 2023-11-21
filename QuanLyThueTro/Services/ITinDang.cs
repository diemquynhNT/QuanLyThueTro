﻿using QuanLyThueTro.Dto;
using QuanLyThueTro.Model;

namespace QuanLyThueTro.Services
{
    public interface ITinDang
    {
        public List<TinDang> GetAll();
        public List<TinDang> GetTinDangByIdUser(string id);
        public List<PhongTro> GetAllPhong();
        public TinDang GetTinDangById(string idTin);
        public PhongTro GetPhongById(string idTin);
        public Task<TinDang> AddTinDang(TinDang tin,PhongTro phong);
        public Task<TinDang> UpdateTinDang(TinDang tin,PhongTro phong);
        public Task<bool> DeleteTinDang(string idTinDang);
        public bool IsValidTinDang(string idTinDang);
        public void DuyetTin(TinDang tin);
        public List<TinDang> Filter(int thang, bool trangThai);
        public Task AddHinhanh(string tinId, IFormFile file);
        public Task<Images> GetImage(string idTin);
        public void HuyDuyetTin(TinDang tin);




    }
}

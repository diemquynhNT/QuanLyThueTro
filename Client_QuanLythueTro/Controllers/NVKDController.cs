﻿using Client_QuanLythueTro.APIGateWay;
using Client_QuanLythueTro.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Web;




namespace Client_QuanLythueTro.Controllers
{
    public class NVKDController : Controller
    {
        private readonly APIGateWayTinDang apiGateWay;
        private readonly APIGateWayDichVu apiGateWayDichVu;
        public NVKDController(APIGateWayTinDang aPIGate, APIGateWayDichVu apiGateWayDichVu)
        {
            this.apiGateWay = aPIGate;
            this.apiGateWayDichVu = apiGateWayDichVu;
        }
        private byte[] Base64UrlDecode(string input)
        {
            string base64 = input.Replace('-', '+').Replace('_', '/');
            while (base64.Length % 4 != 0)
            {
                base64 += '=';
            }
            return Convert.FromBase64String(base64);
        }
        public string GetIdUser()
        {
            string token = Request.Cookies["access_token"];
            var tokenParts = token.Split('.');
            var encodedPayload = tokenParts[1];
            var decodedPayload = Base64UrlDecode(encodedPayload);
            var decodedPayloadString = Encoding.UTF8.GetString(decodedPayload);
            var payloadObject = JObject.Parse(decodedPayloadString);
            var img = payloadObject["ImageUrl"]?.Value<string>();
            return img;
        }
        // GET: NVKDController
        public ActionResult TrangChu()
        {
            ViewBag.UserImageUrl = GetIdUser();
            List<TinDang> listTin = apiGateWay.ListTinDang();
            ViewBag.list = listTin;
            return View();
        }
        private void BindDropDownList()
        {
            List<SelectListItem> m = new List<SelectListItem>();
            m.Add(new SelectListItem { Text = "All", Value = "0" });
            m.Add(new SelectListItem { Text = "1", Value = "1" });
            m.Add(new SelectListItem { Text = "2", Value = "2" });
            m.Add(new SelectListItem { Text = "3", Value = "3" });
            m.Add(new SelectListItem { Text = "4", Value = "4" });
            m.Add(new SelectListItem { Text = "5", Value = "5" });
            m.Add(new SelectListItem { Text = "6", Value = "6" });
            m.Add(new SelectListItem { Text = "7", Value = "7" });
            m.Add(new SelectListItem { Text = "8", Value = "8" });
            m.Add(new SelectListItem { Text = "9", Value = "9" });
            m.Add(new SelectListItem { Text = "10", Value = "10" });
            m.Add(new SelectListItem { Text = "11", Value = "11" });
            m.Add(new SelectListItem { Text = "12", Value = "12" });
            TempData["thangs"] = m;
        }
        private void TinhTrangTin()
        {
            List<SelectListItem> tinhTrang = new List<SelectListItem>();
            tinhTrang.Add(new SelectListItem { Text = "Đã duyệt", Value = true.ToString() });
            tinhTrang.Add(new SelectListItem { Text = "Chưa suyệt", Value = false.ToString() });
            TempData["tinhTrang"] = tinhTrang;
        }
        // GET: NVKDController
        public ActionResult QuanLyTinDang()
        {
            ViewBag.UserImageUrl = GetIdUser();
            BindDropDownList();
            TinhTrangTin();
            List<TinDang> listTin = apiGateWay.ListTinDang();
            return View(listTin);
        }
        [HttpPost]
        public ActionResult QuanLyTinDang(int thangs,bool tinhTrang)
        {
            ViewBag.UserImageUrl = GetIdUser();
            BindDropDownList();
            TinhTrangTin();
            List<TinDang> listTin = apiGateWay.FilterTin(thangs, tinhTrang);
            return View(listTin);
        }

        // GET: NVKDController/Details/5
        public ActionResult DetailTinDang(string id)
        {
            ViewBag.UserImageUrl = GetIdUser();
            TinDang tin = apiGateWay.GetTin(id);
            return View(tin);
        }
        [HttpPost]
        public ActionResult SaveImages(string tinId, IFormFile file)
        {
            try
            {
                apiGateWay.AddImg(tinId, file);
                return RedirectToAction("QuanLyTinDang");
            }
            catch (Exception ex)
            {
                throw; 
            }
        }

        public ActionResult CreateTinDang()
        {
            ViewBag.UserImageUrl = GetIdUser();
            TinDang tin = new TinDang();
            return View(tin);
        }

        // POST: NVKDController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTinDang(TinDang tin)
        {
            try
            {
                apiGateWay.CreateTin(tin);
                return RedirectToAction("QuanLyTinDang");
            }
            catch
            {
                return View();
            }
        }

        // GET: NVKDController/Edit/5
        public ActionResult EditTin(string id)
        {
            ViewBag.UserImageUrl = GetIdUser();
            TinDang tin = apiGateWay.GetTin(id);
            return View(tin);
        }

        // POST: NVKDController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTin(TinDang tin)
        {
            try
            {
                apiGateWay.UpdateTin(tin);
                return RedirectToAction("QuanLyTinDang");
            }
            catch
            {
                return View();
            }
        }

        // GET: NVKDController/Delete/5
        //public ActionResult DeleteTin(string id)
        //{
        //    TinDang tin = apiGateWay.GetTin(id);
        //    return View(tin);
        //}

        // POST: NVKDController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult DeleteTin(string id)
        {
            try
            {
                //ViewBag.UserImageUrl = GetIdUser();
                apiGateWay.DeleteTin(id);
                return RedirectToAction("QuanLyTinDang");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult DuyetTin(string id,bool status)
        {
            try
            {
                
                apiGateWay.DuyetTin(id,status);
                return RedirectToAction("QuanLyTinDang");
            }
            catch
            {
                return View();
            }
        }



        // GET: NVKDController
        public ActionResult QuanLyDichVu()
        {
            ViewBag.UserImageUrl = GetIdUser();
            List<DichVuDangTin> listGoiTin = apiGateWayDichVu.ListGoiTin();
            ViewBag.listTK = apiGateWayDichVu.ListTK();
            return View(listGoiTin);
        }
        // GET: NVKDController/Details/5
        public ActionResult DetailDichVu(string id)
        {
            ViewBag.UserImageUrl = GetIdUser();
            TinDang tin = apiGateWay.GetTin(id);
            return View(tin);
        }

        public ActionResult CreateGoiTinDichVu()
        {
            ViewBag.UserImageUrl = GetIdUser();
            DichVuDangTin tin = new DichVuDangTin();
            return View(tin);
        }

        // POST: NVKDController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGoiTinDichVu(DichVuDangTin tin)
        {
            try
            {
                apiGateWayDichVu.CreateGoiTin(tin);
                return RedirectToAction("QuanLyDichVu");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult EditGoiTinDichVu(string id)
        {
            ViewBag.UserImageUrl = GetIdUser();
            DichVuDangTin tin = apiGateWayDichVu.GetGoiTin(id);
            return View(tin);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditGoiTinDichVu(DichVuDangTin tin)
        {
            try
            {
                apiGateWayDichVu.UpdateDichVuDangTin(tin);
                return RedirectToAction("QuanLyDichVu");
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult DeleteGoiTin(string id)
        {
            try
            {
                //ViewBag.UserImageUrl = GetIdUser();
                apiGateWayDichVu.DeleteTin(id);
                return RedirectToAction("QuanLyDichVu");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CreateTK()
        {
            ViewBag.UserImageUrl = GetIdUser();
            LoaiTaiKhoan tin = new LoaiTaiKhoan();
            return View(tin);
        }

        // POST: NVKDController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTK(LoaiTaiKhoan tk)
        {
            try
            {
                apiGateWayDichVu.CreateTK(tk);
                return RedirectToAction("QuanLyDichVu");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult EditTK(string id)
        {
            ViewBag.UserImageUrl = GetIdUser();
            LoaiTaiKhoan tk = apiGateWayDichVu.GetTK(id);
            return View(tk);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTK(LoaiTaiKhoan tk)
        {
            try
            {
                apiGateWayDichVu.UpdateTK(tk);
                return RedirectToAction("QuanLyDichVu");
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult DeleteTK(string id)
        {
            try
            {
                //ViewBag.UserImageUrl = GetIdUser();
                apiGateWayDichVu.DeleteTK(id);
                return RedirectToAction("QuanLyDichVu");
            }
            catch
            {
                return View();
            }
        }


    }
}

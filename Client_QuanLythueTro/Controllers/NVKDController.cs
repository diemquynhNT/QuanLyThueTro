using Client_QuanLythueTro.APIGateWay;
using Client_QuanLythueTro.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Web;





namespace Client_QuanLythueTro.Controllers
{
    public class NVKDController : Controller
    {
        private readonly APIGateWayTinDang apiGateWay;
        private readonly APIGateWayDichVu apiGateWayDichVu;
        private readonly APIGateWayKhuVuc apiGateWayKhuVuc;
        private readonly GiaoDich_Gateway apiGateWayGiaoDich;

        public NVKDController(APIGateWayTinDang aPIGate, APIGateWayDichVu apiGateWayDichVu, APIGateWayKhuVuc APIGateWayKhuVuc, GiaoDich_Gateway apiGateWayGiaoDich)
        {
            this.apiGateWay = aPIGate;
            this.apiGateWayDichVu = apiGateWayDichVu;
            this.apiGateWayKhuVuc = APIGateWayKhuVuc;
            this.apiGateWayGiaoDich = apiGateWayGiaoDich;
        }
       
        // GET: NVKDController
        public ActionResult TrangChu()
        {
            List<TinDang> listTin = apiGateWay.ListTinDang();
            TempData["listGiaoDich"]= apiGateWayGiaoDich.ListGiaoDich().Where(t=>t.ngayGiaoDich.Month==DateTime.Now.Month).ToList();
            return View(listTin);
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
           
            BindDropDownList();
            TinhTrangTin();
            List<TinDang> listTin = apiGateWay.ListTinDang();
            return View(listTin);
        }
        [HttpPost]
        public ActionResult QuanLyTinDang(int thangs,bool tinhTrang)
        {
           
            BindDropDownList();
            TinhTrangTin();
            List<TinDang> listTin = apiGateWay.FilterTin(thangs, tinhTrang);
            return View(listTin);
        }

        // GET: NVKDController/Details/5
        public ActionResult DetailTinDang(string id)
        {
            TinDang tin = apiGateWay.GetTin(id);
            return View(tin);
        }
      

        public ActionResult CreateTinDang()
        {
            TinDang tin = new TinDang();
            return View(tin);
        }

        // POST: NVKDController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateTinDang(TinDang tin, List<IFormFile> listimg)
        {
            try
            {
                TinDang newTin = apiGateWay.CreateTinDang(tin);
                apiGateWay.CreateImage(newTin.idTinDang, listimg);
                TempData["mess"] = "thanhcong";
                return RedirectToAction("QuanLyTinDang");
            }
            catch (Exception ex)
            {
                return Content("Error: " + ex.Message);

            }
        }
        //public ActionResult AddImgToTinDang(string idTinDang)
        //{
        //    ViewBag.id = idTinDang;
        //    return View();
        //}

        // GET: NVKDController/Edit/5
        public ActionResult EditTin(string id)
        {
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
                TempData["AlertMessage"] = "thanhcong";
                return RedirectToAction("EditTin", new { idTinDang = tin.idTinDang });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }
        [HttpPost]
        public ActionResult DeleteTin(string id)
        {
            try
            {
                apiGateWay.DeleteTin(id);
                return RedirectToAction("QuanLyTinDang");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
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
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }



        // GET: NVKDController
        public ActionResult QuanLyDichVu()
        {
            List<DichVuDangTin> listGoiTin = apiGateWayDichVu.ListGoiTin();
            ViewBag.listTK = apiGateWayDichVu.ListTK();
            return View(listGoiTin);
        }
        public ActionResult QuanLyTaiKhoanDV()
        {
           
            List<LoaiTaiKhoan> listtk = apiGateWayDichVu.ListTK();
            return View(listtk);
        }
        // GET: NVKDController/Details/5
        public ActionResult DetailDichVu(string id)
        {
            TinDang tin = apiGateWay.GetTin(id);
            return View(tin);
        }

        public ActionResult CreateGoiTinDichVu()
        {
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
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }
        public ActionResult EditGoiTinDichVu(string id)
        {
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
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
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
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        public ActionResult CreateTK()
        {
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
                return RedirectToAction("QuanLyTaiKhoanDV");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }
        public ActionResult EditTK(string id)
        {
            LoaiTaiKhoan tk =  apiGateWayDichVu.GetTaiKhoan(id);
            return View(tk);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTK(LoaiTaiKhoan tk)
        {
            try
            {
                apiGateWayDichVu.UpdateTK(tk);
                return RedirectToAction("QuanLyTaiKhoanDV");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
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
                return RedirectToAction("QuanLyTaiKhoanDV");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        public ActionResult QuanLyKhuVuc()
        {
            return View();
        }
        public ActionResult QuanLyTinThanhPho(string cityname)
        {
            var listTin = apiGateWay.ListTinDangByIdThanhPho(cityname);
            ViewBag.Cityname = cityname;
            return View(listTin);
        }

        public ActionResult QuanLyGiaoDich()
        {
            var list = apiGateWayGiaoDich.ListGiaoDich();
            return View(list);
        }
        
        public ActionResult ThongTinCT()
        {
            return View();
        }


    }
}

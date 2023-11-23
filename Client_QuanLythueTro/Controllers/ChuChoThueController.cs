using Client_QuanLythueTro.APIGateWay;
using Client_QuanLythueTro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Net.NetworkInformation;
using System.Reflection;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Client_QuanLythueTro.Services;
using Microsoft.IdentityModel.Tokens;

namespace Client_QuanLythueTro.Controllers
{
    public class ChuChoThueController : Controller
    {
        private readonly TinDang_PhongTro_GateWay callTinDangPT;
        private readonly LichXemPhong_GateWay _callLichXemPhong;
        private readonly APIGateWayDichVu _callDichVu;
        private readonly GiaoDich_Gateway _callGiaoDich;
        private readonly IPaymentService _paymentService;

        public ChuChoThueController(TinDang_PhongTro_GateWay callTinDangPT, LichXemPhong_GateWay callLichXemPhong, IPaymentService paymentService, APIGateWayDichVu callDichVu, GiaoDich_Gateway callGiaoDich)
        {
            this.callTinDangPT = callTinDangPT;
            _callLichXemPhong = callLichXemPhong;
            _paymentService = paymentService;
            _callDichVu = callDichVu;
            _callGiaoDich = callGiaoDich;
        }

        public Users GetUser()
        {
            var userJson = HttpContext.Session.GetString("user");
            Users user = JsonConvert.DeserializeObject<Users>(userJson);
            return user;
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("IndexTinDangPT", new RouteValueDictionary(
                                   new { controller = "ChuChoThue", action = "IndexTinDangPT", Id = "" }));
        }

        public IActionResult IndexTinDangPT()
        {
            List<TinDang> listTin = callTinDangPT.ListTinDangPhongTro();
            return View(listTin);
        }

        //
        public IActionResult DetailTinDangPT(string id)
        {
            TinDang tinDang = callTinDangPT.GetTinDang(id);
            List<string> imgList = new List<string>();
            imgList = callTinDangPT.ListImages(id);
            ViewBag.listImg = imgList.ToList();
            return View(tinDang);
        }

        [HttpGet]
        public IActionResult QLTinDangPT(string id)
        {
            IEnumerable<TinDang> tinDangs = callTinDangPT.ListTinDangPTByUser(id);
            return View(tinDangs);
        }

        //create tin đăng + phòng trọ
        [HttpGet]
        public IActionResult CreateTinDangPT()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateTinDangPT(TinDang tinDang)
        {
            try
            {
                tinDang = callTinDangPT.CreateTinDang(tinDang);
                var files = CreateImage();
                callTinDangPT.CreateImage(tinDang.idTinDang, files);
                TempData["AlertMessage"] = "successful";
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message); // Thêm lỗi vào ModelState
                return View(); // Trả về View để hiển thị lỗi
            }
        }

        [HttpPost]
        public IFormFileCollection CreateImage()
        {
            IFormFileCollection files = Request.Form.Files;
            return files;
        }

        [HttpGet]
        public IActionResult EditTinDang(string id)
        {
            TinDang tinDang = callTinDangPT.GetTinDang(id);
            return View(tinDang);
        }

        [HttpPost]
        public IActionResult EditTinDang(TinDang tinDang)
        {
            callTinDangPT.EditTinDang(tinDang.idTinDang, tinDang);
            TempData["AlertMessage"] = "editsuccessful";
            var user = GetUser();
            return RedirectToAction("QLTinDangPT", new RouteValueDictionary(
                                   new { controller = "ChuChoThue", action = "QLTinDangPT", Id = user.idUser }));
        }


        [HttpPost]
        public IActionResult DeleteTinDang(string id)
        {
            callTinDangPT.DeleteTinDang(id);
            var user = GetUser();
            return RedirectToAction("QLTinDangPT", new RouteValueDictionary(
                                  new { controller = "ChuChoThue", action = "QLTinDangPT", Id = user.idUser }));
        }

        //Lich xem phong
        public IActionResult QLLichXemPhong()
        {
            List<LichXemPhong> lichXemList = _callLichXemPhong.ListLichXemPhong();
            return View(lichXemList);
        }

        [HttpGet]
        public IActionResult CreateLichXem()
        {
            var list = callTinDangPT.ListTinDangPhongTro().ToList();
            ViewBag.ListIdTD = list
                .Select(x => new SelectListItem
                {
                    Value = x.idTinDang,
                    Text = x.idTinDang
                })
                .ToList();
            return View();
        }

        [HttpPost]
        public IActionResult CreateLichXem(LichXemPhong lichXemPhong)
        {
            lichXemPhong.idLichXem = "auto";
            _callLichXemPhong.CreateLichXem(lichXemPhong);
            TempData["AlertMessage"] = "successful";
            return View();
        }


        [HttpGet]
        public IActionResult EditLichXem(string id)
        {
            LichXemPhong lichXem = _callLichXemPhong.GetLichXem(id);
            return View(lichXem);
        }

        [HttpPost]
        public IActionResult EditLichXem(LichXemPhong lichXemPhong)
        {
            _callLichXemPhong.EditLichXem(lichXemPhong.idLichXem, lichXemPhong);
            TempData["AlertMessage"] = "editsuccessful";
            return RedirectToAction("QLLichXemPhong");
        }

        //[HttpGet]
        //public IActionResult GetDeleteLichXem(string id)
        //{
        //    ViewBag.strID = id;
        //    return RedirectToAction("QLLichXemPhong");
        //}

        [HttpPost]
        public IActionResult DeleteLichXem(string id)
        {
            _callLichXemPhong.DeleteLichXem(id);
            return RedirectToAction("QLLichXemPhong");
        }

        //Mua dich vụ
        [HttpGet]
        public IActionResult DangKyDichVu()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DangKyDichVu(VNPayInformationModel model, string madv, int httt)
        {
            var dichvu = _callDichVu.GetGoiTin(madv);
            model.OrderType = madv;
            model.OrderDescription = "Đã thanh toán cho loại dịch vụ " + dichvu.loaiDichVu + ", có hạn dùng " + dichvu.hanDung + " ngày.";
            model.Amount = dichvu.giaCa;
            model.Name = GetUser().hoTen + " - #" + GetUser().idUser;
            //httt = 1: VNPay, 2: PayPal, 3: Momo
            if(httt == 1)
            {
                var url = _paymentService.CreateVNPaymentUrl(model, HttpContext);
                return Redirect(url);
            }

            return View();
            
        }

        public IActionResult PaymentCallback()
        {
            var response = _paymentService.VNPaymentExecute(Request.Query);
            if(response.VnPayResponseCode == "00")
            {
                GiaoDich giaoDich = new GiaoDich();
                giaoDich.idGiaoDich = response.PaymentId;
                giaoDich.loaiDichVu = response.OrderDescription;
                giaoDich.tongTien = float.Parse(response.Amount);
                giaoDich.ngayGiaoDich = DateTime.UtcNow;
                giaoDich.note = response.Username + " " + response.InfoDichVu + " Với số tiền " + response.Amount + " VNĐ";
                var cut = response.Username.Split("#");
                giaoDich.idUser = cut[1];
                try
                {
                    _callGiaoDich.CreateGiaoDich(giaoDich);
                }catch(Exception ex)
                {
                    return Redirect(ex.Message);
                }
                return View(response);
            }
            return RedirectToAction("DangKyDichVu");
        }

        public IActionResult LichSuThanhToan()
        {
            Users user = GetUser();
            IEnumerable<GiaoDich> giaoDiches = _callGiaoDich.ListGiaoDich().Where(g=>g.idUser == user.idUser).ToList();
            return View(giaoDiches);
        }

    }
}

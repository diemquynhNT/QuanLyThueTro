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
using Microsoft.AspNetCore.Diagnostics;
using System.Text.RegularExpressions;
using SmartBreadcrumbs.Attributes;

namespace Client_QuanLythueTro.Controllers
{
  
    public class ChuChoThueController : Controller
    {
        private readonly TinDang_PhongTro_GateWay callTinDangPT;
        private readonly APIGateWayTinDang apiTinDang;
        private readonly LichXemPhong_GateWay _callLichXemPhong;
        private readonly APIGateWayDichVu _callDichVu;
        private readonly GiaoDich_Gateway _callGiaoDich;
        private readonly IPaymentService _paymentService;
        private readonly APIGateWayUsers _callWayUsers;

        public ChuChoThueController(TinDang_PhongTro_GateWay callTinDangPT, LichXemPhong_GateWay callLichXemPhong, IPaymentService paymentService, APIGateWayDichVu callDichVu, GiaoDich_Gateway callGiaoDich, APIGateWayTinDang apiTinDang, APIGateWayUsers callWayUsers = null)
        {
            this.callTinDangPT = callTinDangPT;
            _callLichXemPhong = callLichXemPhong;
            _paymentService = paymentService;
            _callDichVu = callDichVu;
            _callGiaoDich = callGiaoDich;
            this.apiTinDang = apiTinDang;
            _callWayUsers = callWayUsers;
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
        [HttpPost]
        public IActionResult IndexTinDangPT(string inputHuyen, string inputTinh, string inputGiaMin,string inputGiaMax,string inputDienTichMin,string inputDienTichMax)
        {
            List<TinDang> listTin = new List<TinDang>();

            if (inputTinh == null && inputDienTichMax==null && inputGiaMax==null)
            {
                listTin = apiTinDang.ListTinDang().Where(t => t.trangThaiTinDang == true).ToList();
                return View(listTin);
            }
            if (inputDienTichMax==null &&inputGiaMax==null)
            {
                listTin = apiTinDang.ListTinDangByIdKhuVuc(inputHuyen, inputTinh);
                return View(listTin);
            }
            if (inputDienTichMax == null)
            {
                float minPrice = float.Parse(inputGiaMin);
                float maxPrice = float.Parse(inputGiaMax);
                return View(apiTinDang.ListTinDangByPrice(inputHuyen, inputTinh, minPrice, maxPrice));
            }
            else if (inputGiaMax == null)
            {
                float min = float.Parse(inputDienTichMin);
                float max = float.Parse(inputDienTichMax);
                return View(apiTinDang.ListTinDangByDienTich(inputHuyen, inputTinh, min, max));
            }
            else
            {
                float minPrice = float.Parse(inputGiaMin);
                float maxPrice = float.Parse(inputGiaMax);
                float min = float.Parse(inputDienTichMin);
                float max = float.Parse(inputDienTichMax);
                listTin = apiTinDang.ListTinDangByDienTichPrice(inputHuyen, inputTinh,  min, max,minPrice, maxPrice);
                return View(listTin);
            }
        }

    
        public IActionResult TinTheoTinhThanh(string thanhpho)
        {
            List<TinDang> listTin = new List<TinDang>();
            listTin = apiTinDang.ListTinDangByIdThanhPho(thanhpho);
            TempData["tinh"] = thanhpho;
            return View(listTin);
        }
        [HttpPost]
        public IActionResult TinTheoTinhThanh(string inputHuyen, string inputTinh, string inputGiaMin, string inputGiaMax, string inputDienTichMin, string inputDienTichMax)
        {
            List<TinDang> listTin = new List<TinDang>();

            if (inputTinh == null && inputDienTichMax == null && inputGiaMax == null)
            {
                listTin = apiTinDang.ListTinDang().Where(t => t.trangThaiTinDang == true).ToList();
                return View(listTin);
            }
            if (inputDienTichMax == null && inputGiaMax == null)
            {
                listTin = apiTinDang.ListTinDangByIdKhuVuc(inputHuyen, inputTinh);
                return View(listTin);
            }
            if (inputDienTichMax == null)
            {
                float minPrice = float.Parse(inputGiaMin);
                float maxPrice = float.Parse(inputGiaMax);
                return View(apiTinDang.ListTinDangByPrice(inputHuyen, inputTinh, minPrice, maxPrice));
            }
            else if (inputGiaMax == null)
            {
                float min = float.Parse(inputDienTichMin);
                float max = float.Parse(inputDienTichMax);
                return View(apiTinDang.ListTinDangByDienTich(inputHuyen, inputTinh, min, max));
            }
            else
            {
                float minPrice = float.Parse(inputGiaMin);
                float maxPrice = float.Parse(inputGiaMax);
                float min = float.Parse(inputDienTichMin);
                float max = float.Parse(inputDienTichMax);
                listTin = apiTinDang.ListTinDangByDienTichPrice(inputHuyen, inputTinh, min, max, minPrice, maxPrice);
                return View(listTin);
            }
        }


        public IActionResult TinTheoTinhThanhKhuVuc(string huyen,string tinh)
        {
            List<TinDang> listTin = new List<TinDang>();
            listTin = apiTinDang.ListTinDangByIdKhuVuc(huyen, tinh);
            TempData["tinh"] = huyen+", "+tinh;
            return View(listTin);
        }


        public IActionResult DetailTinDangPT(string id)
        {
            TinDang tinDang = callTinDangPT.GetTinDang(id);
            List<string> imgList = new List<string>();
            imgList = callTinDangPT.ListImages(id);
            ViewBag.listImg = imgList.ToList();
            var divu = _callDichVu.GetGoiTin(tinDang.idDichVu);
            ViewBag.dichVu = divu.loaiDichVu;
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
                var idDV = _callGiaoDich.GetGiaoDich(GetUser().idUser);
                if (idDV.Count != 0)
                {
                    var gd = idDV[idDV.Count() - 1];
                    tinDang.idDichVu = gd.loaiDichVu.ToString();
                }
                else
                    tinDang.idDichVu = "NODV";

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
            if (files.Count == 0)
                return (IFormFileCollection)BadRequest(error: "null");
            return files;
        }

        [HttpGet]
        public IActionResult EditTinDang(string id)
        {
            TinDang tinDang = callTinDangPT.GetTinDang(id);
            return View(tinDang);
        }

        [HttpGet]
        public IActionResult EditTinDangDaDuyet(string id)
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
            IEnumerable<TinDang> tinDangs = callTinDangPT.ListTinDangPTByUser(GetUser().idUser);
            List<LichXemPhong> lichXemList = new List<LichXemPhong>();
            foreach (var item in tinDangs)
            {
                var list = _callLichXemPhong.GetLichXemByIdTinDang(item.idTinDang);
                foreach(var ls in list)
                {
                    lichXemList.Add(ls);
                }
            }

            return View(lichXemList);
        }

        [HttpGet]
        public IActionResult CreateLichXem(string id)
        { 
            //var list = callTinDangPT.ListTinDangPTByUser(GetUser().idUser);
            //ViewBag.ListIdTD = list
            //    .Select(x => new SelectListItem
            //    {
            //        Value = x.idTinDang,
            //        Text = x.idTinDang
            //    })
            //    .ToList();
            ViewBag.ListIdTD = id;
            return View();
        }

        [HttpPost]
        public IActionResult CreateLichXem(LichXemPhong lichXemPhong)
        {
            lichXemPhong.idLichXem = "auto";
            _callLichXemPhong.CreateLichXem(lichXemPhong);
            TempData["AlertMessage"] = "successful";
            return RedirectToAction("QLLichXemPhong");
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
            List<DichVuDangTin> listGoiTin = _callDichVu.ListGoiTin().OrderByDescending(g=>g.giaCa).ToList();
            ViewBag.listTK = _callDichVu.ListTK();
            return View(listGoiTin);
        }

        [HttpGet]
        public IActionResult BangGiaDichVu()
        {
            List<DichVuDangTin> listGoiTin = _callDichVu.ListGoiTin().OrderByDescending(g => g.giaCa).ToList();
            ViewBag.listTK = _callDichVu.ListTK();
            return View(listGoiTin);
        }

        [HttpPost]
        public async Task<IActionResult> DangKyDichVu(VNPayInformationModel model, string madv, int httt)
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
            }else if(httt == 3)
            {
                MomoInfoModel momoModel = new MomoInfoModel();
                momoModel.FullName = model.Name;
                momoModel.Amount = model.Amount;
                momoModel.OrderId = "123456789";
                momoModel.OrderInfo = model.OrderDescription;
                var response = await _paymentService.CreatePaymentAsync(momoModel);
                return Redirect(response.PayUrl);
            }

            return View();
            
        }

        [HttpGet]
        public IActionResult MomoPaymentCallBack()
        {
            var response = _paymentService.PaymentExecuteAsync(HttpContext.Request.Query);
            if(response.ErrorCode != 42)
            {
                return View(response);
            }else
                return RedirectToAction("DangKyDichVu");
        }

        public IActionResult VNPayPaymentCallback()
        {
            var response = _paymentService.VNPaymentExecute(Request.Query);
            if(response.VnPayResponseCode == "00")
            {
                GiaoDich giaoDich = new GiaoDich();
                giaoDich.idGiaoDich = response.PaymentId;
                giaoDich.loaiDichVu = response.IdDichVu;
                giaoDich.tongTien = float.Parse(response.Amount);
                giaoDich.ngayGiaoDich = DateTime.UtcNow.ToLocalTime() ;
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
            IEnumerable<GiaoDich> giaoDiches = _callGiaoDich.GetGiaoDich(user.idUser);
            return View(giaoDiches);
        }

        public IActionResult ThongTinCaNhan(string id)
        {
            Users u = _callWayUsers.GetUser(id);
            return View(u);
        }
        [HttpPost]
        public IActionResult ThongTinCaNhan(Users u)
        {
            try
            {
                _callWayUsers.UpdateUsers(u);
                TempData["thongbao"] = "thanhcong";
                return View();
            }
            catch
            {
                return View();
            }
        }

        public IActionResult HopDongThueTro()
        {
            return View();
        }


    }
}

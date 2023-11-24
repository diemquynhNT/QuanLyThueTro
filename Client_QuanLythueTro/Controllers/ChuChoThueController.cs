using Client_QuanLythueTro.APIGateWay;
using Client_QuanLythueTro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Client_QuanLythueTro.Controllers
{
    public class ChuChoThueController : Controller
    {
        private readonly TinDang_PhongTro_GateWay callTinDangPT;
        private readonly APIGateWayTinDang apiTinDang;
        private readonly LichXemPhong_GateWay _callLichXemPhong;

        public ChuChoThueController(TinDang_PhongTro_GateWay callTinDangPT, LichXemPhong_GateWay callLichXemPhong, APIGateWayTinDang apiTinDang)
        {
            this.callTinDangPT = callTinDangPT;
            _callLichXemPhong = callLichXemPhong;
            this.apiTinDang = apiTinDang;
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
        public IActionResult IndexTinDangPT(string id)
        {
            List<TinDang> listTin = callTinDangPT.ListTinDangPhongTro();
            return View(listTin);
        }


        //public IActionResult TrangChuTinDang()
        //{
        //    List<TinDang> listTin = callTinDangPT.ListTinDangPhongTro();
        //    return View(listTin);
        //}
        //[HttpPost]
        public IActionResult TrangChuTinDang(string inputDiaDiemDaChon,string inputGiaMin,string inputGiaMax,string inputDienTichMin,string inputDienTichMax)
        {
            List<TinDang> listTin = new List<TinDang>();
            if (inputDiaDiemDaChon==null)
                listTin = apiTinDang.ListTinDang();
            listTin = apiTinDang.ListTinDangByIdKhuVuc(inputDiaDiemDaChon);

            if(inputDienTichMax==null &&inputGiaMax==null)
                return View(listTin);
           
            if(inputDienTichMax==null)
            {
                float minPrice = float.Parse(inputGiaMin);
                float maxPrice = float.Parse(inputGiaMax);
                return View(apiTinDang.ListTinDangByPrice(inputDiaDiemDaChon, minPrice, maxPrice));
            }else if(inputGiaMax==null)
            {
                float min = float.Parse(inputDienTichMin);
                float max = float.Parse(inputDienTichMax);
                return View(apiTinDang.ListTinDangByDienTich(inputDiaDiemDaChon, min, max));
            }    
            return View(listTin);
        }

        public IActionResult TinTheoTinhThanh(string idThanhPho)
        {
            List<TinDang> listTin = new List<TinDang>();
            listTin = apiTinDang.ListTinDangByIdThanhPho(idThanhPho);
            TempData["tinh"] = idThanhPho;
            return View(listTin);
        }

        
        public IActionResult DetailTinDangPT(string id)
        {
            TinDang tinDang = callTinDangPT.GetTinDang(id);
            List<string> imgList = new List<string>();
            imgList = callTinDangPT.ListImages(id);
            ViewBag.listImg = imgList.ToList();
            return View(tinDang);
        }

        public IActionResult QLTinDangPT()
        {
            IEnumerable<TinDang> tinDangs = callTinDangPT.ListTinDangPhongTro();
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
            TempData["AlertMessage"] = "successful";
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
    }
}

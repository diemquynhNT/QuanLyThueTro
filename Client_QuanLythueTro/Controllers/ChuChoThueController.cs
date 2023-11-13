using Client_QuanLythueTro.APIGateWay;
using Client_QuanLythueTro.Models;
using Microsoft.AspNetCore.Mvc;

namespace Client_QuanLythueTro.Controllers
{
    public class ChuChoThueController : Controller
    {
        private readonly TinDang_PhongTro_GateWay callTinDangPT;
        private readonly LichXemPhong_GateWay _callLichXemPhong;

        public ChuChoThueController(TinDang_PhongTro_GateWay callTinDangPT, LichXemPhong_GateWay callLichXemPhong)
        {
            this.callTinDangPT = callTinDangPT;
            _callLichXemPhong = callLichXemPhong;
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

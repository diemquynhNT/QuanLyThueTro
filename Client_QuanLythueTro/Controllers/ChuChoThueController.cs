using Client_QuanLythueTro.APIGateWay;
using Client_QuanLythueTro.Models;
using Microsoft.AspNetCore.Mvc;

namespace Client_QuanLythueTro.Controllers
{
    public class ChuChoThueController : Controller
    {
        private readonly TinDang_PhongTro_GateWay callTinDangPT;

        public ChuChoThueController(TinDang_PhongTro_GateWay callTinDangPT)
        {
            this.callTinDangPT = callTinDangPT;
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
                return RedirectToAction("CreateTinDangPT");
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
    }
}

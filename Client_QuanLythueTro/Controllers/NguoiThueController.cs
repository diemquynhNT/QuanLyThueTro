using Client_QuanLythueTro.APIGateWay;
using Client_QuanLythueTro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Client_QuanLythueTro.Controllers
{
    public class NguoiThueController : Controller
    {
        private readonly APIGateWayTinDang context;
        private readonly APIGateWayLichXemPhong apiLichXem;

        public NguoiThueController(APIGateWayTinDang _context, APIGateWayLichXemPhong callLichXemPhong)
        {
            context = _context;
            apiLichXem = callLichXemPhong;
        }

        public IActionResult QuanLyTinDang(string idUser)
        {
            IEnumerable<TinDang> tinDangs = context.ListTinDangByIdUser(idUser);
            TempData["idUser"] = idUser;
            return View(tinDangs);
        }
        [HttpPost]
        public IActionResult QuanLyTinDang(string idLoaiGoiDichVu,string trangThai,string idUser)
        {
            List<TinDang> listTin = new List<TinDang>();
            bool status;
            if (idLoaiGoiDichVu == "All" && trangThai == "All")
                listTin = context.ListTinDangByIdUser(idUser);
            else if(idLoaiGoiDichVu=="All" && trangThai!="All")
            {
                status = Boolean.Parse(trangThai);
                listTin = context.GetTinDangByStatus(idUser,status);
            }
            else if (idLoaiGoiDichVu != "All" && trangThai == "All")
            {
                listTin = context.GetTinDangByGoiDichVu(idUser, idLoaiGoiDichVu);
            }
            else
            {
                status = Boolean.Parse(trangThai);
                listTin = context.GetTinDangByGoiDichVuAndStatus(idUser,idLoaiGoiDichVu, status);
            }
                return View(listTin);
        }

        public ActionResult CreateTinDang()
        {
            TinDang tin = new TinDang();
            return View(tin);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateTinDang(TinDang tin,List<IFormFile> listimg)
        {
            try
            {
                TinDang newTin = context.CreateTinDang(tin);
                context.AddImagesAsync(newTin.idTinDang, listimg);
                TempData["mess"] = "thanhcong";
                return RedirectToAction("QuanLyTinDang", new { idUser = tin.idUser });
            }
            catch (Exception ex)
            {
                TempData["mess"] = "loi";
                return Content("Error: " + ex.Message);

            }
        }
        public ActionResult EditTinDang(string id)
        {
            TinDang tin = context.GetTin(id);
            return View(tin);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditTinDang(TinDang tin)
        {
            try
            {
                context.UpdateTin(tin);
                TempData["AlertMessage"] = "successful";
                return RedirectToAction("EditTinDang", new { idTinDang = tin.idTinDang });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        //[HttpPost]
        //public ActionResult DeleteTin(string id,string idUser)
        //{
        //    try
        //    {
        //        context.DeleteTin(id);
        //        return RedirectToAction("QuanLyTinDang", new { idUser = idUser });
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("", ex.Message);
        //        return View();
        //    }
        //}

        public IActionResult QuanLyLichXemPhong(string idTin)
        {
            IEnumerable<LichXemPhong> list = apiLichXem.GetListXemPhong(idTin);
            return View(list);
        }
        [HttpPost]
        public ActionResult SendLichXem(LichXemPhong lichxem)
        {
            try
            {
                lichxem.trangThai = false;
                lichxem.idLichXem = "auto";
                apiLichXem.CreateLichXemPhong(lichxem);
                TempData["thongbao"] = "sendLich";
                return RedirectToAction("DetailTinDangPT", "ChuChoThue", new { id = lichxem.idTinDang });

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        public IActionResult QuanLyTinYeuThichCaNhan(string idUser)
        {
            IEnumerable<TinDang> list = context.ListTinDangYeuThich(idUser);
            return View(list);
        }
        public IActionResult QuanLyLichXemCaNhan(string sdt)
        {
            IEnumerable<LichXemPhong> list = apiLichXem.GetListLichXemPhongUser(sdt);
            return View(list);
        }

    }
}

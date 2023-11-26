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
        private readonly APIGateWayUsers aPIGateWayUsers;
        private readonly LichXemPhong_GateWay _callLichXemPhong;


        public NguoiThueController(APIGateWayTinDang _context, APIGateWayLichXemPhong callLichXemPhong,LichXemPhong_GateWay callLichXem, APIGateWayUsers aPIGateWayUsers)
        {
            context = _context;
            apiLichXem = callLichXemPhong;
            _callLichXemPhong = callLichXem;
            this.aPIGateWayUsers = aPIGateWayUsers;
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
                context.CreateImage(newTin.idTinDang, listimg);
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
            TempData["IdTin"] = idTin;
            return View(list);
        }
        [HttpGet]
        public IActionResult CreateLichXem(string id)
        {
            var list = context.ListTinDangByIdUser(id).ToList();
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

        public IActionResult DetailLichXem(string idLichXem)
        {
            LichXemPhong lich = apiLichXem.GetLichXem(idLichXem);
           return View(lich);
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
        public IActionResult ThongTinCaNhan(string idUser)
        {
           Users u=aPIGateWayUsers.GetUser(idUser);
            return View(u);
        }
        [HttpPost]
        public IActionResult ThongTinCaNhan(Users u)
        {
            try
            {
                aPIGateWayUsers.UpdateUsers(u);
                TempData["thongbao"] = "thanhcong";
                return View();
            }
            catch
            {
                return View();
            }
        }

    }
}

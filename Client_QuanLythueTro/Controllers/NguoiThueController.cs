using Client_QuanLythueTro.APIGateWay;
using Client_QuanLythueTro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.ProjectModel;
using SmartBreadcrumbs.Attributes;
using SmartBreadcrumbs.Nodes;

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
            List<Breadcrumb> breadcrumbs = new List<Breadcrumb>
            {
                new Breadcrumb { Label = "Trang Chủ", Url = "/ChuChoThue/IndexTinDangPT" },
             
            };
            ViewBag.Breadcrumbs = breadcrumbs;
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
        public ActionResult CreateTinDang(string idUser)
        {
            
            List<Breadcrumb> breadcrumbs = new List<Breadcrumb>
            {
                new Breadcrumb { Label = "Trang Chủ", Url = "/ChuChoThue/IndexTinDangPT" },
               new Breadcrumb { Label = "Quản Lý Tin Cá Nhân", Url = Url.Action("QuanLyTinDang", "NguoiThue", new { idUser = idUser }) }

            };
            ViewBag.Breadcrumbs = breadcrumbs;
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
        public ActionResult EditTinDang(string id,string idUser)
        {
            List<Breadcrumb> breadcrumbs = new List<Breadcrumb>
            {
               new Breadcrumb { Label = "Trang Chủ", Url = "/ChuChoThue/IndexTinDangPT" },
               new Breadcrumb { Label = "Quản Lý Tin Cá Nhân", Url = Url.Action("QuanLyTinDang", "NguoiThue", new { idUser = idUser }) }

            };
            ViewBag.Breadcrumbs = breadcrumbs;
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
                TempData["AlertMessage"] = "thanhcong";
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

        public IActionResult QuanLyLichXemPhong(string idTin,string idUser)
        {
            IEnumerable<LichXemPhong> list = apiLichXem.GetListXemPhong(idTin);
            List<Breadcrumb> breadcrumbs = new List<Breadcrumb>
            {
               new Breadcrumb { Label = "Trang Chủ", Url = "/ChuChoThue/IndexTinDangPT" },
               new Breadcrumb { Label = "Quản Lý Tin Cá Nhân", Url = Url.Action("QuanLyTinDang", "NguoiThue", new { idUser = idUser }) }

            };
            ViewBag.Breadcrumbs = breadcrumbs;
            TempData["IdTin"] = idTin;
            return View(list);
        }
        [HttpGet]
        public IActionResult CreateLichXem(string id)
        {
            List<Breadcrumb> breadcrumbs = new List<Breadcrumb>
            {
               new Breadcrumb { Label = "Trang Chủ", Url = "/ChuChoThue/IndexTinDangPT" },
               new Breadcrumb { Label = "Quản Lý Tin Cá Nhân", Url = Url.Action("QuanLyTinDang", "NguoiThue", new { idUser = id }) }

            };
            ViewBag.Breadcrumbs = breadcrumbs;
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
            return RedirectToAction("QuanLyLichXemPhong", new { idTin = lichXemPhong.idTinDang });
        }

        public IActionResult DetailLichXem(string idLichXem, string idUser,string idTin)
        {
            List<Breadcrumb> breadcrumbs = new List<Breadcrumb>
            {
               new Breadcrumb { Label = "Trang Chủ", Url = "/ChuChoThue/IndexTinDangPT" },
               new Breadcrumb { Label = "Quản lý Tin Cá Nhân",Url = Url.Action("QuanLyTinDang", "NguoiThue", new { idUser = idUser }) },
               new Breadcrumb { Label = "Quản Lý Lịch Xem Phòng", Url = Url.Action("QuanLyLichXemPhong", "NguoiThue", new { idUser = idUser,idTin=idTin }) }

            };
            ViewBag.Breadcrumbs = breadcrumbs;
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
            List<Breadcrumb> breadcrumbs = new List<Breadcrumb>
            {
               new Breadcrumb { Label = "Trang Chủ", Url = "/ChuChoThue/IndexTinDangPT" },

            };
            ViewBag.Breadcrumbs = breadcrumbs;
            IEnumerable<TinDang> list = context.ListTinDangYeuThich(idUser);
            return View(list);
        }
        public IActionResult QuanLyLichXemCaNhan(string sdt)
        {
            List<Breadcrumb> breadcrumbs = new List<Breadcrumb>
            {
               new Breadcrumb { Label = "Trang Chủ", Url = "/ChuChoThue/IndexTinDangPT" },

            };
            ViewBag.Breadcrumbs = breadcrumbs;
            IEnumerable<LichXemPhong> list = apiLichXem.GetListLichXemPhongUser(sdt);
            return View(list);
        }
        public IActionResult ThongTinCaNhan(string idUser)
        {
            List<Breadcrumb> breadcrumbs = new List<Breadcrumb>
            {
               new Breadcrumb { Label = "Trang Chủ", Url = "/ChuChoThue/IndexTinDangPT" },

            };
            ViewBag.Breadcrumbs = breadcrumbs;
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

using Client_QuanLythueTro.APIGateWay;
using Client_QuanLythueTro.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text;

namespace Client_QuanLythueTro.Controllers
{
    public class AdminController : Controller
    {
        private readonly APIGateWayUsers apiGateWay;
        private readonly APIGateWayTinDang apiGateWayTinDang;

        public AdminController(APIGateWayUsers aPIGate, APIGateWayTinDang apiGateWayTinDang)
        {
            this.apiGateWay = aPIGate;
            this.apiGateWayTinDang = apiGateWayTinDang;
        }
       

        public ActionResult TrangChuAdmin()
        {
            List<Users> listNv = apiGateWay.ListUsers().Where(t=>t.trangThai==true && t.idLoaiTK!="NV").ToList();
            List<Users> listGuest = apiGateWay.ListUsers().Where(t => t.trangThai == true && t.idLoaiTK=="NV").ToList();
            TempData["soKH"] = listGuest;
            TempData["sonv"] = listNv;
            List<TinDang> listTin = apiGateWayTinDang.ListTinDang().ToList();

            return View(listTin);
        }
        public ActionResult QuanLyNhanVien()
        {

            List<Users> listEmloyee = apiGateWay.ListUsers().Where(t=> t.idLoaiTK == "NV").ToList();
            return View(listEmloyee);
        }
        [HttpPost]
        public ActionResult QuanLyNhanVien(string trangThai)
        {
            List<Users> list = new List<Users>();
            if(trangThai=="All")
                list = apiGateWay.ListUsers().Where(t => t.idLoaiTK == "NV").ToList();
            else if(trangThai=="Ngưng hoạt động")
                list = apiGateWay.ListUsers().Where(t=>t.idLoaiTK == "NV" && t.trangThai==false).ToList();
            else
                list = apiGateWay.ListUsers().Where(t =>t.idLoaiTK == "NV" && t.trangThai == true).ToList();

            return View(list);
        }

        
        public JsonResult ValidatePass(string pass1,string pass2)
        {
            System.Threading.Thread.Sleep(200);
            if (pass1 == pass2)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }

        }
        public ActionResult DetailUsers(string id)
        {
      
            Users users = apiGateWay.GetUser(id);
            return View(users);
        }

        public ActionResult CreateUser()
        {
   
            Users u = new Users();
            return View(u);
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(Users u,IFormFile Avatar)
        {
            try
            {
                
                u.Avatar = Avatar;
                apiGateWay.CreateUser(u);
                TempData["mess"] = "oke";
                return RedirectToAction("QuanLyNhanVien");
            }
            catch
            {
                TempData["mess"] = "loi";
                return View();
            }
        }
        // GET: AdminController/Edit/5
        public ActionResult EditProfile(string id)
        {
            Users users = apiGateWay.GetUser(id);
            return View(users);
        }
        // GET: AdminController/Edit/5
        public ActionResult EditUsers(string id)
        {

            Users users = apiGateWay.GetUser(id);
            return View(users);
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUsers(Users u)
        {
            try
            {
                apiGateWay.UpdateUsers(u);
                TempData["AlertMessage"] = "thanhcong";
                return RedirectToAction("EditUsers", new { idUser = u.idUser });
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult DeleteUsers(string id)
        {
            try
            {
                apiGateWay.DeleteUsers(id);
                return RedirectToAction("QuanLyNhanVien");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult ChangePassword(string id,string newPass)
        {
            try
            {
                apiGateWay.ChangePass(id,newPass);
                ViewBag.thongbao = "thanhcong";
                return RedirectToAction("EditUser/" + id);
            }
            catch
            {
                return RedirectToAction("EditUser/" + id);
            }
        }
        public ActionResult QuanLyKhachHang()
        {
            List<Users> listGuest = apiGateWay.ListUsers().Where(t => t.idLoaiTK != "NV").ToList();
            return View(listGuest);
        }
        [HttpPost]
        public ActionResult QuanLyKhachHang(string loai)
        {
            List<Users> list = new List<Users>();
            if (loai == "All")
                list = apiGateWay.ListUsers().Where(t => t.idLoaiTK != "NV").ToList();
            else
                list = apiGateWay.ListUsers().Where(t => t.idLoaiTK == loai ).ToList();
            return View(list);
        }
        public ActionResult CreateGuest()
        {

            Users u = new Users();
            return View(u);
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGuest(Users u, IFormFile Avatar)
        {
            try
            {

                u.Avatar = Avatar;
                apiGateWay.CreateUser(u);
                TempData["mess"] = "oke";
                return RedirectToAction("QuanLyNhanVien");
            }
            catch
            {
                TempData["mess"] = "loi";
                return View();
            }
        }
        public ActionResult EditGuest(string id)
        {

            Users users = apiGateWay.GetUser(id);
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditGuest(Users u)
        {
            try
            {
                apiGateWay.UpdateUsers(u);
                return RedirectToAction("QuanLyKhachHang");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult DetailGuest(string id)
        {
            Users users = apiGateWay.GetUser(id);
            return View(users);
        }

        public ActionResult ThongTinCongTy()
        {
            return View();
        }

      
    }
}

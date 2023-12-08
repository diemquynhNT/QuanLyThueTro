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
        public AdminController(APIGateWayUsers aPIGate)
        {
            this.apiGateWay = aPIGate;
        }
        
        public ActionResult TrangChuAdmin()
        {
            
            return View();
        }
        public ActionResult QuanLyNhanVien()
        {

            List<Users> listEmloyee = apiGateWay.ListEmployee();
            return View(listEmloyee);
        }

        public ActionResult QuanLyKhachHang()
        {

            List<Users> list = apiGateWay.ListGuest();
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
                return RedirectToAction("QuanLyNhanVien");
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

        public ActionResult EditGuest(string id)
        {

            Users users = apiGateWay.GetUser(id);
            return View(users);
        }

        // POST: AdminController/Edit/5
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

        public ActionResult ThongTinCT(string id)
        {
            return View();
        }
    }
}

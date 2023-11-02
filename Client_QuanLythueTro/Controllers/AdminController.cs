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
        public ActionResult CreateUser(Users u,IFormFile file)
        {
            try
            {
                
                u.Avatar = file;
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
    }
}

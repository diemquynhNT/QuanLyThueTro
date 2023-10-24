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
        private byte[] Base64UrlDecode(string input)
        {
            string base64 = input.Replace('-', '+').Replace('_', '/');
            while (base64.Length % 4 != 0)
            {
                base64 += '=';
            }
            return Convert.FromBase64String(base64);
        }
        public string GetIdUser()
        {
            string token = Request.Cookies["access_token"];
            var tokenParts = token.Split('.');
            var encodedPayload = tokenParts[1];
            var decodedPayload = Base64UrlDecode(encodedPayload);
            var decodedPayloadString = Encoding.UTF8.GetString(decodedPayload);
            var payloadObject = JObject.Parse(decodedPayloadString);
            var img = payloadObject["ImageUrl"]?.Value<string>();
            return img;
        }
        // GET: AdminController
        public ActionResult TrangChuAdmin()
        {
            ViewBag.UserImageUrl = GetIdUser();
            return View();
        }
        public ActionResult QuanLyNhanVien()
        {
            List<Users> listEmloyee = apiGateWay.ListEmployee();
            return View(listEmloyee);
        }

        public ActionResult DetailUsers(string id)
        {
            ViewBag.UserImageUrl = GetIdUser();
            Users users = apiGateWay.GetUser(id);
            return View(users);
        }

        public ActionResult CreateUser()
        {
            ViewBag.UserImageUrl = GetIdUser();
            Users u = new Users();
            return View(u);
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(Users u)
        {
            try
            {
                apiGateWay.CreateUser(u);
                return RedirectToAction("QuanLyNhanVien");
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Edit/5
        public ActionResult EditUsers(string id)
        {
            ViewBag.UserImageUrl = GetIdUser();
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

using Client_QuanLythueTro.APIGateWay;
using Client_QuanLythueTro.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Client_QuanLythueTro.Controllers
{
    public class NVKDController : Controller
    {
        private readonly APIGateWayTinDang apiGateWay;
        public NVKDController(APIGateWayTinDang aPIGate)
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
        // GET: NVKDController
        public ActionResult TrangChu()
        {
            ViewBag.UserImageUrl = GetIdUser();
            List<TinDang> listTin = apiGateWay.ListTinDang();
            ViewBag.list = listTin;
            return View();
        }

        // GET: NVKDController
        public ActionResult QuanLyTinDang()
        {
            List<TinDang> listTin = apiGateWay.ListTinDang();
            return View(listTin);
        }

        // GET: NVKDController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult CreateTinDang()
        {
            TinDang tin = new TinDang();
            return View(tin);
        }

        // POST: NVKDController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTinDang(TinDang tin)
        {
            try
            {
                apiGateWay.CreateTinDang(tin);
                return RedirectToAction("QuanLyTinDang");
            }
            catch
            {
                return View();
            }
        }

        // GET: NVKDController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NVKDController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NVKDController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NVKDController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

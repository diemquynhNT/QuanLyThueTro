using Client_QuanLythueTro.APIGateWay;
using Client_QuanLythueTro.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Client_QuanLythueTro.Controllers
{
    public class LoginController : Controller
    {
        private readonly APIGateWayUsers apiGateWay;
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;

        public LoginController(HttpClient httpClient, IJSRuntime jsRuntime, APIGateWayUsers aPIGate)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
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
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var data = new
            {
                userName = username,
                password = password
            };

            var response = await _httpClient.PostAsJsonAsync("https://localhost:7034/api/Login/LoginEmployee", data);
            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                
                var tokenParts = token.Split('.');
                var encodedPayload = tokenParts[1];
                var decodedPayload = Base64UrlDecode(encodedPayload);
                var decodedPayloadString = Encoding.UTF8.GetString(decodedPayload);
                var payloadObject = JObject.Parse(decodedPayloadString);
                var id = payloadObject["idUser"]?.Value<string>();
                Users u = apiGateWay.GetUser(id);
                var userJson = JsonConvert.SerializeObject(u);
                HttpContext.Session.SetString("user", userJson);

                var role = payloadObject["roles"]?.Value<string>();
                TempData["error"] = "thanhcong";
                if (role == "NVKD")
                {
                    return RedirectToAction("TrangChu", "NVKD");
                }    
                else if (role == "admin")
                    return RedirectToAction("TrangChuAdmin", "Admin");
               
                return View();
              
            }
            else
            {
                TempData["error"] = "loginfailed";
                return View("Login");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        public IActionResult LogoutCustomer()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LoginCustomer");
        }

        public IActionResult LoginCustomer()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginCustomer(string username, string password)
        {
            var data = new
            {
                userName = username,
                password = password
            };

            var response = await _httpClient.PostAsJsonAsync("https://localhost:7034/api/Login/LoginGuest", data);
            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();

                var tokenParts = token.Split('.');
                var encodedPayload = tokenParts[1];
                var decodedPayload = Base64UrlDecode(encodedPayload);
                var decodedPayloadString = Encoding.UTF8.GetString(decodedPayload);
                var payloadObject = JObject.Parse(decodedPayloadString);
                var id = payloadObject["idUser"]?.Value<string>();

                Users u = apiGateWay.GetUser(id);
                var userJson = JsonConvert.SerializeObject(u);
                HttpContext.Session.SetString("user", userJson);
                return RedirectToAction("IndexTinDangPT", "ChuChoThue");


                //    return View();

            }
            else
            {
                TempData["error"] = "loginfailed";
                return View("LoginCustomer");
            }
        }
        public IActionResult Register()
        {
            Users u = new Users();
            return View(u);
        }
        [HttpPost]
        public IActionResult Register(Users u,IFormFile hinh)
        {
            u.Avatar = hinh;
            u.idLoaiTK = "BT";
            u.ngayThamGia = DateTime.Today;
            apiGateWay.CreateUser(u);
            TempData["error"] = "dangkithanhcong";
            return View("LoginCustomer");
        }
        public IActionResult QuenMatKhau()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using Newtonsoft.Json.Linq;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Client_QuanLythueTro.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;

        public LoginController(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;

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

            var response = await _httpClient.PostAsJsonAsync("https://localhost:7034/api/Login/Login", data);
            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                // Lưu token vào cookie
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.UtcNow.AddDays(1),
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict,
                    Secure = true // Đảm bảo sử dụng giao thức HTTPS để sử dụng cookie này
                };

                Response.Cookies.Append("access_token", token, cookieOptions);

                var tokenParts = token.Split('.');
                var encodedPayload = tokenParts[1];
                var decodedPayload = Base64UrlDecode(encodedPayload);
                var decodedPayloadString = Encoding.UTF8.GetString(decodedPayload);
                var payloadObject = JObject.Parse(decodedPayloadString);
                var role = payloadObject["roles"]?.Value<string>();
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
                TempData["error"] = "An error occurred";
                return View();
            }
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("access_token");

            return RedirectToAction("Login");
        }

    }
}

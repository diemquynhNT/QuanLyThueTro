using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
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

        public LoginController(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
                var tokenParts = token.Split('.');

                var encodedPayload = tokenParts[1];
                var decodedPayload = Base64UrlDecode(encodedPayload);

                // Convert the decoded payload to a string
                var decodedPayloadString = Encoding.UTF8.GetString(decodedPayload);

                var payloadObject = JObject.Parse(decodedPayloadString);

                // Access the role claim
                var role = payloadObject["roles"]?.Value<string>();

                if (role == "admin")
                {
                    return RedirectToAction("TrangChu", "NVKD");
                }

                if (role == "NVKD")
                {
                    return RedirectToAction("NVKDPage");
                }

                // Chuyển hướng mặc định nếu role không xác định
                return RedirectToAction("TrangChu", "NVKD");
            }
            else
            {
                // Xử lý lỗi
                TempData["Error"] = "An error occurred";
                return View();
            }
        }

    }
}

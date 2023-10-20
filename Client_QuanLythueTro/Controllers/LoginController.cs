using Microsoft.AspNetCore.Mvc;

namespace Client_QuanLythueTro.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}

using Client_QuanLythueTro.APIGateWay;
using Client_QuanLythueTro.Models;
using Microsoft.AspNetCore.Mvc;

namespace Client_QuanLythueTro.Controllers
{
    public class ChuChoThueController : Controller
    {
        private readonly APIGateWayTinDang aPIGateWayTinDang;

        public ChuChoThueController(APIGateWayTinDang aPIGateWayTinDang)
        {
            this.aPIGateWayTinDang = aPIGateWayTinDang;
        }

        public IActionResult Index()
        {
            List<TinDang> listTin = aPIGateWayTinDang.ListTinDang();
            return View(listTin);
        }
    }
}

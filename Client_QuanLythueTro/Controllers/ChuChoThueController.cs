using Client_QuanLythueTro.APIGateWay;
using Client_QuanLythueTro.Models;
using Microsoft.AspNetCore.Mvc;

namespace Client_QuanLythueTro.Controllers
{
    public class ChuChoThueController : Controller
    {
        private readonly TinDang_PhongTro_GateWay callTinDangPT;

        public ChuChoThueController(TinDang_PhongTro_GateWay callTinDangPT)
        {
            this.callTinDangPT = callTinDangPT;
        }

        public IActionResult IndexTinDangPT()
        {
            List<TinDang> listTin = callTinDangPT.ListTinDangPhongTro();
            return View(listTin);
        }

        public IActionResult DetailTinDangPT(string id)
        {
            //TinDang tinDang = callTinDangPT.GetTinDang(id);
            //return View(tinDang);
            return View();
        }
    }
}

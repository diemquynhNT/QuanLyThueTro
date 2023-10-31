using Client_QuanLythueTro.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;

namespace Client_QuanLythueTro.APIGateWay
{
    public class TinDang_PhongTro_GateWay
    {
        private HttpClient _httpClient;
        Uri baseAddress = new Uri("https://localhost:7034/api/TinDangPhongTro");
        public TinDang_PhongTro_GateWay()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }

        public List<TinDang> ListTinDangPhongTro()
        {
            List<TinDang> tinDangPTList = new List<TinDang>();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress).Result;

            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                tinDangPTList = JsonConvert.DeserializeObject<List<TinDang>>(data);
            }
            return tinDangPTList;
        }

        public TinDang GetTinDang(string id)
        {
            TinDang tinDang = new TinDang();
            _httpClient.BaseAddress = new Uri(baseAddress + "/" + id);
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                tinDang = JsonConvert.DeserializeObject<TinDang>(data);
            }
            return tinDang;
        }
    }
}

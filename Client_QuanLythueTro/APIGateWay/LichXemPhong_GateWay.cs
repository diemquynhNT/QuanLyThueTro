using Client_QuanLythueTro.Models;
using Newtonsoft.Json;

namespace Client_QuanLythueTro.APIGateWay
{
    public class LichXemPhong_GateWay
    {
        private HttpClient _httpClient;
        Uri baseAddress = new Uri("https://localhost:7034/api/LichXemPhongs");
        public LichXemPhong_GateWay()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }

        public List<LichXemPhong> ListLichXemPhong()
        {
            List<LichXemPhong> lichXemPTList = new List<LichXemPhong>();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                lichXemPTList = JsonConvert.DeserializeObject<List<LichXemPhong>>(data);
            }
            return lichXemPTList;
        }
    }
}

using Client_QuanLythueTro.Models;
using Newtonsoft.Json;
using System.Text;

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

        public LichXemPhong CreateLichXem(LichXemPhong lichXemPhong)
        {
            try
            {
                string data = JsonConvert.SerializeObject(lichXemPhong);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress, content).Result;
                if (!response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception(result);
                }
                string result2 = response.Content.ReadAsStringAsync().Result;
                var datacol = JsonConvert.DeserializeObject<LichXemPhong>(result2);

                if (datacol.idLichXem != null)
                    lichXemPhong = datacol;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return lichXemPhong;
        }
    }
}

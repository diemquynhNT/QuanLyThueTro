using Client_QuanLythueTro.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace Client_QuanLythueTro.APIGateWay
{
    public class GiaoDich_Gateway
    {
        private HttpClient _httpClient;
        Uri baseAddress = new Uri("https://localhost:7034/api/GiaoDiches");

        public GiaoDich_Gateway()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }

        public List<GiaoDich> ListGiaoDich()
        {
            List<GiaoDich> gdList = new List<GiaoDich>();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                gdList = JsonConvert.DeserializeObject<List<GiaoDich>>(data);
            }
            return gdList;
        }

        public List<GiaoDich> GetGiaoDich(string id)
        {
            List<GiaoDich> gdList = new List<GiaoDich>();
            _httpClient.BaseAddress = new Uri(baseAddress + "/" + id);
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                gdList = JsonConvert.DeserializeObject<List<GiaoDich>>(data);
            }
            return gdList;
        }

        public GiaoDich CreateGiaoDich(GiaoDich giaodich)
        {
            try
            {
                string data = JsonConvert.SerializeObject(giaodich);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress, content).Result;
                if (!response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception(result);
                }
                string result2 = response.Content.ReadAsStringAsync().Result;
                var datacol = JsonConvert.DeserializeObject<GiaoDich>(result2);

                if (datacol.idGiaoDich != null)
                    giaodich = datacol;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return giaodich;
        }
    }
}

using Client_QuanLythueTro.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Client_QuanLythueTro.APIGateWay
{
    public class APIGateWayKhuVuc
    {
        private string urlKhuVuc = "https://localhost:7034/api/KhuVucs";
        private string urlThanhPho = "https://localhost:7034/api/KhuVucs";
        private HttpClient httpClient = new HttpClient();

        public List<Khuvucs> ListKhuvuc()
        {
            List<Khuvucs> list = new List<Khuvucs>();
            if (urlKhuVuc.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(urlKhuVuc).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var datacol = JsonConvert.DeserializeObject<List<Khuvucs>>(result);
                    if (datacol != null)
                        list = datacol;
                }
                else
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("loi" + ex.Message);
            }
            finally { }
            return list;
        }

    }
}

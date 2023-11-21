using Client_QuanLythueTro.Models;
using Newtonsoft.Json;
using System.Net;

namespace Client_QuanLythueTro.APIGateWay
{
    public class APIGateWayLichXemPhong
    {
        private string url = "https://localhost:7034/api/LichXemPhongs";
        private HttpClient httpClient = new HttpClient();

        //goi tin
        public List<LichXemPhong> GetListXemPhong(string id)
        {
            List<LichXemPhong> list = new List<LichXemPhong>();
            url = url + "/GetLichXemByIdTin?id=" + id;
            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var datacol = JsonConvert.DeserializeObject<List<LichXemPhong>>(result);

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

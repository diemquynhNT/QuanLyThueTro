using Client_QuanLythueTro.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;

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
        public List<LichXemPhong> GetListLichXemPhongUser(string sdt)
        {
            List<LichXemPhong> list = new List<LichXemPhong>();
            url = url + "/GetLichXemByIdUser/" + sdt;
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
        public int CountLichXem(string idTin)
        {
            int count = 0 ;
            url = url + "/Count/" + idTin;
            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var datacol = JsonConvert.DeserializeObject<int>(result);
                    if (datacol != null || datacol!=0)
                        count = datacol;
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
            return count;
        }
        public LichXemPhong CreateLichXemPhong(LichXemPhong lich)
        {
            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            try
            {
                string json = JsonConvert.SerializeObject(lich);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = httpClient.PostAsync(url, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<LichXemPhong>(result);
                    if (data != null)
                        lich = data;
                }
                else
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return lich;

        }
        public LichXemPhong GetLichXem(string id)
        {
            LichXemPhong lich = new LichXemPhong();
            url = url + "/" + id;
            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var datacol = JsonConvert.DeserializeObject<LichXemPhong>(result);

                    if (datacol != null)
                        lich = datacol;
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
            return lich;
        }

    }
}

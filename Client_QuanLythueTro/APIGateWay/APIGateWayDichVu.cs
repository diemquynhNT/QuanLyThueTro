using Client_QuanLythueTro.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Client_QuanLythueTro.APIGateWay
{
    public class APIGateWayDichVu
    {
        private string urlGoiTin = "https://localhost:7034/api/DichVuDangTins";
        private string urlTaiKhoan = "https://localhost:7034/api/LoaiTaiKhoans";
        private HttpClient httpClient = new HttpClient();

        public List<DichVuDangTin> ListGoiTin()
        {
            List<DichVuDangTin> listTin = new List<DichVuDangTin>();
            if (urlGoiTin.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(urlGoiTin).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var datacol = JsonConvert.DeserializeObject<List<DichVuDangTin>>(result);

                    if (datacol != null)
                        listTin = datacol;
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
            return listTin;
        }

        //create
        public DichVuDangTin CreateGoiTin(DichVuDangTin goitin)
        {

            if (urlGoiTin.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            try
            {
                string json = JsonConvert.SerializeObject(goitin);

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = httpClient.PostAsync(urlGoiTin, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<DichVuDangTin>(result);

                    if (data != null)
                        goitin = data;
                }
                else
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }

            return goitin;
        }



        public DichVuDangTin GetGoiTin(string id)
        {
            DichVuDangTin tin = new DichVuDangTin();
            urlGoiTin = urlGoiTin + "/" + id;
            if (urlGoiTin.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(urlGoiTin).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var datacol = JsonConvert.DeserializeObject<DichVuDangTin>(result);

                    if (datacol != null)
                        tin = datacol;
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
            return tin;
        }

        // update
        public DichVuDangTin UpdateDichVuDangTin(DichVuDangTin tin)
        {

            if (urlGoiTin.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string id = tin.idDichVu;
            urlGoiTin = urlGoiTin + "/" + id;
            string json = JsonConvert.SerializeObject(tin);

            try
            {
                HttpResponseMessage response = httpClient.PutAsync(urlGoiTin, new StringContent(json, Encoding.UTF8, "application/json")).Result;
                if (!response.IsSuccessStatusCode)
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
            return tin;
        }
        //delete
        public void DeleteTin(string id)
        {
            try
            {
                if (urlGoiTin.Trim().Substring(0, 5).ToLower() == "https")
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                string deleteUrl = $"{urlGoiTin}/{id}";

                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(deleteUrl),
                    Method = HttpMethod.Delete
                };

                var response = httpClient.SendAsync(request).Result;

                if (!response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception(result);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }
        
    }
}

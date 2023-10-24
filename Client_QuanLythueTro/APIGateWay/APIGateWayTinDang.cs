using Client_QuanLythueTro.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Client_QuanLythueTro.APIGateWay
{
    public class APIGateWayTinDang
    {
        private string url = "https://localhost:7034/api/TinDang";
        private HttpClient httpClient = new HttpClient();

        public List<TinDang> ListTinDang()
        {
            List<TinDang> listTin = new List<TinDang>();
            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var datacol = JsonConvert.DeserializeObject<List<TinDang>>(result);

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
        public void CreateTin(TinDang ls)
        {

            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            try
            {
                // Chuyển đối tượng LoaiSach thành định dạng JSON
                string json = JsonConvert.SerializeObject(ls);

                // Tạo nội dung yêu cầu HTTP
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Gửi yêu cầu POST đến URL của API để tạo LoaiSach mới
                HttpResponseMessage response = httpClient.PostAsync(url, content).Result;

                if (!response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception(result);
                }
                //if (response.IsSuccessStatusCode)
                //{
                //    // Đọc dữ liệu trả về và chuyển đổi thành đối tượng LoaiSach
                //    string result = response.Content.ReadAsStringAsync().Result;
                //    var data = JsonConvert.DeserializeObject<TinDang>(result);

                //    if (data != null)
                //        ls = data;
                //}
                //else
                //{
                //    // Xử lý khi có lỗi xảy ra trong quá trình gọi API
                //    string result = response.Content.ReadAsStringAsync().Result;
                //    throw new Exception(result);
                //}
            }
            catch (Exception ex)
            {
                // Xử lý lỗi chung
                throw new Exception("Lỗi: " + ex.Message);
            }

        }



        public TinDang GetTin(string id)
        {
            TinDang tin = new TinDang();
            url = url + "/" + id;
            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var datacol = JsonConvert.DeserializeObject<TinDang>(result);

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
        public TinDang UpdateTin(TinDang tin)
        {

            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string id = tin.idTinDang;
            url = url + "/" + id;
            string json = JsonConvert.SerializeObject(tin);

            try
            {
                HttpResponseMessage response = httpClient.PutAsync(url, new StringContent(json, Encoding.UTF8, "application/json")).Result;
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
                if (url.Trim().Substring(0, 5).ToLower() == "https")
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                string deleteUrl = $"{url}/{id}";

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
        public void DuyetTin(string id, bool status)
        {
            try
            {
                var requestUrl = url + "/DuyetTin?id=" + id + "&status=" + status;

                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(requestUrl),
                    Method = HttpMethod.Post
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

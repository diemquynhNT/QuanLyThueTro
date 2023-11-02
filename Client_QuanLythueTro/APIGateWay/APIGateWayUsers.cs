using Client_QuanLythueTro.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Client_QuanLythueTro.APIGateWay
{
    public class APIGateWayUsers
    {
        private string url = "https://localhost:7034/api/Users";
        private HttpClient httpClient = new HttpClient();

        public List<Users> ListEmployee()
        {
            List<Users> listEmployee = new List<Users>();
            url = url + "/GetEmployee";

            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var datacol = JsonConvert.DeserializeObject<List<Users>>(result);

                    if (datacol != null)
                        listEmployee = datacol;
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
            return listEmployee;
        }

        //create
        public Users CreateUser(Users u)
        {

            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            try
            {
                //MultipartFormDataContent
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(u.hoTen), "hoTen");
                content.Add(new StringContent(u.emailUser), "emailUser");
                content.Add(new StringContent(u.sdtUsers), "sdtUsers");
                content.Add(new StringContent(u.ngayThamGia.ToString()), "ngayThamGia");
                content.Add(new StringContent(u.userName), "userName");
                content.Add(new StringContent(u.passwordUser), "passwordUser");
                content.Add(new StringContent(u.gioiTinh), "gioiTinh");
                content.Add(new StringContent(u.idChucVu), "idChucVu");
                content.Add(new StringContent(u.idLoaiTK), "idLoaiTK");

                if (u.Avatar != null && u.Avatar.Length > 0)
                {
                    var streamContent = new StreamContent(u.Avatar.OpenReadStream());
                    content.Add(streamContent, "imge", u.Avatar.FileName);
                }
                HttpClient httpClient = new HttpClient();
                var response = httpClient.PostAsync(url, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<Users>(result);

                    if (data != null)
                        u = data;
                }
                else
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception(result);
                }
                return u;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }



        public Users GetUser(string id)
        {
            Users tin = new Users();
            url = url + "/" + id;
            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var datacol = JsonConvert.DeserializeObject<Users>(result);

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
        //public async Task<string> GetImageUser(string idUser)
        //{
        //    string img;
        //    string url = "https://localhost:7034/api/Users/GetImage?id=" + idUser;

        //    if (url.Trim().Substring(0, 5).ToLower() == "https")
        //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        //    try
        //    {
                
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Lỗi: " + ex.Message);
        //    }

        //    return img;
        //}


        // 
        public Users UpdateUsers(Users tin)
        {

            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string id = tin.idUser;
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
        public void DeleteUsers(string id)
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
    }
}

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

        public List<Users> ListUsers()
        {
            List<Users> list = new List<Users>();
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
        //public List<Users> ListEmployee()
        //{
        //    List<Users> listEmployee = new List<Users>();
        //    url = url + "/GetEmployee";

        //    if (url.Trim().Substring(0, 5).ToLower() == "https")
        //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //    try
        //    {
        //        HttpResponseMessage response = httpClient.GetAsync(url).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            string result = response.Content.ReadAsStringAsync().Result;
        //            var datacol = JsonConvert.DeserializeObject<List<Users>>(result);

        //            if (datacol != null)
        //                listEmployee = datacol;
        //        }
        //        else
        //        {
        //            string result = response.Content.ReadAsStringAsync().Result;
        //            throw new Exception(result);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("loi" + ex.Message);
        //    }
        //    finally { }
        //    return listEmployee;
        //}

        //public List<Users> ListGuest()
        //{
        //    List<Users> listEmployee = new List<Users>();
        //    url = url + "/GetGuest";

        //    if (url.Trim().Substring(0, 5).ToLower() == "https")
        //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //    try
        //    {
        //        HttpResponseMessage response = httpClient.GetAsync(url).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            string result = response.Content.ReadAsStringAsync().Result;
        //            var datacol = JsonConvert.DeserializeObject<List<Users>>(result);

        //            if (datacol != null)
        //                listEmployee = datacol;
        //        }
        //        else
        //        {
        //            string result = response.Content.ReadAsStringAsync().Result;
        //            throw new Exception(result);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("loi" + ex.Message);
        //    }
        //    finally { }
        //    return listEmployee;
        //}

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

        // 
        public Users UpdateUsers(Users u)
        {

            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string id = u.idUser;
            url = url + "/" + id;
            //MultipartFormDataContent
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(u.hoTen), "hoTen");
            content.Add(new StringContent(u.emailUser), "emailUser");
            content.Add(new StringContent(u.sdtUsers), "sdtUsers");
            content.Add(new StringContent(u.ngayThamGia.ToString()), "ngayThamGia");
            content.Add(new StringContent(u.userName), "userName");
            content.Add(new StringContent(u.gioiTinh), "gioiTinh");
            content.Add(new StringContent(u.idChucVu), "idChucVu");
            content.Add(new StringContent(u.idLoaiTK), "idLoaiTK");


            HttpClient httpClient = new HttpClient();
            var response = httpClient.PutAsync(url, content).Result;
            try
            {
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
            return u;
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

        public void ChangePass(string id, string pass)
        {
            try
            {
                if (url.Trim().Substring(0, 5).ToLower() == "https")
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                string newUrl = $"https://localhost:7034/api/Users/ChangePass?id={id}&pass={pass}";

                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(newUrl),
                    Method = HttpMethod.Put
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
        public void ChangeImg(string id, IFormFile file)
        {
            string urlChange = "https://localhost:7034/api/Users/ChangeImage?id=" + id;
            if (urlChange.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            try
            {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(id), "id");
                if (file != null && file.Length > 0)
                {
                    var streamContent = new StreamContent(file.OpenReadStream());
                    content.Add(streamContent, "file", file.FileName);
                }
                else
                {
                    throw new Exception("No file selected");
                }
                HttpClient httpClient = new HttpClient();
                var response = httpClient.PutAsync(urlChange, content).Result;

                if (!response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

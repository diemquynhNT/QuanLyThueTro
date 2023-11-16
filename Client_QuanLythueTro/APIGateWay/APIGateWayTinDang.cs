using Client_QuanLythueTro.Models;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using static System.Net.WebRequestMethods;
using System.Net.Http;

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
        public List<Khuvucs> ListKhuVuc()
        {
            List<Khuvucs> listTin = new List<Khuvucs>();
            url= "https://localhost:7034/api/KhuVucs";
            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var datacol = JsonConvert.DeserializeObject<List<Khuvucs>>(result);

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


        public List<TinDang> FilterTin(int thang,bool status)
        {
            List<TinDang> listTin = new List<TinDang>();
            url = url + "/Filter";
            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                string apiUrlWithParams = url + "?thang=" + thang + "&status=" + status;

                HttpResponseMessage response = httpClient.GetAsync(apiUrlWithParams).Result;
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

        //public async Task CreateTinAsync(TinDang tin)
        //{
        //    if (url.Trim().Substring(0, 5).ToLower() == "https")
        //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        //    try
        //    {
        //        var content = new MultipartFormDataContent();
        //        content.Add(new StringContent(tin.tieuDe), "tieuDe");
        //        content.Add(new StringContent(tin.loaiTin), "loaiTin");
        //        content.Add(new StringContent(tin.sdtNguoiLienHe), "sdtNguoiLienHe");
        //        content.Add(new StringContent(tin.nguoiLienHe), "nguoiLienHe");
        //        content.Add(new StringContent(tin.doiTuongChoThue), "doiTuongChoThue");
        //        content.Add(new StringContent(tin.soLuongPhong.ToString()), "soLuongPhong");
        //        content.Add(new StringContent(tin.idKhuVuc), "idKhuVuc");
        //        content.Add(new StringContent(tin.diaChi), "diaChi");
        //        content.Add(new StringContent(tin.giaPhong.ToString()), "giaPhong");
        //        content.Add(new StringContent(tin.dienTich.ToString()), "dienTich");
        //        content.Add(new StringContent(tin.moTa), "moTa");
        //        content.Add(new StringContent(tin.tienDien.ToString()), "tienDien");
        //        content.Add(new StringContent(tin.tienNuoc.ToString()), "tienNuoc");
        //        content.Add(new StringContent(tin.tienDichVu.ToString()), "tienDichVu");
        //        content.Add(new StringContent(tin.soLuongPhong.ToString()), "soLuongPhong");
        //        foreach (var item in tin.listimg)
        //        {
        //            var streamContent = new StreamContent(item.OpenReadStream());
        //            streamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
        //            {
        //                Name = "listimg",
        //                FileName = item.FileName
        //            };
        //            content.Add(streamContent);
        //        }

        //        HttpResponseMessage response = await httpClient.PostAsync(url, content);

        //        if (!response.IsSuccessStatusCode)
        //        {
        //            string result =await response.Content.ReadAsStringAsync();
        //            throw new Exception(result);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Lỗi: " + ex.Message);
        //    }
        //}

        public async Task<TinDang> CreateTin(TinDang tin)
        {
            TinDang newtin = new TinDang();
            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            try
            {
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                };

                string tinJson = System.Text.Json.JsonSerializer.Serialize(tin, options);
                var content = new StringContent(tinJson, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    throw new Exception(result);
                }else
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<TinDang>(result);

                    if (data != null)
                        newtin = data;
                }    
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
            return newtin;

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

        public async Task AddImg(string id, IFormFile img)
        {
            try
            {
                url = url + "/SaveImages";
                using (var httpClient = new HttpClient())
                {
                    using (var form = new MultipartFormDataContent())
                    {
                        form.Add(new StringContent(id), "tinId");
                        form.Add(new StreamContent(img.OpenReadStream()), "file", img.FileName);

                        var response = await httpClient.PostAsync(url, form);
                        response.EnsureSuccessStatusCode();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }




    }
}

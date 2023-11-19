using Client_QuanLythueTro.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Client_QuanLythueTro.APIGateWay
{
    public class TinDang_PhongTro_GateWay
    {
        private HttpClient _httpClient;
        Uri baseAddress = new Uri("https://localhost:7034/api/TinDangPhongTro");
        public TinDang_PhongTro_GateWay()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }

        public List<TinDang> ListTinDangPhongTro()
        {
            List<TinDang> tinDangPTList = new List<TinDang>();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress).Result;

            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                tinDangPTList = JsonConvert.DeserializeObject<List<TinDang>>(data);
            }
            return tinDangPTList;
        }

        public List<TinDang> ListTinDangPTByUser(string iduser)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseAddress + "/GetByIdUser/" + iduser);
            List<TinDang> tinDangPTList = new List<TinDang>();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                tinDangPTList = JsonConvert.DeserializeObject<List<TinDang>>(data);
            }
            return tinDangPTList;
        }

        public TinDang GetTinDang(string id)
        {
            TinDang tinDang = new TinDang();
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseAddress + "/" + id);
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                tinDang = JsonConvert.DeserializeObject<TinDang>(data);
            }
            return tinDang;
        }

        public TinDang CreateTinDang(TinDang tinDang)
        {
            try
            {
                string data = JsonConvert.SerializeObject(tinDang);
                StringContent content = new StringContent( data, Encoding.UTF8, "application/json" );
                HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress, content ).Result;
                if (!response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception(result);
                }
                string result2 = response.Content.ReadAsStringAsync().Result;
                var datacol = JsonConvert.DeserializeObject<TinDang>(result2);

                if (datacol.idTinDang != null)
                    tinDang = datacol;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return tinDang;
        }


        public TinDang EditTinDang(string id, TinDang tinDang)
        {
            try
            {
                string data = JsonConvert.SerializeObject(tinDang);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                _httpClient.BaseAddress = new Uri(baseAddress + "/" + id);
                HttpResponseMessage response = _httpClient.PutAsync(_httpClient.BaseAddress, content).Result;
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
            return tinDang;
        }

        public void DeleteTinDang(string id)
        {
            try
            {
                _httpClient.BaseAddress = new Uri(baseAddress + "/" + id);
                HttpResponseMessage response = _httpClient.DeleteAsync(_httpClient.BaseAddress).Result;
                if (!response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("Error Occured at the API Endpoint, Error Info. " + result);
                }
            }
            catch
            {
                throw;
            }
            finally { }
            return;
        }

        public void CreateImage(string idTinDang, IFormFileCollection files)
        {
            try
            {
                _httpClient = new HttpClient();
                _httpClient.BaseAddress = new Uri(baseAddress + "/AddImages/" + idTinDang);
                if(files != null)
                {
                    
                }var formData = new MultipartFormDataContent();
                    foreach (var file in files)
                    {
                        
                        //formData.Add(new StringContent(idTinDang), "tinDangId");
                        formData.Add(new StreamContent(file.OpenReadStream()), "files", file.FileName);
                        
                    }
                        HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress, formData).Result;
                        if (!response.IsSuccessStatusCode)
                        {
                            string result = response.Content.ReadAsStringAsync().Result;
                            throw new Exception(result);
                        }
                //string data = JsonConvert.SerializeObject(idTinDang, file);
                //StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            //return tinDang;
        }

        public List<string> ListImages(string idTinDang)
        {
            List<string> imgList = new List<string>();
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseAddress + "/GetImages/" + idTinDang);
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                imgList = JsonConvert.DeserializeObject<List<string>>(data);
            }
            return imgList;
        }
    }
}

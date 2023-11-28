using Client_QuanLythueTro.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Security.Policy;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Client_QuanLythueTro.APIGateWay
{
    public class LichXemPhong_GateWay
    {
        private HttpClient _httpClient;
        Uri baseAddress = new Uri("https://localhost:7034/api/LichXemPhongs");
        public LichXemPhong_GateWay()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }

        public List<LichXemPhong> ListLichXemPhong()
        {
            List<LichXemPhong> lichXemPTList = new List<LichXemPhong>();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                lichXemPTList = JsonConvert.DeserializeObject<List<LichXemPhong>>(data);
            }
            return lichXemPTList;
        }

        public LichXemPhong GetLichXem(string id)
        {
            LichXemPhong lichXem = new LichXemPhong();
            _httpClient.BaseAddress = new Uri(baseAddress + "/" + id);
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                lichXem = JsonConvert.DeserializeObject<LichXemPhong>(data);
            }
            return lichXem;
        }

        public List<LichXemPhong> GetLichXemByIdTinDang(string id)
        {
            _httpClient = new HttpClient();
            List<LichXemPhong> lichXem = new List<LichXemPhong>();
            _httpClient.BaseAddress = new Uri(baseAddress + "/GetByIdTinDang/" + id);
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress).Result;

            if (response.IsSuccessStatusCode && response!=null)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                lichXem = JsonConvert.DeserializeObject<List<LichXemPhong>>(data);
            }
            return lichXem;
        }

        public LichXemPhong CreateLichXem(LichXemPhong lichXemPhong)
        {
            try
            {
                string data = JsonConvert.SerializeObject(lichXemPhong);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress, content).Result;
                if (!response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception(result);
                }
                string result2 = response.Content.ReadAsStringAsync().Result;
                var datacol = JsonConvert.DeserializeObject<LichXemPhong>(result2);

                if (datacol.idLichXem != null)
                    lichXemPhong = datacol;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return lichXemPhong;
        }

        public LichXemPhong EditLichXem(string id, LichXemPhong lichXemPhong)
        {
            try
            {
                string data = JsonConvert.SerializeObject(lichXemPhong);
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
            return lichXemPhong;
        }

        public void DeleteLichXem(string id)
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
    }
}

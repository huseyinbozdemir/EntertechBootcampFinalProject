using EntertechFP.API.Responses;
using EntertechFP.EL.Concrete;
using EntertechFP.UI.Models;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace EntertechFP.UI.Requests
{
    public class RequestHelper
    {
        public async Task<BaseResponse<T>> Get<T>(string url, string key)
        {
            using(var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("ApiKey",key);
                    var response = await client.GetStringAsync($"http://localhost:38734/api/{url}");
                    var json = JsonConvert.DeserializeObject<BaseResponse<T>>(response);
                    return json;
                }
                catch (Exception)
                {
                    throw new Exception();
                }
            }
        }
        public async Task<BaseResponse<T>> Post<T>(string url, string key, T data)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("ApiKey", key);
                    var serialized = JsonConvert.SerializeObject(data);
                    var response = await client.PutAsync($"http://localhost:38734/api/{url}",new StringContent(serialized,Encoding.UTF8,"application/json"));
                    var content = response.Content.ReadAsStringAsync().Result;
                    var json = JsonConvert.DeserializeObject<BaseResponse<T>>(content);
                    return json;
                }
                catch (Exception)
                {
                    throw new Exception();
                }
            }
        }
    }

}

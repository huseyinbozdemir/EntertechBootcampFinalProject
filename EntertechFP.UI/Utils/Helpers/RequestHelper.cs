using EntertechFP.UI.Models.Responses;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EntertechFP.UI.Utils.Helpers
{
    public enum ActionType
    {
        Get,
        Post,
        Put,
        Patch,
        Delete
    }
    public class RequestHelper
    {
        private string apiKey;
        public RequestHelper(string key)
        {
            apiKey = key;
        }
        public async Task<BaseResponseDto<T>> Action<T>(string url, ActionType actionType, T? data)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string serialized = "";
                    HttpResponseMessage response = null;
                    url = $"http://localhost:50375/api/{url}";
                    client.DefaultRequestHeaders.Add("ApiKey", apiKey);
                    var item = (data is not null) ? data.GetType().GetProperties() : null;
                    if (actionType != ActionType.Get && actionType != ActionType.Delete)
                        serialized = JsonSerializer.Serialize(data,data.GetType(),new JsonSerializerOptions { DefaultIgnoreCondition=JsonIgnoreCondition.WhenWritingNull});
                    switch (actionType)
                    {
                        case ActionType.Get:
                            response = await client.GetAsync(url);
                            break;
                        case ActionType.Post:
                            response = await client.PostAsync(url, new StringContent(serialized, Encoding.UTF8, "application/json"));
                            break;
                        case ActionType.Put:
                            response = await client.PutAsync(url, new StringContent(serialized, Encoding.UTF8, "application/json"));
                            break;
                        case ActionType.Patch:
                            response = await client.PatchAsync(url, new StringContent(serialized, Encoding.UTF8, "application/json"));
                            break;
                        case ActionType.Delete:
                            response = await client.DeleteAsync(url);
                            break;
                    }
                    var content = response.Content.ReadAsStringAsync().Result;
                    var obj = JsonSerializer.Deserialize<BaseResponseDto<T>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return obj;
                }
                catch (Exception ex)
                {

                    return new BaseResponseDto<T> { Success = false, Message = ex.Message };
                }
            }

        }
    }
}

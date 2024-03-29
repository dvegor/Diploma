using IncidentPrioritization.Interfaces;
using Newtonsoft.Json;
using Serilog;
using System.Text;

namespace IncidentPrioritization.Services
{
    public class WebApi : IWebApi
    {
        public async Task<T> GetAsync<T>(string api, string HeaderAccept = "application/json")
        {
            using (HttpClient client = new HttpClient(new HttpClientHandler { UseDefaultCredentials = true }))
            {
                try
                {
                    client.DefaultRequestHeaders.Add("Accept", HeaderAccept);

                    var response = client.GetAsync(api).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<T>(content);
                    }
                }
                catch (Exception exp)
                {
                    Log.Warning(exp, $"WebApi.GetAsync: {api}");
                }

                return default(T);
            }
        }

        public async Task DeleteAsync(string api)
        {
            using (HttpClient client = new HttpClient(new HttpClientHandler { UseDefaultCredentials = true }))
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                await client.DeleteAsync(api);
            }
        }

        public async Task<U> PostAsync<T, U>(string api, T obj)
        {
            try
            {
                var content = await GetPostResultAsync(api, obj);
                return JsonConvert.DeserializeObject<U>(content);
            }
            catch (Exception exp)
            {
                Log.Warning(exp, $"WebApi.PostAsync Url: {api}");
                return default(U);
            }
        }

        public async Task<string> PostAsync<T>(string api, T obj)
        {
            string result = string.Empty;
            try
            {
                result = await GetPostResultAsync(api, obj);
            }
            catch (Exception exp)
            {
                Log.Warning(exp, $"WebApi.PostAsync Url: {api}");
            }

            return result;
        }

        private async Task<string> GetPostResultAsync<T>(string api, T obj)
        {
            using (HttpClient client = new HttpClient(new HttpClientHandler { UseProxy = false, UseDefaultCredentials = true }))
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                var response = await client.PostAsync(api, new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json"));
                try
                {
                    response.EnsureSuccessStatusCode();
                    string result = await response.Content.ReadAsStringAsync();
                    return result;

                }
                catch (Exception exp)
                {
                    Log.Warning(exp, $"WebApi.GetPostResultAsync Url: {api}");
                    return string.Empty;
                }

            }
        }
    }
}
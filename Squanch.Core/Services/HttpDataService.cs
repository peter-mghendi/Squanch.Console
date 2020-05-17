using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Squanch.Core.Services
{
    public class HttpDataService
    {
        private readonly Dictionary<string, object> responseCache;
        private readonly HttpClient client;

        public HttpDataService(string defaultBaseUrl = "")
        {
            client = new HttpClient();

            if (!string.IsNullOrEmpty(defaultBaseUrl))
            {
                client.BaseAddress = new Uri($"{defaultBaseUrl}/");
            }

            responseCache = new Dictionary<string, object>();
        }

        public async Task<T> GetAsync<T>(string uri, Dictionary<string, string> parameters = null, bool forceRefresh = false)
        {
            T result = default;
            // uri = parameters == null ? uri : uri + parameters; // TODO

            if (forceRefresh || !responseCache.ContainsKey(uri))
            {
                var json = await client.GetStringAsync(uri);
                result = await Task.Run(() => JsonSerializer.Deserialize<T>(json));

                if (responseCache.ContainsKey(uri))
                {
                    responseCache[uri] = result;
                }
                else
                {
                    responseCache.Add(uri, result);
                }
            }
            else
            {
                result = (T)responseCache[uri];
            }

            return result;
        }
    }
}

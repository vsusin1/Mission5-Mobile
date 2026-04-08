
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace AppSicilyLines
{
    internal class Helper
    {
        static HttpClient client = new HttpClient()
        {
            BaseAddress = new Uri("http://localhost:5288")
        };
        public static async Task<T?> GetHttpResource<T>(string uri)
        {
            T? content = default;

            var response = await client.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            if (response != null)
            {
                var responseString = await response.Content.ReadAsStringAsync();

                if (responseString == null)
                {
                    return default;
                }

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                content = JsonSerializer.Deserialize<T>(responseString, options);
            }

            return content ;
        }

        public static async Task<HttpStatusCode> EditProfileInfo(Client profile)
        {
            var json = JsonSerializer.Serialize(profile);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            if (client == null)
            {
                client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:5288");
            }

            var sendResult = await client.PutAsync($"api/client/{profile.Id}/editProfile", content);
            return sendResult.StatusCode;

        }
    }
}

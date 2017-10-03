﻿using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SensorThings.Client
{
    public static class Http
    {
        public static T GetJson<T>(string url)
        {
            var client = new HttpClient();
            var response = client.GetAsync(url).Result;
            string strJson = response.Content.ReadAsStringAsync().Result;
            var items = JsonConvert.DeserializeObject<T>(strJson);
            return items;
        }

        public static T PostJson<T>(string url, T entity)
        {
            var client = new HttpClient();
            var serialized = JsonConvert.SerializeObject(entity);
            var buffer = System.Text.Encoding.UTF8.GetBytes(serialized);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responseMessage = client.PostAsync(url, byteContent).Result;
            var response = responseMessage.Content.ReadAsStringAsync().Result;
            var item = JsonConvert.DeserializeObject<T>(response);
            return item;
        }
    }
}
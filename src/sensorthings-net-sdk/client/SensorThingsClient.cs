﻿using Newtonsoft.Json;
using SensorThings.Core.Core;
using System.Net.Http;

namespace SensorThings.Client
{
    public class SensorThingsClient
    {
        private HttpClient httpClient;

        public SensorThingsClient(string Server)
        {
            this.Server = Server;
            httpClient = new HttpClient();
        }

        public string Server { get; set; }

        public FeatureOfInterest GetFeatureOfInterest()
        {
            var url = Server + "FeatureOfInterest";

            var response = httpClient.GetAsync(url).Result;
            string strJson = response.Content.ReadAsStringAsync().Result;
            var foi = JsonConvert.DeserializeObject<FeatureOfInterest>(strJson);
            return foi;
        }
    }
}

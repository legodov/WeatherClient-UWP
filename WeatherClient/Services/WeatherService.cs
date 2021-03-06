﻿using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Linq;
using WeatherClient.Models;
using Windows.Storage;

namespace WeatherClient.Services
{
    public class WeatherService : IWeatherService
    {
        private static HttpClient _client;
        private static readonly int _minCountDays;
        private static readonly int _maxCountDays;
        private static readonly string _configFilePath;

        static WeatherService()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _minCountDays = 1;
            _maxCountDays = 17;
            _configFilePath = "./weatherConfig.xml";
        }

        public int MinCountDays
        {
            get { return _minCountDays; }
        }
        public int MaxCountDays
        {
            get { return _maxCountDays; }
        }
        public async Task<WeatherData> GetWeatherAsync(string cityName, int countDays)
        {
            if (countDays < _minCountDays || countDays > _maxCountDays)
                throw new WeatherException(WeatherError.IncorrectCountDays, "Incorrect count days");
            string url = GetUrl(cityName, countDays);
            using (HttpResponseMessage response = await _client.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string forecastJson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<WeatherData>(forecastJson);
                }
                else throw new WeatherException(WeatherError.WeatherNotFound, "Weather not found");
            }
        }
        private string GetUrl(string cityName, int countDays)
        {
            var apiUrl = GetUrlFromFile();
            return string.Format(apiUrl + cityName + "/" + countDays);
        }
        private string GetUrlFromFile()
        {
            StorageFolder installedLocation = Windows.ApplicationModel.Package.Current.InstalledLocation;
            XDocument config = XDocument.Load(installedLocation.Path + _configFilePath);
            return config.Descendants("configSection")
                         .Where(p => p.FirstAttribute.Name == "name" &&
                                     p.FirstAttribute.Value == "binaryWeatherApiUrl")
                         .SingleOrDefault().Value;
        }
    }
}
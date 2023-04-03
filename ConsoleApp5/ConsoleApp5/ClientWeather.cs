using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    public class WeatherModel
    {
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public WeatherResultRoot WeatherResult { get; set; }
        public string Error { get; set; }

        public void PrintResult()
        {
            Console.WriteLine($"Error: {this.Error}");
            Console.WriteLine($"Message {this.Message}");
            Console.WriteLine($"StatusCode: {this.StatusCode}");

            if (this.Error == null)
            {
                Console.WriteLine($"coord.lon: {this.WeatherResult.coord.lon}");
                Console.WriteLine($"coord.lat: {this.WeatherResult.coord.lat}");
                Console.WriteLine($"weather.id: {this.WeatherResult.weather[0].id}");
                Console.WriteLine($"weather.main: {this.WeatherResult.weather[0].main}");
                Console.WriteLine($"weather.description: {this.WeatherResult.weather[0].description}");
                Console.WriteLine($"weather.icon: {this.WeatherResult.weather[0].icon}");
                Console.WriteLine($"base: {this.WeatherResult.@base}");
                Console.WriteLine($"main.temp: {this.WeatherResult.main.temp}");
                Console.WriteLine($"main.feels_like: {this.WeatherResult.main.feels_like}");
                Console.WriteLine($"main.temp_min: {this.WeatherResult.main.temp_min}");
                Console.WriteLine($"main.temp_max: {this.WeatherResult.main.temp_max}");
                Console.WriteLine($"main.pressure: {this.WeatherResult.main.pressure}");
                Console.WriteLine($"main.humidity: {this.WeatherResult.main.humidity}");
                Console.WriteLine($"visibility: {this.WeatherResult.visibility}");
                Console.WriteLine($"wind.speed: {this.WeatherResult.wind.speed}");
                Console.WriteLine($"wind.deg: {this.WeatherResult.wind.deg}");
                Console.WriteLine($"wind.gust: {this.WeatherResult.wind.gust}");
                Console.WriteLine($"rain.oneHour: {this.WeatherResult.rain?._1h}");
                Console.WriteLine($"clouds.all: {this.WeatherResult.clouds.all}");
                Console.WriteLine($"dt: {this.WeatherResult.dt}");
                Console.WriteLine($"sys.type: {this.WeatherResult.sys.type}");
                Console.WriteLine($"sys.id: {this.WeatherResult.sys.id}");
                Console.WriteLine($"sys.country: {this.WeatherResult.sys.country}");
                Console.WriteLine($"sys.sunrise: {this.WeatherResult.sys.sunrise}");
                Console.WriteLine($"sys.sunset: {this.WeatherResult.sys.sunset}");
                Console.WriteLine($"timezone: {this.WeatherResult.timezone}");
                Console.WriteLine($"id: {this.WeatherResult.id}");
                Console.WriteLine($"name: {this.WeatherResult.name}");
                Console.WriteLine($"cod: {this.WeatherResult.cod}");
            }
        }
    }

    public class ClientWeather
    {

        private readonly string key;
        private readonly HttpClient client;
        private WeatherModel Weathermodel;

        public ClientWeather(string key)
        {
            this.client = new HttpClient();
            this.key = key;
        }

        public async Task<WeatherModel> Get()
        {
            string city = "Kyiv";
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&units=metric" +
            $"&appid={this.key}";

            try
            {
                HttpResponseMessage response = await client.GetAsync($"{url}");
                string json = await response.Content.ReadAsStringAsync();
                Console.WriteLine(json);
                WeatherResultRoot weatherData = JsonSerializer.Deserialize<WeatherResultRoot>(json);

                return new WeatherModel
                {
                    Message = "Data retrieved successfully",
                    StatusCode = response.StatusCode,
                    WeatherResult = weatherData,
                    Error = null
                };
            }
            catch (Exception ex)
            {
                return new WeatherModel
                {
                    Message = "Error retrieving data",
                    StatusCode = HttpStatusCode.InternalServerError,
                    WeatherResult = null,
                    Error = ex.Message
                };
            }
        }

        public async Task<WeatherModel> Post(string city)
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&units=metric" +
            $"&appid={this.key}";
            Dictionary<string, string> requestData = new Dictionary<string, string>() { {"city", city} };
            try
            {
                FormUrlEncodedContent requestBody = new FormUrlEncodedContent(requestData);
                HttpResponseMessage response = await client.PostAsync(url, requestBody);
                string json = await response.Content.ReadAsStringAsync();
                Console.WriteLine(json);
                WeatherResultRoot weatherData = JsonSerializer.Deserialize<WeatherResultRoot>(json);

                return new WeatherModel
                {
                    Message = "Data retrieved successfully",
                    StatusCode = response.StatusCode,
                    WeatherResult = weatherData,
                    Error = null
                };
            }
            catch (Exception ex)
            {
                return new WeatherModel
                {
                    Message = "Error retrieving data",
                    StatusCode = HttpStatusCode.InternalServerError,
                    WeatherResult = null,
                    Error = ex.Message
                };
            }
        }

        
    }
}

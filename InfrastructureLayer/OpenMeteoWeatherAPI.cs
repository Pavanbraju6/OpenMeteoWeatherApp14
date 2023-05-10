using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DomainLayer.Models;

namespace InfrastructureLayer
{
    public class OpenMeteoWeatherAPI : IOpenMeteoWeatherAPI
    {
        private const string API_URL = "https://api.open-meteo.com/v1/forecast";

        public  WeatherData GetWeatherData(int numDays, double lat, double lon)
        {
            WeatherData weatherData = new WeatherData();
            try
            {

                // Build the API request URL
                string apiUrl = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&past_days={numDays}&daily=temperature_2m_max,temperature_2m_min&timezone=GMT&daily=snowfall_sum&temperature_2m";
                // string apiUrl = $"{API_URL}?latitude={lat}&longitude={lon}&hourly=temperature_2m,snowdepth&daily=temperature_2m_max,temperature_2m_min,snowfall";
                if (numDays > 1)
                {
                    apiUrl += $"&days={numDays}";
                }

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                // Read the API response and parse it as JSON
                string responseData = "";
                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream);
                    responseData = reader.ReadToEnd();
                }
                dynamic parsedData = JsonConvert.DeserializeObject(responseData);


                // Extract the weather data from the API response
                dynamic dailyData = parsedData.daily;
                dynamic[] weatherDataInfo = new dynamic[numDays];
                
                weatherData.NumberOfDays = numDays;
                weatherData.Latitude = lat;
                weatherData.Longitude = lon;
                List<WeatherDataPoint> weatherDataPointList = new List<WeatherDataPoint>();
                for (int i = 0; i < numDays; i++)
                {
                    WeatherDataPoint weatherDataPoint = new WeatherDataPoint(); 
                    dynamic numberOfDays = dailyData.time[i];
                    double lowTempC = dailyData.temperature_2m_min[i];
                    double highTempC = dailyData.temperature_2m_max[i];
                    double snowfall = dailyData.snowfall_sum[i];

                    double lowTempF = ConvertCelsiusToFahrenheit(lowTempC);
                    double highTempF = ConvertCelsiusToFahrenheit(highTempC);
                    var temperatureFahrenheit = new { lowTempF, highTempF };

                    weatherDataInfo[i] = new { numberOfDays, temperatureFahrenheit, snowfall };
                    weatherDataPoint.LowTemperatureCelsius = lowTempC;
                    weatherDataPoint.HighTemperatureCelsius= highTempC;
                    weatherDataPoint.Snowfall = snowfall;
                    weatherDataPoint.HighTemperatureFahrenheit= highTempF;
                    weatherDataPoint.LowTemperatureFahrenheit = lowTempF;
                    weatherDataPointList.Add(weatherDataPoint);
                }
                weatherData.DataPoints = weatherDataPointList;
                // Serialize the weather data as JSON and save to a file
                string today = DateTime.Now.ToString("yyyyMMdd");
                string fileName = $"weatherExport_{today}.json";
                string json = JsonConvert.SerializeObject(weatherDataInfo, Newtonsoft.Json.Formatting.Indented);
                string downloadFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
                string filePath = Path.Combine(downloadFolder, fileName);

                // Create the file and save it to the user's downloads folder
                File.WriteAllText(filePath, json);

                Console.WriteLine($"File saved to {filePath}");


                return weatherData;

            }
            catch (Exception)
            {
                return weatherData;
            }
           
        }

        public static double ConvertCelsiusToFahrenheit(double celsius)
        {
            return (celsius * 9 / 5) + 32;
        }

    }

}

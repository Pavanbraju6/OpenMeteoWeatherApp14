using System;
using System.Collections.Generic;
using DomainLayer.Models;
using DomainLayer.Validation;
using InfrastructureLayer;
using Newtonsoft.Json;

namespace OpenMeteoWeatherApp.ApplicationLayer
{
    public class OpenMeteoWeatherApp1 : IApplicationLayer
    {
        private readonly IOpenMeteoWeatherAPI _infrastructureLayer;

        public OpenMeteoWeatherApp1()
        {
            _infrastructureLayer = new OpenMeteoWeatherAPI();
        }

        public WeatherData GetWeatherData(int days, double latitude, double longitude)
        {
            var weatherDataList = new List<WeatherData>();

            // Validate the latitude and longitude values
            if (!WeatherDataValidator.ValidateLatitude(latitude) || !WeatherDataValidator.ValidateLongitude(longitude))
            {
                throw new ArgumentException("invalid latitude or longitude.");
            }

            // Call the OpenMeteoWeatherAPI to get the weather data
            var openMeteoWeatherData = _infrastructureLayer.GetWeatherData(days, latitude, longitude);


            return openMeteoWeatherData;
        }

       
    }
}

using System.Collections.Generic;
using DomainLayer.Models;

namespace OpenMeteoWeatherApp.ApplicationLayer
{
    public interface IApplicationLayer
    {
        WeatherData GetWeatherData(int days, double latitude, double longitude);
    }
}

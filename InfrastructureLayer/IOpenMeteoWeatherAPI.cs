using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer
{
    public interface IOpenMeteoWeatherAPI
    {
        WeatherData GetWeatherData(int numDays, double lat, double lon);
    }

}

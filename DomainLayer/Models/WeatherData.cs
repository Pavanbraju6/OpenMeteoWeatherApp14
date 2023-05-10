using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DomainLayer.Models
{
    public class WeatherData
    {
        public int NumberOfDays { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<WeatherDataPoint> DataPoints { get; set; }
    }

    public class WeatherDataPoint
    {
        public double LowTemperatureCelsius { get; set; }
        public double HighTemperatureCelsius { get; set; }
        public double Snowfall { get; set; }
        public double LowTemperatureFahrenheit { get; set; }
        public double HighTemperatureFahrenheit { get; set; }
    }
}








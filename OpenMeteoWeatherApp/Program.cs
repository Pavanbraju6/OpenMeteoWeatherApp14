using OpenMeteoWeatherApp.ApplicationLayer;
using System;


namespace OpenMeteoWeatherApp.PresentationLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            int days = 0;
            double latitude = 0.0;
            double longitude = 0.0;

            // Read command line parameters
            if (args.Length == 3 &&
                int.TryParse(args[0], out days) &&
                double.TryParse(args[1], out latitude) &&
                double.TryParse(args[2], out longitude))
            {
                IApplicationLayer appLayer = new OpenMeteoWeatherApp1();
                var weatherData = appLayer.GetWeatherData(days, latitude, longitude);

                Console.WriteLine($"Weather data is a {weatherData}");
                
            }
            else
            {
                Console.WriteLine("Invalid command line parameters.");
            }
        }
    }
}

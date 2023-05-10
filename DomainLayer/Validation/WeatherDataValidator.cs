using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Validation
{
    public class WeatherDataValidator
    {
        public static bool ValidateLatitude(double latitude)
        {
            if (latitude < -90 || latitude > 90)
            {
                return false;
            }
            return true;
        }
        public static bool ValidateLongitude(double longitude)
        {
            if (longitude < -180 || longitude > 180)
            {
                return false;
            }
            return true;
        }


    }
}

using GoogleMaps.LocationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheMusicExchangeProject.Controllers
{
    public static class CalculateDistance
    {
        public struct Coords
        {
            public double Latitude;
            public double Longitude;
        }

        public enum Units
        {
            Miles,
            Kilometres
        }

        public static double? BetweenTwoPostCodes(double latA, double longA, double latB, double longB, Units units)
        {
            var addressA = new Coords
            {
                Latitude = latA,
                Longitude = longA
            };
            
            var addressB = new Coords
            {
                Latitude = latB,
                Longitude = longB
            };
            return addressA.DistanceTo(addressB, units);
        }

        public static double DistanceTo(this Coords from, Coords to, Units units)
        {
            // Applies the Haversine Formula.  
            var dLat1InRad = from.Latitude * (Math.PI / 180.0);
            var dLong1InRad = from.Longitude * (Math.PI / 180.0);
            var dLat2InRad = to.Latitude * (Math.PI / 180.0);
            var dLong2InRad = to.Longitude * (Math.PI / 180.0);

            var dLongitude = dLong2InRad - dLong1InRad;
            var dLatitude = dLat2InRad - dLat1InRad;

            // Intermediate result a.  
            var a = Math.Pow(Math.Sin(dLatitude / 2.0), 2.0) +
                    Math.Cos(dLat1InRad) * Math.Cos(dLat2InRad) *
                    Math.Pow(Math.Sin(dLongitude / 2.0), 2.0);

            // Intermediate result c (great circle / shortest distance in Radians).  
            var c = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0 - a));

            // Apply unit of measurement and round result to 2 d.p. 
            var radius = 6371;
            if (units == Units.Miles) radius = 3959;
            double resultRounded = Math.Round((radius * c), 2);
            return resultRounded;
        }
    }
}

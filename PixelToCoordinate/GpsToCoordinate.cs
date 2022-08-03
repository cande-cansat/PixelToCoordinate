using System;
using System.Collections.Generic;
using System.Text;

namespace PixelToCoordinate
{
    internal class GpsToCoordinate
    {
        private Coordinate Origin;
        private Coordinate recentCoordinate;

        public GpsToCoordinate() { }
        public GpsToCoordinate(Coordinate Origin, Coordinate recentCoordinate)
        {
            this.Origin = Origin;
            this.recentCoordinate = recentCoordinate;
        }

        private float measure(float lat1, float lon1, float lat2, float lon2)
        {  // generally used geo measurement function
            var R = 6378.137; // Radius of earth in KM
            var dLat = lat2 * Math.PI / 180 - lat1 * Math.PI / 180;
            var dLon = lon2 * Math.PI / 180 - lon1 * Math.PI / 180;
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
            Math.Cos(lat1 * Math.PI / 180) * Math.Cos(lat2 * Math.PI / 180) *
            Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c;
            return (float)d * 1000; // meters
        }

        public Coordinate convertGpsToCoordinate()
        {
            float x = measure(Origin.item1, Origin.item2, recentCoordinate.item1, Origin.item2);
            float y = measure(recentCoordinate.item1, Origin.item2, recentCoordinate.item1, recentCoordinate.item2);
            return new Coordinate(x, y, recentCoordinate.item3);
        }

    }
}

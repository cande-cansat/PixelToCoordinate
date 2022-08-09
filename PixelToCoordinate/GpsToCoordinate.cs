using System;
using System.Collections.Generic;
using System.Text;

namespace PixelToCoordinate
{
    internal class GpsToCoordinate
    {
        private Coordinate Origin;
        private Coordinate recentCoordinate;

        private const double UNIT_DISTANCE = 0.00000898335;

        public GpsToCoordinate() { }
        public GpsToCoordinate(Coordinate Origin, Coordinate recentCoordinate)
        {
            this.Origin = Origin;
            this.recentCoordinate = recentCoordinate;
        }

        private float measure(double lat1, double lon1, double lat2, double lon2)
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
            float x = measure(Origin.ditem1, Origin.ditem2, recentCoordinate.ditem1, Origin.ditem2);
            float y = measure(recentCoordinate.ditem1, Origin.ditem2, recentCoordinate.ditem1, recentCoordinate.ditem2);
            return new Coordinate(x, y, recentCoordinate.item3);
        }

        public Coordinate convertGpsToCoordinate(Coordinate Origin, Coordinate recentCoordinate)
        {
            float x = measure(Origin.ditem1, Origin.ditem2, recentCoordinate.ditem1, Origin.ditem2);
            float y = measure(recentCoordinate.ditem1, Origin.ditem2, recentCoordinate.ditem1, recentCoordinate.ditem2);
            return new Coordinate(x, y, recentCoordinate.ditem3);
        }

        public Coordinate convertCoordinateToGPS()
        {
            double x = recentCoordinate.ditem1 * UNIT_DISTANCE + Origin.ditem1;
            double y = recentCoordinate.ditem2 * UNIT_DISTANCE + Origin.ditem2;
            double z = recentCoordinate.ditem3 + Origin.ditem3;
            return new Coordinate(x, y, z);
        }

    }
}

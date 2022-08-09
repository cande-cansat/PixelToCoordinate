using System;
using System.Collections.Generic;

namespace PixelToCoordinate
{
    class Program
    {
        static void Main(string[] args)
        {
            double px, py, d;
            float roll, pitch, yaw;
            float gpsLat, gpsLong, gpsAlt;

            px = 248; 
            py = 195;
            d = 0.109486835375;

            roll = 0;
            pitch = 0;
            yaw = 0;

            gpsLat = (float)37.58277;
            gpsLong = (float)127.0267;
            gpsAlt = 86;

            PixelToCoordinateTranslator pTranslator = new PixelToCoordinateTranslator(px, py, d);
            Coordinate spherical = pTranslator.pixetToSpherical();
            Coordinate cartesian = Coordinate.sphericalToCartesian(spherical);

            SystemReviser reviser = new SystemReviser(roll, pitch, yaw);
            Coordinate targetCoordinate = reviser.revise(cartesian, yaw);

            GpsToCoordinate gpsToCoordinate = new GpsToCoordinate(new Coordinate(gpsLat, gpsLong, gpsAlt), targetCoordinate);
            Coordinate targetGpsCoordinate = gpsToCoordinate.convertCoordinateToGPS();


            Console.WriteLine();
        }
    }
}

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

            roll = (float)164.15;
            pitch = (float)-4.73;
            yaw = (float)-51.96;

            gpsLat = (float)37.58277;
            gpsLong = (float)127.0267;
            gpsAlt = 86;

            //PixelToCoordinateTranslator pTranslator = new PixelToCoordinateTranslator(px, py, d);
            //Coordinate spherical = pTranslator.pixetToSpherical();
            Coordinate spherical = new Coordinate((float)0.150611443, (float)1.666666667, (float)100);
            Console.WriteLine(String.Format("Spherical : {0},{1},{2}", spherical.item1, spherical.item2, spherical.item3));
            
            Coordinate cartesian = Coordinate.sphericalToCartesian(spherical);
            Console.WriteLine(String.Format("Cartesian : {0},{1},{2}", cartesian.item1, cartesian.item2, cartesian.item3));


            SystemReviser reviser = new SystemReviser(roll, pitch, yaw);
            Coordinate targetCoordinate = reviser.revise(cartesian, yaw);
            Console.WriteLine(String.Format("targetCoordinate : {0},{1},{2}", targetCoordinate.item1, targetCoordinate.item2, targetCoordinate.item3));

            GpsToCoordinate gpsToCoordinate = new GpsToCoordinate(new Coordinate(gpsLat, gpsLong, gpsAlt), targetCoordinate);
            Coordinate targetGpsCoordinate = gpsToCoordinate.convertCoordinateToGPS();
            Console.WriteLine(String.Format("{0},{1},{2}", targetGpsCoordinate.ditem1, targetGpsCoordinate.ditem2, targetGpsCoordinate.ditem3));
        }
    }
}

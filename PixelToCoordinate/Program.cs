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
            px = 248;
            py = 195;
            d = 0.109486835375;

            roll = 0;
            pitch = 0;
            yaw = 0;

            PixelToCoordinateTranslator pTranslator = new PixelToCoordinateTranslator(px, py, d);
            List<double> coordinateValue = pTranslator.calcDegreeFromPixel();
            Coordinate spherical = new Coordinate((float)coordinateValue[0], (float)coordinateValue[1], (float)coordinateValue[2]);
            Coordinate cartesian = Coordinate.sphericalToCartesian(spherical);

            SystemReviser reviser = new SystemReviser(roll, pitch, yaw);
            Coordinate targetCoordinateCartessian = reviser.revise(cartesian);


            Console.WriteLine();
        }
    }
}

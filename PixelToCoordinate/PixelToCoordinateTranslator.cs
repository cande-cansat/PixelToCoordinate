using System;
using System.Collections.Generic;
using System.Text;

namespace PixelToCoordinate
{
    public class PixelToCoordinateTranslator
    { 
        private readonly int[] rowDegreeList = { -65, -60, -55, -50, -45, -40, -35, -30, -25, -20, -15, -10, -5, 0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65 };
        private readonly int[] colDegreeList = { 50, 45, 40, 35, 30, 25, 20, 15, 10, 5, 0, -5, -10, -15, -20, -25, -30, -35, -40, -45, -50 };

        private readonly int[] rowDiffRadial = { 25, 26, 31, 20, 25, 21, 24, 21, 25, 19, 30, 26, 27, 27, 26, 30, 19, 25, 21, 24, 21, 25, 20, 31, 26, 25 };
        private readonly int[] colDiffRadial = { 22, 25, 21, 24, 21, 25, 18, 30, 27, 27, 27, 27, 30, 18, 25, 21, 24, 21, 25, 22 };

        private readonly int[] accRowPixelData = { 0, 25, 51, 82, 102, 127, 148, 172, 193, 218, 237, 267, 293, 320, 347, 373, 403, 422, 447, 468, 492, 513, 538, 558, 589, 615, 640 };
        private readonly int[] accColPixelData = { 0, 22, 47, 68, 92, 113, 138, 156, 186, 213, 240, 267, 294, 324, 342, 367, 388, 412, 433, 458, 480 };

        private double d;
        private double tx;
        private double ty;

        private double phi;
        private double theta;
        public PixelToCoordinateTranslator(double tx, double ty, double d)
        {
            this.d = d;
            this.tx = tx;
            this.ty = ty;
        }


        private void calcDegreeByRadial()
        {
            Console.WriteLine($"d: {d}");
            for (int i = 0; i < accRowPixelData.Length; i++)
            {
                if (accRowPixelData[i + 1] > tx)
                {
                    int offset = 5;
                    double lessDeg = (double)rowDegreeList[i];
                    phi = (((double)(tx - accRowPixelData[i]) / (double)rowDiffRadial[i]) * (double)(offset)) + (double)lessDeg;
                    
                    Console.WriteLine($"tx : {tx}, i : {i}, phi : {(tx - accRowPixelData[i])}/{rowDiffRadial[i] * offset} + {lessDeg} = {phi}");
                    break;
                }
            }

            for (int i = 0; i < accColPixelData.Length; i++)
            {
                if (accColPixelData[i + 1] > ty)
                {
                    int offset = -5;
                    double lessDeg = (double)colDegreeList[i];
                    theta = 90 - ((((double)(ty - accColPixelData[i]) / (double)colDiffRadial[i]) * (double)(offset)) + (double)lessDeg);

                    Console.WriteLine($"ty : {ty}, i : {i}, theta : 90 - {(ty - accColPixelData[i])}/{colDiffRadial[i] * offset} + {lessDeg} = {theta}");
                    break;
                }
            }
        }

        public Coordinate pixetToSpherical()
        {
            calcDegreeByRadial();
            Coordinate retVal = new Coordinate(d, phi, theta);
            return retVal;
        }
    }
}
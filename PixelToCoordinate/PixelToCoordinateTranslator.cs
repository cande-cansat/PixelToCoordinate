using System;
using System.Collections.Generic;
using System.Text;

namespace SatGS.PathFinder
{
    public class PixelToCoordinateTranslator
    {
        private readonly int[] rowDegreeList = { -65, -60, -55, -50, -45, -40, -35, -30, -25, -20, -15, -10, -5, 0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65 };
        private readonly int[] colDegreeList = { 50, 45, 40, 35, 30, 25, 20, 15, 10, 5, 0, -5, -10, -15, -20, -25, -30, -35, -40, -45, -50 };

        private readonly int[] rowDiffRadial = { 25, 26, 31, 20, 25, 21, 24, 21, 25, 19, 30, 26, 27, 27, 26, 30, 19, 25, 21, 24, 21, 25, 20, 31, 26, 25 };

        private List<double> rowTargetDiffRadial = new List<double>();
        private List<double> rowAccRadial = new List<double>();

        private readonly int[] colDiffRadial = { 22, 25, 21, 24, 21, 25, 18, 30, 27, 27, 27, 27, 30, 18, 25, 21, 24, 21, 25, 22 };
        private List<double> colTargetDiffRadial = new List<double>();
        private List<double> colAccRadial = new List<double>();

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

        private void calcDistanceBetweenRadial()
        {
            double acc = 0;

            foreach (int diff in rowDiffRadial)
            {
                acc += diff * d;
                rowAccRadial.Add(acc);
                rowTargetDiffRadial.Add(diff * d);
            }

            acc = 0;
            foreach (int diff in colDiffRadial)
            {
                acc += diff * d;
                colAccRadial.Add(acc);
                colTargetDiffRadial.Add(diff * d);
            }

        }

        private void calcDegreeByRadial()
        {
            Console.WriteLine($"d: {d}");
            for (int i = 0; i < rowAccRadial.Count; i++)
            {
                if (rowAccRadial[i+1] > tx) {
                    int offset = tx < 320 ? -5 : 5;
                    double lessDeg = (double)rowDegreeList[i];
                    phi = (((double)(tx - rowAccRadial[i]) / (double)rowTargetDiffRadial[i]) * (double)(offset)) + (double)lessDeg;
                    Console.WriteLine($"{(double)(tx - rowAccRadial[i])}");
                    Console.WriteLine($"{((double)(tx - rowAccRadial[i]) / (double)rowTargetDiffRadial[i])}");
                    Console.WriteLine($"{(((double)(tx - rowAccRadial[i]) / (double)rowTargetDiffRadial[i]) * (double)(offset))}");
                    Console.WriteLine($"tx: {tx}, i:{i}, rowAccRadial[i] = {rowAccRadial[i]}, phi = {tx - rowAccRadial[i]}/{rowTargetDiffRadial[i] * offset} + {lessDeg} = {phi}");
                    break;
                }
            }

            for (int i = 0; i < colAccRadial.Count; i++)
            {
                if (colAccRadial[i+1] > ty)
                {
                    int offset = tx < 240 ? 5 : -5;
                    int lessDeg = colDegreeList[i];
                    theta = (double)90 - ((double)(ty - colAccRadial[i]) / (double)colTargetDiffRadial[i] * (double)offset + (double)lessDeg);
                    Console.WriteLine($"ty = {ty}, colAccRadial[i] = {colAccRadial[i]}, theta = 90 - {ty - colAccRadial[i]}/{colTargetDiffRadial[i] * offset} + {lessDeg} = {theta}");
                    break;
                }
            }
        }

        public List<double> calcDegreeFromPixel()
        {
            calcDistanceBetweenRadial();
            calcDegreeByRadial();
            List<double> retVal = new List<double>();
            retVal.Add((double)d);
            retVal.Add(phi);
            retVal.Add(theta);
            return retVal;
        }
    }
}
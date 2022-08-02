using System;
using System.Collections.Generic;
using System.Text;

namespace SatGS.PathFinder
{
    public class PixelToCoordinateTranslator
    {
        private readonly int[] rowDegreeList = { -65, -60, -55, -50, -45, -40, -35, -30, -25, -20, -15, -10, -5, 0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65 };
        private readonly int[] colDegreeList = { -50, -45, -40, -35, -30, -25, -20, -15, -10, -5, 0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50};

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
            int acc = 0;

            foreach(int diff in rowDiffRadial)
            {
                acc += diff;
                rowAccRadial.Add(acc);
                rowTargetDiffRadial.Add(diff * d);
            }


            /*
            foreach(int diff in rowDiffRadial)
            {
                int last = rowTargetDiffRadial.IndexOf(rowTargetDiffRadial.Count - 1);
                int value = diff * d;
                rowTargetDiffRadial.Add(value);
                rowAccRadial.Add(value + last);
            }*/

            acc = 0;
            foreach (int diff in colDiffRadial)
            {
                acc += diff;
                colAccRadial.Add(acc);
                colTargetDiffRadial.Add(diff * d);
            }

            /*
            foreach (int diff in colDiffRadial)
            {
                int last = colTargetDiffRadial.IndexOf(colTargetDiffRadial.Count - 1);
                int value = diff * d;
                colTargetDiffRadial.Add(value);
                colAccRadial.Add(value + last);
            }
            */
        }

        private void calcDegreeByRadial()
        {
            rowAccRadial.Reverse();
            for(int i=0; i < rowAccRadial.Count; i++)
            {
                if(rowAccRadial[i] < tx)
                {
                    int lessDeg = rowDegreeList[i];
                    phi = (tx - rowAccRadial[i]) / rowTargetDiffRadial[i] * 5 + lessDeg;
                    Console.WriteLine($"tx: {tx}, rowAccRadial[i] = {rowAccRadial[i]}, phi = {tx - rowAccRadial[i]}/{rowTargetDiffRadial[i] * 5} + {lessDeg} = {phi}");
                    break;
                }
            }

            colAccRadial.Reverse();
            for (int i = 0; i < colAccRadial.Count; i++)
            {
                if (colAccRadial[i] < tx)
                {
                    int lessDeg = colDegreeList[i];
                    theta = 90 - ((ty - colAccRadial[i]) / colTargetDiffRadial[i] * 5 + lessDeg);
                    Console.WriteLine($"ty = {ty}, colAccRadial[i] = {colAccRadial[i]}, theta = {ty - colAccRadial[i]}/{colTargetDiffRadial[i] * 5} + {lessDeg} = {theta}");
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

using System;
using System.Collections.Generic;
using System.Text;

namespace PixelToCoordinate
{
    class PixelToCoordinateTranslator
    {
        private readonly int[] degreeList = { -65, -60, -55, -50, -45, -40, -35, -30, -25, -20, -15, -10, -5, 0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65 };

        private readonly int[] rowDiffRadial = { 25, 26, 31, 20, 25, 21, 24, 21, 25, 19, 30, 26, 27, 27, 26, 30, 19, 25, 21, 24, 21, 25, 20, 31, 26, 25 };
                                              
        private List<int> rowTargetDiffRadial = new List<int>();
        private List<int> rowAccRadial = new List<int>();

        private readonly int[] colDiffRadial = { 22, 25, 21, 24, 21, 25, 18, 30, 27, 27, 27, 27, 30, 18, 25, 21, 24, 21, 25, 22 };
        private List<int> colTargetDiffRadial = new List<int>();
        private List<int> colAccRadial = new List<int>();

        private int d;
        private int tx;
        private int ty;

        private double phi;
        private double theta;
        public PixelToCoordinateTranslator(int tx, int ty, int d)
        {
            this.d = d;
            this.tx = tx;
            this.ty = ty;
        }

        private void calcDistanceBetweenRadial()
        {
            foreach(int diff in rowDiffRadial)
            {
                int last = rowTargetDiffRadial.IndexOf(rowTargetDiffRadial.Count - 1);
                int value = diff * d;
                rowTargetDiffRadial.Add(value);
                rowAccRadial.Add(value + last);
            }
            foreach(int diff in colDiffRadial)
            {
                int last = colTargetDiffRadial.IndexOf(colTargetDiffRadial.Count - 1);
                int value = diff * d;
                colTargetDiffRadial.Add(value);
                colAccRadial.Add(value + last);
            }
        }

        private void calcDegreeByRadial()
        {
            rowAccRadial.Reverse();
            for(int i=0; i < rowAccRadial.Count; i++)
            {
                if(rowAccRadial[i+1] < tx)
                {
                    int lessDeg = degreeList[i + 1];
                    phi = (tx - rowAccRadial[i + 1]) / rowTargetDiffRadial[i] * lessDeg + lessDeg;
                }
            }

            colAccRadial.Reverse();
            for (int i = 0; i < colAccRadial.Count; i++)
            {
                if (colAccRadial[i + 1] < tx)
                {
                    int lessDeg = degreeList[i + 1];
                    theta = 90 - ((ty - colAccRadial[i + 1]) / colTargetDiffRadial[i] * lessDeg + lessDeg);
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

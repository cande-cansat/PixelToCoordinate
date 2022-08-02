using System;
using System.Collections.Generic;
using System.Text;

namespace PixelToCoordinate
{
    class PixelToCoordinateTranslator
    {
        private readonly int[] rowDiffRadial = { 25, 26, 31, 20, 25, 21, 24, 21, 25, 19, 30, 26, 27, 27, 26, 30, 19, 25, 21, 24, 21, 25, 20, 31, 26, 25 };
        private List<int> rowTargetDiffRadial;

        private readonly int[] colDiffRadial = { 22, 25, 21, 24, 21, 25, 18, 30, 27, 27, 27, 27, 30, 18, 25, 21, 24, 21, 25, 22 };
        private List<int> colTargetDiffRadial;

        private int d;
        public PixelToCoordinateTranslator(int tx, int ty, int d)
        {
            this.d = d;
        }

        private void calcDistanceBetweenRadial()
        {
            foreach(int diff in rowDiffRadial)
            {
                rowTargetDiffRadial.Add(diff * d);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace PixelToCoordinate
{
    class SystemReviser
    {
        private float roll, pitch, yaw;
        private Coordinate target;
        private float[,] reviseMatrix;
        public SystemReviser()
        {

        }
        public SystemReviser(float roll, float pitch, float yaw)
        {
            this.roll = roll;
            this.pitch = pitch;
            this.yaw = yaw;
        }

        private float cos(float v)
        {
            return (float)Math.Cos(v);
        }
        private float sin(float v)
        {
            return (float)Math.Sin(v);
        }
        public Coordinate revise(Coordinate cartesian, float roll, float pitch, float yaw)
        {
            reviseMatrix = new float[3,3]
            {
                {
                    cos(pitch)*cos(roll),
                    sin(yaw)*sin(pitch)*cos(roll) - cos(yaw)*sin(roll),
                    cos(yaw)*sin(pitch)*cos(roll)+sin(yaw)*sin(roll)
                },
                {
                    cos(pitch)*sin(roll),
                    sin(yaw)*sin(pitch)*sin(roll) + cos(yaw)*cos(roll),
                    cos(yaw)*sin(pitch)*sin(roll)-sin(yaw)*cos(roll)
                },
                {
                    -sin(pitch), sin(yaw)*cos(pitch), cos(yaw)*cos(pitch)
                }
            };
            List<float> revisedCoordinate = new List<float>();

            for(int i=0; i<3; i++)
            {
                revisedCoordinate.Add(reviseMatrix[i,0] * cartesian.item1 + reviseMatrix[i,1] * cartesian.item2 + reviseMatrix[i,2] * cartesian.item3);
            }
            Coordinate retVal = new Coordinate(revisedCoordinate[0], revisedCoordinate[1], revisedCoordinate[2]);
            return retVal;
        }

    }
}

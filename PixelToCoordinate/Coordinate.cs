using System;
using System.Collections.Generic;
using System.Text;

namespace PixelToCoordinate
{
    public class Coordinate
    {
        public float item1;
        public float item2;
        public float item3;

        public double ditem1;
        public double ditem2;
        public double ditem3;

        public bool isDouble = false;

        public Coordinate() { }
        public Coordinate(float item1, float item2, float item3)
        {
            this.item1 = item1;
            this.item2 = item2;
            this.item3 = item3;
        }
        public Coordinate(double ditem1, double ditem2, double ditem3)
        {
            this.ditem1 = ditem1;
            this.ditem2 = ditem2;
            this.ditem3 = ditem3;
            isDouble = true;
        }

        public static Coordinate vectorSum(Coordinate T1, Coordinate T2)
        {
            if(T1.isDouble && T2.isDouble)
            {
                return new Coordinate(
                    T1.ditem1 + T2.ditem1,
                    T1.ditem2 + T2.ditem2,
                    T1.ditem3 + T2.ditem3
                );
            }
            return new Coordinate(
                T1.item1 + T2.item1,
                T1.item2 + T2.item2,
                T1.item3 + T2.item3
            );
        }

        public static Coordinate realNumMul(Coordinate T1, float n)
        {
            return new Coordinate(T1.item1 * n, T1.item2 * n, T1.item3 * n);
        }

        public static Coordinate sphericalToCartesian(Coordinate sphericalCoord)
        {
            float rho = sphericalCoord.item1;
            float phi = sphericalCoord.item2;
            float theta = sphericalCoord.item3;
            return new Coordinate(
                    (float)(rho * Math.Sin(theta) * Math.Cos(phi)),
                    (float)(rho * Math.Sin(theta) * Math.Sin(phi)),
                    (float)(rho * Math.Cos(theta))
               );
        }

    }

    public class PositionData
    {
        private Coordinate satellitePos;
        private Coordinate relativeTargetSphericalPos;
        private Coordinate absoluteTargetCartesianPos;

        public PositionData() { }
        public PositionData(Coordinate satellitePos, Coordinate relativeTargetSphericalPos)
        {
            this.satellitePos = satellitePos;
            this.relativeTargetSphericalPos = relativeTargetSphericalPos;
        }
        public void setSatellitePos(Coordinate satellitePos)
        {
            this.satellitePos = satellitePos;
        }
        public void setRelativeTargetSphericalPos(Coordinate relativeTargetSphericalPos)
        {
            this.relativeTargetSphericalPos = relativeTargetSphericalPos;
        }

        public Coordinate getAbsoluteTargetCartesianPos()
        {
            return this.absoluteTargetCartesianPos;
        }

        public void calcAbsolutePos()
        {
            Coordinate cartesian = Coordinate.sphericalToCartesian(this.relativeTargetSphericalPos);
            this.absoluteTargetCartesianPos = Coordinate.vectorSum(cartesian, satellitePos);
        }
    }
}
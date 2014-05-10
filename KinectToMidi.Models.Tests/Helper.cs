using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KinectToMidi.Models.Tests
{
    public class Helper
    {
        public static Cuboid MakeCuboidAroundZero()
        {
            var cuboid = new Cuboid();
            cuboid.Height = 1;
            cuboid.Length = 1;
            cuboid.Width = 1;
            cuboid.Point = new Point3D() {X = -0.5F, Y = -0.5F, Z = -0.5F};
            return cuboid;
        }

        public static Cuboid MakeCuboidWithNoZero()
        {
            var cuboid = new Cuboid();
            cuboid.Height = 1;
            cuboid.Length = 1;
            cuboid.Width = 1;
            cuboid.Point = new Point3D() {X = 0.5F, Y = 0.5F, Z = 0.5F};
            return cuboid;
        }

        public static int GetRightHandIndex()
        {
            return ((JointType[]) Enum.GetValues(typeof (JointType))).ToList().IndexOf(JointType.HandRight);
        }
    }
}

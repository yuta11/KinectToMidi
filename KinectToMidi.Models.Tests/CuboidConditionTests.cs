using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KinectToMidi.Models;
using NUnit.Framework;
using Microsoft.Kinect;
namespace KinectToMidi.Models.Tests
{
    [TestFixture()]
    public class CuboidConditionTests
    {
        [Test()]
        public void CheckCondition_RightHandInCuboid_ReturnsTrue()
        {
            Skeleton skeleton = new Skeleton();

            CuboidCondition cuboidCondition = new CuboidCondition()
            {
                Cuboid = new Cuboid
                {
                    Height = 1,
                    Length = 1,
                    Width = 1,
                    Point = new Point3D() { X = -0.5F, Y = -0.5F, Z = -0.5F }
                }
            };

            Assert.AreEqual(true, cuboidCondition.CheckCondition(skeleton));
        }

        [Test()]
        public void CheckCondition_RightHandOutOfCuboid_ReturnsTrue()
        {
            var skeleton = new Skeleton();

            var cuboidCondition = new CuboidCondition()
            {
                Cuboid = new Cuboid
                {
                    Height = 1,
                    Length = 1,
                    Width = 1,
                    Point = new Point3D() { X = 0.5F, Y = 0.5F, Z = 0.5F }
                }
            };

            Assert.AreEqual(false, cuboidCondition.CheckCondition(skeleton));
        }
    }
}

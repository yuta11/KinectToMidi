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
                Cuboid = Helper.MakeCuboidAroundZero(),
                JointIndex = Helper.GetRightHandIndex()
            };

            Assert.AreEqual(true, cuboidCondition.CheckCondition(skeleton));
        }

        [Test()]
        public void CheckCondition_RightHandOutOfCuboid_ReturnsFalse()
        {
            var skeleton = new Skeleton();

            var cuboidCondition = new CuboidCondition()
            {
                Cuboid = Helper.MakeCuboidWithNoZero(),
               JointIndex = Helper.GetRightHandIndex()
            };

            Assert.AreEqual(false, cuboidCondition.CheckCondition(skeleton));
        }
    }
}

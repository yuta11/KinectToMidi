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
    public class RelatedSphereConditionTests
    {
        [Test()]
        public void CheckCondition_RightHandInSphere_ReturnsTrue()
        {
            Skeleton skeleton = new Skeleton();

            RelatedSphereCondition sphereCondition = new RelatedSphereCondition()
            {
                Sphere = new Sphere
                {
                    Radius = 1,
                    CenterPoint = new Point3D() { X = 0F, Y = 0F, Z = 0F }
                }
            };

            Assert.AreEqual(true, sphereCondition.CheckCondition(skeleton));
        }

        [Test()]
        public void CheckCondition_RightHandOutOfSphere_ReturnsTrue()
        {
            Skeleton skeleton = new Skeleton();

            RelatedSphereCondition sphereCondition = new RelatedSphereCondition()
            {
                Sphere = new Sphere
                {
                    Radius = 1,
                    CenterPoint = new Point3D() { X = 2F, Y = 2F, Z = 2F }
                }
            };

            Assert.AreEqual(false, sphereCondition.CheckCondition(skeleton));
        }
    }
}

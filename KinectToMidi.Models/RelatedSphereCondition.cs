using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace KinectToMidi.Models
{
    /// <summary>
    /// Condition where a skeleton point coordinates compare against other skeleton point coordinates
    /// </summary>
    [DataContract]
    [KnownType(typeof(Sphere))]
    [KnownType(typeof(Point3D))]
    public class RelatedSphereCondition: BaseCondition
    {
        public RelatedSphereCondition()
        {
            Sphere = new Sphere();
        }

        #region preperties
        /// <summary>
        /// First skeleton joint index
        /// </summary>
        [DataMember]
        public int JointFirstIndex { get; set; }

        /// <summary>
        /// Second skeleton joint to compare
        /// </summary>
        [DataMember]
        public int JointSecondIndex { get; set; }

        /// <summary>
        /// Sphere coordinates
        /// </summary>
        [DataMember]
        public Sphere Sphere { get; set; }
        #endregion preperties

        #region relay commands
        /// <summary>
        /// Compare the point coordinates against the sphere around another point
        /// </summary>
        /// <returns>returns true if the point in the area</returns>
        public override bool CheckCondition(Skeleton skeleton)
        {
            SkeletonPoint p1 = skeleton.Joints.ElementAt(JointFirstIndex).Position;
            SkeletonPoint p2 = skeleton.Joints.ElementAt(JointSecondIndex).Position;

            return p1.X > p2.X + Sphere.CenterPoint.X - Sphere.Radius && p1.X < p2.X + Sphere.CenterPoint.X + Sphere.Radius &&
                p1.Y > p2.Y + Sphere.CenterPoint.Y - Sphere.Radius && p1.Y < p2.Y + Sphere.CenterPoint.Y + Sphere.Radius &&
                p1.Z > p2.Z + Sphere.CenterPoint.Z - Sphere.Radius && p1.Z < p2.Z + Sphere.CenterPoint.Z + Sphere.Radius;
        }
        #endregion relay commands
    }
}

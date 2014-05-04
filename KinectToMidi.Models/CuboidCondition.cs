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
    /// Condition where a skeleton point coordinates compare against a spicified area
    /// </summary>
    [DataContract]
    [KnownType(typeof(Cuboid))]
    public class CuboidCondition: BaseCondition
    {
        public CuboidCondition()
        {
            Cuboid = new Cuboid();
        }

        #region preperties
        /// <summary>
        /// Skeleton joint index
        /// </summary>
        [DataMember]
        public int JointIndex { get; set; }

        /// <summary>
        /// Cuboid singleton
        /// </summary>
        [DataMember]
        public Cuboid Cuboid { get; set; }
        #endregion preperties

        #region public methods
        /// <summary>
        /// Compare point coordinates against the specified cuboid area
        /// </summary>
        /// <param name="skeleton"></param>
        /// <param name="sensor"></param>
        /// <returns>returns true if the point in the area</returns>
        public override bool CheckCondition(Skeleton skeleton)
        {
            SkeletonPoint p = skeleton.Joints.ElementAt(JointIndex).Position;

            return p.X > Cuboid.Point.X && 
                p.Y > Cuboid.Point.Y && 
                p.Z > Cuboid.Point.Z && 
                p.Z < Cuboid.Point.X + Cuboid.Width && p.Y < Cuboid.Point.Y + Cuboid.Height && 
                p.Z < Cuboid.Point.Z + Cuboid.Length;
        }
        #endregion public methods
    }
}

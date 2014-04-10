using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace KinectToMidi.Models
{
    /// <summary>
    /// Cuboid figure
    /// </summary>
    [DataContract]
    [KnownType(typeof(Point3D))]
    public class Cuboid
    {
        public Cuboid()
        {
            Point = new Point3D();
        }

        /// <summary>
        /// Start point
        /// </summary>
        [DataMember]
        public Point3D Point;

        /// <summary>
        /// Heght if spicified cube
        /// </summary>
        [DataMember]
        public float Height { get; set; }

        /// <summary>
        /// Width if spicified cube
        /// </summary>
        [DataMember]
        public float Width { get; set; }

        /// <summary>
        /// Length if spicified cube
        /// </summary>
        [DataMember]
        public float Length { get; set; }
    }
}

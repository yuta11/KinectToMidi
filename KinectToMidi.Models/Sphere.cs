using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace KinectToMidi.Models
{
    /// <summary>
    /// Sphere figure
    /// </summary>
    [DataContract]
    [KnownType(typeof(Point3D))]
    public class Sphere
    {
        public Sphere()
        {
            CenterPoint = new Point3D();
        }

        /// <summary>
        /// Center point
        /// </summary>
        [DataMember]
        public Point3D CenterPoint{get; set;}

        /// <summary>
        /// Radius of skeleton point to specify active area
        /// </summary>
        [DataMember]
        public float Radius { get; set; }
    }
}

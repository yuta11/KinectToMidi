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
    /// Base condition class
    /// </summary>
    [DataContract]
    public abstract class BaseCondition
    {
        public abstract bool CheckCondition(Skeleton skeleton);
    }
}

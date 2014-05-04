using System.Linq.Expressions;
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

        private JointType[] m_AllJoints;
        public JointType[] AllJoints
        {
            get { return m_AllJoints ?? (m_AllJoints = (JointType[]) Enum.GetValues(typeof (JointType))); }
        }
    }
}

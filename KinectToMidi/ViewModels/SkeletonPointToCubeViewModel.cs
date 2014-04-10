using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;
using Microsoft.Kinect;
using GalaSoft.MvvmLight.Command;
using KinectToMidi.Models;

namespace KinectToMidi.ViewModels
{
    public class SkeletonPointToCubeViewModel : ViewModelBase, IRemovable
    {
        private CuboidCondition m_cuboidCondition;
        /// <summary>
        /// Working constructor
        /// </summary>
        public SkeletonPointToCubeViewModel(CuboidCondition cuboidCondition)
        {
            RemoveCommand = new RelayCommand(() => Remove());
            m_cuboidCondition = cuboidCondition;
        }

        /// <summary>
        /// Constructor for designer
        /// </summary>
        public SkeletonPointToCubeViewModel()
            : this(new CuboidCondition())
        {
        }

        #region properties
        #region ICondition

        private Action m_delegateToRemove;
        /// <summary>
        /// Delegate for removing condition object that should be implemented by the owner
        /// </summary>
        public Action DelegateToRemove
        {
            get { return m_delegateToRemove; }
            set { m_delegateToRemove = value; }
        }
        #endregion ICondition

        private List<ConditionTypes> m_AllConditionTypes;
        /// <summary>
        /// List of all condition types
        /// </summary>
        public List<ConditionTypes> AllConditionTypes
        {
            get
            {
                if (m_AllConditionTypes != null)
                {
                    m_AllConditionTypes = new List<ConditionTypes>(Enum.GetValues(typeof(ConditionTypes)).Cast<ConditionTypes>());
                }
                return m_AllConditionTypes;
            }
        }

        /// <summary>
        /// Skeleton joint
        /// </summary>
        public JointType Joint
        {
            get
            {
                return AllJoints[JointIndex];
            }
        }

        /// <summary>
        /// Skeleton joint index
        /// </summary>
        public int JointIndex
        {
            get
            {
                return m_cuboidCondition.JointIndex;
            }
            set
            {
                m_cuboidCondition.JointIndex = value;
                RaisePropertyChanged(TextProperty);
            }
        }

        private List<JointType> m_AllJoints;
        /// <summary>
        /// List of all possible joints
        /// </summary>
        public List<JointType> AllJoints
        {
            get
            {
                if (m_AllJoints == null)
                {
                    m_AllJoints = new List<JointType>();
                    m_AllJoints = new List<JointType>(Enum.GetValues(typeof(JointType)).Cast<JointType>());
                }
                return m_AllJoints;
            }
        }

        public const string XProperty = "X";

        /// <summary>
        /// X coordinate value
        /// </summary>
        public float X
        {
            get { return m_cuboidCondition.Cuboid.Point.X; }
            set
            {
                m_cuboidCondition.Cuboid.Point.X = value;
                RaisePropertyChanged(XProperty);
                RaisePropertyChanged(TextProperty);
            }
        }

        /// <summary>
        /// Y coordinate value
        /// </summary>
        public float Y
        {
            get { return m_cuboidCondition.Cuboid.Point.Y; }
            set { m_cuboidCondition.Cuboid.Point.Y = value; RaisePropertyChanged(TextProperty); }
        }

        /// <summary>
        /// Z coordinate value
        /// </summary>
        public float Z
        {
            get { return m_cuboidCondition.Cuboid.Point.Z; }
            set { m_cuboidCondition.Cuboid.Point.Z = value; RaisePropertyChanged(TextProperty); }
        }

        /// <summary>
        /// Heght if spicified cube
        /// </summary>
        public float Height
        {
            get { return m_cuboidCondition.Cuboid.Height; }
            set { m_cuboidCondition.Cuboid.Height = value; RaisePropertyChanged(TextProperty); }
        }

        /// <summary>
        /// Width if spicified cube
        /// </summary>
        public float Width
        {
            get { return m_cuboidCondition.Cuboid.Width; }
            set { m_cuboidCondition.Cuboid.Width = value; RaisePropertyChanged(TextProperty); }
        }

        /// <summary>
        /// Length if spicified cube
        /// </summary>
        public float Length
        {
            get { return m_cuboidCondition.Cuboid.Length; }
            set { m_cuboidCondition.Cuboid.Length = value; RaisePropertyChanged(TextProperty); }
        }

        public const string TextProperty = "Text";
        /// <summary>
        /// Caption text
        /// </summary>
        public string Text
        {
            get
            {
                return string.Format("{0} x:{1} y:{2} z:{3} w:{4} h:{5} l:{6}",
                    Joint.ToString(), X, Y, Z, Width, Height, Length);
            }
        }
        #endregion properties

        #region relay commands
        /// <summary>
        /// Commant to remove condition
        /// </summary>
        public RelayCommand RemoveCommand
        {
            get;
            private set;
        }
        #endregion relay commands

        #region private methods
        /// <summary>
        /// Remove object from somewhere
        /// </summary>
        private void Remove()
        {
            if (DelegateToRemove != null)
                DelegateToRemove();
        }
        #endregion private methods
    }
}

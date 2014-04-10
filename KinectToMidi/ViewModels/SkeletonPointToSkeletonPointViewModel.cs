using System;
using System.Collections.Generic;
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
    /// <summary>
    /// ViewModel for skeleton point against skeleton point condition
    /// </summary>
    public class SkeletonPointToSkeletonPointViewModel : ViewModelBase, IRemovable
    {
        private RelatedSphereCondition m_relatedSphereCondition;
        /// <summary>
        /// Working constructor
        /// </summary>
        /// <param name="block"></param>
        public SkeletonPointToSkeletonPointViewModel(RelatedSphereCondition relatedSphereCondition)
        {
            RemoveCommand = new RelayCommand(() => Remove());

            m_ConditionType = ConditionTypes.SkeletonPointToSkeletonPoint;
            m_relatedSphereCondition = relatedSphereCondition;
        }

        /// <summary>
        /// Constructor for designer
        /// </summary>
        public SkeletonPointToSkeletonPointViewModel()
            : this(new RelatedSphereCondition())
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

        private ConditionTypes m_ConditionType;
        /// <summary>
        /// Current condition type
        /// </summary>
        public ConditionTypes ConditionType
        {
            get { return m_ConditionType; }
            set { m_ConditionType = value; }
        }
        #endregion ICondition

        private List<ConditionTypes> m_AllConditionTypes;
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
        /// First skeleton joint to compare
        /// </summary>
        public JointType JointFirst
        {
            get { return AllJoints[JointFirstIndex]; }
        }

        /// <summary>
        /// Second skeleton joint to compare
        /// </summary>
        public JointType JointSecond
        {
            get { return AllJoints[JointSecondIndex]; }
        }

        /// <summary>
        /// First skeleton joint index
        /// </summary>
        public int JointFirstIndex
        {
            get
            {
                return m_relatedSphereCondition.JointFirstIndex;
            }
            set { m_relatedSphereCondition.JointFirstIndex = value; RaisePropertyChanged(TextProperty); }
        }

        /// <summary>
        /// Second skeleton joint to compare
        /// </summary>
        public int JointSecondIndex
        {
            get
            {
                return m_relatedSphereCondition.JointSecondIndex;
            }
            set { m_relatedSphereCondition.JointSecondIndex = value; RaisePropertyChanged(TextProperty); }
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

        /// <summary>
        /// X coordinate value
        /// </summary>
        public float X
        {
            get { return m_relatedSphereCondition.Sphere.CenterPoint.X; }
            set { m_relatedSphereCondition.Sphere.CenterPoint.X = value; RaisePropertyChanged(TextProperty); }
        }

        /// <summary>
        /// Y coordinate value
        /// </summary>
        public float Y
        {
            get { return m_relatedSphereCondition.Sphere.CenterPoint.Y; }
            set { m_relatedSphereCondition.Sphere.CenterPoint.Y = value; RaisePropertyChanged(TextProperty); }
        }

        /// <summary>
        /// Z coordinate value
        /// </summary>
        public float Z
        {
            get { return m_relatedSphereCondition.Sphere.CenterPoint.Z; }
            set { m_relatedSphereCondition.Sphere.CenterPoint.Z = value; RaisePropertyChanged(TextProperty); }
        }

        /// <summary>
        /// Radius of skeleton point to specify active area
        /// </summary>
        public float Radius
        {
            get { return m_relatedSphereCondition.Sphere.Radius; }
            set { m_relatedSphereCondition.Sphere.Radius = value; RaisePropertyChanged(TextProperty); }
        }

        public const string TextProperty = "Text";
        /// <summary>
        /// Caption text
        /// </summary>
        public string Text
        {
            get
            {
                return string.Format("{0}-{1} x:{2} y:{3} z:{4} r:{5}",
                    JointFirst.ToString(), JointSecond.ToString(), X, Y, Z, Radius);
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

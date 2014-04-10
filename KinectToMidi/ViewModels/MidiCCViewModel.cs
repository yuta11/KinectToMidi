using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Microsoft.Kinect;
using GalaSoft.MvvmLight.Command;
using KinectToMidi.Models;

namespace KinectToMidi.ViewModels
{
    /// <summary>
    /// MIDI CC view model class
    /// </summary>
    class MidiCCViewModel : ViewModelBase, IRemovable
    {
        private MidiCC m_midiCC;
        /// <summary>
        /// Working constructor
        /// </summary>
        /// <param name="midiSettings">current midi settings</param>
        /// <param name="block"></param>
        public MidiCCViewModel(MidiCC miciCC)
        {
            RemoveCommand = new RelayCommand(() => Remove());

            m_midiCC = miciCC;
        }

        /// <summary>
        /// Constructor for designer
        /// </summary>
        public MidiCCViewModel()
        {
        }

        #region properties
        #region IRemovable
        private Action m_delegateToRemove;
        /// <summary>
        /// Delegate for the removing midi object that should be implemented by a owner
        /// </summary>
        public Action DelegateToRemove
        {
            get { return m_delegateToRemove; }
            set { m_delegateToRemove = value; }
        }
        #endregion IRemovable

        /// <summary>
        /// MIDI signal event type
        /// </summary>
        public SignalEventTypes SignalEventType
        {
            get { return m_midiCC.SignalEventType; }
            set { m_midiCC.SignalEventType = value; }
        }

        /// <summary>
        /// MIDI Control Change
        /// </summary>
        public byte CC
        {
            get { return m_midiCC.CC; }
            set { m_midiCC.CC = value; RaisePropertyChanged(TextProperty); }
        }

        /// <summary>
        /// Minimum MIDI CC value expression string
        /// </summary>
        public string MinString
        {
            get { return m_midiCC.MinString; }
            set
            {
                m_midiCC.MinString = value;
            }
        }

        /// <summary>
        /// Minimum MIDI CC value
        /// </summary>
        public byte Min
        {
            get { return m_midiCC.Min; }
            set { m_midiCC.Min = value; }
        }

        /// <summary>
        /// Maximum MIDI CC value expression string
        /// </summary>
        public string MaxString
        {
            get { return m_midiCC.MaxString; }
            set
            {
                m_midiCC.MaxString = value;
            }
        }

        /// <summary>
        /// Maximum MIDI CC value
        /// </summary>
        public byte Max
        {
            get { return m_midiCC.Max; }
            set { m_midiCC.Max = value; }
        }

        /// <summary>
        /// MIDI Channel number
        /// </summary>
        public byte Channel
        {
            get { return m_midiCC.Channel; }
            set { m_midiCC.Channel = value; RaisePropertyChanged(TextProperty); }
        }

        private List<SignalEventTypes> m_AllSignalEventTypes;
        /// <summary>
        /// List of MIDI signal event types
        /// </summary>
        public List<SignalEventTypes> AllSignalEventTypes
        {
            get
            {
                if (m_AllSignalEventTypes == null)
                    m_AllSignalEventTypes = new List<SignalEventTypes>(Enum.GetValues(typeof(SignalEventTypes)).Cast<SignalEventTypes>());
                return m_AllSignalEventTypes;
            }
        }

        private List<JointType> m_AllJoints;
        /// <summary>
        /// List of skeleton joint types
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
        /// Skeleton joint type
        /// </summary>
        public JointType Joint
        {
            get { return m_midiCC.Joint; }
            set { m_midiCC.Joint = value; }
        }

        public const string TextProperty = "Text";
        /// <summary>
        /// Caption text
        /// </summary>
        public string Text
        {
            get { return string.Format("ch:{0} cc:{1}", Channel, CC); }
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

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
using MidiNoteSpace = KinectToMidi.Models.MidiNote;

namespace KinectToMidi.ViewModels
{
    /// <summary>
    /// MIDI Note view model class
    /// </summary>
    public class MidiNoteViewModel : ViewModelBase, IRemovable
    {
        private MidiNote m_midiNote;
        /// <summary>
        /// Working constructor
        /// </summary>
        /// <param name="midiSettings">current midi settings</param>
        public MidiNoteViewModel(MidiNote midiNote)
        {
            RemoveCommand = new RelayCommand(() => Remove());

            m_midiNote = midiNote;
        }

        /// <summary>
        /// Constructor for designer
        /// </summary>
        public MidiNoteViewModel()
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
            get { return m_midiNote.SignalEventType; }
            set { m_midiNote.SignalEventType = value; }
        }

        private List<MidiNoteSpace.NoteStates> m_AllNoteStates;
        /// <summary>
        /// List of all note states
        /// </summary>
        public List<MidiNoteSpace.NoteStates> AllNoteStates
        {
            get
            {
                if(m_AllNoteStates == null)
                    m_AllNoteStates = new List<MidiNoteSpace.NoteStates>(
                        Enum.GetValues(typeof(MidiNoteSpace.NoteStates)).Cast<MidiNoteSpace.NoteStates>());
                return m_AllNoteStates;
            }
        }

        /// <summary>
        /// MIDI note state
        /// </summary>
        public MidiNoteSpace.NoteStates NoteState
        {
            get { return m_midiNote.NoteState; }
            set { m_midiNote.NoteState = value; RaisePropertyChanged(TextProperty); }
        }

        /// <summary>
        /// Midi note
        /// </summary>
        public byte Note
        {
            get { return m_midiNote.Note; }
            set { m_midiNote.Note = value; RaisePropertyChanged(TextProperty); }
        }

        /// <summary>
        /// Midi note velocity
        /// </summary>
        public byte Velocity
        {
            get { return m_midiNote.Velocity; }
            set { m_midiNote.Velocity = value; }
        }

        /// <summary>
        /// Midi channel number
        /// </summary>
        public byte Channel
        {
            get { return m_midiNote.Channel; }
            set { m_midiNote.Channel = value; RaisePropertyChanged(TextProperty); }
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

        public const string TextProperty = "Text";
        /// <summary>
        /// Caption text
        /// </summary>
        public string Text
        {
            get { return string.Format("ch:{0} note:{1}", Channel, Note); }
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

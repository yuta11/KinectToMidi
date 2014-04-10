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
    /// MIDI note
    /// </summary>
    [DataContract]
    [KnownType(typeof(BaseMidiSignal))]
    public class MidiNote: BaseMidiSignal
    {
        public MidiNote()
        {
            NoteState = NoteStates.On;
            Note = 30;
            Velocity = 127;
        }

        #region properties
        /// <summary>
        /// MIDI note state
        /// </summary>
        [DataMember]
        public NoteStates NoteState { get; set; }

        /// <summary>
        /// Midi note
        /// </summary>
        [DataMember]
        public byte Note { get; set; }

        /// <summary>
        /// Midi note velocity
        /// </summary>
        [DataMember]
        public byte Velocity { get; set; }
        #endregion properties

        #region public methods
        /// <summary>
        /// Send MIDI signal with using skeleton coordinations and math expressions
        /// </summary>
        public override void Send(Skeleton skeleton)
        {
            MidiSettings.MidiSettingsInstance.SendNote(Note, Velocity, Channel, NoteState);
        }
        #endregion public methods

        /// <summary>
        /// MIDI note states
        /// </summary>
        public enum NoteStates
        {
            On,
            Off
        }
    }
}

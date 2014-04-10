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
    /// Base MIDI signal class
    /// </summary>
    [DataContract]
    public abstract class BaseMidiSignal
    {
        /// <summary>
        /// MIDI Channel number
        /// </summary>
        [DataMember]
        public byte Channel { get; set; }

        /// <summary>
        /// MIDI signal event type
        /// </summary>
        [DataMember]
        public SignalEventTypes SignalEventType { get; set; }

        /// <summary>
        /// Send MIDI CC signal with using skeleton coordinations and math expressions
        /// </summary>
        public abstract void Send(Skeleton skeleton);
    }

    /// <summary>
    /// Signal event types
    /// </summary>
    public enum SignalEventTypes
    {
        /// <summary>
        /// fires when point is moved in a specified area
        /// </summary>
        In,

        /// <summary>
        /// fires when point is moved over a specified area
        /// </summary>
        Over,

        /// <summary>
        /// fires when point is moved out of a specified area
        /// </summary>
        Out
    }
}

using Microsoft.Kinect;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace KinectToMidi.Models
{
    /// <summary>
    /// Block of kinect conditions and midi signals
    /// </summary>
    [DataContract]
    [KnownType(typeof(CuboidCondition))]
    [KnownType(typeof(RelatedSphereCondition))]
    [KnownType(typeof(MidiCC))]
    [KnownType(typeof(MidiNote))]
    public class ConditionsBlock
    {
        public ConditionsBlock()
        {
            Conditions = new List<BaseCondition>();
            MidiSignals = new List<BaseMidiSignal>();
            Enabled = true;
            Name = "block";
        }

        #region preperties
        /// <summary>
        /// Conditions collection
        /// </summary>
        [DataMember]
        public List<BaseCondition> Conditions { get; set; }

        /// <summary>
        /// Collection of midi signals in the current block
        /// </summary>
        [DataMember]
        public List<BaseMidiSignal> MidiSignals { get; set; }

        /// <summary>
        /// Block name
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Is this block enabled
        /// </summary>
        [DataMember]
        public bool Enabled { get; set; }
        #endregion preperties

        #region public methods
        private bool m_CurrentStateIsIn = false;
        /// <summary>
        /// Check if the skeleton current state corresponds to the block of conditions and midi event types then send these midi signals
        /// </summary>
        public void HandleConditions(Skeleton skeleton)
        {
            //check conditions
            bool isin = Conditions.FirstOrDefault(c => !c.CheckCondition(skeleton)) == null;

            //check midi signal event type and send the signal
            foreach (BaseMidiSignal midiSignal in MidiSignals)
            {
                if ((isin && !m_CurrentStateIsIn && midiSignal.SignalEventType == SignalEventTypes.In)
                    || (isin && m_CurrentStateIsIn && midiSignal.SignalEventType == SignalEventTypes.Over)
                    || (!isin && m_CurrentStateIsIn && midiSignal.SignalEventType == SignalEventTypes.Out))
                {
                    midiSignal.Send(skeleton);
                }
            }

            m_CurrentStateIsIn = isin;
        }
        #endregion public methods
    }
}

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
    /// MIDI CC 
    /// </summary>
    [DataContract]
    [KnownType(typeof(BaseMidiSignal))]
    public class MidiCC: BaseMidiSignal
    {
        private ExpressionEvaluator m_expressionMin;
        private ExpressionEvaluator m_expressionMax;

        public MidiCC()
        {
            m_expressionMin = new ExpressionEvaluator();
            m_expressionMax = new ExpressionEvaluator();

            MinString = (0).ToString();
            Min = 0;
            MaxString = (127).ToString();
            Max = 127;
        }

        #region preperties
        /// <summary>
        /// MIDI Control Change
        /// </summary>
        [DataMember]
        public byte CC { get; set; }

        /// <summary>
        /// Minimum MIDI CC value expression string
        /// </summary>
        [DataMember]
        public string MinString { get; set; }

        /// <summary>
        /// Minimum MIDI CC value
        /// </summary>
        [DataMember]
        public byte Min { get; set; }

        /// <summary>
        /// Maximum MIDI CC value expression string
        /// </summary>
        [DataMember]
        public string MaxString { get; set; }

        /// <summary>
        /// Maximum MIDI CC value
        /// </summary>
        [DataMember]
        public byte Max { get; set; }

        /// <summary>
        /// Skeleton joint type
        /// </summary>
        [DataMember]
        public JointType Joint { get; set; }

                /// <summary>
        /// MIDI signal event type
        /// </summary>
        [DataMember]
        public SignalEventTypes SignalEventType { get; set; }
        #endregion preperties

        #region public methods
        /// <summary>
        /// Send MIDI CC signal with using skeleton coordinations and math expressions
        /// </summary>
        public override void Send(Skeleton skeleton)
        {
            var sp = skeleton.Joints[Joint].Position;

            m_expressionMin.InitDynamicExpression(MinString);
            m_expressionMax.InitDynamicExpression(MaxString);

            Min = m_expressionMin.GetValue(sp.X, sp.Y, sp.Z, MinString, Min);
            Max = m_expressionMax.GetValue(sp.X, sp.Y, sp.Z, MaxString, Max);

            MidiSettings.MidiSettingsInstance.SendCC(CC, Min, Max, Channel);
        }
        #endregion public methods
    }
}

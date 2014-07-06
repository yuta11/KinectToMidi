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

        private string m_MinString;
        /// <summary>
        /// Minimum MIDI CC value expression string
        /// </summary>
        [DataMember]
        public string MinString {
            get { return m_MinString; }
            set
            {
                m_MinString = value;
                if(m_expressionMin != null)
                    m_expressionMin.InitDynamicExpression(m_MinString);
            }
        }

        /// <summary>
        /// Minimum MIDI CC value
        /// </summary>
        [DataMember]
        public byte Min { get; set; }

        private string m_MaxString;
        /// <summary>
        /// Maximum MIDI CC value expression string
        /// </summary>
        [DataMember]
        public string MaxString {
            get { return m_MaxString; }
            set
            {
                m_MaxString = value;
                if(m_expressionMax != null)
                    m_expressionMax.InitDynamicExpression(MaxString);
            } 
        }

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

        #endregion preperties

        #region public methods
        /// <summary>
        /// Send MIDI CC signal with using skeleton coordinations and math expressions
        /// </summary>
        public override void Send(Skeleton skeleton)
        {
            var sp = skeleton.Joints[Joint].Position;
            byte min, max;

            if (byte.TryParse(MinString, out min))
                Min = min;
            else
            {
                if (m_expressionMin != null)
                    m_expressionMin.InitDynamicExpression(m_MinString);
                Min = m_expressionMin.GetValue(sp.X, sp.Y, sp.Z, MinString, Min);
            }

            if (byte.TryParse(MaxString, out max))
                Max = max;
            else
            {
                if (m_expressionMax != null)
                    m_expressionMax.InitDynamicExpression(MaxString);
                Max = m_expressionMax.GetValue(sp.X, sp.Y, sp.Z, MaxString, Max);
            }

            MidiSettings.MidiSettingsInstance.SendCC(CC, Min, Max, Channel);
        }
        #endregion public methods
    }
}

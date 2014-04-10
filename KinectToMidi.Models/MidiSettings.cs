using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace KinectToMidi.Models
{
    /// <summary>
    /// Working with midi devices
    /// </summary>
    public class MidiSettings
    {
        private MIDIWrapper.Instrument m_MidiInstrument;

        public MidiSettings()
        {
            m_MidiInstrument = new MIDIWrapper.Instrument();
            m_MidiInstrument.OutputChannel = 0;
        }

        #region static
        private static MidiSettings m_midiSettingsInstance;
        /// <summary>
        /// Instance of MidiSettings class
        /// </summary>
        public static MidiSettings MidiSettingsInstance
        {
            get
            {
                if (m_midiSettingsInstance == null)
                    m_midiSettingsInstance = new MidiSettings();
                return MidiSettings.m_midiSettingsInstance;
            }
        }
        #endregion static

        #region public methods
        /// <summary>
        /// Closes MIDI port if opened
        /// </summary>
        public void ClosePort()
        {
            if (m_MidiInstrument.Engaged)
            {
                m_MidiInstrument.Close();
            }
        }

        /// <summary>
        /// Opens MIDI specified port
        /// </summary>
        /// <param name="portName"></param>
        /// <returns></returns>
        public bool OpenPort(string portName)
        {
            m_MidiInstrument.OutputDeviceName = portName;

            try
            {
                m_MidiInstrument.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Send MIDI note signal
        /// </summary>
        public void SendNote(byte note, byte velocity, byte channel, MidiNote.NoteStates noteState)
        {
            if (noteState == MidiNote.NoteStates.On)
                m_MidiInstrument.PlayNote(channel, note, velocity);
            else
                m_MidiInstrument.StopNote(channel, note, velocity);
        }

        /// <summary>
        /// Send MIDI CC signal
        /// </summary>
        public void SendCC(byte CC, byte min, byte max, byte channel)
        {
            byte Max = max;
            m_MidiInstrument.SendControllerChange(channel, CC, ref max, min);
        }
        #endregion public methodsregion public methods
    }
}

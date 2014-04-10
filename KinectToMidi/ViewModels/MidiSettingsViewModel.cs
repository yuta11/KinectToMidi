using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using MIDIWrapper;
using KinectToMidi.Models;

namespace KinectToMidi.ViewModels
{
    /// <summary>
    /// ViewModel for MIDI settings
    /// </summary>
    public class MidiSettingsViewModel
    {
        public MidiSettingsViewModel()
        {
            if (!GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                RefreshOutDeviceNames();
                if (OutDeviceNames.Count > 0)
                {
                    CurrentOutDeviceName = OutDeviceNames[0];
                }
            }
        }

        #region public properties
        private ObservableCollection<string> m_OutDeviceNames;
        /// <summary>
        /// Output MIDI devices
        /// </summary>
        public ObservableCollection<string> OutDeviceNames
        {
            get { return m_OutDeviceNames; }
            set { m_OutDeviceNames = value; }
        }

        private string m_CurrentOutDeviceName;
        /// <summary>
        /// Name of current output device name
        /// </summary>
        public string CurrentOutDeviceName
        {
            get { return m_CurrentOutDeviceName; }
            set
            {
                if (m_CurrentOutDeviceName != value)
                {
                    m_CurrentOutDeviceName = value;
                    //close the previous device if opened
                    MidiSettings.MidiSettingsInstance.ClosePort();

                    if (MidiSettings.MidiSettingsInstance.OpenPort(value))
                        ViewModelLocator.HideWarning();
                    else
                        ViewModelLocator.ShowWarning(Properties.Resources.MidiErrorText);
                }
            }
        }
        #endregion public properties

        #region public methods
        /// <summary>
        /// Refresh list of output devices' names
        /// </summary>
        public void RefreshOutDeviceNames()
        {
            string[] devNames = Instrument.OutDeviceNames();
            m_OutDeviceNames = new ObservableCollection<string>(devNames);
        }
        #endregion public methods
    }
}

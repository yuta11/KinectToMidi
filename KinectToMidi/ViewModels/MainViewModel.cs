using GalaSoft.MvvmLight;
using KinectToMidi.Models;
using KinectToMidi.Properties;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace KinectToMidi.ViewModels
{
    /// <summary>
    /// Main view model class that corresponds to a main window/control
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private KinectSensorChooser m_sensorChooser;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MainViewModel()
        {
            #region ralay command initialization
            RefreshMidiDevicesCommand = new RelayCommand(() =>
            {
                MidiSettingsVM.RefreshOutDeviceNames();
            });

            SaveCommand = new RelayCommand(() =>
            {
                string fileName;
                if (ViewModelLocator.UIService.ShowSaveFileDialog("BlockSet", ".xml", "xml files (.xml)|*.xml", out fileName) == true)
                    SerializationTools.WriteObject(fileName, BlocksSetVM.BlocksSet);
            });

            LoadCommand = new RelayCommand(() =>
            {
                LoadProject();
            });

            ShowVideoWindow = new RelayCommand(() => ViewModelLocator.UIService.ShowVideoWindow());

            ShowJointsMap = new RelayCommand(() => ViewModelLocator.UIService.ShowJointsMap());
            #endregion ralay command initialization

            BlocksSetVM = new ConditionsMappingSetViewModel(new BlocksSet());
            m_sensorChooser = new KinectSensorChooser();
            if (!GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
                Init();
        }

        #region properties
        private bool m_warningShowed;
        /// <summary>
        /// True if warning is showed
        /// </summary>
        public bool WarningShowed
        {
            get { return m_warningShowed; }
            set
            {
                m_warningShowed = value;
                RaisePropertyChanged("WarningShowed");
            }
        }

        private string m_warningText;
        /// <summary>
        /// Warning text
        /// </summary>
        public string WarningText
        {
            get { return m_warningText; }
            set
            {
                m_warningText = value;
                RaisePropertyChanged("WarningText");
            }
        }

        MidiSettingsViewModel m_midiSettingsVM;
        /// <summary>
        /// Midi settings
        /// </summary>
        public MidiSettingsViewModel MidiSettingsVM
        {
            get
            {
                if (m_midiSettingsVM == null)
                    m_midiSettingsVM = new MidiSettingsViewModel();
                return m_midiSettingsVM;
            }
        }

        /// <summary>
        /// Sensor chooser is used to automate finding and updating of Kinect sensor
        /// </summary>
        public KinectSensorChooser SensorChooser
        {
            get { return m_sensorChooser; }
            set { m_sensorChooser = value; }
        }

        private ConditionsMappingSetViewModel m_blocksSetVM;
        /// <summary>
        /// Set of conditions and midi signals
        /// </summary>
        public ConditionsMappingSetViewModel BlocksSetVM
        {
            get
            {
                return m_blocksSetVM;
            }
            set
            {
                m_blocksSetVM = value;
                RaisePropertyChanged("BlocksSetVM");
            }
        }

        private string m_coordianates;
        /// <summary>
        /// Current coordinates of right hand
        /// </summary>
        public string RightHandCoordianates
        {
            get { return m_coordianates; }
            set
            {
                m_coordianates = value;
                RaisePropertyChanged("RightHandCoordianates");
            }
        }
        #endregion properties

        #region relay commands
        /// <summary>
        /// Command to refresh a midi device list
        /// </summary>
        public RelayCommand RefreshMidiDevicesCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Command to save set
        /// </summary>
        public RelayCommand SaveCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Command to load set
        /// </summary>
        public RelayCommand LoadCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Command to add condition
        /// </summary>
        public RelayCommand ShowVideoWindow
        {
            get;
            private set;
        }

        /// <summary>
        /// Command to show joints map
        /// </summary>
        public RelayCommand ShowJointsMap
        {
            get;
            private set;
        }
        #endregion relay commands

        #region private methods
        /// <summary>
        /// Get first skeleton from all available
        /// </summary>
        /// <param name="e"></param>
        /// <returns>returns first skeleton object</returns>
        private Skeleton GetFirtstSkeleton(AllFramesReadyEventArgs e)
        {
            using (SkeletonFrame skeletonFrameData = e.OpenSkeletonFrame())
            {
                if (skeletonFrameData == null)
                    return null;

                //get all skeletons
                Skeleton[] allSkeletons = new Skeleton[skeletonFrameData.SkeletonArrayLength];
                skeletonFrameData.CopySkeletonDataTo(allSkeletons);

                //get the first from all available
                Skeleton first = allSkeletons.ToList().FirstOrDefault(s => s.TrackingState == SkeletonTrackingState.Tracked);

                return first;
            }
        }

        /// <summary>
        /// Handles all conditions-midi blocks to send midi signals
        /// </summary>
        private void HandleBlocks(Skeleton skeleton)
        {
            m_blocksSetVM.HandleBlocks(skeleton);
        }

        /// <summary>
        /// Initialize Kinect objects
        /// </summary>
        private void Init()
        {
            //subscribe to Kinect changed event
            m_sensorChooser.KinectChanged += (sender, e) =>
                {
                    if (e.OldSensor != null)
                        e.OldSensor.Stop();

                    if (e.NewSensor != null)
                    {
                        e.NewSensor.ColorStream.Enable();
                        e.NewSensor.DepthStream.Enable();
                        e.NewSensor.SkeletonStream.Enable();
                        //e.NewSensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated; //TEMP

                        //that fires everytime when skeleton coordinates are ready
                        e.NewSensor.AllFramesReady += (s, args) =>
                            {
                                Skeleton first = GetFirtstSkeleton(args);
                                if (first == null)
                                {
                                    return;
                                }

                                //get a right hand coordinates
                                SkeletonPoint p = first.Joints[JointType.HandRight].Position;
                                RightHandCoordianates = string.Format("{0}, {1}, {2}", p.X, p.Y, p.Z);

                                //for each new coordinates update handle conditions-midi blocks
                                Parallel.Invoke(() => HandleBlocks(first));
                            };

                        e.NewSensor.Start();
                    }
                };

            m_sensorChooser.Start();
        }

        private void LoadProject()
        {
            string fileName;
            if (ViewModelLocator.UIService.ShowOpenFileDialog(".xml", "xml files (.xml)|*.xml", out fileName) == true)
            {
                var blocksSet = SerializationTools.ReadObject(fileName);
                if (blocksSet == null)
                    WarningHelper.ShowWarning(Resources.WarningMessage_CannotLoadProject);
                else
                {
                    WarningHelper.HideWarning();
                    var blocksSetVM = new ConditionsMappingSetViewModel(blocksSet);
                    BlocksSetVM = blocksSetVM;
                }
            }
        }
        #endregion private methods
    }
}

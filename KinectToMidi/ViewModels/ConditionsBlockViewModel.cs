using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Microsoft.Kinect;
using MIDIWrapper;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;
using KinectToMidi.Models;

namespace KinectToMidi.ViewModels
{
    /// <summary>
    /// ViewModel for conditions-midi block
    /// </summary>
    public class ConditionsBlockViewModel : ViewModelBase
    {
        private ConditionsBlock m_conditionsBlock;

        /// <summary>
        /// Working constructor
        /// </summary>
        /// <param name="midiSettings"></param>
        public ConditionsBlockViewModel(ConditionsBlock block)
        {
            m_conditionsBlock = block;
            Init();
        }

        /// <summary>
        /// Constructor for designer
        /// </summary>
        public ConditionsBlockViewModel()
            : this(new ConditionsBlock())
        {
            Init();
        }

        #region preperties
        private ObservableCollection<IRemovable> m_Conditions;
        /// <summary>
        /// Conditions collection
        /// </summary>
        public ObservableCollection<IRemovable> Conditions
        {
            get { return m_Conditions; }
            set { m_Conditions = value; }
        }

        private List<ConditionTypes> m_AllConditionTypes;
        /// <summary>
        /// List of all available condition types
        /// </summary>
        public List<ConditionTypes> AllConditionTypes
        {
            get
            {
                if (m_AllConditionTypes == null)
                {
                    m_AllConditionTypes = new List<ConditionTypes>(Enum.GetValues(typeof(ConditionTypes)).Cast<ConditionTypes>());
                }
                return m_AllConditionTypes;
            }
        }

        private ConditionTypes m_SelectedConditionType;
        /// <summary>
        /// Selected condition type
        /// </summary>
        public ConditionTypes SelectedConditionType
        {
            get { return m_SelectedConditionType; }
            set { m_SelectedConditionType = value; }
        }

        private ObservableCollection<IRemovable> m_MidiSignals;
        /// <summary>
        /// Collection of midi signals in the current block
        /// </summary>
        public ObservableCollection<IRemovable> MidiSignals
        {
            get { return m_MidiSignals; }
            set { m_MidiSignals = value; }
        }

        private List<MidiSignalTypes> m_AllMidiSignalTypes;
        /// <summary>
        /// List of all available midi signal types
        /// </summary>
        public List<MidiSignalTypes> AllMidiSignalTypes
        {
            get
            {
                if (m_AllMidiSignalTypes == null)
                    m_AllMidiSignalTypes = new List<MidiSignalTypes>(Enum.GetValues(typeof(MidiSignalTypes)).Cast<MidiSignalTypes>());
                return m_AllMidiSignalTypes;
            }
        }

        private MidiSignalTypes m_SelectedMidiSignalType = MidiSignalTypes.MidiCC;
        /// <summary>
        /// Selected midi signal type
        /// </summary>
        public MidiSignalTypes SelectedMidiSignalType
        {
            get { return m_SelectedMidiSignalType; }
            set { m_SelectedMidiSignalType = value; }
        }

        /// <summary>
        /// Block name
        /// </summary>
        public string Name
        {
            get
            {
                return m_conditionsBlock.Name;
            }
            set
            {
                m_conditionsBlock.Name = value;
                RaisePropertyChanged("Name");
            }
        }

        /// <summary>
        /// Is this block enabled
        /// </summary>
        public bool Enabled
        {
            get { return m_conditionsBlock.Enabled; }
            set
            {
                m_conditionsBlock.Enabled = value;
                RaisePropertyChanged("Enabled");
            }
        }

        /// <summary>
        /// Delegate to remove item from parent collection
        /// </summary>
        public Action DelegateToRemove { get; set; }
        #endregion preperties

        #region relay commands
        /// <summary>
        /// Command to add condition
        /// </summary>
        public RelayCommand<ConditionTypes> AddConditionCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Command to add midi
        /// </summary>
        public RelayCommand<MidiSignalTypes> AddMidiCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Command to remove
        /// </summary>
        public RelayCommand RemoveCommand
        {
            get;
            private set;
        }
        #endregion relay commands

        #region private methods
        /// <summary>
        /// Initialize properties, first conditions and midi signals
        /// </summary>
        private void Init()
        {
            AddConditionCommand = new RelayCommand<ConditionTypes>(condType => AddCondition(condType, null));
            AddMidiCommand = new RelayCommand<MidiSignalTypes>(midiType => AddMidi(midiType, null));
            RemoveCommand = new RelayCommand(() => Remove());

            //Init child conditions
            Conditions = new ObservableCollection<IRemovable>();
            if (m_conditionsBlock.Conditions.Count > 0)
            {
                foreach (var item in m_conditionsBlock.Conditions)
                {
                    AddCondition(item is CuboidCondition? 
                        ConditionTypes.SkeletonPointToCordinate: 
                        ConditionTypes.SkeletonPointToSkeletonPoint, item);
                }
            }
            else
            {
                //add first conditions
                AddCondition(ConditionTypes.SkeletonPointToCordinate, null);
                AddCondition(ConditionTypes.SkeletonPointToSkeletonPoint, null);
            }

            //Init child midi signals
            MidiSignals = new ObservableCollection<IRemovable>();
            if (m_conditionsBlock.MidiSignals.Count > 0)
            {
                foreach (var item in m_conditionsBlock.MidiSignals)
                {
                    AddMidi(item is MidiCC ?
                        MidiSignalTypes.MidiCC :
                        MidiSignalTypes.MidiNote, item);
                }
            }
            else
            {
                //add first midi signals
                AddMidi(MidiSignalTypes.MidiCC, null);
                AddMidi(MidiSignalTypes.MidiNote, null);
            }
        }

        /// <summary>
        /// Remove block from somewhere
        /// </summary>
        private void Remove()
        {
            if (DelegateToRemove != null)
                DelegateToRemove();
        }

        /// <summary>
        /// Add condition
        /// </summary>
        /// <param name="conditionType">type of condition</param>
        private void AddCondition(ConditionTypes conditionType, BaseCondition condition)
        {
            IRemovable conditionVM = null;
            switch (conditionType)
            {
                case ConditionTypes.SkeletonPointToCordinate:
                    if (condition == null)
                    {
                        condition = new CuboidCondition();
                        m_conditionsBlock.Conditions.Add(condition);
                    }
                    conditionVM = new SkeletonPointToCubeViewModel((CuboidCondition)condition);
                    break;
                case ConditionTypes.SkeletonPointToSkeletonPoint:
                    if (condition == null)
                    {
                        condition = new RelatedSphereCondition();
                        m_conditionsBlock.Conditions.Add(condition);
                    }
                    conditionVM = new SkeletonPointToSkeletonPointViewModel((RelatedSphereCondition)condition);
                    break;
            }

            if (conditionVM != null)
            {
                Conditions.Add(conditionVM);

                //add remove delegate
                conditionVM.DelegateToRemove = () =>
                {
                    m_conditionsBlock.Conditions.Remove(condition);
                    Conditions.Remove(conditionVM);
                };
            }
        }

        /// <summary>
        /// Add midi signal
        /// </summary>
        /// <param name="midiType">type of midi signal</param>
        private void AddMidi(MidiSignalTypes midiType, BaseMidiSignal midiSignal)
        {
            IRemovable midiVM = null;
            switch (midiType)
            {
                case MidiSignalTypes.MidiCC:
                    if (midiSignal == null)
                    {
                        midiSignal = new MidiCC();
                        m_conditionsBlock.MidiSignals.Add(midiSignal);
                    }
                    midiVM = new MidiCCViewModel((MidiCC)midiSignal);
                    break;
                case MidiSignalTypes.MidiNote:
                    if (midiSignal == null)
                    {
                        midiSignal = new MidiNote();
                        m_conditionsBlock.MidiSignals.Add(midiSignal);
                    }
                    midiVM = new MidiNoteViewModel((MidiNote)midiSignal);
                    break;
            }

            if (midiVM != null)
            {
                MidiSignals.Add(midiVM);

                //add remove delegate
                midiVM.DelegateToRemove = () =>
                {
                    m_conditionsBlock.MidiSignals.Remove(midiSignal);
                    MidiSignals.Remove(midiVM);
                };
            }
        }
        #endregion private methods
    }
}

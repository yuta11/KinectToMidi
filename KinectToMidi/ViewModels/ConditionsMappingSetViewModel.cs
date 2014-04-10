using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Kinect;
using MIDIWrapper;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;
using KinectToMidi.Models;

namespace KinectToMidi.ViewModels
{
    /// <summary>
    /// View model for block's list
    /// </summary>
    public class ConditionsMappingSetViewModel : ViewModelBase
    {
        public ConditionsMappingSetViewModel(BlocksSet blocksSet)
        {
            AddBlockCommand = new RelayCommand(() => AddBlock(null));
     
            ConditionsBlocks = new ObservableCollection<ConditionsBlockViewModel>();
            m_blocksSet = blocksSet;

            //init childs
            if (blocksSet.ConditionsBlocks.Count > 0)
            {
                foreach (var item in blocksSet.ConditionsBlocks)
                {
                    AddBlock(item);
                }
            }
            else
            {
                //add first block
                AddBlock(null);
            }
        }

        #region properties
        private BlocksSet m_blocksSet;
        /// <summary>
        /// Blocks' set
        /// </summary>
        public BlocksSet BlocksSet
        {
            get { return m_blocksSet; }
        }

        private ObservableCollection<ConditionsBlockViewModel> m_ConditionsBlocks;
        /// <summary>
        /// Collection of all conditions-midi blocks
        /// </summary>
        public ObservableCollection<ConditionsBlockViewModel> ConditionsBlocks
        {
            get { return m_ConditionsBlocks; }
            set { m_ConditionsBlocks = value; }
        }
        #endregion properties

        #region relay commands
        /// <summary>
        /// Command to add a block
        /// </summary>
        public RelayCommand AddBlockCommand
        {
            get;
            private set;
        }
        #endregion relay commands

        #region public methods
        /// <summary>
        /// Handle all blocks and send midi signals if that is required
        /// </summary>
        public void HandleBlocks(Skeleton skeleton)
        {
            m_blocksSet.HandleBlocks(skeleton);
        }
        #endregion public methods

        #region private methods
        /// <summary>
        /// Add new block with default settings to colletion
        /// </summary>
        private void AddBlock(ConditionsBlock conditionsBlock)
        {
            string name = null;
            if (conditionsBlock == null)
            {
                conditionsBlock = new ConditionsBlock();
                m_blocksSet.ConditionsBlocks.Add(conditionsBlock);
                name = conditionsBlock.Name + ConditionsBlocks.Count.ToString();
            }

            var condBlockVM = new ConditionsBlockViewModel(conditionsBlock);
            ConditionsBlocks.Add(condBlockVM);

            if(name != null)
                condBlockVM.Name = name;

            //implement remove delegate
            condBlockVM.DelegateToRemove = () =>
            {
                m_blocksSet.ConditionsBlocks.Remove(conditionsBlock);
                ConditionsBlocks.Remove(condBlockVM);
            };
        }
        #endregion private methods
    }
}

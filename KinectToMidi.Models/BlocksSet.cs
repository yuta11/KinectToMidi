using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace KinectToMidi.Models
{
    /// <summary>
    /// This class handles all conditons-midi blocks
    /// </summary>
    public class BlocksSet
    {
        public BlocksSet()
        {
            ConditionsBlocks = new List<ConditionsBlock>();
        }

        #region properties
        /// <summary>
        /// Collection of all conditions-midi blocks
        /// </summary>
        [DataMember]
        public List<ConditionsBlock> ConditionsBlocks { get; set; }
        #endregion properties

        /// <summary>
        /// Handle all blocks and send midi signals if that is required
        /// </summary>
        public void HandleBlocks(Skeleton skeleton)
        {
            foreach (var item in ConditionsBlocks)
            {
                if (item.Enabled)
                    item.HandleConditions(skeleton);
            }
        }
    }
}

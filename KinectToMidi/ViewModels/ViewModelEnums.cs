using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectToMidi.ViewModels
{
    /// <summary>
    /// All condition types
    /// </summary>
    public enum ConditionTypes
    {
        [Helpers.LocalizedDescriptionAttributre("SkeletonPointToCordinate", typeof(Properties.Resources))]
        /// <summary>
        /// Comparing a skeleton point coordinates against a coordinates in a area
        /// </summary>
        SkeletonPointToCordinate,

        [Helpers.LocalizedDescriptionAttributre("SkeletonPointToSkeletonPoint", typeof(Properties.Resources))]
        /// <summary>
        /// Comparing a skeleton poing coordinates against other skeleton point coordinate
        /// </summary>
        SkeletonPointToSkeletonPoint
    }

    /// <summary>
    /// Midi signal types
    /// </summary>
    public enum MidiSignalTypes
    {
        [Helpers.LocalizedDescriptionAttributre("MidiCC", typeof(Properties.Resources))]
        /// <summary>
        /// CC midi signals
        /// </summary>
        MidiCC,

        [Helpers.LocalizedDescriptionAttributre("MidiNote", typeof(Properties.Resources))]
        /// <summary>
        /// Note midi signals
        /// </summary>
        MidiNote
    }
}

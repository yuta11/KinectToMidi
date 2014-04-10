using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Kinect;

namespace KinectToMidi.ViewModels
{
    public interface IRemovable
    {
        /// <summary>
        /// Delegate for removing condition object that should be implemented by the owner
        /// </summary>
        Action DelegateToRemove { get; set; }
    }
}

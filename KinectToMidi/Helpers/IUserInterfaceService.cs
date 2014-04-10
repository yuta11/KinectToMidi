using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectToMidi.Helpers
{
    /// <summary>
    /// All UI functionality that can be used from a view model
    /// </summary>
    public interface IUserInterfaceService
    {
        /// <summary>
        /// Show Save file dialog
        /// </summary>
        /// <param name="defaultExtension"></param>
        /// <param name="filterString"></param>
        /// <param name="fileName"></param>
        /// <returns>true if accepted</returns>
        bool? ShowSaveFileDialog(string defaultFileName, string defaultExtension,
                    string filterString, out string fileName);

        /// <summary>
        /// Show Open file dialog
        /// </summary>
        /// <param name="defaultExtension"></param>
        /// <param name="filterString"></param>
        /// <param name="fileName"></param>
        /// <returns>true if accepted</returns>
        bool? ShowOpenFileDialog(string defaultExtension,
            string filterString, out string fileName);

        /// <summary>
        /// Open a window with Kinect output
        /// </summary>
        void ShowVideoWindow();

        /// <summary>
        /// Show message box
        /// </summary>
        void ShowMessageBox(string message);

        /// <summary>
        /// Show skeleton map
        /// </summary>
        void ShowJointsMap();
    }
}

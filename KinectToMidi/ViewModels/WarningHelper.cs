using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;

namespace KinectToMidi.ViewModels
{
    /// <summary>
    /// Warning visualisation utils
    /// </summary>
    public class WarningHelper
    {
        /// <summary>
        /// Show warning message
        /// </summary>
        public static void ShowWarning(string text)
        {
            var mainVM = ServiceLocator.Current.GetInstance<MainViewModel>();
            if (mainVM != null)
            {
                if (!mainVM.WarningShowed)
                    mainVM.WarningShowed = true;
                mainVM.WarningText = text;
            }
        }

        /// <summary>
        /// Hide warning message
        /// </summary>
        public static void HideWarning()
        {
            var mainVM = ServiceLocator.Current.GetInstance<MainViewModel>();
            if (mainVM != null && mainVM.WarningShowed)
                mainVM.WarningShowed = false;
        }
    }
}

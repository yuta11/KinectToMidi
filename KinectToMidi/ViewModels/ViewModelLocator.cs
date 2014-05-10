using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using KinectToMidi.Helpers;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Diagnostics;
using System.IO;
using System.Xml;

namespace KinectToMidi.ViewModels
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
        }

        /// <summary>
        /// Provides access to UIService functionality
        /// </summary>
        public static IUserInterfaceService UIService
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the Main ViewModel instance.
        /// </summary>
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public void Cleanup()
        {
            Main.SensorChooser.Stop();
        }
    }
}
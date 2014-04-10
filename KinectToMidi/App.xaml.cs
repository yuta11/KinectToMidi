using GalaSoft.MvvmLight.Threading;
using KinectToMidi.Helpers;
using KinectToMidi.ViewModels;
using KinectToMidi.Views;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace KinectToMidi
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, IUserInterfaceService
    {
        const string m_vmLocatorDSName = "ViewModelLocatorDataSource";
        public App()
        {
            Startup += Application_Startup;
            Exit += Application_Exit;
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            DispatcherHelper.Initialize();
            ViewModelLocator.UIService = this;
        }

        private void Application_Exit(object sender, EventArgs e)
        {
            ViewModelLocator vmLocator = this.FindResource(m_vmLocatorDSName) as ViewModelLocator;
            if(vmLocator != null)
                vmLocator.Cleanup();
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var recordFormat = "{0}: {1}";
            var error = string.Format(recordFormat, e.Exception.Message, e.Exception.StackTrace);
            Exception exception;
            while (e.Exception.InnerException != null)
            {
                exception = e.Exception.InnerException;
                error += Environment.NewLine +
                    string.Format(recordFormat, exception.Message, exception.StackTrace);
            }
            var errorWindow = new ErrorWindow(error).ShowDialog();
        }

        #region IUserInterfaceService
        /// <summary>
        /// Show Save file dialog
        /// </summary>
        /// <param name="defaultExtension"></param>
        /// <param name="filterString"></param>
        /// <param name="fileName"></param>
        /// <returns>true if accepted</returns>
        public bool? ShowSaveFileDialog(string defaultFileName, string defaultExtension,
            string filterString, out string fileName)
        {
            SaveFileDialog svDlg = new SaveFileDialog();
            svDlg.FileName = defaultFileName;
            svDlg.DefaultExt = defaultExtension;
            svDlg.Filter = filterString;
            bool? res = svDlg.ShowDialog();
            fileName = svDlg.FileName;
            return res;
        }

        /// <summary>
        /// Show Open file dialog
        /// </summary>
        /// <param name="defaultExtension"></param>
        /// <param name="filterString"></param>
        /// <param name="fileName"></param>
        /// <returns>true if accepted</returns>
        public bool? ShowOpenFileDialog(string defaultExtension,
            string filterString, out string fileName)
        {
            OpenFileDialog ofDlg = new OpenFileDialog();
            ofDlg.DefaultExt = defaultExtension;
            ofDlg.Filter = filterString;
            bool? res = ofDlg.ShowDialog();
            fileName = ofDlg.FileName;
            return res;
        }

        /// <summary>
        /// Open a window with Kinect output
        /// </summary>
        public void ShowVideoWindow()
        {
               VideoWindow m_videoWindow = new VideoWindow();
            m_videoWindow.Show();
        }

        /// <summary>
        /// Show message box
        /// </summary>
        public void ShowMessageBox(string message)
        {
            MessageBox.Show(message);
        }

        private static JointsMapWindow m_jointsMapWindow;
        /// <summary>
        /// Open a window with Kinect output
        /// </summary>
        public void ShowJointsMap()
        {
            if (m_jointsMapWindow == null)
                m_jointsMapWindow = new JointsMapWindow();
            m_jointsMapWindow.Show();
        }
        #endregion IUserInterfaceService
    }
}

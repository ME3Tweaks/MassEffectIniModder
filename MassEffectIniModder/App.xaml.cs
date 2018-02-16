/*=============================================
Copyright (c) 2018 ME3Tweaks
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
=============================================*/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MassEffectIniModder
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        /// <summary>
        /// Application Entry Point.
        /// </summary>
        [STAThread]
        public static void Main()
        {

            string[] args = Environment.GetCommandLineArgs();
            var executinglocation = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            if (args.Length == 2 && args[1] == "-updatemode")
            {
                string updateErrorMessage = "";
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        DirectoryInfo dir = new DirectoryInfo(executinglocation);
                        FileInfo[] files = dir.GetFiles();
                        foreach (FileInfo file in files)
                        {
                            string temppath = Path.Combine(executinglocation + "\\..", file.Name);
                            file.CopyTo(temppath, true);
                        }
                        Process.Start(executinglocation + "\\..\\MassEffectIniModder.exe");
                        Environment.Exit(0);
                    }
                    catch (Exception e)
                    {
                        updateErrorMessage = e.Message;
                        System.Threading.Thread.Sleep(500);
                    }
                }
                MessageBox.Show("Error occured while updating: " + updateErrorMessage+"\nIf this issue continues, you may need to manually download a new copy of this program.", "Update error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }
            if (Directory.Exists(executinglocation + "\\Update"))
            {
                try
                {
                    Directory.Delete(executinglocation + "\\Update", true);
                }
                catch (Exception) { }
            }

            //normal boot
            var application = new App();
            application.Dispatcher.UnhandledException += OnDispatcherUnhandledException;
            application.InitializeComponent();
            application.Run();
        }

        static void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            string errorMessage = string.Format("MassEffectIniModder has crashed! Stacktrace:\n\n");
            string st = FlattenException(e.Exception);
            MessageBox.Show(errorMessage + st, "Application crash", MessageBoxButton.OK, MessageBoxImage.Error);
            //MetroDial.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //e.Handled = true;
        }

        public static string FlattenException(Exception exception)
        {
            var stringBuilder = new StringBuilder();

            while (exception != null)
            {
                stringBuilder.AppendLine(exception.Message);
                stringBuilder.AppendLine(exception.StackTrace);

                exception = exception.InnerException;
            }

            return stringBuilder.ToString();
        }

    }
}

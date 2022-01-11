using System;
using System.Reflection;
using System.Windows.Forms;

namespace ImageStack
{
    static class Program
    {
        public static string Name
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Name;
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // make sure the application actually dies when an unhandled exception turns up
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);

            // In the case of an unhandled exception, run the following procedure (turned off in
            // debug for easier tracking)
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            Application.Run(new SelectionForm());
        }

        /// <summary>
        /// Triggered on unhandled exception. Writes the the log then closes the program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;

            // Write exception details the log file
            Logger.Write(ex);

            MessageBox.Show(
                "An unhandled error occured. The program will now close.\n" +
                "Full details have been written to the log.\n" +
                "Message:\n" + ex.Message,
                Name + ": Unhandled Exception",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
                );

            // stop the program
            Environment.Exit(1);
        }
    }
}

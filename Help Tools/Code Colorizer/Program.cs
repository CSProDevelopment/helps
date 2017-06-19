using System;
using System.Windows.Forms;

namespace Code_Colorizer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

			Array commandArgs = Environment.GetCommandLineArgs();

			if( commandArgs.Length == 2 && ((string)commandArgs.GetValue(1)).Equals("/copy") )
				Application.Run(new DesignerCopierForm());

			else
	            Application.Run(new MainForm());
        }
    }
}

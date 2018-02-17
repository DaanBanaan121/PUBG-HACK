using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BATTLEGROUNDS_EXERNAL
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            if (Environment.GetCommandLineArgs().Length < 2)
            {
                MessageBox.Show("No handle");
                return;
            }
            // pExceptionHandler
            AppDomain.CurrentDomain.UnhandledException += (object sender, UnhandledExceptionEventArgs e) => MessageBox.Show(e.ExceptionObject.ToString());

            // Handle from hLeaker
            G.hProcess = (IntPtr)Convert.ToUInt32(Environment.GetCommandLineArgs()[1]);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CheatForm());
        }
    }
}

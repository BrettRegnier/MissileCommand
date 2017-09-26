using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Missile_Command___Final
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainMenuForm());
                //Application.Run(new CommandForm(1));
                //Application.Run(new Form1());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went horribly wrong - " + ex.Message);
            }
        }
    }
}

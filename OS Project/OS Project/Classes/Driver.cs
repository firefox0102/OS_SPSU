using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using OS_Project.Classes;

namespace OS_Project
{
    static class Driver
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Generic stuff that allows the window to show up
            /*Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            Application.Run(new Form1());
            */
            LongTermScheduler LongTermScheduler = new LongTermScheduler();
            ShortTermScheduler ShortTermScheduler = new ShortTermScheduler();
            CPU cpu = new CPU();
            Thread newThread = new Thread(cpu.run);
            newThread.Start();
            CPU cpu2 = new CPU();
            Thread newThread2 = new Thread(cpu2.run);
            newThread2.Start();
        }
    }
}

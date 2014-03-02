using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
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
            Loader.load();
            LongTermScheduler.Instance.UpdateLTS();
            CPU cpu = new CPU();
            while(Memory.Instance.currentSize != 0)
            {
                cpu.run();
                LongTermScheduler.Instance.UpdateLTS();
            }
            
            //Thread newThread = new Thread(cpu.run);
            //newThread.Start();
           // CPU cpu2 = new CPU();
            //Thread newThread2 = new Thread(cpu2.run);
            //newThread2.Start();
            
        }
    }
}

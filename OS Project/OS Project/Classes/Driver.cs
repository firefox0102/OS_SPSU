using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Diagnostics;
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
            CPU cpu2 = new CPU();
            CPU cpu3 = new CPU();
            CPU cpu4 = new CPU();
            double counter = 0;
            for(int i = 0; i < 3; 
            while(Memory.Instance.currentSize != 0 && counter < 7.5)
            {
                LongTermScheduler.Instance.UpdateLTS();

                if (counter < 7.75)
                {
                    cpu.run();
                    counter += .25;
                }
                if (counter < 7.75)
                {
                    cpu2.run();
                    counter += .25;
                }
                if (counter < 7.75)
                {
                    cpu3.run();
                    counter += .25;
                }
                if (counter < 7.75)
                {
                    cpu4.run();
                    counter += .25;
                }
                
                /*(LongTermScheduler.Instance.nextJob == 31)
                    break;*/
            }
            string[] results = new string[32];
            List<PCB> tempPCB = Disk.Instance.diskProcessTable;

            double totalWaiting = 0;
            double totalRunning = 0;
            for(int i = 0; i < tempPCB.Count; ++i){
                results[i] = " Job : "+tempPCB[i].id+"\n Wait time : "+tempPCB[i].waitingTime.Elapsed.TotalMilliseconds+"\n Run Time: "+tempPCB[i].elapsedTime.Elapsed.TotalMilliseconds;
                totalWaiting += (double)tempPCB[i].waitingTime.Elapsed.TotalMilliseconds;
                totalRunning += (double)tempPCB[i].elapsedTime.Elapsed.TotalMilliseconds;
            }
            results[30] = "Total Wait time: "+(totalWaiting)+"  Average Wait Time: "+(totalWaiting/30);
            results[31] = "Total Run time: "+(totalRunning)+"  Average Run Time: "+(totalRunning/30);
            File.Delete("Results.txt");
            File.WriteAllLines("Results.txt", results);

        }
    }
}

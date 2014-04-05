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
            for (int run = 0; run < 3; ++run)
            {
                while (Memory.Instance.currentSize != 0 && counter < 7.5)
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
                string[] results = new string[33];
                string[] results2 = new string[33];
                string[] results3 = new string[33];
                string[] results4 = new string[33];
                string[] results5 = new string[33];

                
                List<PCB> tempPCB = Disk.Instance.diskProcessTable;

                double totalWaiting = 0;
                double totalRunning = 0;
                results[0] = "Job #, Wait Time, Run Time";
                for (int i = 0; i < tempPCB.Count; ++i)
                {
                    results[i+1] = tempPCB[i].id + "," + tempPCB[i].waitingTime.Elapsed.TotalMilliseconds + "," + tempPCB[i].elapsedTime.Elapsed.TotalMilliseconds;
                    totalWaiting += (double)tempPCB[i].waitingTime.Elapsed.TotalMilliseconds;
                    totalRunning += (double)tempPCB[i].elapsedTime.Elapsed.TotalMilliseconds;
                }

                for(int i = 0; i< cpu.iocounter.Length; i++)
                {
                    results2[i] = i +","+ cpu.iocounter[i].ToString();
                }

                for (int i = 0; i < cpu2.iocounter.Length; i++)
                {
                    results3[i] = i + "," + cpu2.iocounter[i].ToString();
                }

                for (int i = 0; i < cpu3.iocounter.Length; i++)
                {
                    results4[i] = i + "," + cpu3.iocounter[i].ToString();
                }


                for (int i = 0; i < cpu4.iocounter.Length; i++)
                {
                    results5[i] = i + "," + cpu4.iocounter[i].ToString();
                }







               

                results[31] = "Total Wait time: ," + (totalWaiting) + ",  Average Wait Time:, " + (totalWaiting / 30);
                results[32] = "Total Run time: ," + (totalRunning) + ",  Average Run Time: ," + (totalRunning / 30);
                
                string trialFile2 = "cpuio.csv";
                string trialFile3 = "cpuio2.csv";
                string trialFile4 = "cpuio3.csv";
                string trialFile5 = "cpuio4.csv";
                

                string trialFile = "Results" + run + ".csv";
                File.Delete(trialFile);
                File.WriteAllLines(trialFile, results);

               

                File.WriteAllLines(trialFile2, results2);
                File.WriteAllLines(trialFile3, results3);
                File.WriteAllLines(trialFile4, results4);
                File.WriteAllLines(trialFile5, results5);

            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Diagnostics;
using OS_Project;

namespace OS_Project
{

    public static class Driver
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        ///
        public static CPU cpu = new CPU();
        public static CPU cpu2 = new CPU();
        public static CPU cpu3 = new CPU();
        public static CPU cpu4 = new CPU();

  //      [STAThread]
        static void Main()
        {
            //Generic stuff that allows the window to show up
            /*Application.EnableVisualStyles();
Application.SetCompatibleTextRenderingDefault(true);
Application.Run(new Form1());
*/
            Loader init_loader = new Loader();
            init_loader.load();
            LongTermScheduler.Instance.addToSTScheduler();
            int i = 0;


            Action cpu1R = new Action(cpu.run);
            Action cpu2R = new Action(cpu2.run);
            Action cpu3R = new Action(cpu3.run);
            Action cpu4R = new Action(cpu4.run);
            //Action ltsr = new Action(LongTermScheduler.Instance.addToSTScheduler);

            Task task1 = new Task(cpu1R);
            Task task2 = new Task(cpu2R);
            Task task3 = new Task(cpu3R);
            Task task4 = new Task(cpu4R);

            while (i < 30)
            {
                if (ShortTermScheduler.Instance.ReadyQueue.Count < 0 )
                {
                    break;
                }

                task1.Start();

                if (ShortTermScheduler.Instance.ReadyQueue.Count < 0)
                {
                    break;  
                }
 
                task2.Start();

                if (ShortTermScheduler.Instance.ReadyQueue.Count < 0)
                {
                    break;
                }

                task3.Start();

                if (ShortTermScheduler.Instance.ReadyQueue.Count < 0)
                {
                    break;
                }
                
                task4.Start();
                
                task1.Wait();
                task2.Wait();
                task3.Wait();
                task4.Wait();

                task1 = new Task(cpu1R);
                task2 = new Task(cpu2R);
                task3 = new Task(cpu3R);
                task4 = new Task(cpu4R);

                Console.WriteLine("This is driver run: " + i);
                LongTermScheduler.Instance.addToSTScheduler();
                i++;
                Console.ReadLine();
                

                
            }


            Console.ReadLine();


            //string[] results = new string[33];
            //string[] results2 = new string[33];
            //string[] results3 = new string[33];
            //string[] results4 = new string[33];
            //string[] results5 = new string[33];


            //List<PCB> tempPCB = Disk.Instance.diskProcessTable;








            //double totalWaiting = 0;
            //double totalRunning = 0;
            //results[0] = "Job #, Wait Time, Run Time";
            //for (int i = 0; i < tempPCB.Count; ++i)
            //{
            // results[i+1] = tempPCB[i].id + "," + tempPCB[i].waitingTime.Elapsed.TotalMilliseconds + "," + tempPCB[i].elapsedTime.Elapsed.TotalMilliseconds;
            // totalWaiting += (double)tempPCB[i].waitingTime.Elapsed.TotalMilliseconds;
            // totalRunning += (double)tempPCB[i].elapsedTime.Elapsed.TotalMilliseconds;
            //}

            //for(int i = 0; i< cpu.iocounter.Length; i++)
            //{
            // results2[i] = i +","+ cpu.iocounter[i].ToString();
            //}

            //for (int i = 0; i < cpu2.iocounter.Length; i++)
            //{
            // results3[i] = i + "," + cpu2.iocounter[i].ToString();
            //}

            //for (int i = 0; i < cpu3.iocounter.Length; i++)
            //{
            // results4[i] = i + "," + cpu3.iocounter[i].ToString();
            //}


            //for (int i = 0; i < cpu4.iocounter.Length; i++)
            //{
            // results5[i] = i + "," + cpu4.iocounter[i].ToString();
            //}


            //results[31] = "Total Wait time: ," + (totalWaiting) + ", Average Wait Time:, " + (totalWaiting / 30);
            //results[32] = "Total Run time: ," + (totalRunning) + ", Average Run Time: ," + (totalRunning / 30);

            //string trialFile2 = "cpuio.csv";
            //string trialFile3 = "cpuio2.csv";
            //string trialFile4 = "cpuio3.csv";
            //string trialFile5 = "cpuio4.csv";


            //string trialFile = "Results" + run + ".csv";
            //File.Delete(trialFile);
            //File.WriteAllLines(trialFile, results);



            //File.WriteAllLines(trialFile2, results2);
            //File.WriteAllLines(trialFile3, results3);
            //File.WriteAllLines(trialFile4, results4);
            //File.WriteAllLines(trialFile5, results5);

        }
    }
}


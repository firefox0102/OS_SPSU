using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OS_Project;

namespace OS_Project
{
    public class LongTermScheduler{
        // Will first get empty pages from RAM
        // then will take next job not loaded into RAM
        // load job into RAM into available pages.
        // Will have to update the the RAM pageManager
        // to show no longer empty.

        public static volatile LongTermScheduler LTS;
        private static Object locker = new Object();

        //Processes that have been loaded into RAM
        public List<int> LoadedProcesses;
        public int nextJob;

        public LongTermScheduler()
        {
            nextJob = 0;
            LoadedProcesses = new List<int>();
        }

        //singleton for remote access in other classes
        public static LongTermScheduler Instance
        {
            get
            {
                if (LTS == null)
                {
                    lock (locker)
                    {
                        LTS = new LongTermScheduler();
                    }
                }
                return LTS;
            }
        }

        public void removeFromRAM(PCB tempPCB)
        {
            // Gets a PCB's physical Memory Frame locations and clears them
            // The Indexes for the Frames that are cleared get
            for (int i = 0; i < tempPCB.pageTable.Count; ++i)
            {
                Memory.Instance.memory[ tempPCB.pageTable[i] ].Clear();
                Memory.Instance.pageManager.Add(tempPCB.pageTable[i]);
            }
            tempPCB.pageTable.Clear();
        }



        public void addToSTScheduler()
        {
            // will need to ensure that the STS has a queue that
            // can be accessed without disrupting operation

            //The while loop can be removed because it was for testing
            bool keepGoing = true;
            while (keepGoing && nextJob < 30)
            {
                PCB tempPCB = Disk.Instance.diskProcessTable[nextJob];

                if (Memory.Instance.pageManager.Count > tempPCB.totalPages)
                {

                    // Instructions Disk Pages
                    int current_instrStartPage = tempPCB.diskInstrStartPos;
                    int current_instrEndPage = tempPCB.diskInstrEndPos;

                    // Data Disk Pages
                    int current_dataStartPage = tempPCB.diskDataStartPos;
                    int current_dataEndPage = tempPCB.diskDataEndPos;

                    int k = 0;
                    int j = 0;
                    for (; current_instrStartPage <= current_instrEndPage; )
                    {
                        // The try block is in case that a page is not a full 4 words
                        // to prevent a null element that causes an error
                        try
                        {
                            string element = Disk.Instance.diskData[current_instrStartPage][j];
                            Memory.Instance.memory[Memory.Instance.pageManager[0]].Add(element);
                        }
                        catch
                        {
                            tempPCB.logicalMemInstr.Add(k);
                            tempPCB.pageTable.Add(Memory.Instance.pageManager[0]);
                            Memory.Instance.pageManager.RemoveAt(0);
                            break;
                        }
                        j++;
                        if (j > 3)
                        {
                            tempPCB.logicalMemInstr.Add(k);
                            k++;
                            tempPCB.pageTable.Add(Memory.Instance.pageManager[0]);
                            Memory.Instance.pageManager.RemoveAt(0);
                            current_instrStartPage++;
                            j = 0;
                        }
                    }

                    // k = 0;
                    // need k to not be zero because the element at index 0
                    // for the logicalMemData will be the first page for data pages
                    // in RAM DO NOT CHANGEs
                    k = tempPCB.logicalMemInstr.Count;
                    j = 0;
                    for (; current_dataStartPage <= current_dataEndPage; )
                    {
                        // The try block is in case that a page is not a full 4 words
                        // to prevent a null element that causes an error
                        try
                        {
                            string element = Disk.Instance.diskData[current_dataStartPage][j];
                            Memory.Instance.memory[Memory.Instance.pageManager[0]].Add(element);
                        }
                        catch
                        {
                            tempPCB.logicalMemData.Add(k);
                            tempPCB.pageTable.Add(Memory.Instance.pageManager[0]);
                            Memory.Instance.pageManager.RemoveAt(0);
                            break;
                        }
                        j++;
                        if (j > 3)
                        {
                            tempPCB.logicalMemData.Add(k);
                            k++;
                            tempPCB.pageTable.Add(Memory.Instance.pageManager[0]);
                            Memory.Instance.pageManager.RemoveAt(0);
                            current_dataStartPage++;
                            j = 0;
                        }
                    }
                    tempPCB.state = OS_Project.PCB.Status.ready;
                    tempPCB.waitingTime.Start();
                    LoadedProcesses.Add(tempPCB.id);
                    nextJob++;
                    Console.WriteLine("Loaded Job: " + (nextJob - 1));
                    Console.WriteLine("----------------------------");
                    ShortTermScheduler.Instance.AddToShortTermScheduler(tempPCB);
                    //ShortTermScheduler.Instance.ReadyQueue.Add(tempPCB);
                }
                else
                {
                    keepGoing = false;
                }

            }

        }

        
    }// end of class
}// end of namespace

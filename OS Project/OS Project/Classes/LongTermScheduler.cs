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

        public static LongTermScheduler LTS;

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
                    LTS = new LongTermScheduler();
                }
                return LTS;
            }
        }

        public void addToSTScheduler()
        {
            // will need to ensure that the STS has a queue that
            // can be accessed without disrupting operation

            //The while loop can be removed because it was for testing
            bool keepGoing = true;
            while (keepGoing)
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

                    k = 0;
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
                   // ShortTermScheduler.Instance.ReadyQue.add(tempPCB);

                }
                else
                {
                    keepGoing = false;
                }

            }

        }

        
    }// end of class
}// end of namespace

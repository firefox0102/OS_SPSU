using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OS_Project.Classes;

namespace OS_Project
{
    public class LongTermScheduler
    {
        public int nextJob;
        public int counter;
        public static LongTermScheduler lts;
        //Processes that have been loaded into RAM
        public List<PCB> LoadedProcesses;
        //Processes that have not been loaded into RAM yet
        public List<PCB> ProcessQueue;

        public LongTermScheduler()
        {
            nextJob = 1;
            LoadedProcesses = new List<PCB>();
            ProcessQueue = new List<PCB>();
        }

        public LongTermScheduler(int jid)
        {
            nextJob = jid;
            LoadedProcesses = new List<PCB>();
            ProcessQueue = new List<PCB>();
        }

        //singleton for remote access in other classes
        public static LongTermScheduler Instance
        {
            get
            {
                if (lts == null)
                {
                    lts = new LongTermScheduler();
                }
                return lts;
            }
        }

        public void UpdateLTS()
        {
            int loop = 0;
            if (LoadedProcesses == null)
            {
                LoadedProcesses = new List<PCB>();
            }
            if (ProcessQueue == null)
            {
                ProcessQueue = new List<PCB>();
            }
            
            while(Memory.Instance.currentSize <= 1024)
            {
                if(loop > 50)
                {
                    break;
                }
                
                GetNextProcess();
                AddToSTScheduler();
                //if (done)
                  //  break;
                loop++;
            }
        }

        public void GetNextProcess()
        {
            //get next instruction from the disk
            bool success = false;
            List<PCB> temp = Disk.Instance.diskProcessTable;
            if (LoadedProcesses.Count <= 30)
            {
                for (int i = 0; i < temp.Count; i++)
                {
                    if (temp[i].id == nextJob)
                    {
                        Disk.Instance.diskProcessTable[i].state = PCB.Status.waiting;
                        ProcessQueue.Add(temp[i]);
                        success = true;
                        break;
                    }
                }
                if (success)
                {
                    nextJob = ProcessQueue.ElementAt(ProcessQueue.Count - 1).id + 1;
                }
            }
        }

        public void Clean()
        {
            Memory.Instance.wipeMemory();
            if (nextJob == 31)
            {
                lts = new LongTermScheduler(16);
            }
            else
                Console.WriteLine("Error");
            LongTermScheduler.Instance.UpdateLTS();
        }
        public void AddToSTScheduler()
        {
            int pc = 0;
            if (ProcessQueue.Count != 0)
            {
                PCB p = ProcessQueue[pc];
                bool success = false;

                while (success != true)
                {
                    //check if data and instruction will fit in RAM
                    if (p.totalLength + Memory.Instance.currentSize <= 1024)
                    {
                        int newInstrStartPos = 0;
                        int newInstrEndPos = 0;
                        int newDataStartPos = 0;
                        int newDataEndPos = 0;

                        //add the instruction to memory
                        for (int i = p.diskInstrStartPos; i <= p.diskInstrEndPos; i++)
                        {
                            //update the start pos
                            if (i == p.diskInstrStartPos)
                            {
                                newInstrStartPos = Memory.Instance.memory.Count;
                            }
                            //update the end pos
                            if (i == p.diskInstrEndPos)
                            {
                                newInstrEndPos = Memory.Instance.memory.Count - 1;
                            }
                            Memory.Instance.memory.Add(Disk.Instance.diskData[i]);
                        }

                        //add the Data to Memory
                        for (int i = p.diskDataStartPos; i <= p.diskDataEndPos; i++)
                        {
                            //update the start position
                            if (i == p.diskDataStartPos)
                            {
                                newDataStartPos = Memory.Instance.memory.Count - 1;
                            }
                            //Update the new end position
                            if (i == p.diskDataEndPos)
                            {
                                newDataEndPos = Memory.Instance.memory.Count - 1;
                            }
                            Memory.Instance.memory.Add(Disk.Instance.diskData[i]);
                        }

                        p.memDataStartPos = newDataStartPos;
                        p.memDataEndPos = newDataEndPos;
                        p.memInstrStartPos = newInstrStartPos;
                        p.memInstrEndPos = newInstrEndPos;

                        //Update the current size of RAM
                        Memory.Instance.currentSize += p.totalLength;

                        //Add PCB to Short Term Scheduler
                        p.state = PCB.Status.ready;
                        p.waitingTime.Start();
                        ShortTermScheduler.Instance.AddToShortTermScheduler(p);
                        LoadedProcesses.Add(p);
                        ProcessQueue.RemoveAt(pc);
                        pc++;
                    }
                    else
                    {
                        GetNextProcess();
                        p = ProcessQueue[pc];
                        pc++;

                        break;
                    }
                    if (pc >= ProcessQueue.Count)
                    {
                        success = true;
                    }
                }
                //done = true;
            }
        }
    }
}

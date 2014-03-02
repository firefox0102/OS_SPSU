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
        public static LongTermScheduler lts;
        //Processes that have been loaded into RAM
        public List<PCB> LoadedProcesses;
        //Processes that have not been loaded into RAM yet
        public List<PCB> ProcessQueue;

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
            while(Memory.Instance.currentSize <= 1024)
            {
                if (LoadedProcesses == null)
                {
                    LoadedProcesses = new List<PCB>();
                }
                if(ProcessQueue == null)
                {
                    ProcessQueue = new List<PCB>();
                }
                GetNextProcess();
                AddToSTScheduler();
            }
        }

        public void GetNextProcess()
        {
            //get next instruction from the disk
            List<PCB> temp = Disk.Instance.diskProcessTable;
            if (LoadedProcesses.Count <= 30)
            {
                for (int j = 0; j < temp.Count; j++)
                {
                    int matches = 0;
                    //get job id for each process
                    //see if its in RAM or Completed
                    PCB p = temp[j];
                    for (int i = 0; i < LoadedProcesses.Count; i++)
                    {
                        //for context switching, we need to check if its created or waiting, and NOT terminated
                        if (p.id == LoadedProcesses[i].id)
                        {
                            matches++;
                        }
                    }
                    if (matches == 0)
                    {
                        p.state = PCB.Status.waiting;
                        ProcessQueue.Add(p);
                        break;
                    }
                }
            }
        }

        public void AddToSTScheduler()
        {
            int pc = 0;
            PCB p = ProcessQueue[pc];
            bool success = false;            

            while(success != true)
            {
                //check if data and instruction will fit in RAM
                if(p.sizeInBytes + Memory.Instance.currentSize <= 1024)
                {
                    int newInstrStartPos = 0;
                    int newInstrEndPos = 0;
                    int newDataStartPos = 0;
                    int newDataEndPos = 0;

                    //add the instruction to memory
                    for(int i = p.diskInstrStartPos; i <= p.diskInstrEndPos; i++)
                    {
                        //update the start pos
                        if(i == p.diskInstrStartPos)
                        {
                                newInstrStartPos = Memory.Instance.memory.Count;
                        }
                        //update the end pos
                        if(i == p.diskInstrEndPos)
                        {
                                newInstrEndPos = Memory.Instance.memory.Count;
                        }
                        Memory.Instance.memory.Add(Disk.Instance.diskData[i]);
                    }

                    //add the Data to Memory
                    for(int i = p.diskDataStartPos; i < p.diskDataEndPos; i++)
                    {
                        //update the start position
                        if(i == p.diskDataStartPos)
                        {
                            newDataStartPos = Memory.Instance.memory.Count;
                        }
                        //Update the new end position
                        if(i == p.diskDataEndPos)
                        {
                            newDataEndPos = Memory.Instance.memory.Count;
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
                    ShortTermScheduler.Instance.AddToShortTermScheduler(p);
                    
                    success = true;
                }
                else
                {
                    GetNextProcess();
                    pc++;
                    p = ProcessQueue[pc];
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Project.Classes
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

        public void GetNextProcess()
        {
            //get next instruction from the disk
            List<PCB> temp = Disk.Instance.diskProcessTable;

            foreach (PCB p in LoadedProcesses)
            {
                int matches = 0;
                //get job id for each process
                //see if its in RAM or Completed
                for(int i = 0; i < temp.Count; i++)
                {
                    if(p.id == temp[i].id){
                        matches++;
                    }
                }
                if (matches == 0)
                {
                    ProcessQueue.Add(p);
                    break;
                }
            }
        }

        public void AddToSTScheduler()
        {
            GetNextProcess();
            int pc = 0;
            PCB p = ProcessQueue[pc];
            bool success = false;            

            while(success != true)
            {
                //check if data and instruction will fit in RAM
                if(p.sizeInBytes + Memory.Instance.currentSize <= Memory.Instance.)
                {
                    int newInstrStartPos;
                    int newInstrEndPos;
                    int newDataStartPos;
                    int newDataEndPos;

                    //add the instruction to memory
                    for(int i = p.diskInstrStartPos; i <= p.diskInstrEndPos; i++)
                    {
                        //update the start pos
                        if(i == p.diskInstrStartPos)
                        {
                            newInstrStartPos = Memory.Instance.memory.Count - 1;
                        }
                        //update the end pos
                        if(i == p.diskInstrEndPos)
                        {
                            newInstrEndPos = Memory.Instance.memory.Count - 1;
                        }
                        Memory.Instance.memory.Add(Disk.Instance.diskData[i]);
                    }

                    //add the Data to Memory
                    for(int i = p.diskDataStartPos; i < p.diskDataEndPos; i++)
                    {
                        //update the start position
                        if(i == p.diskDataStartPos)
                        {
                            newDataStartPos = Memory.Instance.memory.Count - 1;
                        }
                        //Update the new end position
                        if(i == p.diskDataEndPos)
                        {
                            newDataEndPos = Memory.Instance.memory.Count - 1;
                        }
                        Memory.Instance.memory.Add(Disk.Instance.diskData[i]);
                    }

                    p.memDataStartPos = newDataStartPos;
                    p.memDataEndPos = newDataEndPos;
                    p.memInstrStartPos = newDataStartPos;
                    p.memInstrEndPos = newDataEndPos;

                    //Update the current size of RAM
                    Memory.Instance.currentSize += p.sizeInBytes;

                    //Add PCB to Short Term Scheduler
                    ShortTermScheduler.Instance.ReadyQueue.Add(p);
                    
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

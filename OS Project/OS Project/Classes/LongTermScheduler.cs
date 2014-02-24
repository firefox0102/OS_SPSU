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
        //create a list of loaded processes
        public List<Process> LoadedProcesses;
        public List<Process> ProcessQueue;

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

        public PCB GetNextProcess()
        {
            //get next instruction from the disk
            List<Process> temp = Disk.Instance.diskProcessTable;
            PCB pcb = new PCB();

            foreach (Process p in temp)
            {
                int matches = 0;
                //get job id for each process
                //see if its in RAM or Completed
                for(int i = 0; i < temp.Count; i++)
                {
                    if(p.jobID == temp[i].jobID){
                        matches++;
                    }
                }
                if (matches == 0)
                {
                    ProcessQueue.Add(p);
                    pcb = new PCB(p);
                    break;
                }
            }

            return pcb;
        }

        public void AddToSTScheduler()
        {
            PCB pcb = GetNextProcess();
            Process p = ProcessQueue[0];
            Memory.Instance.write()
            //Add PCB to Short Term Scheduler
        }
    }
}

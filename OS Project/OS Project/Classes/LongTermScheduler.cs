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
        public List<Process> 

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
            foreach (Process p in Instance.diskProcessTable)
            {
                int matches = 0;
                //get job id for each process
                //see if its in RAM or Completed
                for(int i = 0; i < diskProcessTable.Size????; )
                {
                    if(p.id == )
                        matches++;
                }
                if(matches == 0)

            }
            
            return PCB;
        }

        public void AddToSTScheduler()
        {
            Process p = GetNextProcess();
            PCB pcb = new PCB(p);
            //write process to RAM
            //Add PCB to Short Term Scheduler
        }
    }
}

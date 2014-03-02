using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Project.Classes
{
    public class ShortTermScheduler
    {
        public static ShortTermScheduler sts;
        public List<PCB> ReadyQueue;

        //singleton for remote access in other classes
        public static ShortTermScheduler Instance
        {
            get
            {
                if (sts == null)
                {
                    sts = new ShortTermScheduler();
                }
                return sts;
            }
        }

        public void AddToShortTermScheduler(PCB p)
        {
            if(ReadyQueue == null)
            {
                ReadyQueue = new List<PCB>();
            }
            ReadyQueue.Add(p);
        }
        
        public PCB getNextJob()
        {
            if (ReadyQueue.Count > 0)
            {
                PCB i = ReadyQueue[0];//gets first element
                ReadyQueue.RemoveAt(0);//removed first element like a pop front
                return i;
            }
            else
            {
                LongTermScheduler.Instance.Clean();
                PCB i = ReadyQueue[0];//gets first element
                ReadyQueue.RemoveAt(0);//removed first element like a pop front
                return i;
            }

        }
    }
}

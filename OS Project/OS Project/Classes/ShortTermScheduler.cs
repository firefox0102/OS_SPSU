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
            ReadyQueue.OrderByDescending(x => x.instrLength);
        }



        public PCB getNextJobSJF()
        {
            

            if(Driver.cpu.currentPCB.instrLength - Driver.cpu.processPosition > ReadyQueue[0].instrLength- ReadyQueue[0].processPosition )
            {
                Dispatcher.contextSwitch(ReadyQueue[0], Driver.cpu);
                ReadyQueue[0].waitingTime.Stop();
            }


            if (Driver.cpu2.currentPCB.instrLength - Driver.cpu.processPosition > ReadyQueue[0].instrLength - ReadyQueue[0].processPosition)
            {
                Dispatcher.contextSwitch(ReadyQueue[0], Driver.cpu2);
                ReadyQueue[0].waitingTime.Stop();
            }


            if (Driver.cpu3.currentPCB.instrLength - Driver.cpu.processPosition > ReadyQueue[0].instrLength - ReadyQueue[0].processPosition)
            {
                Dispatcher.contextSwitch(ReadyQueue[0], Driver.cpu3);
                ReadyQueue[0].waitingTime.Stop();
            }


            if (Driver.cpu4.currentPCB.instrLength - Driver.cpu.processPosition > ReadyQueue[0].instrLength - ReadyQueue[0].processPosition)
            {
                Dispatcher.contextSwitch(ReadyQueue[0], Driver.cpu4);
                ReadyQueue[0].waitingTime.Stop();
            }

            



        }


        public PCB getNextJobPriority()
        {





        }




     /* code from part one kept for refrence
      * 
      * 
      * 
        public PCB getNextJob()
        {
            if (ReadyQueue.Count > 0)
            {
                PCB i = ReadyQueue[0];//gets first element
                ReadyQueue[0].waitingTime.Stop();
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
      */
    }
}

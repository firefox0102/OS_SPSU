using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OS_Project;

namespace OS_Project
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

        //Constructor
        public ShortTermScheduler()
        {
            ReadyQueue = new List<PCB>();
        }

        //Might not need this method anymore
        public void AddToShortTermScheduler(PCB p)
        {
            ReadyQueue.Add(p);
            Sort();
        }
        
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
                //This is so dirty lol
                PCB i = ReadyQueue[0];//gets first element
                ReadyQueue[0].waitingTime.Stop();
                ReadyQueue.RemoveAt(0);//removed first element like a pop front
                return i;
            }
        }

        //***************************************************************************
        //New Stuff
        //***************************************************************************

        //Don't think I need this anymore, but hey...
        /*public PCB Swap(PCB p)
        {
            ReadyQueue[0].waitingTime.Stop();
            PCB temp = ReadyQueue[0];
            ReadyQueue[0] = p;
            ReadyQueue[0].waitingTime.Start();
            ReadyQueue[0].state = PCB.Status.waiting;
            //Assuming the process is being sent to the CPU
            temp.state = PCB.Status.running;
            return temp;
        }*/

        public void Sort()
        {
            //Sorting algorithm is determined by the CompareTo Method in the PCB class
            ReadyQueue.Sort();
        }

        //Context Switching
        //Checks to see if the next job is shorter than any of the currently running jobs
        public void switchNextJobSJF()
        {
            if((Driver.cpu.currentPCB.instrLength - Driver.cpu.processPosition) > (ReadyQueue[0].instrLength- ReadyQueue[0].pc))
            {
                PCB temp = Driver.cpu.currentPCB;
                temp.state = PCB.Status.waiting;
                temp.waitingTime.Start();
                Dispatcher.Instance.sendProcess(Driver.cpu);
                ReadyQueue.Add(temp);
                Sort();
                Console.WriteLine("CONTEXT SWITCH MOTHER FUCKER!!!");
            }

            //if ((Driver.cpu2.currentPCB.instrLength - Driver.cpu2.processPosition) > (ReadyQueue[0].instrLength - ReadyQueue[0].pc))
            //{
            //    PCB temp = Driver.cpu2.currentPCB;
            //    temp.state = PCB.Status.waiting;
            //    temp.waitingTime.Start();
            //    Dispatcher.Instance.sendProcess(Driver.cpu2);
            //    ReadyQueue.Add(temp);
            //    Sort();
            //    Console.WriteLine("CONTEXT SWITCH MOTHER FUCKER!!!");
            //}

            //if ((Driver.cpu3.currentPCB.instrLength - Driver.cpu3.processPosition) > (ReadyQueue[0].instrLength - ReadyQueue[0].pc))
            //{
            //    PCB temp = Driver.cpu3.currentPCB;
            //    temp.state = PCB.Status.waiting;
            //    temp.waitingTime.Start();
            //    Dispatcher.Instance.sendProcess(Driver.cpu3);
            //    ReadyQueue.Add(temp);
            //    Sort();
            //    Console.WriteLine("CONTEXT SWITCH MOTHER FUCKER!!!");
            //}

            //if ((Driver.cpu4.currentPCB.instrLength - Driver.cpu4.processPosition) > (ReadyQueue[0].instrLength - ReadyQueue[0].pc))
            //{
            //    PCB temp = Driver.cpu4.currentPCB;
            //    temp.state = PCB.Status.waiting;
            //    temp.waitingTime.Start();
            //    Dispatcher.Instance.sendProcess(Driver.cpu4);
            //    ReadyQueue.Add(temp);
            //    Sort();
            //    Console.WriteLine("CONTEXT SWITCH MOTHER FUCKER!!!");
            //}
        }

        //Context Switching
        //Checks to see if the next job is higher priority than any of the currently running jobs
        public void switchNextJobPriority()
        {
            if ((Driver.cpu.currentPCB.priority) < (ReadyQueue[0].priority))
            {
                PCB temp = Driver.cpu.currentPCB;
                temp.state = PCB.Status.waiting;
                temp.waitingTime.Start();
                Dispatcher.Instance.sendProcess(Driver.cpu);
                ReadyQueue.Add(temp);
                Sort();
                Console.WriteLine("CONTEXT SWITCH");
            }

            //if ((Driver.cpu2.currentPCB.priority) < (ReadyQueue[0].priority))
            //{
            //    PCB temp = Driver.cpu2.currentPCB;
            //    temp.state = PCB.Status.waiting;
            //    temp.waitingTime.Start();
            //    Dispatcher.Instance.sendProcess(Driver.cpu2);
            //    ReadyQueue.Add(temp);
            //    Sort();
            //}

            //if ((Driver.cpu3.currentPCB.priority) < (ReadyQueue[0].priority))
            //{
            //    PCB temp = Driver.cpu3.currentPCB;
            //    temp.state = PCB.Status.waiting;
            //    temp.waitingTime.Start();
            //    Dispatcher.Instance.sendProcess(Driver.cpu3);
            //    ReadyQueue.Add(temp);
            //    Sort();
            //}

            //if ((Driver.cpu4.currentPCB.priority) < (ReadyQueue[0].priority))
            //{
            //    PCB temp = Driver.cpu4.currentPCB;
            //    temp.state = PCB.Status.waiting;
            //    temp.waitingTime.Start();
            //    Dispatcher.Instance.sendProcess(Driver.cpu4);
            //    ReadyQueue.Add(temp);
            //    Sort();
            //}
        }
    }
}

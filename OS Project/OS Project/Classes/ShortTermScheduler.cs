﻿using System;
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
                PageManager.Instance.Clean();
                PCB i = ReadyQueue[0];//gets first element
                ReadyQueue.RemoveAt(0);//removed first element like a pop front
                return i;
            }

        }

        //***************************************************************************
        //New Stuff
        //***************************************************************************

        public PCB Swap(PCB p)
        {
            ReadyQueue[0].waitingTime.Stop();
            PCB temp = ReadyQueue[0];
            //add running context switch********************************
            ReadyQueue[0] = p;
            ReadyQueue[0].waitingTime.Start();
            //add wait context switch***********************************
            return temp;
        }

        public void Sort()
        {
            //Shortest Job first 
            ShortestJobFirst();
            //Priority Sort
            //PrioritySort();
        }

        public void ShortestJobFirst()
        {
            //sort the readyqueue by shortest job first
        }

        public void PrioritySort()
        {
            //priority sort the ready queue
        }
         public PCB getNextJobSJF()
 +        {
 +            
 +
 +            if(Driver.cpu.currentPCB.instrLength - Driver.cpu.processPosition > ReadyQueue[0].instrLength- ReadyQueue[0].processPosition )
 +            {
 +                Dispatcher.contextSwitch(ReadyQueue[0], Driver.cpu);
 +                ReadyQueue[0].waitingTime.Stop();
 +            }
 +
 +
 +            if (Driver.cpu2.currentPCB.instrLength - Driver.cpu.processPosition > ReadyQueue[0].instrLength - ReadyQueue[0].processPosition)
 +            {
 +                Dispatcher.contextSwitch(ReadyQueue[0], Driver.cpu2);
 +                ReadyQueue[0].waitingTime.Stop();
 +            }
 +
 +
 +            if (Driver.cpu3.currentPCB.instrLength - Driver.cpu.processPosition > ReadyQueue[0].instrLength - ReadyQueue[0].processPosition)
 +            {
 +                Dispatcher.contextSwitch(ReadyQueue[0], Driver.cpu3);
 +                ReadyQueue[0].waitingTime.Stop();
 +            }
 +
 +
 +            if (Driver.cpu4.currentPCB.instrLength - Driver.cpu.processPosition > ReadyQueue[0].instrLength - ReadyQueue[0].processPosition)
 +            {
 +                Dispatcher.contextSwitch(ReadyQueue[0], Driver.cpu4);
 +                ReadyQueue[0].waitingTime.Stop();
 +            }
 +
 +            
 +
 +
 +
 +        }
 +
 +
 +        public PCB getNextJobPriority()
 +        {
 +
 +
 +
 +
 +
 +        }
 +
 +      //run a sorting algorithm to sort the ready queue
 +
 +
 +     /* code from part one kept for refrence
 +      * 
 +      * 
 +      * 
    }
}

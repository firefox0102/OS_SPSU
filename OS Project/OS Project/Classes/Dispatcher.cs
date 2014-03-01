﻿/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OS_Project.Classes;

namespace OS_Project
{
    public class Dispatcher
    {
        public static Dispatcher dis;
        private bool idle;
        public Dispatcher()
        {
        }
        public static Dispatcher Instance
        {
            get
            {
                if (dis == null)
                {
                    dis = new Dispatcher();
                }
                return dis;
            }
        }
        //Drew will write the dispatcher
        public void sendProcess(CPU cpu)
        {
            PCB pid = ShortTermScheduler.Instance.getNextJob();
            //CPU.Instance.instructionList = Memory.Instance.memory[current.memPos];
            List<string> instuctionList = Memory.memory.GetRange(pid.memInstrStartPos, pid.totalLength);
            cpu.ProgramCache = instructionList;
            pid.elapsedTime.Start();
            pid.Status.state = running;
            cpu.currentPCB = pid;
            cpu.idle = false;
        }
        
        //dispatcher seems to send instuctions one at a time to the cpu. we can implement this by calling a get current instuction function or some other way
        public bool isIdle()
        {
            return idle;
        }
        public void terminateProcess()
        {
           //get current process start and end and then clear ram
        }

    }
}
*/

using System;
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
        public bool idle = true;
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
           /* PCB pid = ShortTermScheduler.Instance.getNextJob();
            //CPU.Instance.instructionList = Memory.Instance.memory[current.memPos];
            List<string> instuctionList = Memory.Instance.memory.GetRange(pid.memInstrStartPos, pid.totalLength);
            cpu.ProgramCache = instuctionList;
            cpu.register = pid.registers;
            pid.elapsedTime.Start();
            pid.state = PCB.Status.running;
            cpu.currentPCB = pid;
            cpu.idle = false;*/
            
            //what is the input cache, what is the output cache and what is teh instruction class
            //only need to pass output chache in if it has been changed and is not zero
            
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


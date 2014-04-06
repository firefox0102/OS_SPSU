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
            //instructionCache
            //inputCache
            //outputCache
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
            //16 words/instrcuions/data per cache.
            
        }
        public List<string> PageFault(PCB currentPCB, int y, String x, string cache)
        {
            //y is teh current set of 4 frames, the retrun is the next or previous frame based on up or down
            //in sting x
            //if up, return the instruction set from the page before this one
            //cache can be instrcution, input or output 
            
            //if teh next set has less than 4 frames, send it just whats left over.  cache does not need
            //to alwasy be full
            if(cache.Equals("instructiion")){
                if (x.Equals("up"))
                {
                    if(y > 0)
                    {
                        //interact with instruction logicl memory and page table
                        //logical_mem
                    }
                }
            //if down, return the instruction set from the page after this one
                if (x.Equals("down"))
                {
                    if (y < /*page table size-1*/)
                    {
                        //somehow get the currentPcb.pagetable[y+1] instruction set
                    }
                }
                
            }
            //end instrcution
            if (x.Equals("up"))
            {
                if(y > 0)
                {
                    //somehow get the currentPcb.pagetable[y-1] instruction set
                }
            }
            //if down, return the instruction set from the page after this one
            if (x.Equals("down"))
            {
                if (y < /*page table size-1*/)
                {
                    //somehow get the currentPcb.pagetable[y+1] instruction set
                }
            }
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


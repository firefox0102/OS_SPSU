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
            PCB pid = ShortTermScheduler.Instance.getNextJob();
            //CPU.Instance.instructionList = Memory.Instance.memory[current.memPos];
            //List<string> instuctionList = Memory.Instance.memory.GetRange(pid.memInstrStartPos, pid.totalLength);
            //cpu.ProgramCache = instuctionList;
            cpu.instructionCache = PageFault(pid, 0,"up", "instruction" );
            cpu.inputCache = PageFault(pid, 0,"up", "input" );
            cpu.outputCache = PageFault(pid, 0,"up", "output" );
            cpu.register = pid.registers;
            pid.elapsedTime.Start();
            pid.state = PCB.Status.running;
            cpu.currentPCB = pid;
            cpu.idle = false;
            
            //what is the input cache, what is the output cache and what is teh instruction class
            //only need to pass output chache in if it has been changed and is not zero
            //16 words/instrcuions/data per cache.
            
        }
        public List<string> getFrames(List<string> log){
            List<string> hold = new List();
            for (int i = 0; i< log.Count; i++){
                hold.AddRange(Memory.Instance.memory[log[i]]);
            }
            return hold;
        }
        public List<string> PageFault(PCB currentPCB, int y, String x, string cache)
        {
            //y is teh current set of 4 frames, the retrun is the next or previous frame set based on up or down
            //in sting x
            //if up, return the instruction set from the page before this one
            //cache can be instrcution, input or output 
            
            //if teh next set has less than 4 frames, send it just whats left over.  cache does not need
            //to alwasy be full
            List set = new List;
            if(cache.Equals("instruction")){
                if (x.Equals("up"))
                {
                    if(y > 0)
                    {
                        //interact with instruction logicl memory and page table
                        //logical_memInstr
                        List memLoc = PCB.Instance.getLocations(y*4, 1);
                    }
                }
            //if down, return the instruction set from the page after this one
                if (x.Equals("down"))
                {
                    if (y >=1)
                    {
                        //somehow get the currentPcb.pagetable[y+1] instruction set
                        int h = y-1;
                        List memLoc = PCB.Instance.getLocations(h*4, 1);
                    }
                }
                
            }
             else if(cache.Equals("input")){
                if (x.Equals("up"))
                {
                    if(y > 0)
                    {
                        //interact with data in logicl memory and page table
                        //logical_memDataIn
                         
                        List memLoc = PCB.Instance.getLocations(y*4, 2);
                    }
                }
            //if down, return the instruction set from the page after this one
                if (x.Equals("down"))
                {
                    if (y >0)
                    {
                        //somehow get the currentPcb.pagetable[y+1] instruction set
                         int h = y-1;
                        List memLoc = PCB.Instance.getLocations(h*4, 2);
                    }
                }
                
            }
            //end input
            else if(cache.Equals("output")){
                if (x.Equals("up"))
                {
                    if(y > 0)
                    {
                        //interact with data out logicl memory and page table
                        //logical_memDataOut
                        int offset = y*4;
                        int od = 19+y;
                        List memLoc = PCB.Instance.getLocations(od, 2);
                    }
                }
            //if down, return the instruction set from the page after this one
                if (x.Equals("down"))
                {
                    if (y < /*page table size-1*/)
                    {
                        //somehow get the currentPcb.pagetable[y+1] instruction set
                        int offset = (y-1)*4;
                        int od = 19+y;
                        List memLoc = PCB.Instance.getLocations(od, 2);
                    }
                }
                
            }
            //end output
            for(int i = 0; i<memLoc.Count; i++)
                set.AddRange(Memory.Instance.memory[memLoc[i]]);
            
            return set;
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
        //write void contextSwitch(PCB, cpu){}

    }
}


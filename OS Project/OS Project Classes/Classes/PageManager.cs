using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Project
{
    class PageManager
    {
        public static PageManager pm;

        //singleton for remote access in other classes
        public static PageManager Instance
        {
            get
            {
                if (pm == null)
                {
                    pm = new PageManager();
                }
                return pm;
            }
        }

        public void Clean(PCB terminatedPCB)
        {
            //clean the terminated processes from ram
        }

        public int NumEmptyFrames()
        {
            //return how many pages are available

        }

        public int NextEmptyFrame()
        {
            //return the number for the next empty frame in RAM
        }

        public List<string> PageFault(PCB currentPCB, int y, String x)
        {
            //if up, return the instruction set from the page before this one
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OS_Project
{
    public class Loader
    {
        StreamReader sr;
        PCB pcb;
        
        public Loader(p)
        {
            StreamReader sr = new Streamreader();
            pcb = p;
        }
        
        string getFromFile()
        {
            if(!sr.eof())
            {
                if(/*I didn't hit the next// marks*/)
                {
                    Process temp = new Process();
                    storeInPCB(pcb, temp);
                }
            }
        }
        
        string convertToBinaryString()
        {
            
        }
        
        void storeInPCB(p)
        {
           //store a reference of the process in the pcb
        }
    }
}

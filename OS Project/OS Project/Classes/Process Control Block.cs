using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OS_Project.Classes;

namespace OS_Project
{
    class PCB
    {
        public int id;
        public int memPos;
        public int pc;
        public int memDataPos;
        public int jobLength;
        public bool done;

        public enum Status {error, created, ready, waiting, running, terminated }; //created instead of new since new is resered word
        public Status state;
        public List<string> registers;//16 long
   
        public PCB()
        {
            id = -1;
            memPos = -1;
            pc = -1;
            memDataPos = -1;
            jobLength = -1;
            state = Status.error;
            for (int i = 0; i < 16; i++)
            {
                registers[i] = "00000000000000000000000000000000";
            }       
        }
        /*public PCB(Process p)
        {
            id = p.id;
            memPos = p.memPos;
            pc = p.pc;
            memDataPos = p.memDataPos;
            jobLength = p.jobLength;
            state = p.state;
            for (int i = 0; i < 16; i++)
            {
                registers[i] = "00000000000000000000000000000000";
            }      
        }*/
        public bool isDone()
        {
            return done;
        }
        
    }
}

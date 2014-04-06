using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using OS_Project;

namespace OS_Project{

    public class PCB{

        public static PCB pcb;
        public int id;
        public int pc;
        public int priority;
        // Will contain the indexes of frames for instructions
        public List<int> logical_memInstr;
        // Will contain the indexes of frames for data
        public List<int> logical_memDataIn;
        public List<int> logical_memDataOut;
        public List<int> pageTable; 

        public int instrLength;
        public int totalLength;

        public int diskInstrStart; 
        public int diskInstrEnd;
        public int diskDataInStart;   
        public int diskDataInEnd;  
        public int diskDataOutStart;  
        public int diskDataOutEnd;

        public enum Status { error, created, ready, waiting, running, terminated }; //created instead of new since new is resered word
        public Status state;
        public Stopwatch elapsedTime;
        public Stopwatch waitingTime;

        public PCB(int id, int priority, int startPos){
            this.id = id;
            pc = 0;
            this.priority = priority;

            diskInstrStart = startPos;
            diskInstrEnd = -1;
            diskDataInStart = -1;
            diskDataInEnd = -1;
            diskDataOutStart = -1;    
            diskDataOutEnd = -1;

            logical_memInstr = new List<int>(0);
            logical_memDataIn = new List<int>(0) ;
            logical_memDataOut = new List<int>(0);
            pageTable = new List<int>(0);

            instrLength = -1;
            totalLength = -1;
            state = Status.created;
            elapsedTime = new Stopwatch();
            waitingTime = new Stopwatch();
        }

        public void printPCBInfo(){
            Console.WriteLine("ID : "+id );
            Console.WriteLine("PC : "+pc );
            Console.WriteLine("Priority   : "+priority );
            Console.WriteLine("Disk Instructions Start   : "+diskInstrStart);
            Console.WriteLine("Disk Intructions End   : "+ diskInstrEnd);
            Console.WriteLine("Disk DataIn Start   : "+diskDataInStart);
            Console.WriteLine("Disk DataIn End   : "+diskDataInEnd );
            Console.WriteLine("Disk DataOut Start   : " + diskDataOutStart);
            Console.WriteLine("Disk DataOut End   : " + diskDataOutEnd);
        }

    }

}

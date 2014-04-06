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

        public int instrLength;
        public int totalLength;

        public List<int> logical_memInstr;
        public List<int> logical_memDataIn;
        public List<int> logical_memDataOut;
        public List<int> pageTable;

        public int[] diskInstrStartPos; // now an array of 2 coordinates
        public int[] diskInstrEndPos;   // now an array of 2 coordinates
        public int[] diskDataStartPos;  // now an array of 2 coordinates
        public int[] diskDataEndPos;    // now an array of 2 coordinates

        public enum Status { error, created, ready, waiting, running, terminated }; //created instead of new since new is resered word
        public Status state;
        public Stopwatch elapsedTime;
        public Stopwatch waitingTime;

        public PCB(int id, int priority, int[] startPos){
            this.id = id;
            pc = 0;
            this.priority = priority;

            logical_memInstr = new List<int>(0);
            logical_memDataIn = new List<int>(0);
            logical_memDataOut = new List<int>(0);
            pageTable = new List<int>(0);

            diskInstrStartPos = new int[2];
            diskInstrEndPos = new int[2];
            diskDataStartPos = new int[2];
            diskDataEndPos = new int[2];    

            instrLength = 0;
            totalLength = -1;

            diskInstrStartPos[0] = startPos[0];
            diskInstrStartPos[1] = startPos[1];

            state = Status.created;
            elapsedTime = new Stopwatch();
            waitingTime = new Stopwatch();

        }

        public void printPCBInfo(){
            Console.WriteLine("ID : "+id );
            Console.WriteLine("PC : "+pc );
            Console.WriteLine("Priority   : "+priority );
            Console.WriteLine("Instructions Length   : " + instrLength);
            Console.WriteLine("Disk Instructions Start   : "+diskInstrStartPos[0]+","+diskInstrStartPos[1] );
            Console.WriteLine("Disk Intructions End   : "+ diskInstrEndPos[0]+","+diskInstrEndPos[1]);
            Console.WriteLine("Disk Data Start   : "+diskDataStartPos[0]+","+diskDataStartPos[1] );
            Console.WriteLine("Disk Data End   : "+diskDataEndPos[0]+","+diskDataEndPos[1] );
        }

    }

}

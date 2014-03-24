using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using OS_Project;

namespace OS_Project
{

    public class PCB
    {
        public static PCB pcb;
        public int id;
        public int pc;
        public int priority;

        public int memInstrStartPos;
        public int memInstrEndPos;

        public int memDataStartPos;
        public int memDataEndPos;

        public int instrLength;
        public int dataLength;
        public int processPosition;
        // Instructions and Data together
        public int totalLength;
        public int sizeInBytes;

        public int diskInstrStartPos;
        public int diskInstrEndPos;
        public int diskDataStartPos;
        public int diskDataEndPos;

        public enum Status { error, created, ready, waiting, running, terminated }; //created instead of new since new is resered word
        public Status state;
        public Stopwatch elapsedTime;
        public Stopwatch waitingTime;
        public int[] registers;

        public PCB(int id, int priority, int diskInstrStartPos){
            this.id = id;
            pc = 0;
            this.priority = priority;

            memInstrStartPos = -1;
            memInstrEndPos = -1;
            memDataStartPos = -1;
            memDataEndPos = -1;

            instrLength = -1;
            dataLength = -1;
            totalLength = -1;
            sizeInBytes = -1;

            this.diskInstrStartPos = diskInstrStartPos;
            diskInstrEndPos = -1;
            diskDataStartPos = -1;
            diskDataEndPos = -1;

            state = Status.created;
            elapsedTime = new Stopwatch();
            waitingTime = new Stopwatch();
            registers = new int[16];
            for (int i = 0; i < 16; i++){
                registers[i] = 0;
            }

        }

        public void printPCBInfo(){
            Console.WriteLine("ID : "+id );
            Console.WriteLine("PC : "+pc );
            Console.WriteLine("Priority   : "+priority );
            Console.WriteLine("Disk Instructions Start   : "+diskInstrStartPos );
            Console.WriteLine("Disk Intructions End   : "+ diskInstrEndPos);
            Console.WriteLine("Disk Data Start   : "+diskDataStartPos );
            Console.WriteLine("Disk Data End   : "+diskDataEndPos );
        }

    }

}

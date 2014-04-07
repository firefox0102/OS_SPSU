using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using OS_Project;

namespace OS_Project{

    public class PCB : IComparable<PCB>{

        public static PCB pcb;
        public int id;
        public int pc;
        public int priority;

        public int instrLength;
        public int totalLength;
        public int totalPages;

        public List<int> logicalMemInstr;
        public List<int> logicalMemData;
        public List<int> pageTable;

        public int diskInstrStartPos; 
        public int diskInstrEndPos;   
        public int diskDataStartPos;  
        public int diskDataEndPos;    

        public enum Status { error, created, ready, waiting, running, terminated }; //created instead of new since new is resered word
        public Status state;
        public Stopwatch elapsedTime;
        public Stopwatch waitingTime;
        public int[] registers;

        public PCB(int id, int priority, int startPos){
            this.id = id;
            pc = 0;
            this.priority = priority;

            logicalMemInstr = new List<int>(0);
            logicalMemData = new List<int>(0);
            pageTable = new List<int>(0);

            diskInstrStartPos = startPos;
            diskInstrEndPos = 0;
            diskDataStartPos = 0;
            diskDataEndPos = 0;    

            instrLength = 0;
            totalLength = 0;
            totalPages = 0;

            registers = new int[16];
    
            state = Status.created;
            elapsedTime = new Stopwatch();
            waitingTime = new Stopwatch();

        }

        public void printPCBInfo(){
            Console.WriteLine("ID : "+id );
            Console.WriteLine("Instruction Length : "+instrLength );
            Console.WriteLine("Instcution Pages Count: " + (int)Math.Ceiling(instrLength / 4.0) );
            Console.WriteLine("Priority   : "+priority );
            Console.WriteLine("Total Length   : " + totalLength);
            Console.WriteLine("Total Pages    : " + totalPages);
            Console.WriteLine("Disk Instructions Start   : "+diskInstrStartPos);
            Console.WriteLine("Disk Intructions End   : "+ diskInstrEndPos);
            Console.WriteLine("Disk Data Start   : "+diskDataStartPos );
            Console.WriteLine("Disk Data End   : "+diskDataEndPos );
            Console.WriteLine("-----------------------------------");
        }

        //********************************************************
        //Sorting stuff
        //********************************************************
        public int CompareTo(PCB p)
        {
            //If you are sorting by Shortest Job First, use this code
            //*************************************************************

            if ((this.instrLength - this.pc) > (p.instrLength - p.pc))
                return 1;
            else if ((this.instrLength - this.pc) < (p.instrLength - p.pc))
                return -1;
            else
                return 0;

            //*************************************************************


            //If you are sorting by Priority, use this code
            //*************************************************************
            /*
            if (this.priority > p.priority)
                return 1;
            else if (this.priority < p.priority)
                return -1;
            else
                return 0;
            */
            //*************************************************************
        }
        public List<int> getLocations(int start, int kind){
            List<int> loc = new List();
            if(kind == 1){
                 //instuctons
                 int x = logicalMemInstr.Count - start;
                 for (int i = 0; i<x; i++){
                     loc.add(logicalMemInstr[start]);
                     start++;
                 }
                 return loc;
             }
             else if(kind ==2){
                 //data
                 int x = logicalMemData.Count - start;
                 for (int i = 0; i<x; i++){
                     loc.add(logicalMemData[start]);
                     start++;
                 }
                 return loc;
             }
             else{
                CustomException ex =
                     new CustomException("Instrcutions or data not specified in geLocations() PCB");
         
                throw ex;
         }
    }

}

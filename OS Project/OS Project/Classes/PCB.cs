using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OS_Project;

namespace OS_Project
{

    class PCB
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
        // Instructions and Data together
        public int totalLength;
        public int sizeInBytes;

        public int diskInstrStartPos;
        public int diskInstrEndPos;
        public int diskDataStartPos;
        public int diskDataEndPos;

        public bool done;
        public enum Status { error, created, ready, waiting, running, terminated }; //created instead of new since new is resered word
        public Status state;

        public List<string> registers;//16 long

        //singleton for remote access in other classes
        public static PCB Instance{
            get{
                return pcb;
            }
        }

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

            done = false;
            state = Status.created;
            for (int i = 0; i < 16; i++){
                registers[i] = "00000000000000000000000000000000";
            }

        }

        public int calcSizeOfJob(PCB job){
            return ((totalLength * 32) / 8);
        }

    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Project{

    class Process{
        public static Process job;
        public int jobID;
        public int priority;
        public int jobLength;

        public int jobSize;

        public int instructionsStartPos;
        public int instructionsEndPos;

        public int dataStartPos;
        public int dataEndPos;

        public enum Status { error, created, ready, waiting, running, terminated }; //created instead of new since new is resered word
        public Status state;

        //singleton for remote access in other classes
        public static Process Instance
        {
            get
            {
                if (job == null)
                {
                    job = new Process();
                }
                return job;
            }
        }
     

        public Process(int id, int priority, int jobLength, int instructionsStartPos){
            this.jobID = id;
            this.priority = priority;
            this.jobLength = jobLength;
            this.instructionsStartPos = instructionsStartPos;

            instructionsStartPos = -1;
            dataStartPos = -1;
            dataEndPos = -1;
            state = Status.created;
        }


    }


}

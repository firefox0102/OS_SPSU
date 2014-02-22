using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Project.Classes
{
    class Process
    {
        private int id;
        private int diskPos;
        private int memPos;
        private int pc;
        private int diskDataPos;
        private int memDataPos;

        private enum State { created, ready, waiting, running, terminated}; //created instead of new since new is resered word
            
        public Process()
        {
            //default constcutor
        }

        //getters and setters? maybe just all public?


    }
}

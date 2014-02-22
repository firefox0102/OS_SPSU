using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Project
{
    public class Dispatcher
    {
        public Process current;
        private bool idle;
        public Dispatcher()
        {
            

            
        }
        //Drew will write the dispatcher
        public void sendProcess(int processId)
        {
            //get from pcb
            //send to cpu/set values for cpu to read
            idle = false;
        }
        //dispatcher seems to send instuctions one at a time to the cpu. we can implement this by calling a get current instuction function or some other way
        public bool isIdle()
        {
            return idle;
        }
        public void terminateProcess()
        {
           //get current process start and end and then clear ram
        }

    }
}

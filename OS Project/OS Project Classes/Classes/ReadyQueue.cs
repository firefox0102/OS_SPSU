using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Project.Classes
{
    class ReadyQueue
    {
        private static ReadyQueue q;
        List<int> readyQ = new List<int>();

        public ReadyQueue() 
        { 
            
        
        }
        public static ReadyQueue GetQueue()
        {
            if (q == null)
            {
                q = new ReadyQueue();
            }
            return q;
        }

        public void addProcess(int ProcessId)
        {
            readyQ.Add(ProcessId);
        }
        public void removeProcess(int ProcessId)
        {
            readyQ.Remove(ProcessId);
        }

        public int getNextJob()
        {
            int i = readyQ[0];//gets first element
            readyQ.RemoveAt(0);//removed first element like a pop front
            return i;

        }

        public int getQueueCount()
        {
            return readyQ.Count();
        }

        


    }
}

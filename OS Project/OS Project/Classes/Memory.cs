using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Project
{
    public class Memory
    {
        public const int maxSize = 1024;
        public List<List<string>> memory;
        public int currentSize;
        
        public Memory()
        {
            memory = new List<List<string>>();
            currentSize = 0;
        }
        
        List<string> getJob(int index)
        {
            return memory[index];
        }
        
        void write(List<string> temp)
        {
            int newTot = temp.Count() + currentSize;
            if ((currentSize < 1024) && (newTot < 1024))
            {
                memory.Add(temp);
                currentSize += temp.Count();
            }
            else
            {
                throw new ArgumentException("Memory overflow");
            }
        }
        
        void removeJob(int index)
        {
            memory.RemoveAt(index);
        }
    }
}

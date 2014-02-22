using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Project
{
    public class Memory
    {
        const int maxSize = 1024;
        List<List<string>> memory 
        
        public Memory()
        {
            memory = new Memory();
        }
        
        string get(int index, int pc)
        {
            return memory[index, pc];
        }
        
        void set(List<string> temp)
        {
            memory.Add(temp);
        }
        
        void remove(int index)
        {
            memory.Remove[index];
        }
    }
}

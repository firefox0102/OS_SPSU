using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OS_Project
{
    public class Memory
    {
        public static Memory Ram;
        public const int maxSize = 1024;
        public List<List<string>> memory;
        public int currentSize;
        
        public Memory()
        {
            memory = new List<List<string>>();
            currentSize = 0;
        }
        public Memory(List<string> temp)
        {
            memory[0] = temp;
        }


        //singleton for remote access in other classes
        public static Memory Instance
        {
            get
            {
                if (Ram == null)
                {
                    Ram = new Memory();
                }
                return Ram;
            }
        }
        public List<string> read(int index)
        {
            return memory[index];
        }
        
        public void write(List<string> temp)
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
        
        public void remove(int index)
        {
            memory.RemoveAt(index);
        }
    }
}

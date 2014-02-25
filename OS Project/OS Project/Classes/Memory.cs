using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OS_Project{
    public class Memory
    {
        public static Memory Ram;
        public const int maxSize = 1024;
        public List<string> memory;
        public int currentSize;
        public int freeSpace;
        
        public Memory(){
            memory = new List<string>(0);
            currentSize = 0;
            freeSpace = 1024;
        }

    }
}

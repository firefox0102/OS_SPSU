using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Project
{
    class Disk
    {
        List<int> Disk {get; set;}
        
        void AddToDisk(int Data)
        {
            Disk.add(Data);
        }
        
        GetData(int DataStart, int DataSize)
        {
            list<int> FromDisk = new List<int>();
            
            for(int i = DataStart; i < (DataStart + DataSize); i++)
                FromDisk.add(Disk[i]);
                
            return FromDisk;
        }
    
    }
}

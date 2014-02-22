using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Project
{
    class Disk
    {
        List<string> Disk {get; set;}
        
        public Disk()
        {
            Disk = new List<string>();
        }
        
        void AddToDisk(string Data)
        {
            Disk.add(Data);
        }
        
        GetData(int DataStart, int DataSize)
        {
            list<string> FromDisk = new List<string>();
            
            for(int i = DataStart; i < (DataStart + DataSize); i++)
                FromDisk.add(Disk[i]);
                
            return FromDisk;
        }
        
        
    }
}

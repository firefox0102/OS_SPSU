using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Project
{
    public class Disk
    {
        List<list<string>> Disk;
        List<list<int>> DiskRecord;
        
        public Disk()
        {
            Disk = new List<string>();
        }
        
        void AddToDisk(string Data)
        {
            Disk.add(Data);
        }
        
        list<string> GetData(int DataStart)
        {
            return Disk[DataStart];
        }
    }
}

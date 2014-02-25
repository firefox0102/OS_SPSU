using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Project{

    public class Disk{

        public static Disk disk;
        public List<string> diskData;
        public List<PCB> diskProcessTable;
        public int diskSize;
        public int numberProcesses;

        public Disk(){
            diskData = new List<string>(0);
            diskProcessTable = new List<PCB>(0);
            diskSize = 0;
            numberProcesses = 0;
        }

        //singleton for remote access in other classes
        public static Disk Instance{
            get{
                if (disk == null){
                    disk = new Disk();
                }
                return disk;
            }
        }
     

    }

}

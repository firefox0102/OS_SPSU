using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OS_Project;

namespace OS_Project.Classes{

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
        public static Disk Instance
        {
            get
            {
                if (disk == null)
                {
                    disk = new Disk();
                }
                return disk;
            }
        }

        public void printDiskData(){
            for (int i = 0; i < diskData.Count; i++ ){
                Console.WriteLine("Line "+i+": "+ diskData[i]);
            }
            Console.WriteLine("Disk size: " + diskSize + " Bytes");
        }

        public void printDiskProcessTable(){
            foreach (PCB job in diskProcessTable){
                job.printPCBInfo();
            }
        }

        public int calcDiskSize(){
            int tempSize = 0;
            for(int i = 0; i < diskProcessTable.Count; ++i){
                tempSize += diskProcessTable[i].totalLength;
            }
            diskSize = tempSize;
            return tempSize;
        }


    }

}

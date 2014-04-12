using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OS_Project;

namespace OS_Project{

    public class Disk{

        public static Disk disk;
        //Changed disk data structure to a List of Lists containing 4 words each (1 page)
        public List<List<string>> diskData;
        public int currentPage;
        public int currentElement;
        public List<PCB> diskProcessTable;
        public int diskUsedPages;
        public int numberProcesses;

        public Disk(){
            // New disk structure is 512 pages of 4 words each
            diskData = new List<List<string>>(512);
            for (int i = 0; i < 512; i++){
                List<string> temp = new List<string>(4);
                diskData.Add(temp);
            }

            diskProcessTable = new List<PCB>(0);
            diskUsedPages = 0;
            numberProcesses = 0;
            currentPage = 0;
            currentElement = 0;
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
        
        public void printDiskProcessTable(){
            foreach (PCB job in diskProcessTable){
                job.printPCBInfo();
            }
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("TOTAL USED DISK PAGES: " + diskUsedPages);


        }


    }


}

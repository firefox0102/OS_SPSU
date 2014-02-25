using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OS_Project;

namespace OS_Project.Classes
{
    class Loader
    {
        static void Main(string[] args)
        {

            Disk disk = new Disk();

            string[] lines = System.IO.File.ReadAllLines(@"D:\\j\\Documents\\CS 3243-OS\\OS_SPSU-master\\OS Project\\OS Project\\Classes\\DataFile.txt");
            lines.ToList();

            int jobID = 0;
            int diskPos = 0;

            foreach (string line in lines)
            {
                Console.WriteLine("This is disk Position: " + diskPos);
                string tempLine = "";
                int tempBinary = 0;
                if (line.Contains("JOB"))
                {

                    jobID++;

                    int lineLength = line.Count();
                    // Gets the priority and converts it to an int
                    int tempPriority = Convert.ToInt32(line.Substring(lineLength - 1));
                    disk.diskProcessTable.Add(new PCB(jobID, tempPriority, diskPos));
                    disk.numberProcesses++; 
                }
                else if (line.Contains("Data"))
                {
                    disk.diskProcessTable[jobID - 1].diskInstrEndPos = diskPos - 1;
                    disk.diskProcessTable[jobID - 1].diskDataStartPos = diskPos;
                    disk.diskProcessTable[jobID - 1].instrLength = disk.diskProcessTable[jobID - 1].diskInstrEndPos - disk.diskProcessTable[jobID - 1].diskInstrStartPos;
                }
                else if (line.Contains("END"))
                {
                    disk.diskProcessTable[jobID - 1].diskDataEndPos = diskPos - 1;
                    disk.diskProcessTable[jobID - 1].dataLength = disk.diskProcessTable[jobID - 1].diskDataEndPos - disk.diskProcessTable[jobID - 1].diskDataStartPos;
                    disk.diskProcessTable[jobID - 1].totalLength = disk.diskProcessTable[jobID - 1].instrLength + disk.diskProcessTable[jobID - 1].dataLength + 2;
                    disk.diskProcessTable[jobID - 1].sizeInBytes = disk.diskProcessTable[jobID - 1].totalLength * 4;
                    
                }
                else
                {
                    //will call the convert hex to binary then to string
                    tempLine = line.Substring(2);
                    Console.WriteLine(tempLine);
                    tempBinary = Convert.ToInt32(tempLine, 16);
                    tempLine = Convert.ToString(tempBinary, 2);

                    //Necessary to add leading zeros to ensure 32 bits
                    while (tempLine.Count() != 32){
                        tempLine = tempLine.Insert(0, "0");
                    }
                    

                    disk.diskData.Add(tempLine);
                    diskPos++;
                }


            }
            disk.calcDiskSize();
            disk.printDiskProcessTable();
            disk.printDiskData();

        }//end main


    }//end class

}//end namespace
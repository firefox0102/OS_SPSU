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
        public static Disk disk = Disk.Instance;

        public static void load()
        {

            

            //Need to find a standard place for the file within our visual studio project folder
            string[] lines = System.IO.File.ReadAllLines(@"C:\users\peter\documents\visual studio 2013\Projects\OS Project\OS Project\DataFile2-Cleaned.txt");
            lines.ToList();

            int jobID = 0;
            int diskPos = 0;

            foreach (string line in lines)
            {
                Console.WriteLine("This is disk Position: " + diskPos);
                string tempLine = "";
                if (line.Contains("JOB"))
                {

                    jobID++;
                    // Gets the priority and converts it to an int
                    int lastSpace = line.LastIndexOf(' ');
                    int tempPriority = Convert.ToInt32(line.Substring(lastSpace + 1), 16);
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
                    //Multiply each line in the job by 32 for number of bits and then divide by 8 to get bytes
                    disk.diskProcessTable[jobID - 1].sizeInBytes = (disk.diskProcessTable[jobID - 1].totalLength);

                }
                else
                {
                    //will call the convert hex to binary then to string
                    tempLine = line.Substring(2);
                    Console.WriteLine(tempLine);
                    disk.diskData.Add(tempLine);
                    diskPos++;
                }
            }


        }//end main


    }//end class

}//end namespace

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OS_Project;

namespace OS_Project{

    public class Loader{
        //public static Disk disk = Disk.Instance;

        static void Main()
        {
            Disk disk = new Disk();
            //Need to find a standard place for the file within our visual studio project folder
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\j\Documents\Visual Studio 2012\Projects\OS_TESTER\OS_TESTER\DataFile2-Cleaned.txt");
            lines.ToList();

            int jobID = 0;
            int diskPos = 0;
            // Creates a temp array that will pass the Frame information 
            // about a Procss to the PCB for storage
            int[] tempInstrStartPos = new int[2];
            int[] tempInstrEndPos = new int[2]{-1,-1};
            int[] tempDataStartPos = new int[2]{-1,-1};
            int[] tempDataEndPos = new int[2]{-1, -1};

            foreach (string line in lines)
            {
                string tempLine = "";
                if (line.Contains("JOB"))
                {
                    jobID++;
                    // Gets the priority and converts it to an int
                    int lastSpace = line.LastIndexOf(' ');
                    int tempPriority = Convert.ToInt32(line.Substring(lastSpace + 1), 16);
                    tempInstrStartPos = new int[2] { disk.currentPage, disk.currentElement };
                    disk.diskProcessTable.Add(new PCB(jobID, tempPriority, tempInstrStartPos));
                    disk.numberProcesses++;
                }
                else if (line.Contains("Data"))
                {
                    disk.diskProcessTable[jobID - 1].diskInstrEndPos = new int[] { disk.currentPage, disk.currentElement - 1 };
                    disk.diskProcessTable[jobID - 1].diskDataStartPos = new int[] { disk.currentPage, disk.currentElement };
                    // WILL CREATE A WAY TO CALCULATE JOB LENGTH
                    //disk.diskProcessTable[jobID - 1].instrLength = disk.diskProcessTable[jobID - 1].diskInstrEndPos - disk.diskProcessTable[jobID - 1].diskInstrStartPos;
                }
                else if (line.Contains("END"))
                {
                    disk.diskProcessTable[jobID - 1].diskDataEndPos = new int[] { disk.currentPage, disk.currentElement - 1 };
                    //WILL NEED TO RECALCULATE THIS VARIABLES AT SOME POINT
                    //disk.diskProcessTable[jobID - 1].dataLength = disk.diskProcessTable[jobID - 1].diskDataEndPos - disk.diskProcessTable[jobID - 1].diskDataStartPos;
                    //disk.diskProcessTable[jobID - 1].totalLength = disk.diskProcessTable[jobID - 1].instrLength + disk.diskProcessTable[jobID - 1].dataLength + 2;
                    //Multiply each line in the job by 32 for number of bits and then divide by 8 to get bytes
                    //disk.diskProcessTable[jobID - 1].sizeInBytes = (disk.diskProcessTable[jobID - 1].totalLength);

                }
                else
                {
                    // Checks to see if currentElement is greater than 3
                    // if it is it will mean a new page is 
                    if (disk.currentElement > 3){
                        disk.currentElement = 0;
                        disk.currentPage++;
                    } 
                    //will call the convert hex to binary then to string
                    tempLine = line.Substring(2);
                    Console.WriteLine(tempLine);
                    disk.diskData[disk.currentPage].Add(tempLine);
                    disk.currentElement++;
                    diskPos++;
                }
            }
            disk.printDiskProcessTable();
            Console.Read();

        }//end main
   

    }//end class



}//end namespace

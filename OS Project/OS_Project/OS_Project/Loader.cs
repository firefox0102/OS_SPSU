using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OS_Project;

namespace OS_Project
{

    public class Loader
    {
        //public static Disk disk = Disk.Instance;

        public void load()
        {
            //Need to find a standard place for the file within our visual studio project folder
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\pfinn\Documents\GitHub\OS_SPSU\OS Project\OS_Project\OS_Project\DataFile2-Cleaned.txt");
            lines.ToList();


            // Creates a temp array that will pass the Frame information 
            // about a Procss to the PCB for storage

            int jobID = 0;
            bool stillInstr = true;

            foreach (string line in lines)
            {
                if (line.Contains("JOB"))
                {
                    jobID++;
                    stillInstr = true;

                    // Gets the priority and converts it to an int
                    int lastSpace = line.LastIndexOf(' ');
                    string stringPriority = line.Substring(lastSpace + 1);
                    int tempPriority = Convert.ToInt32(stringPriority, 16);

                    int tempInstrStartPos = Disk.Instance.currentPage;
                    Disk.Instance.diskProcessTable.Add(new PCB(jobID, tempPriority, tempInstrStartPos));
                    Disk.Instance.numberProcesses++;
                }
                else if (line.Contains("Data"))
                {
                    if (Disk.Instance.currentElement > 0)
                    {
                        stillInstr = false;
                        Disk.Instance.diskProcessTable[jobID - 1].diskInstrEndPos = Disk.Instance.currentPage;
                        Disk.Instance.currentPage++;
                        Disk.Instance.currentElement = 0;
                        Disk.Instance.diskUsedPages++;
                        Disk.Instance.diskProcessTable[jobID - 1].diskDataStartPos = Disk.Instance.currentPage;
                    }
                    else
                    {
                        stillInstr = false;
                        Disk.Instance.diskProcessTable[jobID - 1].diskInstrEndPos = Disk.Instance.currentPage - 1;
                        Disk.Instance.currentElement = 0;
                        Disk.Instance.diskProcessTable[jobID - 1].diskDataStartPos = Disk.Instance.currentPage;
                    }
                }
                else if (line.Contains("END"))
                {
                    if (Disk.Instance.currentElement > 0)
                    {
                        Disk.Instance.diskProcessTable[jobID - 1].diskDataEndPos = Disk.Instance.currentPage;
                        Disk.Instance.diskProcessTable[jobID - 1].totalPages = (int)Math.Ceiling(Disk.Instance.diskProcessTable[jobID - 1].totalLength / 4.0);
                        Disk.Instance.currentPage++;
                        Disk.Instance.currentElement = 0;
                        Disk.Instance.diskUsedPages++;
                    }
                    else
                    {
                        Disk.Instance.diskProcessTable[jobID - 1].diskDataEndPos = Disk.Instance.currentPage - 1;
                        Disk.Instance.diskProcessTable[jobID - 1].totalPages = (int)Math.Ceiling(Disk.Instance.diskProcessTable[jobID - 1].totalLength / 4.0);
                        Disk.Instance.currentElement = 0;
                    }

                }
                else
                {
                    // Checks to see if currentElement is greater than 3
                    // if it is it will mean a new page is


                    //will call the convert hex to binary then to string
                    string tempLine = line.Substring(2);
                    Disk.Instance.diskData[Disk.Instance.currentPage].Add(tempLine);

                    if (stillInstr) Disk.Instance.diskProcessTable[jobID - 1].instrLength++;
                    Disk.Instance.diskProcessTable[jobID - 1].totalLength++;

                    Disk.Instance.currentElement++;
                    if (Disk.Instance.currentElement > 3)
                    {
                        Disk.Instance.currentElement = 0;
                        Disk.Instance.currentPage++;
                        Disk.Instance.diskUsedPages++;
                    }
                }



            }
            Disk.Instance.printDiskProcessTable();
            Console.ReadLine();
            //LongTermScheduler.Instance.addToSTScheduler();
            //Memory.Instance.printMemDump();
            //Console.ReadLine();
            //for (int i = 0; i < Memory.Instance.pageManager.Count; i++)
            //{
            //    Console.WriteLine(Memory.Instance.pageManager[i]);
            //}
            //Console.ReadLine();

        }//end main


    }//end class



}//end namespace

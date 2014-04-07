using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OS_Project;

namespace OS_Project
{
    public class LongTermScheduler{
        // Will first get empty pages from RAM
        // then will take next job not loaded into RAM
        // load job into RAM into available pages.
        // Will have to update the the RAM emptyFrameIndexes
        // to show no longer empty.

        public static LongTermScheduler LTS;

        //Processes that have been loaded into RAM
        public List<int> LoadedProcesses;
        public int nextJob;

        public LongTermScheduler()
        {
            nextJob = 1;
            LoadedProcesses = new List<int>();
        }

        //singleton for remote access in other classes
        public static LongTermScheduler Instance
        {
            get
            {
                if (LTS == null)
                {
                    LTS = new LongTermScheduler();
                }
                return LTS;
            }
        }

        public void addToSTScheduler(){
        // will need to ensure that the STS has a queue that
        // can be accessed without disrupting operation
            PCB tempPCB = Disk.Instance.diskProcessTable[0];
            //if (Memory.Instance.emptyFrameIndexes.Count > tempPCB.totalPages){

                // Instr Coordinates
                int current_instrStartPage =  tempPCB.diskInstrStartPos;
                int current_instrEndPage = tempPCB.diskInstrEndPos;
  
                // Data Coordinates
                int current_dataStartPage = tempPCB.diskDataStartPos;
                int current_dataEndPage = tempPCB.diskDataEndPos;

                int j = 0;
                for (; current_instrStartPage <= current_instrEndPage; )
                {
                    try
                    {
                        string element = Disk.Instance.diskData[current_instrStartPage][j];
                        Memory.Instance.memory[Memory.Instance.emptyFrameIndexes[0]].Add(element);
                    }
                    catch
                    {
                        break;
                    }
                    j++;
                    if (j > 3)
                    {
                        Memory.Instance.emptyFrameIndexes.RemoveAt(0);
                        current_instrStartPage++;
                        j = 0;
                    }
                }
   

                j = 0;
                for (; current_dataStartPage <= current_dataEndPage; )
                {
                    try
                    {
                        string element = Disk.Instance.diskData[current_dataStartPage][j];
                        Memory.Instance.memory[Memory.Instance.emptyFrameIndexes[0]].Add(element);
                    }
                    catch
                    {
                        break;
                    }
                    j++;
                    if (j > 3)
                    {
                        Memory.Instance.emptyFrameIndexes.RemoveAt(0);
                        current_dataStartPage++;
                        j = 0;
                    }
                }



                // Load the Processes Instructions and Data into RAM
                // Update the PCB's logical_memInstr
                // Update the PCB's logical_memData
                // Update RAM emptyFrameIndexes
                // Update the PCB's pageTable 

            //}



            
        }



        
    }// end of class
}// end of namespace

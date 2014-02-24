using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OS_Project
{
    public class Loader
    {
        /// <summary>
        /// The loader will load the file information onto the disk
        /// the disk will create a process with the object variables
        /// - Job Number
        /// - Priority
        /// - Job length
        /// - List for instructions
        /// - List for data
        /// The file lines will be parsed into 32 bit lenth binary
        /// except for first three variables which are created first
        /// from the first commented lines of the file.
        /// </summary>
        
        string getFromFile(){
            Disk disk = new Disk();
            
           List<string> lines = File.ReadAllLines("DataFile2-Jobs1+2.txt").ToList();

           List<Process> processList = new List<Process>(0);
           List<int> jobLocations;

           int jobID = 0;
           int jobLocation = -1;
           int diskLinePosition = 0;

           // Goes through each line from the file
           // and finds the jobs and creates a process
           // this process is added to the processList
           // which will be given to the disk
           foreach(string line in lines){
               if (line.Contains("// Job")) {
                   jobID++;
                   jobLocation++;
                   int lineLength = line.Count();
                   // Gets the priority and converts it to an int
                   int tempPriority = int.Parse( ( line.Substring( lineLength - 1 ) ) ); 


                   disk.diskProcessTable.Add(new Process(jobID));

               }
               else if(line.Contains("// Data")){

               }
               else if(line.Contains("// End")){

               }
               else{
                   //will call the convert hex to binary then to string
                    
                   disk.diskData.Add(line);
               }
                 
        }
        
        string convertToBinaryString(string temp){
            // Strips the hexadecimal tag from the front of lines
            // Converts the hex to binary
            // Casts the binary as a string
            string convertedText = "";
            temp = temp.Substring(2);
            int temp = Convert.ToInt32(line, 2);

            return convertedText;
        }


    }
}

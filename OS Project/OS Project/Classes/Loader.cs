using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OS_Project.Classes;
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

        StreamReader sr;
        
        public Loader()
        {
            StreamReader sr = new StreamReader();
        }
        
        string getFromFile()
        {
            if( !sr.eof() ){
                
                    Process temp = new Process();
                }
            }
        
        string convertToBinaryString(string temp){
            // Strips the hexadecimal tag from the front of lines
            // Converts the hex to binary
            // Casts the binary as a string         
            string convertedText = temp.Substring(2);
            

            return convertedText;
        }
    }
}

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
        StreamReader sr;
        
        public Loader()
        {
            StreamReader sr = new StreamReader();
        }
        
        string getFromFile()
        {
            if(!sr.eof())
            {
                if(/*I didn't hit the next// marks*/)
                {
                    //dont forget to create the process class
                    Process temp = new Process();
                }
            }
        }
        
        string convertToBinaryString()
        {
            
        }
    }
}

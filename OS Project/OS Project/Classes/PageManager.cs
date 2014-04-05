using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Project
{
    class PageManager
    {
        
        //Constructor
        public static PageManager pm;

        //singleton for remote access in other classes
        public static PageManager Instance
        {
            get
            {
                if (pm == null)
                {
                    pm = new PageManager();
                }
                return pm;
            }
        }

        public void Clean()
        {
            //clean the terminated processes from where???
        }

        public int NumEmptyFrames()
        {
            //return how many pages are available

        }

        public int NextEmptyFrame()
        {
            //return the number for the next empty frame in RAM
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Project.Classes
{
    class ShortTermScheduler
    {
        List<PCB> PCBList;

        //singleton for remote access in other classes
        public static LongTermScheduler Instance
        {
            get
            {
                if (lts == null)
                {
                    lts = new LongTermScheduler();
                }
                return lts;
            }
        }


    }
}

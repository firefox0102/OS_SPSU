using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Project.Classes
{
    class ShortTermScheduler
    {
        public static ShortTermScheduler sts;
        List<PCB> PCBList;

        //singleton for remote access in other classes
        public static ShortTermScheduler Instance
        {
            get
            {
                if (sts == null)
                {
                    sts = new ShortTermScheduler();
                }
                return sts;
            }
        }

        public void AddToShortTermScheduler(PCB p)
        {
            PCBList.Add(p);
        }
    }
}

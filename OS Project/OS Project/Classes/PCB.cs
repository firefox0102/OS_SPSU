using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OS_Project.Classes;

namespace OS_Project.Classes
{
    class PCB
    {
        public List<PCBObject> PCB;

        public PCB() { }
        public PCBObject getPCBWithId(int id)
        {
            for(int i = 0; i <PCB.Count() ;i ++)
            {
                if(PCB[i].id == id)
                {
                    return PCB[i];
                }
            }
            PCBObject error = new PCBObject();
            return error;

        }
        public void add(PCBObject x)
        {
            PCB.Add(x);
        }

    }
}

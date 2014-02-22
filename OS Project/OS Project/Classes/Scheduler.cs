using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Project
{
    class Scheduler
    {
        LongTermScheduler lts;
        ShortTermScheduler sts;
        
        public Scheduler()
        {
            lts = new LongTermScheduler();
            sts = new ShortTermScheduler();
        }
        
        //make sure MAIN calls the update continuously
        public void Update()
        {
            LongTermScheduler.Update();
            ShortTermScheduler.Update();
        }
    }
    
    class LongTermScheduler()
    {
        //get a job from the disk, pass that job to RAM IF SPACE IF AVAILABLE
        public void Update()
        {
            //update logic   
        }
    }
    
    class ShortTermScheduler()
    {
        //pulls off of RAM and passes to the dispatcher
        public void Update()
        {
            //update logic
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OS_Project.Classes
{
    public class CPU
    {
        public int[] register;
        //do we need a program cache????
        public Process currentProcess;
        public int processPosition;
        //accumulator stores results
        // zero is register always set to zero
        public const int Accumulator = 0;
        public const int Zero = 0;

        public CPU()
        {
            register = new int[16];
            processPosition = 0;

        }

        void run()
        {
            Fetch();
            Decode();
            Execute();
        }


        void Fetch()
        {
            //is passed pcb id then gets pcb info

        }

        void Decode()
        {

        }

        void Execute()
        {


        }

        void Arithmetic()
        {

        }

        void BranchandImmediate()
        {

        }

        void UnconditionalJump()
        {

        }

        void IO()
        {

        }

    }
}


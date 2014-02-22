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
            String opCode = currentProcess.opCode;
            int tempRegister1 = currentProcess.Register1;
            int tempRegister2 = currentProcess.Register2;
            int address = currentProcess.Address;
            string hex = "0";
            
            switch(opCode)
            {
                case "000000":
                    if(tempRegister1 != 0 && tempRegister2 != 0)
                    {
                        //I have no idea what to even do here
                    }
                    else
                    {
                        //Again, what the heck do I do here
                    }
                    register[Accumulator] += int.Parse(hex, System.Globalization.NumberStyles.HexNumber)
                    break;
                case "000001":
                    string write = "0x" + register[Accumulator].ToString("X8");
                    break;
            }
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


﻿using System;
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
        public String currentProcess;
        public PCB currentPCB;
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
            //supposed to convert to binary, but it doesn't need to decode because its already in binary
        }

        void Execute()
        {
            string instructionFormat = currentProcess.Substring(0, 2);

            if (instructionFormat == "00")
                Arithmetic();
            else if (instructionFormat == "01")
                BranchandImmediate();
            else if (instructionFormat == "10")
                UnconditionalJump();
            else if (instructionFormat == "11")
                IO();
            else
                Console.Out.WriteLine("INSTRUCTION FORMAT DETERMINATION MUFFED UP");
            
        }

        void Arithmetic()
        {
            string opCode = currentProcess.Substring(2, 6);
            string s1 = currentProcess.Substring(8, 4);
            string s2 = currentProcess.Substring(12, 16);


        }

        void BranchandImmediate()
        {

        }

        void UnconditionalJump()
        {

        }

        void IO()
        {
        String opCode = currentProcess.Substring(2,4);
            int tempRegister1 = currentPCB.registers[2];
            int tempRegister2 = currentPCB.registers[3];
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
    }
}


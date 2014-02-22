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
            int s1 = Int32.Parse(currentProcess.Substring(8, 4));
            int s2 = Int32.Parse(currentProcess.Substring(12, 4));
            int D = Int32.Parse(currentProcess.Substring(16, 4));

            switch (opCode)
            {
                case "000100":  //MOV
                    register[s1] = register[s2];
                    break;
                case "000101":  //ADD
                    register[D] = register[s1] + register[s2];
                    break;
                case "000110":  //SUB
                    register[D] = register[s1] - register[s2];
                    break;
                case "000111":  //MUL
                    register[D] = register[s1] * register[s2];
                    break;
                case "001000":  //DIV
                    register[D] = register[s1] / register[s2];
                    break;
                case "001001":  //AND
                    if (register[s1] != 0 && register[s2] != 0)
                        register[D] = 1;
                    else
                        register[D] = 0;
                    break;
                case "001010":  //OR
                    if (register[s1] == 0 && register[s2] == 0)
                        register[D] = 0;
                    else
                        register[D] = 1;
                    break;
                case "010000":  //SLT
                    if (register[s1] < register[s2])
                        register[D] = 1;
                    else
                        register[D] = 0;
                    break;
                default : 
                    break;

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


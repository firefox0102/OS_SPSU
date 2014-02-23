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
        //public PCB currentPCB;
        public int processPosition;
        //accumulator stores results
        // zero is register always set to zero
        public const int Accumulator = 0;
        public const int Zero = 0;
        public List<String> instructionList;
        public CPU()
        {
            register = new int[16];
            processPosition = 0;

        }

        public void run()
        {
            Fetch();
            Decode();
            Execute();
        }


        private void Fetch()
        {
            //is passed pcb id then gets pcb info
            //FILLS instructionList
           // Dispatcher.Instance.sendProcess();
            instructionList = new List<string>(new string[] {   "0xC050005C",
                                                                "0x4B060000",
                                                                "0x4B010000",
                                                                "0x4B000000",
                                                                "0x4F0A005C",
                                                                "0x4F0D00DC",
                                                                "0x4C0A0004",
                                                                "0xC0BA0000",
                                                                "0x42BD0000",
                                                                "0x4C0D0004",
                                                                "0x4C060001",
                                                                "0x10658000",
                                                                "0x56810018",
                                                                "0x4B060000",
                                                                "0x4F0900DC",
                                                                "0x43970000",
                                                                "0x05070000",
                                                                "0x4C060001",
                                                                "0x4C090004",
                                                                "0x10658000",
                                                                "0x5681003C",
                                                                "0xC10000AC",
                                                                "0x92000000" });

        }

        private void Decode()
        {
            //supposed to convert to binary, but it doesn't need to decode because its already in binary
        }

        private void Execute()
        {
            for(int i = 0; i<instructionList.Count; i++ )
            {
                currentProcess = instructionList[i];
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
            
            
        }

        void Arithmetic()
        {
            string opCode = currentProcess.Substring(2, 6);
            int s1 = Convert.ToInt32(currentProcess.Substring(8, 4),2);
            int s2 = Convert.ToInt32(currentProcess.Substring(12, 4),2);
            int D = Convert.ToInt32(currentProcess.Substring(16, 4),2);

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
                default:
                    break;

            }
        }
        
        
        String convertAddress(int address)
        {
            String newCurrentProcess;
            String currentaddress = address.ToString();
            int decimaladdress = Convert.ToInt32(currentaddress, 2);
            newCurrentProcess = instructionList[decimaladdress];

            return newCurrentProcess;
        }





        void BranchandImmediate()
        {
            String opCode = currentProcess.Substring(2, 4);
            int B = Convert.ToInt32(currentProcess.Substring(8,4),2);
            int D = Convert.ToInt32(currentProcess.Substring(12,4),2);
            int address = Convert.ToInt32(currentProcess.Substring(16,16),2);
           
            switch (opCode)
            {
                case "001011":  //MOVI
                    register[D] = register[B];
                    break;
                case "001100":  //ADDI
                    register[D] = register[B] + address;
                    break;
                case "001101":  //MULI
                    register[D] = register[B] * address;
                    break;
                case "001110":  //DIVI
                    register[D] = register[B] / address;
                    break;
                case "001111":  //LDI
                    register[D] = address;
                    break;
                case "010001":  //SLTI
                    if (register[B] < address)
                        register[D] = 1;
                    else
                        register[D] = 0;
                    break;
                case "010101":  //BEQ
                    if (register[D] == register[B])
                        currentProcess = convertAddress(address);
                    break;
                case "010110":  //BNE
                    if (register[D] != register[B])
                        currentProcess = convertAddress(address);
                    break;
                case "010111":  //BEZ
                    if (register[D] == register[Zero])
                        currentProcess = convertAddress(address);
                    break;
                case "011000":  //BNZ
                    if (register[D] != register[Zero])
                        currentProcess = convertAddress(address);
                    break;
                case "011001":  //BGZ
                    if (register[D] > register[Zero])
                        currentProcess = convertAddress(address);
                    break;
                case "011010":  //BLZ
                    if (register[D] < register[Zero])
                        currentProcess = convertAddress(address);
                    break;
            }
        }

        void UnconditionalJump()
        {
            String opCode = currentProcess.Substring(2, 4);
            int address = Int32.Parse(currentProcess.Substring(8, 24));
            switch (opCode)
            {
                case "010010":  //HLT
                    //End of program
                    //currentPCB.done=true;
                    break;
                case "010100":  //JMP
                    currentProcess = convertAddress(address);
                    break;
            }
        }

        void IO()
        {
            String opCode = currentProcess.Substring(2, 4);
            int tempRegister1 = Int32.Parse(currentProcess.Substring(8, 4));
            int tempRegister2 = Int32.Parse(currentProcess.Substring(12, 4));
            int address = Int32.Parse(currentProcess.Substring(16, 16));
            int ipBuffer = 0;

            switch (opCode)
            {
                case "000000":
                    if (tempRegister1 != 0 && tempRegister2 != 0)
                    {
                        ipBuffer = Int32.Parse(convertAddress(tempRegister2));
                    }
                    {
                        ipBuffer = Int32.Parse(convertAddress(address));
                    }
                    register[0] += ipBuffer;
                    break;
                case "000001":
                    {
                        string opBuffer = register[0].ToString();

                        break;

                    }
        
        }

        }
    }
}


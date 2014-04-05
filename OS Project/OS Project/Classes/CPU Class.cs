using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;



namespace OS_Project
{
     public class CPU
    {
        public int[] register;

        public String currentProcess;
        public PCB currentPCB;
        public int processPosition;
        public const int Accumulator = 0;
        public const int Zero = 1;
        public int pc = 0;
        public int pageSet = 1;
        public List<String> instructionCache;
        public List<String> inputCache;
        public List<String> outputCache;
        public bool idle;
        public int[] iocounter = new int[33];
        
        
        public CPU()
        
        {
            register = new int[16];
            processPosition = 0;

        }

        public void run()
        {
            pc = 0;
            Console.WriteLine(pc);

            Fetch();
            Execute();
            Console.WriteLine("this job is: "+this.currentPCB.id);

        }


        private void Fetch()
        {
             
             
             
             Dispatcher.Instance.sendProcess(this);
            //is passed pcb id then gets pcb info
            //FILLS instructionList
         

        }


        public void Execute()
        {

            
            for(pc = 0; pc<currentPCB.instrLength  ; pc++ )//get from pcb
            {
                //converts the pc to operate inside of the instruction cache and not the full list of instructions
                if (pageSet > 1)
                {
                    pc = pc - ((pageSet - 1) * 16);
                }

                //pagefault
                if(pc<0)
                {
                    instructionCache = PageManager.Instance.PageFault(pageSet, "down");
                    pageSet--;
                }

                //pc is in current page context and executes instruction
                else if (pc >= 0 &&pc < 16)
                {

                    //              Console.WriteLine(pc);
                    currentProcess = Convert.ToString(Convert.ToInt32(instructionCache[pc], 16), 2);
                    currentProcess = currentProcess.PadLeft(32, '0');

                    string instructionFormat = currentProcess.Substring(0, 2);

                    if (instructionFormat.Equals("00"))
                    {
                        Arithmetic();
                        //           Console.WriteLine(pc);
                    }
                    else if (instructionFormat.Equals("01"))
                    {
                        BranchandImmediate();
                        //           Console.WriteLine(pc);
                    }
                    else if (instructionFormat.Equals("10"))
                        UnconditionalJump();
                    else if (instructionFormat.Equals("11"))
                        IO();
                    else
                        Console.Out.WriteLine("INSTRUCTION FORMAT DETERMINATION MUFFED UP");

                    /*           for (int j = 0; j < 16; j++)
                               {
                                   Console.WriteLine(register[j]);
                                   //    Console.WriteLine("j is equal to" + j);
                               }
                               Console.WriteLine("i is equal to::" + pc);
                      */
                }

                //pagefault
                else if (pc >= 16)
                {

                    instructionCache = PageManager.Instance.PageFault(currentPCB, pageSet,"up");
                    pageSet++;
                }
            }
            
            idle = true;
            currentPCB.state = PCB.Status.terminated;
            currentPCB.elapsedTime.Stop();            
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
        
        String Decode(int address)
        {
            String newCurrentProcess;
            String currentaddress = address.ToString();
            newCurrentProcess = Convert.ToString(address, 2);
            newCurrentProcess = newCurrentProcess.PadLeft(32, '0');


            return newCurrentProcess;
        }

        void BranchandImmediate()
        {
            String opCode = currentProcess.Substring(2, 6);
            int B = Convert.ToInt32(currentProcess.Substring(8,4),2);
            int D = Convert.ToInt32(currentProcess.Substring(12,4),2);
            int address = Convert.ToInt32(currentProcess.Substring(16,16),2);
           
            switch (opCode)
            {
                case "001011":  //MOVI
                    register[D] = address;
                    break;
                case "001100":  //ADDI
                    if (address == 4)
                        register[D] += address / 4;
                    else if (address == 0)
                        register[D] = register[B] + address;
                    else
                        register[D] = register[D] + address;
                    break;
                case "001101":  //MULI
                    register[D] += B * (address);
                    break;
                case "001110":  //DIVI
                    register[D] += B / (address);
                    break;
                case "001111":  //LDI
                    register[D] = address/4;
                    break;
                case "010001":  //SLTI
                    if (register[B] < (address))
                        register[D] = 1;
                    else
                        register[D] = 0;
                    break;
                case "010101":  //BEQ
                    if (register[D] == register[B])
                    {
                        pc = (address/4)-1;
                    }
                    break;
                case "010110":  //BNE
                    if (register[D] != register[B])
                    {
                        pc = (address/4)-1;
                    }
                    break;
                case "010111":  //BEZ
                    if (register[D] == register[Zero])
                    {
                        pc = (address/4)-1;
                    }
                    break;
                case "011000":  //BNZ
                    if (register[D] != register[Zero])
                    {
                        pc = (address/4)-1;
                    }
                    break;
                case "011001":  //BGZ
                    if (register[D] > register[Zero])
                    {
                        pc = (address/4)-1;
                    }
                    break;
                case "011010":  //BLZ
                    if (register[D] < register[Zero])
                    {
                        pc = (address/4)-1;
                    }
                    
                    break;
                case "000010": //ST
                    if (D != 0 && B != 0)
                    {
                        if(D<16)
                            instructionCache[register[D]] = register[B].ToString(); 
                        if(D>=16 && D<32)
                            inputCache[register[D]] = register[B].ToString();
                        if(D>=32)
                            outputCache[register[D]] = register[B].ToString();


                    }
                    else
                    {
                        Console.WriteLine("ST");
                    }
                    
                    break;
                case "000011": //LW
                    register[D] = int.Parse(ProgramCache[register[B]]) ;
                    break;

            }
        }

        void UnconditionalJump()
        {
            String opCode = currentProcess.Substring(2, 6);
            int address = Convert.ToInt32(currentProcess.Substring(16, 16), 2);
            switch (opCode)
            {
                case "010010":  //HLT
                    Console.WriteLine("process halted");
            //        Console.ReadLine();
                    currentPCB.state = PCB.Status.terminated;

                    break;
                case "010100": //JMP
                    
                    pc = (address / 4)-1;
                    
                    break;
                default:
                    break;
            }
        }

        void IO()
        {
           
            
            iocounter[currentPCB.id] = iocounter[currentPCB.id] +1;

            String opCode = currentProcess.Substring(2, 6);
            int tempRegister1 = Convert.ToInt32(currentProcess.Substring(8, 4),2);
            int tempRegister2 = Convert.ToInt32(currentProcess.Substring(12, 4),2);
            int address = Convert.ToInt32(currentProcess.Substring(16, 16),2);
            switch (opCode)
            {
                case "000000":
                    if (tempRegister1 != 0 && tempRegister2 != 0)
                    {
                        register[tempRegister1] = int.Parse(ProgramCache[register[tempRegister2]], System.Globalization.NumberStyles.HexNumber);
                    }
                    else
                    {
                        register[tempRegister1] = int.Parse(ProgramCache[address/4],System.Globalization.NumberStyles.HexNumber);
                    }
                    break;
                case "000001":
                    {
                        string opBuffer = register[0].ToString();
                        Console.WriteLine("opBuffer is equal to "+opBuffer);
           //             Console.ReadLine();
                        break;

                    }
        
             }
        }
    }
}



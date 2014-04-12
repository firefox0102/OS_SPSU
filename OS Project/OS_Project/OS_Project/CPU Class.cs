using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace OS_Project
{
     public class CPU
    {
        public int[] register;

        public String currentProcess;
        public PCB currentPCB;
        public int offset = 0;
        public int processPosition;
        public const int Accumulator = 0;
        public const int Zero = 1;
        public int pc = 0;
        public int instructionPageSet = 1;
        public int inputPageSet = 1;
        public int outputPageSet = 1;
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
            offset = currentPCB.instrLength;
            Decode();
            Execute();
            Console.WriteLine("this job is: "+this.currentPCB.id);

        }


        private void Fetch()
        {
             
             
             
             Dispatcher.Instance.sendProcess(this);
            //is passed pcb id then gets pcb info
            //FILLS instructionList
          
        }

        void writeThrough()
        {
            int count1 = 0;
            int count2 = 0;
            int count3 = 0;
        


            List<String> writethroughinput1 = new List<String>();
            List<String> writethroughoutput1 = new List<String>();

            List<int> location = currentPCB.getLocations(0 + ((inputPageSet - 1) * 16), 2);
            List<int> location2 = currentPCB.getLocations(19 + 0 + ((outputPageSet - 1) * 16), 2);


            foreach (String y in inputCache)
            {

                writethroughinput1[count2] = inputCache[count1];
                count2++;
                
                if (count1 % 4 == 0)
                {
                //    int i = count1 / 4;
                    Memory.Instance.memory[location[count3]].InsertRange(0, writethroughinput1);
                    writethroughinput1.Clear();
                    count2 = 0;
                    count3++;

                }


                count1++;
                

            }

            count1 = 0;
            count2 = 0;
            count3 = 0;


            foreach (String y in outputCache)
            {

                writethroughinput1[count2] = outputCache[count1];
                count2++;

                if (count1 % 4 == 0)
                {
                    //    int i = count1 / 4;
                    Memory.Instance.memory[location[count3]].InsertRange(0, writethroughinput1);
                    writethroughinput1.Clear();
                    count2 = 0;
                    count3++;

                }


                count1++;


            }

        }

        public void Execute()
        {
            

            for(pc = 0; pc<currentPCB.instrLength  ; pc++ )//get from pcb
            {




                //checks size of cache
                //page faults if not same size
                //compares pc to see what cache to use
                if (pc < offset)
                {
                    if (pc > instructionCache.Count)
                    {
                        instructionCache.addRange(Dispatcher.Instance.PageFault(currentPCB, instructionPageSet, "up", "instruction"));
                        instructionPageSet++;
                    }
                    currentProcess = Convert.ToString(Convert.ToInt32(instructionCache[pc], 16), 2);

                }
                if (pc > offset && pc < offset + 20)
                {
                    pc = pc - offset;
                    if (pc > inputCache.Count)
                    {
                        inputCache.addRange(Dispatcher.Instance.PageFault(currentPCB, inputPageSet, "up", "input"));
                        inputPageSet++;
                    }
                    currentProcess = Convert.ToString(Convert.ToInt32(inputCache[pc], 16), 2);
                }
                if (pc > offset + 20)
                {
                    pc = pc - (offset + 20);
                    if (pc > outputCache.Count)
                    {
                        outputCache.addRange(Dispatcher.Instance.PageFault(currentPCB, outputPageSet, "up", "output"));
                        outputPageSet++;
                    }
                    currentProcess = Convert.ToString(Convert.ToInt32(outputCache[pc], 16), 2);
                }




  //              Console.WriteLine(pc);
               
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








                writeThrough();
                
                
                
                
                
                
                
/*           for (int j = 0; j < 16; j++)
                {
                    Console.WriteLine(register[j]);
                    //    Console.WriteLine("j is equal to" + j);
                }
                Console.WriteLine("i is equal to::" + pc);
       */       
            }
            
            idle = true;
            currentPCB.state = PCB.Status.terminated;
            currentPCB.elapsedTime.Stop();            
        }

        void Arithmetic()
        {
            try
            {
                string opCode = currentProcess.Substring(2, 6);
                int s1 = Convert.ToInt32(currentProcess.Substring(8, 4), 2);
                int s2 = Convert.ToInt32(currentProcess.Substring(12, 4), 2);
                int D = Convert.ToInt32(currentProcess.Substring(16, 4), 2);

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
            catch (Exception e)
            {

                //increment all of the caches

            }
        }    
        
        String convertAddress(int address)
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
        //                ProgramCache[register[D]] = register[B].ToString(); 
                 
                        
                    //instructionCache
                    if(register[D] < offset)
                    {
                        while(register[D]>instructionCache.Count)
                        {
                            instructionCache.addRange(Dispatcher.Instance.PageFault(currentPCB, pageSet, "up", "instruction"));
                            instructionPageSet++;
                        }
                        
                        instructionCache[register[D]] =  register[B].ToString();
                    }

                    //inputCache
                    if(register[D] > offset && register[D] < offset +20)
                    {
                        int temp = register[D];
                        temp = temp - offset;

                        while(temp > inputCache.Count)
                        {
                            inputCache.addRange(Dispatcher.Instance.PageFault(currentPCB, pageSet, "up", "input"));
                            inputPageSet++;
                        }

                        inputCache[temp] = register[B].ToString();

                    }
                   
                    //outputCache
                    
                    
                    if(register[D] > offset +20)
                    {

                        int temp = register[D];
                        temp = temp - (offset+20);

                        while(temp > outputCache.Count)
                        {
                            outputCache.addRange(Dispatcher.Instance.PageFault(currentPCB, pageSet, "up", "output"));
                            outputPageSet++;
                        }

                         outputCache[temp] = register[B].ToString();

                    }
                    
                    }
                    else
                    {
                        Console.WriteLine("ST");
                    }
                    
                    break;
                case "000011": //LW
  //                  register[D] = int.Parse(ProgramCache[register[B]]) ;


                     //instructionCache
                    if(register[B] < offset)
                    {
                        while(register[B]>instructionCache.Count)
                        {
                            instructionCache.addRange(Dispatcher.Instance.PageFault(currentPCB, pageSet, "up", "instruction"));
                            instructionPageSet++;
                        }
                    register[D] = int.Parse(instructionCache[register[B]]) ;
                    }

                    //inputCache
                    if(register[B] > offset && register[B] < offset +20)
                    {
                        int temp = register[B];
                        temp = temp - offset;

                        while(temp > inputCache.Count)
                        {
                            inputCache.addRange(Dispatcher.Instance.PageFault(currentPCB, pageSet, "up", "input"));
                            inputPageSet++;
                        }

                        register[D] = int.Parse(inputCache[register[temp]]) ;

                    }
                   
                    //outputCache
                    
                    
                    if(register[B] > offset +20)
                    {

                        int temp = register[B];
                        temp = temp - (offset+20);

                        while(temp > outputCache.Count)
                        {
                            outputCache.addRange(Dispatcher.Instance.PageFault(currentPCB, pageSet, "up", "output"));
                            outputPageSet++;
                        }

                         register[D] = int.Parse(outputCache[register[temp]]) ;

                    }



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
            String opCode = currentProcess.Substring(2, 6);
            int tempRegister1 = Convert.ToInt32(currentProcess.Substring(8, 4),2);
            int tempRegister2 = Convert.ToInt32(currentProcess.Substring(12, 4),2);
            int address = Convert.ToInt32(currentProcess.Substring(16, 16),2);
            switch (opCode)
            {
                case "000000":
                    if (tempRegister1 != 0 && tempRegister2 != 0)
                    {
               //         register[tempRegister1] = int.Parse(ProgramCache[register[tempRegister2]], System.Globalization.NumberStyles.HexNumber);

                        int temp = tegister[tempRegister2];

                        //instructionCache
                        if (temp < offset)
                        {
                            while(temp > outputCache.Count)
                            {
                                  instructionCache.addRange(Dispatcher.Instance.PageFault(currentPCB, pageSet, "up", "instruction"));
                                instructionPageSet++;
                            }
                            
                            
                            register[tempRegister1] = int.Parse(instructionCache[temp], System.Globalization.NumberStyles.HexNumber);
                        }
                        //inputCache
                        if (temp > offset && temp < offset + 20)
                        {
                            temp = temp - offset;
                             while(temp > inputCache.Count)
                            {
                                 inputCache.addRange(Dispatcher.Instance.PageFault(currentPCB, pageSet, "up", "input"));
                                 inputPageSet++;
                            }
                            register[tempRegister1] = int.Parse(inputCache[temp], System.Globalization.NumberStyles.HexNumber);
                        }
                        //outputCache
                        if (temp > offset + 20)
                        {
                            temp = temp - (offset+20);
                            while(temp > outputCache.Count)
                            {
                                 outputCache.addRange(Dispatcher.Instance.PageFault(currentPCB, pageSet, "up", "output"));
                                outputPageSet++;
                            }

                            register[tempRegister1] = int.Parse(outputCache[temp], System.Globalization.NumberStyles.HexNumber);
                        }






                    }
                    else
                    {
       //                 register[tempRegister1] = int.Parse(ProgramCache[address/4],System.Globalization.NumberStyles.HexNumber);

                        int temp = address/4;

                        //instructionCache
                        if (temp < offset)
                        {
                            while(temp > outputCache.Count)
                            {
                                instructionCache.addRange(Dispatcher.Instance.PageFault(currentPCB, pageSet, "up", "instruction"));
                                instructionPageSet++;
                            }
                            
                            register[tempRegister1] = int.Parse(instructionCache[temp],System.Globalization.NumberStyles.HexNumber);
                            
                        }
                        //inputCache
                        if (temp > offset && temp < offset + 20)
                        {
                            temp = temp - offset;
                             while(temp > inputCache.Count)
                            {
                                 inputCache.addRange(Dispatcher.Instance.PageFault(currentPCB, pageSet, "up", "input"));
                                 inputPageSet++;
                            }
                            register[tempRegister1] = int.Parse(inputCache[temp], System.Globalization.NumberStyles.HexNumber);
                        }
                        //outputCache
                        if (temp > offset + 20)
                        {
                            temp = temp - (offset+20);
                            while(temp > outputCache.Count)
                            {
                                 outputCache.addRange(Dispatcher.Instance.PageFault(currentPCB, pageSet, "up", "output"));
                                outputPageSet++;
                            }

                            register[tempRegister1] = int.Parse(outputCache[temp], System.Globalization.NumberStyles.HexNumber);
                        }






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



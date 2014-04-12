﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using OS_Project;



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
            offset = currentPCB.instrLength;
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
                currentPCB.pc = pc;

                //converts the pc to operate inside of the instruction cache and not the full list of instructions
                if (pageSet > 1)
                {
                    pc = pc - ((pageSet - 1) * 16);
                }

                //pagefault
                if(pc<0)
                {
                    instructionCache = Dispatcher.Instance.PageFault(currentPCB, pageSet, "down","instructionCache");
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

                    instructionCache = Dispatcher.Instance.PageFault(currentPCB, pageSet,"up","instruction");
                    pageSet++;
                    
                }


                writeThrough();


            }
            
            idle = true;
            currentPCB.state = PCB.Status.terminated;
            currentPCB.elapsedTime.Stop();
            
        }

        void writeThrough()
        {
            int count1 = 0;
            int count2 = 0;

            List<String> writethroughinput1 = new List<String>();
            List<String> writethroughinput2 = new List<String>();
            List<String> writethroughinput3 = new List<String>();
            List<String> writethroughinput4 = new List<String>();

            List<String> writethroughoutput1 = new List<String>();
            List<String> writethroughoutput2 = new List<String>();
            List<String> writethroughoutput3 = new List<String>();
            List<String> writethroughoutput4 = new List<String>();

            foreach(String y in inputCache)
            {
                if (count1 < 4)
                    writethroughinput1[count1] = inputCache[count1];
                if (count1 >= 4 && count1 < 8)
                    writethroughinput2[count1] = inputCache[count1];
                if (count1 >= 8 && count1 < 12)
                    writethroughinput3[count1] = inputCache[count1];
                if (count1 >= 12 && count1 < 16)
                    writethroughinput4[count1] = inputCache[count1];
               
                count1++;
            }

            foreach(String z in outputCache)
            {
                if (count2 < 4)
                    writethroughoutput1[count2] = outputCache[count2];
                if (count2 >= 4 && count2 < 8)
                    writethroughoutput2[count2] = outputCache[count2];
                if (count2 >= 8 && count2 < 12)
                    writethroughoutput3[count2] = outputCache[count2];
                if (count2 >= 12 && count2 < 16)
                    writethroughoutput4[count2] = outputCache[count2];
              
                count2++;
            }

            List<int> location = currentPCB.getLocations(0 + ((pageSet - 1) * 16), 2);
            for (int i = 0; i<location.Count; i++)
            {
                if (i == 0)
                    Memory.Instance.memory[location[i]].InsertRange(0, writethroughinput1);
                if (i == 1)
                    Memory.Instance.memory[location[i]].InsertRange(0, writethroughinput2);
                if (i == 2)
                    Memory.Instance.memory[location[i]].InsertRange(0, writethroughinput3);
                if (i == 3)
                    Memory.Instance.memory[location[i]].InsertRange(0, writethroughinput4);
            }


            List<int> location2 = currentPCB.getLocations(19 + 0 + ((pageSet - 1) * 16), 2);
            for(int j = 0;j< location2.Count; j++)
            {
            Memory.Instance.memory[location2[j]].InsertRange(0, writethroughoutput1);
           
            Memory.Instance.memory[location2[j]].InsertRange(0, writethroughoutput2);
            
            Memory.Instance.memory[location2[j]].InsertRange(0, writethroughoutput3);
            
            Memory.Instance.memory[location2[j]].InsertRange(0, writethroughoutput4);
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
                       if(D>offset && D<offset+20)
                       {
                           if(D-offset>16)
                               Dispatcher.Instance.PageFault(currentPCB, pageSet, "up", "input");
                           if(D-offset<0)
                              Dispatcher.Instance.PageFault(currentPCB, pageSet,"down","input");
                           
                            inputCache[register[D-offset]] = register[B].ToString();
                       }
                       else if(D>=offset+20 )
                       {
                           if(D-(offset+20)<0)
                               Dispatcher.Instance.PageFault(currentPCB, pageSet,"down","output");
                           if (D-(offset+20)>16)
                               Dispatcher.Instance.PageFault(currentPCB, pageSet,"up","output");
                           
                            inputCache[register[D-(offset-20)]] = register[B].ToString();
                       }
                       else if(D<offset)
                           Console.WriteLine("ST instruction is out of bounds");


                    }
                    else
                    {
                        Console.WriteLine("ST instruction broke");
                    }
                    
                    break;
                case "000011": //LW

                    if (D < offset)
                    {
                        if (offset > 16)
                            Dispatcher.Instance.PageFault(currentPCB, pageSet, "up", "input");

                        register[D] = int.Parse(instructionCache[register[B]]);

                    }
                    if (D > offset && D<=offset +20)
                    {
                        if (D - offset > 16)
                            Dispatcher.Instance.PageFault(currentPCB, pageSet, "up", "input");
                        if (D - offset < 0)
                            Dispatcher.Instance.PageFault(currentPCB, pageSet, "down", "input");

                        register[D] = int.Parse(inputCache[register[B]]);
                    }
                    else if (D > offset + 20)
                    {
                        if (D - (offset + 20) < 0)
                            Dispatcher.Instance.PageFault(currentPCB, pageSet, "down", "output");
                        if (D - (offset + 20) > 16)
                            Dispatcher.Instance.PageFault(currentPCB, pageSet, "up", "output");

                        register[D] = int.Parse(outputCache[register[B]]);
                    }






                    /*
                    if(D<=offset)
                      register[D] = int.Parse(instructionCache[register[B]]) ;
                    if(D>offset && D<offset+20)
                    {
                        
                    }
                    */
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
//                    PageManager.Instance.Clean(currentPCB);

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

                        //instructioncache
                        if (tempRegister2 < offset)
                        {
                            if (tempRegister2 > 16)
                            {
                                Dispatcher.Instance.PageFault(currentPCB, pageSet, "up", "instruction");
                                tempRegister2 = tempRegister2 - 16;
                            }
                            register[tempRegister1] = int.Parse(instructionCache[register[tempRegister2]], System.Globalization.NumberStyles.HexNumber);
                        }
                        
                        //inputcache

                        if (tempRegister2 > offset && tempRegister2<offset+20)
                        {
                            if (tempRegister2 > offset + 16)
                            {
                                Dispatcher.Instance.PageFault(currentPCB, pageSet, "up", "input");
                                tempRegister2 = tempRegister2 - (offset + 16);
                            }
                            else
                                tempRegister2 = tempRegister2 - offset;
                           
                            register[tempRegister1] = int.Parse(inputCache[register[tempRegister2]], System.Globalization.NumberStyles.HexNumber);
                        }



                        //outputcache

                         if (tempRegister2 > offset && tempRegister2<offset+20)
                        {
                            if (tempRegister2 > offset + 16)
                            {
                                Dispatcher.Instance.PageFault(currentPCB, pageSet, "up", "output");
                                tempRegister2 = tempRegister2 - (offset + 16);
                            }
                            else
                                tempRegister2 = tempRegister2 - offset;
                           
                            register[tempRegister1] = int.Parse(outputCache[register[tempRegister2]], System.Globalization.NumberStyles.HexNumber);
                        }
                    
                    }


                       // Dispatcher.Instance.PageFault(currentPCB, pageSet, "up", "input");


                    else
                    {

                        
                        //instructioncache
                        if (tempRegister2 < offset)
                        {
                            if (tempRegister2 > 16)
                            {
                                Dispatcher.Instance.PageFault(currentPCB, pageSet, "up", "instruction");
                                tempRegister2 = tempRegister2 - 16;
                            }

                            
                            register[tempRegister1] = int.Parse(instructionCache[address/4], System.Globalization.NumberStyles.HexNumber);
                        }

                        //inputcache

                        if (tempRegister2 > offset && tempRegister2 < offset + 20)
                        {
                            if (tempRegister2 > offset + 16)
                            {
                                Dispatcher.Instance.PageFault(currentPCB, pageSet, "up", "input");
                                tempRegister2 = tempRegister2 - (offset + 16);
                            }
                            else
                                tempRegister2 = tempRegister2 - offset;

                            register[tempRegister1] = int.Parse(inputCache[address/4], System.Globalization.NumberStyles.HexNumber);
                        }



                        //outputcache

                        if (tempRegister2 > offset && tempRegister2 < offset + 20)
                        {
                            if (tempRegister2 > offset + 16)
                            {
                                Dispatcher.Instance.PageFault(currentPCB, pageSet, "up", "output");
                                tempRegister2 = tempRegister2 - (offset + 16);
                            }
                            else
                                tempRegister2 = tempRegister2 - offset;

                            register[tempRegister1] = int.Parse(outputCache[address/4], System.Globalization.NumberStyles.HexNumber);
                        }

            //            register[tempRegister1] = int.Parse(ProgramCache[address / 4], System.Globalization.NumberStyles.HexNumber);
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


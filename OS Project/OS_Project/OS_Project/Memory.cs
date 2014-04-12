using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OS_Project;



namespace OS_Project{
    public class Memory
    {
        public static Memory Ram;
        public List<List<string>> memory;
        public List<int> pageManager;
        
        public Memory(){
            memory = new List<List<string>>(256);
             pageManager= new List<int>(256);
            for (int i = 0; i < 256; i++){
                List<string> temp = new List<string>(4);
                memory.Add(temp);
                pageManager.Add(i);
            }
        }

        public static Memory Instance
        {
            get
            {
                if (Ram == null)
                {
                    Ram = new Memory();
                }
                return Ram;
            }
        }

        public void printMemDump()
        {
            for (int i = 0; i < Memory.Instance.memory.Count; i++){
                int temp = Memory.Instance.memory[i].Count;
                Console.Write("Frame " + i + ": ");
                for (int j = 0; j < temp; j++){
                    Console.Write(j+": "+Memory.Instance.memory[i][j]+" ");
                }
                Console.WriteLine();
            }
        }

    }// end class
}// end namespace

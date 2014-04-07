using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace OS_Project{
    public class Memory
    {
        public static Memory Ram;
        public List<List<string>> memory;
        public int pagesEmpty;
        public List<int> emptyFrameIndexes;
        
        public Memory(){
            memory = new List<List<string>>(256);
            emptyFrameIndexes = new List<int>(256);
            for (int i = 0; i < 256; i++){
                List<string> temp = new List<string>(4);
                memory.Add(temp);
                emptyFrameIndexes.Add(i);
            }
            pagesEmpty = 256;
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

        public void memDump()
        {
            int k = 0;
            for (int i = 0; i < Memory.Instance.memory.Count; i++){
                int temp = Memory.Instance.memory[i].Count;
                for (int j = 0; j < temp; j++){
                    Console.WriteLine(k+": "+Memory.Instance.memory[i][j]);
                    k++;
                }
                
            }
        }

    }// end class
}// end namespace

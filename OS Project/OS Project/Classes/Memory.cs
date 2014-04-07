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

        public List<int> getEmptyPages(){
            List<int> emptyPages = new List<int>(0);
            return null;
        }



    }// end class
}// end namespace

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DESS
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryHelper read = new BinaryHelper("test.bin");
           
            for (int i = 0; i < read.FullBlockSize; i++)
            {
                List<bool> tmp = read.Read64BitsBlock();
                if (tmp==null)
                {
                    Console.WriteLine(i);
                }
                read.Write64BitsBlock(tmp);
            }

            read.Save("maly2.bin");


            Console.WriteLine("elo");

            Console.Read();
        }
    }
}

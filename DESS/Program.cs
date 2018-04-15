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
            string string64bit = "0001001100110100010101110111100110011011101111001101111111110001";
        


            Console.WriteLine();
            Dess dess = new Dess(string64bit, "output.bin", "output.bin");

           

            dess.Encrypt();





            Console.WriteLine();
            Console.Read();
        }
    }
}

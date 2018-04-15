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

            Dess dess = new Dess(string64bit);
            dess.Encrypt("test.bin","output.bin");

            dess.Decrypt("output.bin", "output2.bin");


        }
    }
}

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

            

            for (int i = 0; i < 32; i++)
            {
             

                
                Console.Write((StaticArrays.P[i]-1) +", ");
            
            }

            Console.WriteLine();
            Console.Read();
        }
    }
}

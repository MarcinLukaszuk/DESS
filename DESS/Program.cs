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
            string string64bit = "1001001100110100010101110101100110011011101111001101111111110001";

            Console.WriteLine("MENU");
            Console.WriteLine("1 - zaszyfruj plik");
            Console.WriteLine("2 - odszyfruj plik");
            Console.WriteLine("3 - wyjdź\n\r");

            Dess dess = new Dess(string64bit);
            string input, output = "";

            while (1==1)
            {
                switch (int.Parse(Console.ReadLine()))
                {
                    case 1:
                        Console.WriteLine("Podaj nazwę pliku wejściowego: ");
                        input = Console.ReadLine();
                        Console.WriteLine("Podaj nazwę pliku wyjściowego: ");
                        output = Console.ReadLine();
                        dess.Encrypt(input, output);
                        Console.WriteLine("Plik zaszyfrowano\n\r");
                        break;
                    case 2:
                        Console.WriteLine("Podaj nazwę pliku wejściowego: ");
                        input = Console.ReadLine();
                        Console.WriteLine("Podaj nazwę pliku wyjściowego: ");
                        output = Console.ReadLine();
                        dess.Decrypt(input, output);
                        Console.WriteLine("Plik odszyfrowano\n\r");
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            }

        }
    }
}

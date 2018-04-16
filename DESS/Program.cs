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
            Dess dess;
            KeyGenerator keygen;
            string string64bit = "";

            Console.WriteLine("MENU");
            Console.WriteLine("1 - generuj klucz");
            Console.WriteLine("2 - zaszyfruj plik");
            Console.WriteLine("3 - odszyfruj plik");
            Console.WriteLine("4 - wyjdź\n\r");

            string input, output = "";

            while (1==1)
            {
                switch (int.Parse(Console.ReadLine()))
                {
                    case 1:
                        keygen = new KeyGenerator();
                        keygen.get64bitsArray();
                        string64bit = keygen.Key;
                        Console.WriteLine("Klucz wygenerowano\n\r");
                        break;
                    case 2:
                        dess = new Dess(string64bit);
                        Console.WriteLine("Podaj nazwę pliku wejściowego: ");
                        input = Console.ReadLine();
                        Console.WriteLine("Podaj nazwę pliku wyjściowego: ");
                        output = Console.ReadLine();
                        dess.Encrypt(input, output);
                        Console.WriteLine("Plik zaszyfrowano\n\r");
                        break;
                    case 3:
                        dess = new Dess(string64bit);
                        Console.WriteLine("Podaj nazwę pliku wejściowego: ");
                        input = Console.ReadLine();
                        Console.WriteLine("Podaj nazwę pliku wyjściowego: ");
                        output = Console.ReadLine();
                        dess.Decrypt(input, output);
                        Console.WriteLine("Plik odszyfrowano\n\r");
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESS
{
    public class KeyGenerator
    {
        private string _key;

        public string Key
        {
            get
            {
                return _key;
            }
        }

      
        public KeyGenerator()
        {
            StringBuilder builder = new StringBuilder();
            Random rnd = new Random();
            for (int i = 0; i < 16; i++)
                builder.Append(toChar(rnd.Next(0, 15)));

            _key = builder.ToString();
        }

        public void get64bitsArray()
        {
            StringBuilder builder = new StringBuilder();
            Random rnd = new Random();
            for (int i = 0; i < 64; i++)
                builder.Append(rnd.Next(0, 2));

            _key = builder.ToString();

        }



        private static char toChar(int p)
        {
            if (p < 10)
                return (char)(p + 48);
            else
                return (char)(p + 55);
        }

    }
}

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

        public KeyGenerator() : this(16)
        {

        }

        public KeyGenerator(int length)
        {
            StringBuilder b = new StringBuilder();
            Random rand = new Random();
            for (int i = 0; i < 16; i++)
            {
                b.Append(toChar(rand.Next(0, 15)));
            }
            _key = b.ToString();
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

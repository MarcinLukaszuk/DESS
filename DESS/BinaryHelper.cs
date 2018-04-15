using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESS
{
    public class BinaryHelper
    {
        List<byte> _inputBytesArray;


        LinkedList<byte> _outputBytesArray;
        int _inputByteCounter;

        public int FullBlockCount { get { return _inputBytesArray.Count / 8; } }


        public BinaryHelper(string inputPath)
        {
            _inputBytesArray = File.ReadAllBytes(inputPath).ToList(); 
            _outputBytesArray = new LinkedList<byte>();
            _inputByteCounter = 0;
        }

        /// <summary>
        /// zapis do pliku tablicy _outputBytesArray
        /// </summary>
        /// <param name="outputPath"></param>
        public void Save(string outputPath)
        {
            File.WriteAllBytes(outputPath, _outputBytesArray.ToArray());
        }

        #region Odczyt Zapis bloku
        public List<bool> Read64BitsBlock()
        {
            if (_inputByteCounter + 8 > FullBlockCount * 8)
            {
                Console.WriteLine("niepelny wiersz");
                return null;
            }
            StringBuilder sb = new StringBuilder();
            ///   wyciaga 8 bitow
            var list = _inputBytesArray.GetRange(_inputByteCounter, 8);
            _inputByteCounter += 8;

            ///  tworzy stringa 64bit
            foreach (var item in list)
                sb.Append(ConverByteToBitString(item));

            ///tworzy tablice boolowska
            var boolArray = sb.ToString().Select(x => x == '0' ? false : true).Reverse().ToList();

            return boolArray;
        }
        public void Write64BitsBlock(List<bool> writeArray)
        {
            for (int i = 7; i >= 0; i--)
                _outputBytesArray.AddLast(ConvertBitsArrayToByte(writeArray.GetRange(i * 8, 8).ToArray()));
        }
        #endregion





        #region Konwertery
        private string ConverByteToBitString(byte _byte)
        {
            return Convert.ToString(_byte, 2).PadLeft(8, '0');
        }
        private byte ConvertBitsArrayToByte(bool[] _bitsArray)
        {
            byte val = 0;
            foreach (bool b in _bitsArray.Reverse())
            {
                val <<= 1;
                if (b) val |= 1;
            }
            return val;
        }
        #endregion






    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESS
{
    public class Dess
    {
        private BinaryHelper binaryHelper;
        private string _key;



        #region StatyczneTbalice
        private readonly int[] IP = {57, 49, 41, 33, 25, 17, 9, 1,
                                     59, 51, 43, 35, 27, 19, 11, 3,
                                     61, 53, 45, 37, 29, 21, 13, 5,
                                     63, 55, 47, 39, 31, 23, 15, 7,
                                     56, 48, 40, 32, 24, 16, 8, 0,
                                     58, 50, 42, 34, 26, 18, 10, 2,
                                     60, 52, 44, 36, 28, 20, 12, 4,
                                     62, 54, 46, 38, 30, 22, 14, 6 };

        #endregion


        public Dess(string key, string inputFilePath, string outputFilePath)
        {
            _key = key;
            binaryHelper = new BinaryHelper(inputFilePath);
        }

        public void ExecuteDesEncrypt(List<bool> boolList)
        {



        }







        public bool[] InitialPermutation(bool[] intArray)
        {
            bool[] ip = new bool[64];
            for (int i = 0; i < 64; i++)
            {
                ip[i] = intArray[IP[i]];
            }
            return ip;
        }



    }
}

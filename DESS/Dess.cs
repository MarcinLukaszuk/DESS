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
                ip[i] = intArray[StaticArrays.IP[i]];
            }
            return ip;
        }



    }
}

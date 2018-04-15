using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESS
{
    public class Dess
    {
        private BinaryHelper _binaryHelper;
        private bool[] _key;

        public Dess(string key)
        {
            _key = key.Select(x => x == '1' ? true : false).Reverse().ToArray();
        }

        public void Encrypt(string inputFilePath, string outputFilePath)
        {
            _binaryHelper = new BinaryHelper(inputFilePath);
            var keys = generateKeys(permuteKeyPC1(_key));
            List<bool> tmp = new List<bool>();
            for (int i = 0; i < _binaryHelper.FullBlockCount; i++)
            {
                tmp = _binaryHelper.Read64BitsBlock();

                _binaryHelper.Write64BitsBlock(Encrypt64BitBlock(tmp, keys));
            }

            //kodowanie ostatniego bloku
            tmp = _binaryHelper.ReadLastBlockAndOffset();
            _binaryHelper.Write64BitsBlock(Encrypt64BitBlock(tmp, keys));

            _binaryHelper.Save(outputFilePath);
        }
        public void Decrypt(string inputFilePath, string outputFilePath)
        {
            var keys = generateKeys(permuteKeyPC1(_key));
            _binaryHelper = new BinaryHelper(inputFilePath);
            List<bool> tmp = new List<bool>();

            int fullBlocks = _binaryHelper.FullBlockCount;
            if (_binaryHelper.LastBlockBytes == 0) fullBlocks--;

            for (int i = 0; i < fullBlocks; i++)
            {
                tmp = _binaryHelper.Read64BitsBlock();
                _binaryHelper.Write64BitsBlock(Decrypt64BitBlock(tmp, keys));
            }
            tmp = _binaryHelper.Read64BitsBlock();
            _binaryHelper.Write64BitsBlockAndOffset(Decrypt64BitBlock(tmp, keys));



            _binaryHelper.Save(outputFilePath);
        }

        private List<bool> Decrypt64BitBlock(List<bool> block64Bit, Dictionary<int, bool[]> keys)
        {
            var tmp = InitialPermutation(block64Bit.ToArray());
            bool[] leftBlock = tmp.ToList().GetRange(32, 32).ToArray();
            bool[] rightBlock = tmp.ToList().GetRange(0, 32).ToArray();
            bool[] tmpBlock;

            for (int i = 16; i >= 2; i--)
            {
                tmpBlock = XOR(leftBlock, functionRF(rightBlock, keys[i]));
                leftBlock = rightBlock.ToArray();
                rightBlock = tmpBlock.ToArray();
            }

            tmpBlock = XOR(leftBlock, functionRF(rightBlock, keys[1]));
            leftBlock = tmpBlock.ToArray();

            var tmpConcat = rightBlock.Concat(leftBlock).ToArray();


            return InvertInitialPermutation(tmpConcat).ToList();
        }
        private List<bool> Encrypt64BitBlock(List<bool> block64Bit, Dictionary<int, bool[]> keys)
        {
            var tmp = InitialPermutation(block64Bit.ToArray());
            bool[] leftBlock = tmp.ToList().GetRange(32, 32).ToArray();
            bool[] rightBlock = tmp.ToList().GetRange(0, 32).ToArray();
            bool[] tmpBlock;
            for (int i = 1; i <= 15; i++)
            {
                tmpBlock = XOR(leftBlock, functionRF(rightBlock, keys[i]));
                leftBlock = rightBlock.ToArray();
                rightBlock = tmpBlock.ToArray();
            }

            tmpBlock = XOR(leftBlock, functionRF(rightBlock, keys[16]));
            leftBlock = tmpBlock.ToArray();

            var tmpConcat = rightBlock.Concat(leftBlock).ToArray();


            return InvertInitialPermutation(tmpConcat).ToList();
        }

        private bool[] permuteKeyPC1(bool[] _key)
        {
            bool[] permuted = new bool[56];
            for (int i = 0; i < 56; i++)
            {
                permuted[55 - i] = _key[63 - StaticArrays.PC1[i]];
            }
            return permuted;
        }
        private bool[] permuteKeyPC2(bool[] _key)
        {
            bool[] permuted = new bool[48];
            for (int i = 0; i < 48; i++)
            {
                permuted[47 - i] = _key[55 - StaticArrays.PC2[i]];
            }
            return permuted;
        }


        private Dictionary<int, bool[]> generateKeys(bool[] key)
        {
            Dictionary<int, bool[]> keys = new Dictionary<int, bool[]>();
            Dictionary<int, bool[]> keysC = new Dictionary<int, bool[]>();
            Dictionary<int, bool[]> keysD = new Dictionary<int, bool[]>();

            LinkedList<bool> tmpC = new LinkedList<bool>(key.ToList().GetRange(28, 28));
            LinkedList<bool> tmpD = new LinkedList<bool>(key.ToList().GetRange(0, 28));

            keysC.Add(0, tmpC.ToArray());
            keysD.Add(0, tmpD.ToArray());

            for (int i = 0; i < StaticArrays.shiftsNumber.Length; i++)
            {
                tmpC = shiftLeft(tmpC, StaticArrays.shiftsNumber[i]);
                tmpD = shiftLeft(tmpD, StaticArrays.shiftsNumber[i]);

                keysC.Add(i + 1, tmpC.ToArray());
                keysD.Add(i + 1, tmpD.ToArray());
            }

            bool[] key56Bit;
            bool[] permutedKey56Bit;
            for (int i = 1; i <= 16; i++)
            {
                key56Bit = keysD[i].Concat(keysC[i]).ToArray();
                permutedKey56Bit = permuteKeyPC2(key56Bit);
                keys.Add(i, permutedKey56Bit);
            }
            return keys;
        }
        private LinkedList<bool> shiftLeft(LinkedList<bool> list, int numbers)
        {
            bool tmp;
            for (int i = 0; i < numbers; i++)
            {
                tmp = list.Last();
                list.RemoveLast();
                list.AddFirst(tmp);
            }
            return list;
        }
        private bool[] InitialPermutation(bool[] intArray)
        {
            bool[] ip = new bool[64];
            for (int i = 0; i < 64; i++)
            {
                ip[i] = intArray[StaticArrays.IP[i]];
            }
            return ip;
        }

        private bool[] InvertInitialPermutation(bool[] intArray)
        {
            bool[] ip = new bool[64];
            for (int i = 0; i < 64; i++)
            {
                ip[i] = intArray[StaticArrays.IP1[i]];
            }
            return ip;
        }


        #region Ta funkcja
        private bool[] functionRF(bool[] rightBlock, bool[] _key)
        {
            var afterXor = XOR(permuteE(rightBlock), _key);

            List<bool[]> listBlocks = new List<bool[]>();

            for (int i = 7; i >= 0; i--)
            {
                var lol = afterXor.ToList().GetRange(i * 6, 6).ToArray();
                listBlocks.Add(CalculateNumber(lol, 8 - i));
            }

            var new32bitarray = new bool[0];
            foreach (var item in listBlocks)
                new32bitarray = item.Concat(new32bitarray).ToArray();

            return permuteP(new32bitarray);
        }
        private bool[] permuteE(bool[] _rightBlock)
        {
            bool[] permuted = new bool[48];
            for (int i = 0; i < 48; i++)
            {
                permuted[47 - i] = _rightBlock[31 - StaticArrays.E[i]];
            }
            return permuted;
        }

        private bool[] permuteP(bool[] _rightBlock)
        {
            bool[] permuted = new bool[32];
            for (int i = 0; i < 32; i++)
            {
                permuted[31 - i] = _rightBlock[31 - StaticArrays.P[i]];
            }
            return permuted;
        }

        private bool[] XOR(bool[] tab1, bool[] tab2)
        {
            var tmpTab = new bool[tab1.Length];

            for (int i = 0; i < tab1.Length; i++) tmpTab[i] = tab1[i] == tab2[i] ? false : true;

            return tmpTab;
        }
        private bool[] CalculateNumber(bool[] array, int numberSBlock)
        {
            int row = (array[0] == true ? 1 : 0) +
                      (array[5] == true ? 2 : 0);
            int collumn = (array[1] == true ? 1 : 0) +
                          (array[2] == true ? 2 : 0) +
                          (array[3] == true ? 4 : 0) +
                          (array[4] == true ? 8 : 0);

            int number;
            switch (numberSBlock)
            {
                case 1: number = StaticArrays.S1[row * 16 + collumn]; break;
                case 2: number = StaticArrays.S2[row * 16 + collumn]; break;
                case 3: number = StaticArrays.S3[row * 16 + collumn]; break;
                case 4: number = StaticArrays.S4[row * 16 + collumn]; break;
                case 5: number = StaticArrays.S5[row * 16 + collumn]; break;
                case 6: number = StaticArrays.S6[row * 16 + collumn]; break;
                case 7: number = StaticArrays.S7[row * 16 + collumn]; break;
                case 8: number = StaticArrays.S8[row * 16 + collumn]; break;

                default: number = 0; break;
            }

            return Convert.ToString(number, 2).PadLeft(4, '0').Select(x => x == '1' ? true : false).Reverse().ToArray();
        }

        #endregion






        public void wyswietlTablice(bool[] tablica, int odstep)
        {
            int x = 0;
            foreach (var item in tablica.Reverse())
            {
                if (x++ % odstep == 0)
                {
                    Console.Write(" ");
                }
                Console.Write(item == true ? 1 : 0);
            }
        }


    }


}

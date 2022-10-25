using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaszynaPi.CommonOperations {
    public static class Arithmetics {
        public static bool IsPowerOfTwo(int number) { return true; }
        public static int PowersDifference(uint exp1, uint exp2, uint power = 2) {
            return (int)(Math.Pow(power, exp1) - Math.Pow(power, exp2));
        }

        public static bool IsBitSet(int b, int bitNumber) {
            return (((b >> bitNumber) & 1) != 0);
        }

        public static uint negateBits(int value) {
            return ~(uint)value;
        }

        public static int SetBit(int value, int bit) {
            return (value |= bit);
        }
        public static int ResetBit(int value, int bit) {
            return value &= ~(bit);
        }

        public static uint CreateBitMask(uint noOfZeroes, uint noOfOnes, bool zeroesFirst = true) {
            uint mask = (uint)(((int)(Math.Pow(2, noOfOnes) - 1) << (int)noOfZeroes));
            if (zeroesFirst) return ~mask;
            return mask;
        }

        public static uint ShrinkToWordLength(uint value) {
            uint wordBitsSize = MachineLogic.ArchitectureSettings.GetAddressSpace() + MachineLogic.ArchitectureSettings.GetCodeBits();
            return value % (uint)Math.Pow(2, wordBitsSize);
        }
    }
}

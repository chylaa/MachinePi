﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaszynaPi.CommonOperations {
    public static class Arythmetics {
        public static bool IsPowerOfTwo(int number) { return true;  }
        public static int PowersDifference(uint exp1, uint exp2, uint power = 2) {
            return (int)(Math.Pow(power, exp1) - Math.Pow(power, exp2));
        }

        public static uint negateBits(int value) {
            return ~(uint)value;
        }

        public static uint CreateBitMask(uint noOfZeroes, uint noOfOnes, bool zeroesFirst = true) {
            uint mask = (uint)(((int)(Math.Pow(2, noOfOnes) - 1) << (int)noOfZeroes));
            if(zeroesFirst) return ~mask;
            return mask;
        }
    }
}

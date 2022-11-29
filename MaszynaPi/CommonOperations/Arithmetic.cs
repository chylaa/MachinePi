﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaszynaPi.CommonOperations {
    static class Arithmetic {
        public static bool IsPowerOfTwo(int number) { return true; }
        public static int PowersDifference(uint exp1, uint exp2, uint power = 2) {
            return (int)Math.Abs((Math.Pow(power, exp1) - Math.Pow(power, exp2)));
        }

    }
}
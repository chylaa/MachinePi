using System;

namespace MaszynaPi.CommonOperations {
    /// <summary> Static class providing methods for base arithmetic calculations. </summary>
    static class Arithmetic {

        /// <summary>
        /// Calculates difference between two numbers represented as of <paramref name="base"/> (default 2) to given exponent.
        /// </summary>
        /// <param name="exp1">Exponent of minuend.</param>
        /// <param name="exp2">Exponent of subtrahend.</param>
        /// <param name="base">Power base for both numbers (default 2).</param>
        /// <returns>Integer-casted result of (<paramref name="base"/>^<paramref name="exp1"/> - <paramref name="base"/>^<paramref name="exp2"/>) subtraction.</returns>
        public static int PowersDifference(uint exp1, uint exp2, uint @base = 2) {
            return (int)Math.Abs((Math.Pow(@base, exp1) - Math.Pow(@base, exp2)));
        }

    }
}

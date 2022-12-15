using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaszynaPi.MachineLogic;

namespace MaszynaPi.CommonOperations {
    public static class Bitwise {

        public static bool IsBitSet(int b, int bitNumber) {
            return (((b >> bitNumber) & 1) != 0);
        }

        public static uint NegateBits(int value) {
            return ~(uint)value;
        }

        public static int SetBit(int value, int bit) {
            return (value |= bit);
        }
        public static int ResetBit(int value, int bit) {
            return value &= ~(bit);
        }

        // Returns min amout of bits that are required to represent number as binary
        public static int GetBitsAmount(int number) {
            return (int)Math.Log(number, 2) + 1;
        }

        public static bool IsSignBitSet(uint value, uint numberBitsize) {
            if ((int)value >> (int)(numberBitsize - 1) == 1) return true;
            return false;
        }

        public static uint CreateBitMask(uint noOfZeroes, uint noOfOnes, bool zeroesFirst = true) {
            if (!zeroesFirst)
                return (uint)(((int)(Math.Pow(2, noOfOnes) - 1) << (int)noOfZeroes));
            return (uint)(Math.Pow(2, noOfOnes) - 1);
        }


        // Defines how value should behave on overflow
        // (based on word size if second parameter == 0) 
        public static uint HandleOverflow(uint value, uint bitsize=0) {
            if(bitsize>0)
                return (value & (uint)(Math.Pow(2,bitsize)-1));
            return value & ArchitectureSettings.GetMaxWord();
        }


        public static uint DecodeIntructionArgument(uint Value) {
            return (Value & CreateBitMask(noOfZeroes: ArchitectureSettings.GetCodeBits(), noOfOnes: ArchitectureSettings.GetAddressSpace(), zeroesFirst: true));
        }
        public static uint DecodeInstructionOpcode(uint Value) {
            uint opcode =  (Value & CreateBitMask(noOfZeroes: ArchitectureSettings.GetAddressSpace(), noOfOnes: ArchitectureSettings.GetCodeBits(), zeroesFirst: false));
            return (uint)((int)opcode >> (int)ArchitectureSettings.GetAddressSpace());
        }

        public static uint EncodeInstruction(uint opcode, uint argument) {
            return argument + (uint)((int)opcode << (int)ArchitectureSettings.GetAddressSpace());
        }
    }
}

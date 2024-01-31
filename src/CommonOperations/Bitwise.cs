using System;
using MaszynaPi.MachineLogic;

namespace MaszynaPi.CommonOperations {

    /// <summary> Static class providing methods for base bitwise calculations. </summary>
    public static class Bitwise 
    {

        /// <summary> Returns min amout of bits that are required to represent number as binary.</summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int GetBitsAmount(int number) {
            return (int)Math.Log(number, 2) + 1;
        }

        /// <summary>Checks if most significant bit of <paramref name="value"/> (pointed by <paramref name="numberBitsize"/>) is set.</summary>
        /// <param name="value">InstructionValue to check.</param>
        /// <param name="numberBitsize">Requested <paramref name="value"/> size in bits.</param>
        /// <returns>True if MSB of <paramref name="value"/> as <paramref name="numberBitsize"/>-bit number is set, false otherwise.</returns>
        public static bool IsSignBitSet(uint value, uint numberBitsize) {
            if ((int)value >> (int)(numberBitsize - 1) == 1) return true;
            return false;
        }

        /// <summary>
        /// Converts passed <paramref name="value"/> to signed <see cref="Int32"/> base on most significant bit 
        /// pointed by <paramref name="numberBitsize"/>.
        /// </summary>
        /// <param name="value">InstructionValue to convert</param>
        /// <param name="numberBitsize">Requested <paramref name="value"/> size in bits.</param>
        /// <returns>Sign-extended, max <see cref="Int32"/> value</returns>
        public static int ConvertToSigned(uint value, uint numberBitsize) {
            if(IsSignBitSet(value, numberBitsize)) return (int)(Math.Pow(2,numberBitsize)-value);
            return (int)value;
        }

        /// <summary>Creates unsigned number to act like bitmask with requested bits set. </summary>
        /// <param name="noOfZeroes">Number of consecutive '0' bits.</param>
        /// <param name="noOfOnes">Number of consecutive '1' bits.</param>
        /// <param name="zeroesFirst">If set to true (default), indicates that zero-bits should begin from MSB position.</param>
        /// <returns>Unsigned value which bits are set as requested.</returns>
        public static uint CreateBitMask(uint noOfZeroes, uint noOfOnes, bool zeroesFirst = true) {
            if (!zeroesFirst)
                return (uint)(((int)(Math.Pow(2, noOfOnes) - 1) << (int)noOfZeroes));
            return (uint)(Math.Pow(2, noOfOnes) - 1);
        }

        /// <summary>
        /// Simulates <paramref name="value"/> overflow based on provided 'virtual' <paramref name="bitsize"/>.
        /// If <paramref name="bitsize"/> == 0, overflow is based on <see cref="ArchitectureSettings.GetMaxWord"/>.
        /// </summary>
        /// <param name="value">InstructionValue that can potentially overflow if it would habe <paramref name="bitsize"/> bits.</param>
        /// <param name="bitsize">Virtual <paramref name="value"/> size in bit.</param>
        /// <returns>Original <paramref name="value"/> if it would not overflow when having <paramref name="bitsize"/>, overflown value otherwise.</returns>
        public static uint HandleOverflow(uint value, uint bitsize=0) {
            if(bitsize>0)
                return (value & (uint)(Math.Pow(2,bitsize)-1));
            return value & ArchitectureSettings.GetMaxWord();
        }

        /// <summary>Extracts argumet (address) component of passed <paramref name="InstructionValue"/> base on <see cref="ArchitectureSettings"/>.</summary>
        /// <param name="InstructionValue">InstructionValue representing CPU instruction.</param>
        /// <returns>Argument (address) component of passed <paramref name="InstructionValue"/>.</returns>
        public static uint DecodeIntructionArgument(uint InstructionValue) {
            return (InstructionValue & CreateBitMask(noOfZeroes: ArchitectureSettings.GetCodeBits(), noOfOnes: ArchitectureSettings.GetAddressSpace(), zeroesFirst: true));
        }
        /// <summary>Extracts opcode component of passed <paramref name="InstructionValue"/> base on <see cref="ArchitectureSettings"/>.</summary>
        /// <param name="InstructionValue">InstructionValue representing CPU instruction.</param>
        /// <returns>Opcode of passed <paramref name="InstructionValue"/>.</returns>
        public static uint DecodeInstructionOpcode(uint InstructionValue) {
            uint opcode =  (InstructionValue & CreateBitMask(noOfZeroes: ArchitectureSettings.GetAddressSpace(), noOfOnes: ArchitectureSettings.GetCodeBits(), zeroesFirst: false));
            return (uint)((int)opcode >> (int)ArchitectureSettings.GetAddressSpace());
        }

        /// <summary>
        /// Creates valid CPU instruction from <paramref name="opcode"/> and <paramref name="argument"/> components 
        /// base on <see cref="ArchitectureSettings"/>.
        /// </summary>
        /// <param name="opcode">Opcode component of instruction value.</param>
        /// <param name="argument">Argument (address) component of instruction value.</param>
        /// <returns>New CPU instruction value containing <paramref name="opcode"/> and address <paramref name="argument"/>.</returns>
        public static uint EncodeInstruction(uint opcode, uint argument) {
            return argument + (uint)((int)opcode << (int)ArchitectureSettings.GetAddressSpace());
        }
    }
}

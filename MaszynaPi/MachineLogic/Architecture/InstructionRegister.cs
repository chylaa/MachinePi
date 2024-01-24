using MaszynaPi.CommonOperations;

namespace MaszynaPi.MachineLogic.Architecture 
{
    /// <summary> 
    /// Special <see cref="Register"/> class representing CIR - Current instruction register.
    /// Responsible for storing and separating opcode and address/argument components of processed instruction.
    /// </summary>
    public class InstructionRegister : Register 
    {
        /// <summary>Internal Instruction Argument register.</summary>
        readonly Register AD;  
        /// <summary>Internal Instruction Opcode register.</summary>
        readonly Register KOD;

        /// <summary>Creates new instance of Instruction Register, basing on parent <see cref="Register"/> class.</summary>
        /// <param name="addrBitsize">Currently set size of address/argument component of instruction in bits</param>
        /// <param name="opcodeBitsize">Currently set size of opcode component of instruction in bits</param>
        /// <param name="value">Initialization value of internal <see cref="Value"/></param>
        public InstructionRegister(uint addrBitsize, uint opcodeBitsize,  uint value = Defines.DEFAULT_REG_VAL) : base(addrBitsize + opcodeBitsize, value) {
            AD = new Register(addrBitsize);
            KOD = new Register(opcodeBitsize);
            SetValue(value); // not neccessary?
        }

        /// <summary>Allows to retreive argument component of stored instruction from internal <see cref="AD"/> value.</summary>
        /// <returns>Value of address/argument component of processed instruction.</returns>
        public uint GetArgument() { return AD.GetValue(); }
        /// <summary>Allows to retreive opcode component of stored instruction from internal <see cref="KOD"/> value.</summary>
        /// <returns>Value of opcode component of processed instruction.</returns>
        public uint GetOpcode() { return KOD.GetValue(); }

        /// <summary>
        /// <inheritdoc/><br></br>
        /// Additionally set bitsize of internal address and opcode registers.
        /// </summary>
        /// <param name="addrBitsize"></param>
        /// <param name="opcodeBitsize"></param>
        public override void SetBitsize(uint addrBitsize, uint opcodeBitsize) {
            base.SetBitsize(addrBitsize+opcodeBitsize);
            AD.SetBitsize(addrBitsize);
            KOD.SetBitsize(opcodeBitsize);
        }

        /// <summary>Decodes and separates currently stored instruction value into address and opcode registers.</summary>
        public void DecodeInstruction() {
            AD.SetValue(Bitwise.DecodeIntructionArgument(this.GetValue()));
            KOD.SetValue(Bitwise.DecodeInstructionOpcode(this.GetValue()));
        }

        /// <summary><inheritdoc/></summary>
        public override void Reset() { base.Reset(); AD.Reset(); KOD.Reset(); }

    }
}

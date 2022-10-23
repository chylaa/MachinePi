using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaszynaPi.MachineLogic {

    public class CentralUnitException : Exception { public CentralUnitException(string message) : base(message) { } }

    public abstract class CentralUnit {
        protected uint AddressSpace = Defines.DEFAULT_ADDR_BITS;
        protected uint CodeBits = Defines.DEFAULT_CODE_BITS;
        protected uint GetAddressSpace() { return AddressSpace; }
        protected void SetAddressSpace(uint newAddressSpace) {
            if (newAddressSpace > Defines.ADDRESS_BITS_MAX || newAddressSpace < Defines.ADDRESS_BITS_MIN)
                throw new CentralUnitException("Invalid addresss bits size: " + newAddressSpace.ToString());
            AddressSpace = newAddressSpace;
        }
        protected uint GetCodeBits() { return CodeBits; }
        protected void SetCodeBits(uint newCodeBits) {
            if (newCodeBits > Defines.CODE_BITS_MAX || newCodeBits < Defines.CODE_BITS_MIN)
                throw new CentralUnitException("Invalid code bits size: " + newCodeBits.ToString());
            CodeBits = newCodeBits;
        }
    }
    
    class MachineW : CentralUnit { 
        Memory PaO; // Operation Memory ("FLash"?)
        Bus MagA, MagS; // BUSes
        ArithmetitLogicUnit JAL; 
        Register A, S, AK; // Address Register, Value Register, Accumulator
        Register I; // Instruction Register
        Register L; //Instruction Pointer


        public MachineW(uint addressSpace = Defines.DEFAULT_ADDR_BITS, uint codeBits = Defines.DEFAULT_CODE_BITS) {
            SetAddressSpace(addressSpace);
            SetCodeBits(codeBits);
            PaO = new Memory(addressSpace);
            AK = new Register(); 
            A = new Register();
            S = new Register();
            L = new Register();
            I = new InstructionRegister();
            JAL = new ArithmetitLogicUnit();
            MagA = new Bus();
            MagS = new Bus();
        }
        // ========================== <  Signals Methods > ========--=========================== //

        void czyt() { S.Value = PaO.GetValue(A.Value); }
        void pisz() { PaO.StoreValue(A.Value, S.Value); }
        void wys() { }
        void wes() { }
        void wei() { }
        void il() { }

        void wyak() { }
        void weja() { }

        // ======================= <  User Interface Methods > ================================= //
        public void SetMemoryContent(uint addr, uint value) { PaO.StoreValue(addr, value); }
        public uint GetMemoryContent(uint addr) { return PaO.GetValue(addr); }
        public void ExpandMemory(uint newAddrSpace) { PaO.ExpandMemory(oldAddressSpace:AddressSpace,newAddressSpace:newAddrSpace); }
        public void ExpandAndClearMemory(uint addrSpace) { PaO.InitMemoryContent(addrSpace); }

    }

    class MachineWp : MachineW {

        public MachineWp() { }
    }

    class MachineL : MachineWp {

        public MachineL() { }
    }

    class MachineEW : MachineL {

        public MachineEW() { }
    }



}

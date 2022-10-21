using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaszynaPi.MachineLogic {
    class CentralUnit {

        internal class MachineW { //check what is "internal"
            Memory PaO;

            public MachineW(uint addressSpace = 5, uint codeSize = 3) {
                PaO = new Memory(addressSpace, codeSize);
            }
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


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaszynaPi.MachineAssembler;

namespace MaszynaPi.MachineLogic.Architecture {
    class InstructionDecoder {
        const int JUMP_INDEX_NOT_SET = -1;
        //Loaded from .lst file
        //Dictionary<uint, List<List<string>>> InstructionMap; //(opcode: list of ticks) -> ticks = list of signals
        string JumpLabel; 
        int JumpIndex;
        public string StatementArg { get; set; } 
        
        public InstructionDecoder() {
            StatementArg = "";
            JumpLabel = "";
            JumpIndex = JUMP_INDEX_NOT_SET;
        }

        //Returns empty string if statement not foud (line just starts with label)
        private string GetStatementJumpLabel(bool conditionalStatementResult, List<string> signals) {
            if (conditionalStatementResult) {
                int argPos = signals.IndexOf(Defines.SIGNAL_STATEMENT_THEN) + 1;
                if (argPos == 0 || argPos >= signals.Count) throw new CentralUnitException("No statement jump label in line " + string.Join(" ", signals));
                return signals[argPos];
            } else {
                int elsePos = signals.IndexOf(Defines.SIGNAL_STATEMENT_ELSE.Split(' ')[1]);
                if (elsePos > 0) return signals[elsePos + 1];
            }
            return "";
        }

        //Returns empty string if statement not foud (line just starts with label)
        private string GetSignalStatementArgument(List<string> signals) {
            int argPos = signals.IndexOf(Defines.SIGNAL_STATEMENT_IF) + 1;
            if (argPos == 0 || argPos >= signals.Count) return "";
            return signals[argPos];
        }

        private int GetJumpIndex(List<List<string>> instructionSignals, string label) {
            if (label.Equals("")) return -1;
            for (int i = 0; i < instructionSignals.Count; i++) {
                if (instructionSignals[i][0].Equals(label))
                    return i;
            }
            return -1;
        }

        public int GetNumberOfTicksInInstruction(uint instructionOpcode) {
            return InstructionLoader.GetInstructionSignalsMap()[instructionOpcode].Count;
        }

        public Func<string,bool> OnRequestALUFlagState;

        protected bool RequestALUFlagState(string statementArg) {
            if (OnRequestALUFlagState == null) throw new Exception("[Code Error] OnRequestFlagState handler is not assigned");
            return OnRequestALUFlagState(statementArg);
        }

        public List<string> DecodeActiveSignals(uint instructionOpcode, int tick) {
            List<List<string>> instructionSignals = InstructionLoader.GetInstructionSignalsMap()[instructionOpcode];

            if (instructionSignals[tick].Any(signal => signal.Contains(Defines.SIGNAL_STATEMENT_IF))) {
                StatementArg = GetSignalStatementArgument(instructionSignals[tick]);
                JumpLabel = GetStatementJumpLabel(RequestALUFlagState(StatementArg), instructionSignals[tick]);
                JumpIndex = GetJumpIndex(instructionSignals, JumpLabel);
            }
            return new List<string>(instructionSignals[tick]);
        }

        public int GetJumpIndex(int tick) {
            if (JumpIndex > JUMP_INDEX_NOT_SET) { 
                int ji = JumpIndex;
                JumpIndex = JUMP_INDEX_NOT_SET;
                return ji;
            }
            return tick;
        }
    }
}

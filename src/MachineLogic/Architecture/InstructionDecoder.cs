using System;
using System.Collections.Generic;
using System.Linq;
using MaszynaPi.MachineAssembler;

namespace MaszynaPi.MachineLogic.Architecture {

    /// <summary>
    /// Represents <see cref="CentralProcessingUnit"/>'s component responsible for decoding instructions into 
    /// set of their underlaing microinstructions (uOperations). 
    /// </summary>
    class InstructionDecoder 
    {
        const int JUMP_INDEX_NOT_SET = -1;
        string JumpLabel; 
        int JumpIndex;

        /// <summary>Stores argumet of currently decoded instruction's uOp pseudo-lang conditional statement.</summary>
        public string StatementArg { get; set; } 
        
        /// <summary>Creates new instance of decoder.</summary>
        public InstructionDecoder() {
            StatementArg = "";
            JumpLabel = "";
            JumpIndex = JUMP_INDEX_NOT_SET;
        }

 
        /// <summary>
        /// Searches for jump label in <paramref name="signals"/> that should be next destination within single instruction's one-clock-cycle uOps signals definition.
        /// <paramref name="signals"/> list must contain valid conditional statement with label, otherwise <see cref="CPUException"/> is thrown.
        /// </summary>
        /// <param name="conditionalStatementResult">Calculated result of uOp contitional statement.</param>
        /// <param name="signals">List of signals acive in current clock cycle.</param>
        /// <returns>String with found destination label or <see cref="string.Empty"/> if label was not found.</returns>
        /// <exception cref="CPUException"></exception>
        private string GetStatementJumpLabel(bool conditionalStatementResult, List<string> signals) {
            if (conditionalStatementResult) {
                int argPos = signals.IndexOf(Defines.SIGNAL_STATEMENT_THEN) + 1;
                if (argPos == 0 || argPos >= signals.Count) throw new CPUException("No statement jump label in line " + string.Join(" ", signals));
                return signals[argPos];
            } else {
                int elsePos = signals.IndexOf(Defines.SIGNAL_STATEMENT_ELSE.Split(' ')[1]);
                if (elsePos > 0) return signals[elsePos + 1];
            }
            return string.Empty;
        }

        /// <summary>Sarches for argument argument of microcode conditional statement in passed <paramref name="signals"/> list. </summary>
        /// <param name="signals">List of signals acive in current clock cycle.</param>
        /// <returns>Found argument string or <see cref="string.Empty"/> if conditional statement not found in <paramref name="signals"/>.</returns>
        private string GetSignalStatementArgument(List<string> signals) {
            int argPos = signals.IndexOf(Defines.SIGNAL_STATEMENT_IF) + 1;
            if (argPos == 0 || argPos >= signals.Count) return string.Empty;
            return signals[argPos];
        }

        /// <summary>
        /// Calculates index of line (destination jump) within single instruction <paramref name="instructionSignals"/>, that contains 
        /// destination <paramref name="label"/>. 
        /// </summary>
        /// <param name="instructionSignals">All microinstructions that creates currently decoded instruction.</param>
        /// <param name="label">Microcode destination jump label from conditional statement.</param>
        /// <returns>Index of line in <paramref name="instructionSignals"/> (main list index) that starts with <paramref name="label"/>, -1 if not found.</returns>
        private int GetJumpIndex(List<List<string>> instructionSignals, string label) {
            if (label.Equals("")) return -1;
            for (int i = 0; i < instructionSignals.Count; i++) {
                if (instructionSignals[i][0].Equals(label))
                    return i;
            }
            return -1;
        }

        /// <summary>Calculates number of clock cycles that must be performed to execute instruction with opcode <paramref name="instructionOpcode"/>.</summary>
        /// <param name="instructionOpcode">Opcode of processed instruction.</param>
        /// <returns>Number of cycles neccessary to execute instruction defined by <paramref name="instructionOpcode"/>.</returns>
        public int GetNumberOfTicksInInstruction(uint instructionOpcode) {
            return InstructionLoader.GetInstructionSignalsMap()[instructionOpcode].Count;
        }

        /// <summary> Function hadle for getting from <see cref="ArithmeticLogicUnit"/> specific <see cref="ALUFlags"/> 
        /// state (set/not as boolean true/false), specified as it's name in string format. </summary>
        public Func<string,bool> OnRequestALUFlagState;

        /// <summary>Performs null check and calls <see cref="OnRequestALUFlagState"/> with <paramref name="flagName"/>.</summary>
        /// <param name="flagName">Argument of microop conditional statement as name of ALU's flag.</param>
        /// <returns>True if ALU's related flag (that have <paramref name="flagName"/> name) is set, false otherwise.</returns>
        /// <exception cref="Exception"></exception>
        private bool RequestALUFlagState(string flagName) {
            if (OnRequestALUFlagState == null) throw new Exception("[Code Error] OnRequestFlagState handler is not assigned");
            return OnRequestALUFlagState(flagName);
        }

        /// <summary>
        /// Creates list consisting of names of uOps signals that should be active in current <paramref name="tick"/> of 
        /// currently executed instruction, specified by <paramref name="instructionOpcode"/>.
        /// If any of instruction's micocode lines contains conditional statement, method sets internal
        /// <see cref="StatementArg"/>, <see cref="JumpLabel"/> and <see cref="JumpIndex"/> fields, that allows to
        /// control, instruction later flow.
        /// </summary>
        /// <param name="instructionOpcode">Opcode of currently executed instruction.</param>
        /// <param name="tick">Zero-based number of clock cycle, where cycle 0 is current instruction fetch cycle.</param>
        /// <returns>New list of microinstructions that should be activated.</returns>
        public List<string> DecodeActiveSignals(uint instructionOpcode, int tick) {
            List<List<string>> instructionSignals = InstructionLoader.GetInstructionSignalsMap()[instructionOpcode];

            if (instructionSignals[tick].Any(signal => signal.Contains(Defines.SIGNAL_STATEMENT_IF))) {
                StatementArg = GetSignalStatementArgument(instructionSignals[tick]);
                JumpLabel = GetStatementJumpLabel(RequestALUFlagState(StatementArg), instructionSignals[tick]);
                JumpIndex = GetJumpIndex(instructionSignals, JumpLabel);
            }
            return new List<string>(instructionSignals[tick]);
        }

        /// <summary>
        /// Allows to retreive index of next part of instruction's uOps that should be excuted.
        /// Base on passed current <paramref name="tickidx"/> and internal <see cref="JumpIndex"/> field (set in <see cref="DecodeActiveSignals(uint, int)"/> method).
        /// </summary>
        /// <param name="tickidx">Index of signals set currently executed in pending instrucition.</param>
        /// <returns>Index of instruction's set of microoperations, that should be set active on next clock cycle.</returns>
        public int GetNextSignalsIndex(int tickidx) {
            if (JumpIndex > JUMP_INDEX_NOT_SET) { 
                int ji = JumpIndex;
                JumpIndex = JUMP_INDEX_NOT_SET;
                return ji;
            }
            return tickidx;
        }
    }
}

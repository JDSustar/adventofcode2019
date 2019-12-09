using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019
{
    public class IntCodeMachine
    {
        public enum IntCodeOpCode
        {
            Add = 1,
            Multiply = 2,
            Input = 3,
            Output = 4,
            JumpIfTrue = 5,
            JustIfFalse = 6,
            LessThan = 7,
            Equals = 8,
            AdjustRelativeBase = 9,
            Halt = 99,
        }

        public enum ParameterMode
        {
            PositionMode = 0,
            ImmediateMode = 1,
            RelativeMode = 2,
        }

        public class Instruction
        {
            public IntCodeOpCode OpCode { get; private set; }
            public ParameterMode FirstParameterMode { get; private set; }
            public ParameterMode SecondParameterMode { get; private set; }
            public ParameterMode ThirdParameterMode { get; private set; }

            public string instructionString;

            public Instruction(long instructionValue)
            {
                instructionString = instructionValue.ToString("D5");

                OpCode = (IntCodeOpCode)int.Parse(instructionString.Substring(3, 2));
                FirstParameterMode = (ParameterMode)int.Parse(instructionString.Substring(2, 1));
                SecondParameterMode = (ParameterMode)int.Parse(instructionString.Substring(1, 1));
                ThirdParameterMode = (ParameterMode)int.Parse(instructionString.Substring(0, 1));
            }

            public override string ToString()
            {
                return instructionString + ": " + OpCode.ToString() + ": " + FirstParameterMode.ToString() + " - " + SecondParameterMode.ToString() + " - " + ThirdParameterMode.ToString();
            }
        }

        public List<long> Memory { get; private set; }

        public int InstructionPointer { get; private set; }

        public int CurrentRelativeBase { get; private set; }

        public bool IsRunning { get; private set; }

        public string Output { get; private set; }

        public List<long> Input { get; private set; }

        public IntCodeMachine(long[] memory)
        {
            Memory = new List<long>(memory);
            Memory.AddRange(new long[100000]);
            InstructionPointer = 0;
            Output = "";
            CurrentRelativeBase = 0;
        }

        public IntCodeMachine(long[] memory, int noun, int verb, long[] input) : this(memory)
        {
            Memory[1] = noun;
            Memory[2] = verb;
            Input = new List<long>(input);
        }

        public IntCodeMachine(long[] memory, long[] input) : this(memory)
        {
            Input = new List<long>(input);
        }

        public void Run()
        {
            IsRunning = true;

            bool stopFound = (Memory[InstructionPointer] == 99);

            while (!stopFound)
            {
                stopFound = !EvaluateInstruction(new Instruction(Memory[InstructionPointer]));
            }
        }

        public int GetOutput()
        {
            string temp = this.Output;
            this.Output = "";
            return int.Parse(temp);
        }

        public long GetParameterValue(int position, ParameterMode mode)
        {
            if (mode == ParameterMode.PositionMode)
            {
                return Memory[(int)Memory[position]];
            }
            else if (mode == ParameterMode.ImmediateMode)
            {
                return Memory[position];
            }
            else if (mode == ParameterMode.RelativeMode)
            {
                return Memory[CurrentRelativeBase + (int)Memory[position]];
            }
            else
            {
                throw new NotImplementedException("Unknown Parameter Mode Encountered.");
            }
        }

        public void SetMemoryValue(int position, ParameterMode mode, long value)
        {
            if (mode == ParameterMode.PositionMode)
            {
                int resultPosition = (int)Memory[position];
                Memory[resultPosition] = value;
            }
            else if (mode == ParameterMode.RelativeMode)
            {
                Memory[CurrentRelativeBase + (int)Memory[position]] = value;
            }
            else
            {
                throw new NotImplementedException("Unknown Parameter Mode Encountered.");
            }
        }

        public bool EnterInput(int inputValue)
        {
            Input.Add(inputValue);

            if (IsRunning)
            {
                Run();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EvaluateInstruction(Instruction i)
        {
            Logger.LogMessage(LogLevel.DEBUG, "Evaluating Instruction at Position: " + InstructionPointer);
            Logger.LogMessage(LogLevel.DEBUG, i.ToString());
            if (i.OpCode == IntCodeOpCode.Add)
            {
                long addend1 = GetParameterValue(InstructionPointer + 1, i.FirstParameterMode);
                long addend2 = GetParameterValue(InstructionPointer + 2, i.SecondParameterMode);
                SetMemoryValue(InstructionPointer + 3, i.ThirdParameterMode, addend1 + addend2);
                InstructionPointer += 4;
                return true;
            }
            else if (i.OpCode == IntCodeOpCode.Multiply)
            {
                long factor1 = GetParameterValue(InstructionPointer + 1, i.FirstParameterMode);
                long factor2 = GetParameterValue(InstructionPointer + 2, i.SecondParameterMode);
                SetMemoryValue(InstructionPointer + 3, i.ThirdParameterMode, factor1 * factor2);
                InstructionPointer += 4;
                return true;
            }
            else if (i.OpCode == IntCodeOpCode.Input)
            {
                if (Input == null || Input.Count == 0)
                {
                    return false;
                }

                SetMemoryValue(InstructionPointer + 1, i.FirstParameterMode, Input.First());

                Input.RemoveAt(0);
                InstructionPointer += 2;
                return true;
            }
            else if (i.OpCode == IntCodeOpCode.Output)
            {
                Logger.LogMessage(LogLevel.DEBUG, "Int Code Machine Output: " + GetParameterValue(InstructionPointer + 1, i.FirstParameterMode).ToString());
                Output += GetParameterValue(InstructionPointer + 1, i.FirstParameterMode).ToString();
                InstructionPointer += 2;
                return true;
            }
            else if (i.OpCode == IntCodeOpCode.Halt)
            {
                InstructionPointer += 1;
                IsRunning = false;
                return false;
            }
            else if (i.OpCode == IntCodeOpCode.JumpIfTrue)
            {
                if (GetParameterValue(InstructionPointer + 1, i.FirstParameterMode) == 0)
                {
                    InstructionPointer += 3;
                }
                else
                {
                    InstructionPointer = (int)GetParameterValue(InstructionPointer + 2, i.SecondParameterMode);
                }

                return true;
            }
            else if (i.OpCode == IntCodeOpCode.JustIfFalse)
            {
                if (GetParameterValue(InstructionPointer + 1, i.FirstParameterMode) != 0)
                {
                    InstructionPointer += 3;
                }
                else
                {
                    InstructionPointer = (int)GetParameterValue(InstructionPointer + 2, i.SecondParameterMode);
                }

                return true;
            }
            else if (i.OpCode == IntCodeOpCode.LessThan)
            {
                if (GetParameterValue(InstructionPointer + 1, i.FirstParameterMode) <
                    GetParameterValue(InstructionPointer + 2, i.SecondParameterMode))
                {
                    SetMemoryValue(InstructionPointer + 3, i.ThirdParameterMode, 1);
                }
                else
                {
                    SetMemoryValue(InstructionPointer + 3, i.ThirdParameterMode, 0);
                }

                InstructionPointer += 4;
                return true;
            }
            else if (i.OpCode == IntCodeOpCode.Equals)
            {
                if (GetParameterValue(InstructionPointer + 1, i.FirstParameterMode) ==
                    GetParameterValue(InstructionPointer + 2, i.SecondParameterMode))
                {

                    SetMemoryValue(InstructionPointer + 3, i.ThirdParameterMode, 1);
                }
                else
                {
                    SetMemoryValue(InstructionPointer + 3, i.ThirdParameterMode, 0);
                }

                InstructionPointer += 4;
                return true;
            }
            else if (i.OpCode == IntCodeOpCode.AdjustRelativeBase)
            {
                CurrentRelativeBase += (int)GetParameterValue(InstructionPointer + 1, i.FirstParameterMode);
                InstructionPointer += 2;
                return true;
            }
            else
            {
                throw new NotImplementedException("Unknown instruction type at current Instruction Pointer: " +
                                                  InstructionPointer);
            }
        }
    }
}

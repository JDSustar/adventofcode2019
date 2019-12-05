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
            Halt = 99,
        }

        public enum ParameterMode
        {
            PositionMode = 0,
            ImmediateMode = 1,
        }

        public class Instruction
        {
            public IntCodeOpCode OpCode { get; private set; }
            public ParameterMode FirstParameterMode { get; private set; }
            public ParameterMode SecondParameterMode { get; private set; }
            public ParameterMode ThirdParameterMode { get; private set; }

            public Instruction(int instructionValue)
            {
                string instructionValueString = instructionValue.ToString("D5");

                OpCode = (IntCodeOpCode)int.Parse(instructionValueString.Substring(3, 2));
                FirstParameterMode = (ParameterMode)int.Parse(instructionValueString.Substring(2, 1));
                SecondParameterMode = (ParameterMode)int.Parse(instructionValueString.Substring(1, 1));
                ThirdParameterMode = (ParameterMode)int.Parse(instructionValueString.Substring(0, 1));
            }
        }

        public int[] Memory { get; private set; }

        public int InstructionPointer { get; private set; }

        public string Output { get; private set; }
        public List<int> Input { get; private set; }

        public IntCodeMachine(int[] memory)
        {
            Memory = memory;
            InstructionPointer = 0;
            Output = "";
        }

        public IntCodeMachine(int[] memory, int noun, int verb, int[] input) : this(memory)
        {
            Memory[1] = noun;
            Memory[2] = verb;
            Input = input.ToList();
        }

        public IntCodeMachine(int[] memory, int[] input) : this(memory)
        {
            Input = input.ToList();
        }

        public void Run()
        {
            bool endFound = (Memory[InstructionPointer] == 99);

            while (!endFound)
            {
                endFound = !EvaluateInstruction(new Instruction(Memory[InstructionPointer]));
            }
        }

        public int GetParameterValue(int parameter, ParameterMode mode)
        {
            if (mode == ParameterMode.PositionMode)
            {
                return Memory[parameter];
            }
            else if (mode == ParameterMode.ImmediateMode)
            {
                return parameter;
            }
            else
            {
                throw new NotImplementedException("Unknown Parameter Mode Encountered.");
            }
        }

        public bool EvaluateInstruction(Instruction i)
        {
            if (i.OpCode == IntCodeOpCode.Add)
            {
                int addend1 = GetParameterValue(Memory[InstructionPointer + 1], i.FirstParameterMode);
                int addend2 = GetParameterValue(Memory[InstructionPointer + 2], i.SecondParameterMode);
                int resultPosition = Memory[InstructionPointer + 3];

                Memory[resultPosition] = addend1 + addend2;
                InstructionPointer += 4;
                return true;
            }
            else if (i.OpCode == IntCodeOpCode.Multiply)
            {
                int factor1 = GetParameterValue(Memory[InstructionPointer + 1], i.FirstParameterMode);
                int factor2 = GetParameterValue(Memory[InstructionPointer + 2], i.SecondParameterMode);
                int resultPosition = Memory[InstructionPointer + 3];

                Memory[resultPosition] = factor1 * factor2;
                InstructionPointer += 4;
                return true;
            }
            else if (i.OpCode == IntCodeOpCode.Input)
            {
                if (Input == null || Input.Count == 0)
                {
                    throw new Exception("No Input Values Available!");
                }

                Memory[Memory[InstructionPointer + 1]] = Input.First();
                Input.RemoveAt(0);
                InstructionPointer += 2;
                return true;
            }
            else if (i.OpCode == IntCodeOpCode.Output)
            {
                Output += GetParameterValue(Memory[InstructionPointer + 1], i.FirstParameterMode).ToString();
                InstructionPointer += 2;
                return true;
            }
            else if (i.OpCode == IntCodeOpCode.Halt)
            {
                InstructionPointer += 1;
                return false;
            }
            else if (i.OpCode == IntCodeOpCode.JumpIfTrue)
            {
                if (GetParameterValue(Memory[InstructionPointer + 1], i.FirstParameterMode) == 0)
                {
                    InstructionPointer += 3;
                }
                else
                {
                    InstructionPointer = GetParameterValue(Memory[InstructionPointer + 2], i.SecondParameterMode);
                }

                return true;
            }
            else if (i.OpCode == IntCodeOpCode.JustIfFalse)
            {
                if (GetParameterValue(Memory[InstructionPointer + 1], i.FirstParameterMode) != 0)
                {
                    InstructionPointer += 3;
                }
                else
                {
                    InstructionPointer = GetParameterValue(Memory[InstructionPointer + 2], i.SecondParameterMode);
                }

                return true;
            }
            else if (i.OpCode == IntCodeOpCode.LessThan)
            {
                if (GetParameterValue(Memory[InstructionPointer + 1], i.FirstParameterMode) <
                    GetParameterValue(Memory[InstructionPointer + 2], i.SecondParameterMode))
                {
                    Memory[Memory[InstructionPointer + 3]] = 1;
                }
                else
                {
                    Memory[Memory[InstructionPointer + 3]] = 0;
                }
                 
                InstructionPointer += 4;
                return true;
            }
            else if (i.OpCode == IntCodeOpCode.Equals)
            {
                if (GetParameterValue(Memory[InstructionPointer + 1], i.FirstParameterMode) ==
                    GetParameterValue(Memory[InstructionPointer + 2], i.SecondParameterMode))
                {
                    int resultPosition = GetParameterValue(Memory[InstructionPointer + 3], i.ThirdParameterMode);
                    int resultPosition2 = Memory[InstructionPointer + 3];

                    Memory[Memory[InstructionPointer + 3]] = 1;
                }
                else
                {
                    Memory[Memory[InstructionPointer + 3]] = 0;
                }

                InstructionPointer += 4;
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

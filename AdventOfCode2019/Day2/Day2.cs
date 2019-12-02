using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualBasic;

namespace AdventOfCode2019
{
    public static class Day2
    {
        public static void ExecuteStarOne(string fileLocation = "Day2/Day2.txt")
        {
            string line = File.ReadAllText(fileLocation);

            Logger.LogMessage(LogLevel.INFO, line);

            IntCode intCode = new IntCode(Array.ConvertAll(line.Split(','), int.Parse), 12, 2); 
            intCode.EvaluateCodes();

            Logger.LogMessage(LogLevel.ANSWER, "2A: Value At Position 0: " + intCode.Memory[0].ToString());
        }

        public static void ExecuteStarTwo(string fileLocation = "Day2/Day2.txt")
        {
            string line = File.ReadAllText(fileLocation);

            bool outputFound = false;

            for (int currentNoun = 0; (currentNoun <= 99 && !outputFound); currentNoun++)
            {
                for (int currentVerb = 0; currentVerb <= 99 && !outputFound; currentVerb++)
                {
                    IntCode intCode = new IntCode(Array.ConvertAll(line.Split(','), int.Parse), currentNoun, currentVerb);
                    intCode.EvaluateCodes();

                    if (intCode.Memory[0] == 19690720)
                    {
                        Logger.LogMessage(LogLevel.ANSWER, "2B: Noun: " + currentNoun + "\t Verb: " + currentVerb + "\t Answer: " + (100 * currentNoun + currentVerb));
                    }
                }
            }
        }
    }

    public class IntCode
    {
        public int[] Memory { get; private set; }
        public int[] OriginalMemory { get; private set; }

        public int InstructionPointer { get; private set; }

        public IntCode(int[] memory)
        {
            Memory = memory;
            OriginalMemory = memory;
            InstructionPointer = 0;
        }

        public IntCode(int[] memory, int noun = 12, int verb = 2, int instructionPointer = 0)
        {
            Memory = memory;
            OriginalMemory = memory;
            Memory[1] = noun;
            Memory[2] = verb;
            InstructionPointer = instructionPointer;
        }

        public void EvaluateCodes()
        {
            bool endFound = (Memory[InstructionPointer] == 99);
            
            while (!endFound)
            {
                endFound = !evaluateCurrentInstruction();
            }
        }

        private bool evaluateCurrentInstruction()
        {
            if (Memory[InstructionPointer] == 1)
            {
                int addend1 = Memory[Memory[InstructionPointer + 1]];
                int addend2 = Memory[Memory[InstructionPointer + 2]];
                int resultPosition = Memory[InstructionPointer + 3];

                Memory[resultPosition] = addend1 + addend2;
                InstructionPointer += 4;
                return true;
            }
            else if (Memory[InstructionPointer] == 2)
            {
                int factor1 = Memory[Memory[InstructionPointer + 1]];
                int factor2 = Memory[Memory[InstructionPointer + 2]];
                int resultPosition = Memory[InstructionPointer + 3];

                Memory[resultPosition] = factor1 * factor2;
                InstructionPointer += 4;
                return true;
            }
            else if (Memory[InstructionPointer] == 99)
            {
                InstructionPointer += 1;
                return false;
            }

            throw new NotImplementedException("Unknown instruction type at current Instruction Pointer: " + InstructionPointer);
        }

    }
}

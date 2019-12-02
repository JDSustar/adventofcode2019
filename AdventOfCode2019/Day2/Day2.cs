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

        public IntCode(int[] memory, int noun, int verb)
        {
            Memory = memory;
            OriginalMemory = memory;
            Memory[1] = noun;
            Memory[2] = verb;
        }

        public void EvaluateCodes()
        {
            int currentAddress = 0;
            bool endFound = (Memory[currentAddress] == 99);
            
            while (!endFound)
            {
                evaluateAddress(currentAddress);
                currentAddress += 4;
                endFound = (Memory[currentAddress] == 99);
            }
        }

        private void evaluateAddress(int address)
        {
            if (Memory[address] == 1)
            {
                int addend1 = Memory[Memory[address + 1]];
                int addend2 = Memory[Memory[address + 2]];
                int resultPosition = Memory[address + 3];

                Memory[resultPosition] = addend1 + addend2;
            }
            else if (Memory[address] == 2)
            {
                int factor1 = Memory[Memory[address + 1]];
                int factor2 = Memory[Memory[address + 2]];
                int resultPosition = Memory[address + 3];

                Memory[resultPosition] = factor1 * factor2;
            }
            else if (Memory[address] == 99)
            {
                return;
            }
        }

    }
}

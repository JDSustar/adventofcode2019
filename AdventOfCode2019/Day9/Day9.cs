using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2019
{
    public static class Day9
    {
        public static void ExecuteStarOne(string fileLocation = "Day9/Day9.txt")
        {
            long[] program = File.ReadAllText(fileLocation).Split(',').Select(long.Parse).ToArray();
            long[] input = new List<long>() { 1 }.ToArray();

            IntCodeMachine icm = new IntCodeMachine(program, input);
            icm.Run();

            Logger.LogMessage(LogLevel.ANSWER, "9A: BOOST Keycode: " + icm.Output);
        }

        public static void ExecuteStarTwo(string fileLocation = "Day9/Day9.txt")
        {
            long[] program = File.ReadAllText(fileLocation).Split(',').Select(long.Parse).ToArray();
            long[] input = new List<long>() { 2 }.ToArray();

            IntCodeMachine icm = new IntCodeMachine(program, input);
            icm.Run();

            Logger.LogMessage(LogLevel.ANSWER, "9A: BOOST Coordinates: " + icm.Output);
        }
    }
}

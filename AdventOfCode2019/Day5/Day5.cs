using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode2019
{
    public static class Day5
    {
        public static void ExecuteStarOne(string fileLocation = "Day5/Day5.txt")
        {
            string line = File.ReadAllText(fileLocation);

            var input = new int[1] {1};

            IntCodeMachine machine = new IntCodeMachine(Array.ConvertAll(line.Split(','), int.Parse), input);
            machine.Run();

            Logger.LogMessage(LogLevel.ANSWER, "5A: Machine Output: " + machine.Output);
        }

        public static void ExecuteStarTwo(string fileLocation = "Day5/Day5.txt")
        {
            string line = File.ReadAllText(fileLocation);

            var input = new int[1] { 5 };

            IntCodeMachine machine = new IntCodeMachine(Array.ConvertAll(line.Split(','), int.Parse), input);
            machine.Run();

            Logger.LogMessage(LogLevel.ANSWER, "5B: Machine Output: " + machine.Output);
        }
    }
}

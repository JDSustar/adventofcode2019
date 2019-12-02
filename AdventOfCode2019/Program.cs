using System;

namespace AdventOfCode2019
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.CURRENT_LOG_LEVEL = LogLevel.INFO;
            Day1.ExecuteStarOne("Day1/Day1.txt");
            Day1.ExecuteStarTwo("Day1/Day1.txt");
        }
    }
}

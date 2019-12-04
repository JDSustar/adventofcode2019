using System;

namespace AdventOfCode2019
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.CURRENT_LOG_LEVEL = LogLevel.ANSWER;
            Day1.ExecuteStarOne();
            Day1.ExecuteStarTwo();
            Day2.ExecuteStarOne();
            Day2.ExecuteStarTwo();
            Day3.ExecuteStarOne();
            Day3.ExecuteStarTwo();
        }
    }
}

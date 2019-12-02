using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AdventOfCode2019
{
    public class Day1
    {
        public static int GetFuelRequiredForMass(int mass)
        {
            return mass / 3 - 2;
        }

        public static int GetFuelRequiredForFuel(int fuel)
        {
            int fuelRequired = fuel / 3 - 2;

            if (fuelRequired <= 0)
                return 0;
            else
                return fuelRequired + GetFuelRequiredForFuel(fuelRequired);
        }

        public static void ExecuteStarOne(string fileLocation = "Day1/Day1.txt")
        {
            string[] lines = File.ReadAllLines(fileLocation);

            int totalFuelRequired = 0;

            foreach (var line in lines)
            {
                int moduleFuelRequired = GetFuelRequiredForMass(int.Parse(line));
                Logger.LogMessage(LogLevel.INFO, line + "\t" + moduleFuelRequired);
                totalFuelRequired += moduleFuelRequired;
            }

            Logger.LogMessage(LogLevel.ANSWER, "1A: Total Fuel Required: " + totalFuelRequired.ToString());
        }

        public static void ExecuteStarTwo(string fileLocation = "Day1/Day1.txt")
        {
            string[] lines = File.ReadAllLines(fileLocation);

            int totalFuelRequired = 0;

            foreach (var line in lines)
            {
                int moduleFuelRequired = GetFuelRequiredForMass(int.Parse(line));
                int moduleFuelFuelRequired = GetFuelRequiredForFuel(moduleFuelRequired);
                Logger.LogMessage(LogLevel.INFO, line + "\t" + moduleFuelRequired + "\t" + moduleFuelFuelRequired);
                totalFuelRequired = totalFuelRequired + moduleFuelRequired + moduleFuelFuelRequired;
            }

            Logger.LogMessage(LogLevel.ANSWER, "1B: Total Fuel Required: " + totalFuelRequired.ToString());
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2019
{
    public static class Day7
    {
        public static void ExecuteStarOne(string fileLocation = "Day7/Day7.txt")
        {
            long[] programMemory = File.ReadAllText(fileLocation).Split(',').Select(long.Parse).ToArray();

            var inputs = Utilities.GetPermutations(new List<long>() { 0, 1, 2, 3, 4 }, 5);

            int largestOutput = Int32.MinValue;

            foreach (var input in inputs)
            {
                ThrusterAmplifiers ta = new ThrusterAmplifiers(programMemory, input.ToArray()); ;
                ta.Run();
                if (ta.Output > largestOutput)
                {
                    largestOutput = ta.Output;
                }
            }

            Logger.LogMessage(LogLevel.ANSWER, "7A: Largest Output: " + largestOutput);
        }

        public static void ExecuteStarTwo(string fileLocation = "Day7/Day7.txt")
        {
            long[] programMemory = File.ReadAllText(fileLocation).Split(',').Select(long.Parse).ToArray();

            var inputs = Utilities.GetPermutations(new List<long>() { 5, 6, 7, 8, 9 }, 5);

            int largestOutput = Int32.MinValue;

            foreach (var input in inputs)
            {
                ThrusterAmplifiers ta = new ThrusterAmplifiers(programMemory, input.ToArray()); ;
                ta.Run();
                if (ta.Output > largestOutput)
                {
                    largestOutput = ta.Output;
                }
            }

            Logger.LogMessage(LogLevel.ANSWER, "7B: Largest Output: " + largestOutput);
        }
    }

    public class ThrusterAmplifiers
    {
        public long[] ProgramMemory { get; private set; }

        public long[] Phases { get; private set; }

        public int Output { get; private set; }

        private IntCodeMachine a, b, c, d, e;

        public ThrusterAmplifiers(long[] programMemory, long[] phases)
        {
            ProgramMemory = programMemory;
            Phases = phases;
            a = new IntCodeMachine(programMemory, new List<long>() { phases[0] }.ToArray());
            b = new IntCodeMachine(programMemory, new List<long>() { phases[1] }.ToArray());
            c = new IntCodeMachine(programMemory, new List<long>() { phases[2] }.ToArray());
            d = new IntCodeMachine(programMemory, new List<long>() { phases[3] }.ToArray());
            e = new IntCodeMachine(programMemory, new List<long>() { phases[4] }.ToArray());
        }

        public void Run()
        {
            a.EnterInput(0);
            a.Run();
            b.EnterInput(a.GetOutput());
            b.Run();
            c.EnterInput(b.GetOutput());
            c.Run();
            d.EnterInput(c.GetOutput());
            d.Run();
            e.EnterInput(d.GetOutput());
            e.Run();

            while (e.IsRunning)
            {
                a.EnterInput(e.GetOutput());
                b.EnterInput(a.GetOutput());
                c.EnterInput(b.GetOutput());
                d.EnterInput(c.GetOutput());
                e.EnterInput(d.GetOutput());
            }

            this.Output = e.GetOutput();
        }
    }
}

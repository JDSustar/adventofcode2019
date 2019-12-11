using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2019
{
    public static class Day11
    {
        public static void ExecuteStarOne(string fileLocation = "PuzzleInput/Day11.txt")
        {
            long[] programMemory = File.ReadAllText(fileLocation).Split(',').Select(long.Parse).ToArray();

            HullPaintingRobot hpr = new HullPaintingRobot(programMemory, HullPaintingRobot.Color.Black);
            hpr.Run();

            Logger.LogMessage(LogLevel.ANSWER, "11A: Painted Locations: " + (hpr.Hull.GetLocationsWithColor(HullPaintingRobot.Color.Black) + hpr.Hull.GetLocationsWithColor(HullPaintingRobot.Color.White)));
        }

        public static void ExecuteStarTwo(string fileLocation = "PuzzleInput/Day11.txt")
        {
            long[] programMemory = File.ReadAllText(fileLocation).Split(',').Select(long.Parse).ToArray();

            HullPaintingRobot hpr = new HullPaintingRobot(programMemory, HullPaintingRobot.Color.White);
            hpr.Run();

            Logger.LogMessage(LogLevel.ANSWER, "11A: Registration Number: ");
            Logger.LogMessage(LogLevel.ANSWER, hpr.Hull.ToString());
        }
    }

    public class HullPaintingRobot
    {
        public enum Turn
        {
            Left = 0,
            Right = 1,
        }

        public enum Color
        {
            Black = 0,
            White = 1,
        }

        public enum Direction
        {
            North,
            South,
            West, 
            East
        }

        public IntCodeMachine Computer { get; private set; }

        public Point CurrentLocation { get; private set; }

        public Direction CurrentDirection { get; private set; }

        public Hull Hull = new Hull();

        public HullPaintingRobot(long[] programMemory, Color startColor)
        {
            Computer = new IntCodeMachine(programMemory);
            CurrentLocation = new Point(250, 250);
            CurrentDirection = Direction.North;
            Hull.PaintLocation(CurrentLocation.X, CurrentLocation.Y, startColor);
        }

        public void Run()
        {
            Computer.Run();

            while (Computer.IsRunning)
            {
                Tick();
            }
        }

        public void Tick()
        {
            Computer.EnterInput((int)Hull.GetHullLocationColor(CurrentLocation.X, CurrentLocation.Y));
            Color c = (Color)Computer.GetNextOutput();
            Turn t = (Turn) Computer.GetNextOutput();

            Hull.PaintLocation(CurrentLocation.X, CurrentLocation.Y, c);
            TurnAndAdvance(t);
        }

        public void TurnAndAdvance(Turn d)
        {
            if (CurrentDirection == Direction.North)
            {
                switch (d)
                {
                    case Turn.Left:
                        CurrentDirection = Direction.West;
                        break;
                    case Turn.Right:
                        CurrentDirection = Direction.East;
                        break;
                }
            }
            else if (CurrentDirection == Direction.South)
            {
                switch (d)
                {
                    case Turn.Left:
                        CurrentDirection = Direction.East;
                        break;
                    case Turn.Right:
                        CurrentDirection = Direction.West;
                        break;
                }
            }
            else if (CurrentDirection == Direction.West)
            {
                switch (d)
                {
                    case Turn.Left:
                        CurrentDirection = Direction.South;
                        break;
                    case Turn.Right:
                        CurrentDirection = Direction.North;
                        break;
                }
            }
            else if (CurrentDirection == Direction.East)
            {
                switch (d)
                {
                    case Turn.Left:
                        CurrentDirection = Direction.North;
                        break;
                    case Turn.Right:
                        CurrentDirection = Direction.South;
                        break;
                }
            }

            switch (CurrentDirection)
            {
                case Direction.North:
                    CurrentLocation = new Point(CurrentLocation.X, CurrentLocation.Y + 1);
                    break;
                case Direction.South:
                    CurrentLocation = new Point(CurrentLocation.X, CurrentLocation.Y - 1);
                    break;
                case Direction.East:
                    CurrentLocation = new Point(CurrentLocation.X + 1, CurrentLocation.Y);
                    break;
                case Direction.West:
                    CurrentLocation = new Point(CurrentLocation.X - 1, CurrentLocation.Y);
                    break;
            }
        }
    }

    public class Hull
    {
        private int[,] HullLocations;
        int minX = Int32.MaxValue;
        int minY = Int32.MaxValue;
        int maxX = Int32.MinValue;
        int maxY = Int32.MinValue;

        public Hull()
        {
            HullLocations = Utilities.GetNew2DArray(500, 500, -1);
        }

        public void PaintLocation(int x, int y, HullPaintingRobot.Color c)
        {
            HullLocations[x, y] = (int)c;

            minX = Math.Min(minX, x);
            minY = Math.Min(minY, y);
            maxX = Math.Max(maxX, x);
            maxY = Math.Max(maxY, y);
        }

        public int GetLocationsWithColor(HullPaintingRobot.Color c)
        {
            int numLocations = 0;
            foreach (var hullLocation in HullLocations)
            {
                if (hullLocation == (int) c)
                {
                    numLocations++;
                }
            }

            return numLocations;
        }

        public HullPaintingRobot.Color GetHullLocationColor(int x, int y)
        {
            int color = HullLocations[x, y];
            if (color == -1)
            {
                color = 0;
            }

            return (HullPaintingRobot.Color) color;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (int h = maxY; h >= minY; h--)
            {
                for (int w = minX; w <= maxX; w++)
                {
                    var c = GetHullLocationColor(w, h);
                    if (c == HullPaintingRobot.Color.White)
                    {
                        sb.Append("#");
                    }
                    else
                    {
                        sb.Append(" ");
                    }
                }

                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;

namespace AdventOfCode2019
{
    public static class Day3
    {
        public static void ExecuteStarOne(string fileLocation = "PuzzleInput/Day3.txt")
        {
            string[] lines = File.ReadAllLines(fileLocation);

            WirePanel wp = new WirePanel(lines[0], lines[1]);

            Logger.LogMessage(LogLevel.ANSWER, "3A: Closest Intersection: " + wp.GetClosestIntersectionDistance());
        }

        public static void ExecuteStarTwo(string fileLocation = "PuzzleInput/Day3.txt")
        {
            string[] lines = File.ReadAllLines(fileLocation);

            WirePanel wp = new WirePanel(lines[0], lines[1]);

            Logger.LogMessage(LogLevel.ANSWER, "3A: Fewest Steps to Intersection: " + wp.GetFewestStepsIntersection());
        }
    }

    public class WirePanel
    {
        public Wire WireOne;
        public Wire WireTwo;

        public List<Tuple<int, int>> WireIntersections = new List<Tuple<int, int>>();

        public WirePanel(string wireOneInput, string wireTwoInput)
        {
            WireOne = new Wire(wireOneInput);
            WireTwo = new Wire(wireTwoInput);

            findIntersections();
        }

        private void findIntersections()
        {
            WireIntersections = WireOne.WirePoints.Intersect(WireTwo.WirePoints).ToList();
        }

        public int GetClosestIntersectionDistance()
        {
            int min = Utilities.GetManhattenDistance(WireIntersections.First().Item1, WireIntersections.First().Item2);

            foreach (var wireIntersection in WireIntersections)
            {
                int d = Utilities.GetManhattenDistance(wireIntersection.Item1, wireIntersection.Item2);
                if (d < min)
                    min = d;
            }

            return min;
        }

        public int GetFewestStepsIntersection()
        {
            int min = Int32.MaxValue;

            foreach (var wireIntersection in WireIntersections)
            {
                int numSteps = 0;
                Tuple<int, int > currentPoint = new Tuple<int, int>(0, 0);

                int i = 0;
                while (!currentPoint.Equals(wireIntersection))
                {
                    currentPoint = WireOne.WirePoints[i++];
                }

                numSteps += i;

                currentPoint = new Tuple<int, int>(0, 0);
                int j = 0;
                while (!currentPoint.Equals(wireIntersection))
                {
                    currentPoint = WireTwo.WirePoints[j++];
                }

                numSteps += j;

                if (numSteps < min) min = numSteps;
            }

            return min;
        }
    }

    public class Wire
    {
        public string InputString { get; private set; }

        public List<Tuple<int, int>> WirePoints { get; private set; }


        public Wire(string input)
        {
            WirePoints = new List<Tuple<int, int>>();
            string[] instructions = input.Split(',');
            buildWire(instructions);
        }

        private void buildWire(string[] instructions)
        {
            int currentX = 0;
            int currentY = 0;

            foreach (string instruction in instructions)
            {
                char direction = instruction[0];
                int paces = int.Parse(instruction.Substring(1));

                if (direction == 'U')
                {
                    for (int i = 0; i < paces; i++)
                    {
                        currentY += 1;
                        WirePoints.Add(new Tuple<int, int>(currentX, currentY));
                    }
                }
                else if (direction == 'R')
                {
                    for (int i = 0; i < paces; i++)
                    {
                        currentX += 1;
                        WirePoints.Add(new Tuple<int, int>(currentX, currentY));
                    }
                }
                else if (direction == 'D')
                {
                    for (int i = 0; i < paces; i++)
                    {
                        currentY -= 1;
                        WirePoints.Add(new Tuple<int, int>(currentX, currentY));
                    }
                }
                else if (direction == 'L')
                {
                    for (int i = 0; i < paces; i++)
                    {
                        currentX -= 1;
                        WirePoints.Add(new Tuple<int, int>(currentX, currentY));
                    }
                }
                else
                {
                    throw new NotImplementedException("Unknown direction encountered: " + direction);
                }
            }
        }
    }
}

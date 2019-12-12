using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2019
{
    public static class Day12
    {
        public static void ExecuteStarOne(string fileLocation = "PuzzleInput/Day12.txt")
        {
            List<string> moonStringList = File.ReadAllLines(fileLocation).ToList();

            var jm = new JupiterMoons(moonStringList);

            for (int i = 0; i < 1000; i++)
            {
                jm.Tick();
            }

            Logger.LogMessage(LogLevel.ANSWER, "12A: Jupiter Moon Energy: " + jm.GetSystemEnergy());
        }

        public static void ExecuteStarTwo(string fileLocation = "PuzzleInput/Day12.txt")
        {
            List<string> moonStringList = File.ReadAllLines(fileLocation).ToList();

            var jm = new JupiterMoons(moonStringList);
            var initialJm = jm.Clone() as JupiterMoons;
            jm.Tick();

            long steps = 0;

            while (!jm.Equals(initialJm))
            {
                jm.Tick();
                steps++;
                if (steps % 1000 == 0) Logger.LogMessage(LogLevel.DEBUG, steps.ToString());
            }

            Logger.LogMessage(LogLevel.ANSWER, "12B: Steps to Repeat: " + steps);
        }
    }

    public class JupiterMoons : ICloneable
    {
        public List<Moon> Moons { get; } = new List<Moon>();

        public JupiterMoons(List<string> moonStrings)
        {
            foreach (var moonString in moonStrings)
            {
                Moons.Add(new Moon(moonString));
            }
        }

        public JupiterMoons(JupiterMoons jm)
        {
            foreach (var jmMoon in jm.Moons)
            {
                this.Moons.Add((jmMoon.Clone() as Moon));
            }
        }

        public void Tick()
        {
            foreach (var moon in Moons)
            {
                foreach (var moon1 in Moons)
                {
                    moon1.AdjustVelocityFromGravityOf(moon);
                }
            }

            foreach (var moon in Moons)
            {
                moon.Tick();
            }
        }

        public int GetSystemEnergy()
        {
            int energy = 0;

            foreach (var moon in Moons)
            {
                energy += moon.GetPotentialEnergy() * moon.GetKineticEnergy();
            }

            return energy;
        }

        public override bool Equals(object obj)
        {
            if (obj is JupiterMoons jm)
            {
                if (this.Moons.Count != jm.Moons.Count)
                {
                    return false;
                }

                for (int i = 0; i < this.Moons.Count; i++)
                {
                    if (!this.Moons[i].Equals(jm.Moons[i]))
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return Moons.Sum(m => m.GetHashCode());
        }

        public object Clone()
        {
            return new JupiterMoons(this);
        }
    }

    public class Moon : ICloneable
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Z { get; private set; }

        public int XVelocity { get; private set; }
        public int YVelocity { get; private set; }
        public int ZVelocity { get; private set; }

        public Moon(string input)
        {
            string coordString = input;
            coordString = coordString.Substring(1).Remove(coordString.Length - 2);

            string[] coordsStrings = coordString.Split(',');

            X = int.Parse(coordsStrings[0].Trim().Substring(2));
            Y = int.Parse(coordsStrings[1].Trim().Substring(2));
            Z = int.Parse(coordsStrings[2].Trim().Substring(2));

            XVelocity = 0;
            YVelocity = 0;
            ZVelocity = 0;
        }

        public Moon(Moon m)
        {
            this.X = m.X;
            this.Y = m.Y;
            this.Z = m.Z;

            this.XVelocity = m.XVelocity;
            this.YVelocity = m.YVelocity;
            this.ZVelocity = m.ZVelocity;
        }

        public void AdjustVelocityFromGravityOf(Moon m)
        {
            if (m.X > this.X)
            {
                XVelocity++;
            }
            else if (m.X < this.X)
            {
                XVelocity--;
            }

            if (m.Y > this.Y)
            {
                YVelocity++;
            }
            else if (m.Y < this.Y)
            {
                YVelocity--;
            }

            if (m.Z > this.Z)
            {
                ZVelocity++;
            }
            else if (m.Z < this.Z)
            {
                ZVelocity--;
            }
        }

        public void Tick()
        {
            X += XVelocity;
            Y += YVelocity;
            Z += ZVelocity;
        }

        public int GetPotentialEnergy()
        {
            return Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);
        }

        public int GetKineticEnergy()
        {
            return Math.Abs(XVelocity) + Math.Abs(YVelocity) + Math.Abs(ZVelocity);
        }

        public override bool Equals(object obj)
        {
            if (obj is Moon m)
            {
                return this.X == m.X && this.Y == m.Y && this.Z == m.Z && this.XVelocity == m.XVelocity && this.YVelocity == m.YVelocity && this.ZVelocity == m.ZVelocity;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return (this.X + this.Y + this.Z).GetHashCode();
        }

        public object Clone()
        {
            return new Moon(this);
        }
    }
}

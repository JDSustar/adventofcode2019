using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using MoreLinq;

namespace AdventOfCode2019
{
    public static class Day10
    {
        public static void ExecuteStarOne(string fileLocation = "PuzzleInput/Day10.txt")
        {
            List<string> inputStrings = File.ReadAllLines(fileLocation).ToList();

            var ab = new AsteroidBelt(inputStrings);

            Logger.LogMessage(LogLevel.ANSWER, "10A: Best Asteroid " + ab.BestAsteroid + " Detects: " + ab.GetVisibleAsteroidsFromPoint(ab.BestAsteroid).Count);
        }

        public static void ExecuteStarTwo(string fileLocation = "PuzzleInput/Day10.txt")
        {
            List<string> inputStrings = File.ReadAllLines(fileLocation).ToList();

            var ab = new AsteroidBeltWithLaser(inputStrings);

            Logger.LogMessage(LogLevel.DEBUG, ab.ToString());

            for (int i = 0; i < 200; i++)
            {
                Logger.LogMessage(LogLevel.DEBUG, "Vaporizing: " + i);
                ab.VaporizeNext();
                Logger.LogMessage(LogLevel.DEBUG, ab.ToString());
            }

            Logger.LogMessage(LogLevel.ANSWER, "10A: Last Asteroid: " + ab.LastVaporizedAsteroid + " Value: " + ab.LastVaporizedAsteroid.X * 100 + ab.LastVaporizedAsteroid.Y);
        }
    }

    public class AsteroidBelt
    {
        public List<Point> Asteroids = new List<Point>();

        public Point MaxPoint;

        public Point BestAsteroid;

        public AsteroidBelt(List<string> inputStrings)
        {
            for (int h = 0; h < inputStrings.Count; h++)
            {
                for (int w = 0; w < inputStrings[h].Length; w++)
                {
                    if (inputStrings[h][w] == '#')
                    {
                        Asteroids.Add(new Point(w, h));
                    }
                }
            }

            MaxPoint = new Point(inputStrings[0].Length - 1, inputStrings.Count - 1);
            Logger.LogMessage(LogLevel.DEBUG, "MaxPoint: " + MaxPoint);

            FindBestAsteroid();
        }

        public List<Point> GetVisibleAsteroidsFromPoint(Point startPoint)
        {
            List<Point> visibleAsteroids = new List<Point>();

            for (int x = -MaxPoint.X; x < MaxPoint.X; x++)
            {
                for (int y = -MaxPoint.Y; y < MaxPoint.Y; y++)
                {
                    //Logger.LogMessage(LogLevel.INFO, "Using Slope: " + x + " / " + y);

                    if (x == 0 && Math.Abs(y) != 1)
                    {
                        continue;
                    }
                    else if (y == 0 && Math.Abs(x) != 1)
                    {
                        continue;
                    }

                    int gcd = Utilities.GCD(x, y);

                    if (x / gcd != x || y / gcd != y)
                    {
                        continue;
                    }


                    int i = 1;
                    int lookX = startPoint.X + (x * i);
                    int lookY = startPoint.Y + (y * i);

                    while (lookX >= 0 && lookY >= 0 && lookX <= MaxPoint.X && lookY <= MaxPoint.Y)
                    {
                        Point lookPoint = new Point(lookX, lookY);
                        if (Asteroids.Contains(lookPoint))
                        {
                            Logger.LogMessage(LogLevel.INFO, startPoint + " can see " + lookPoint);

                            visibleAsteroids.Add(lookPoint);
                            break;
                        }
                        else
                        {
                            i++;
                            lookX = startPoint.X + (x * i);
                            lookY = startPoint.Y + (y * i);
                        }
                    };
                }
            }

            return visibleAsteroids;
        }

        private void FindBestAsteroid()
        {
            int maxViewable = 0;

            foreach (var asteroid in Asteroids)
            {
                int viewable = GetVisibleAsteroidsFromPoint(asteroid).Count;
                if (viewable > maxViewable)
                {
                    maxViewable = viewable;
                    BestAsteroid = asteroid;
                }
            }
        }
    }

    public class AsteroidBeltWithLaser : AsteroidBelt
    {
        public Point LaserPosition => this.BestAsteroid;

        public Point LastVaporizedAsteroid { get; private set; }

        public double CurrentLaserAngle;

        public Dictionary<Point, double> AsteroidAngles = new Dictionary<Point, double>();

        public AsteroidBeltWithLaser(List<string> inputStrings) : base(inputStrings)
        {
            LastVaporizedAsteroid = null;
            CurrentLaserAngle = 0;

            foreach (var asteroid in Asteroids)
            {
                Point diff = new Point(asteroid.X - LaserPosition.X, LaserPosition.Y - asteroid.Y);

                AsteroidAngles.Add(asteroid, Utilities.GetAngleInRadians(diff.X, diff.Y));
            }
        }

        public void VaporizeNext()
        {
            if (!AsteroidAngles.Any(kvp => kvp.Value >= CurrentLaserAngle))
            {
                CurrentLaserAngle = 0;
            }

            var next = AsteroidAngles.Where(kvp => kvp.Value >= CurrentLaserAngle).MinBy(kvp => kvp.Value - CurrentLaserAngle).Select(x => x.Key);

            var visibleNext = next.Where(x => GetVisibleAsteroidsFromPoint(LaserPosition).Contains(x));

            if (visibleNext.Count() > 1)
            {
                throw new Exception("MORE THAN ONE NEXT.");
            }

            VaporizeAt(visibleNext.First());
        }

        private bool VaporizeUp()
        {
            var LookPoint = new Point(LaserPosition.X, LaserPosition.Y - 1);

            while (LastVaporizedAsteroid == null && LookPoint.Y >= 0)
            {
                if (Asteroids.Contains(LookPoint))
                {
                    VaporizeAt(LookPoint);
                    return true;
                }
                else
                {
                    LookPoint = new Point(LookPoint.X, LookPoint.Y - 1);
                }
            }

            return false;
        }

        private void VaporizeAt(Point asteroidToVaporize)
        {
            Logger.LogMessage(LogLevel.DEBUG, "Vaporizing: " + asteroidToVaporize);
            CurrentLaserAngle = AsteroidAngles[asteroidToVaporize]+.0001;
            Asteroids.Remove(asteroidToVaporize);
            AsteroidAngles.Remove(asteroidToVaporize);
            LastVaporizedAsteroid = asteroidToVaporize;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int h = 0; h < MaxPoint.Y; h++)
            {
                for (int w = 0; w < MaxPoint.X; w++)
                {
                    var p = new Point(w, h);
                    if (Asteroids.Contains(p))
                    {
                        if (Equals(p, LaserPosition))
                        {
                            sb.Append("0");
                        }
                        else
                        {
                            sb.Append("X");
                        }
                    }
                    else if (Equals(p, LastVaporizedAsteroid))
                    {
                        sb.Append("!");
                    }
                    else
                    {
                        sb.Append(".");
                    }
                }

                sb.Append("\n");
            }

            sb.Append("\n");
            return sb.ToString();
        }
    }
}

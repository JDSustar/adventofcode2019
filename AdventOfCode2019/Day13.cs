using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace AdventOfCode2019
{
    public class Day13
    {
        public static void ExecuteStarOne(string fileLocation = "PuzzleInput/Day13.txt")
        {
            long[] program = File.ReadAllText(fileLocation).Split(',').Select(long.Parse).ToArray();
            long[] input = new List<long>() { 1 }.ToArray();

            ArcadeCabinet ac = new ArcadeCabinet(program, input);

            ac.Run();

            Logger.LogMessage(LogLevel.ANSWER, "13A: Number of Block Tiles: " + ac.Tiles.Count(t => t.TileId == 2));
        }

        public static void ExecuteStarTwo(string fileLocation = "PuzzleInput/Day13.txt")
        {
            long[] program = File.ReadAllText(fileLocation).Split(',').Select(long.Parse).ToArray();
            long[] input = new List<long>() {  }.ToArray();

            program[0] = 2;

            ArcadeCabinet ac = new ArcadeCabinet(program, input);

            ac.Run();
        }

        public class ArcadeCabinet
        {
            public class Tile
            {
                public long X { get; private set; }
                public long Y { get; private set; }
                public long TileId { get; set; }

                public Tile(long x, long y, long tileId)
                {
                    X = x;
                    Y = y;
                    TileId = tileId;
                }
            }

            private IntCodeMachine icm;

            public long CurrentScore { get; private set; }

            public List<Tile> Tiles = new List<Tile>();

            public ArcadeCabinet(long[] program, long[] input)
            {
                icm = new IntCodeMachine(program, input);
            }

            public void Run()
            {
                icm.Run();

                while (icm.IsRunning)
                {
                    while(icm.Output.Count > 0)
                    {
                        long x = icm.GetNextOutput();
                        long y = icm.GetNextOutput();
                        long id = icm.GetNextOutput();

                        if (x == -1 && y == 0)
                        {
                            CurrentScore = id;
                        }
                        else
                        {
                            var t = Tiles.FirstOrDefault(t => t.X == x && t.Y == y);

                            if (t == null)
                            {
                                Tiles.Add(new Tile(x, y, id));
                            }
                            else
                            {
                                t.TileId = id;
                            }
                        }
                    }

                    PrintTiles();


                    if (icm.IsRunning)
                    {

                        Logger.LogMessage(LogLevel.ANSWER, "L/R/N: ");
                        //string i = Console.ReadLine();

                        string i;

                        if (Logger.CURRENT_LOG_LEVEL == LogLevel.DEBUG)
                        {
                            i = Console.ReadLine();
                        }
                        else
                        {
                            var ball = Tiles.First(t => t.TileId == 4);
                            var paddle = Tiles.First(t => t.TileId == 3);

                            if (ball.X > paddle.X)
                            {
                                i = "R";
                            }
                            else if (ball.X < paddle.X)
                            {
                                i = "L";
                            }
                            else
                            {
                                i = "N";
                            }
                        }

                        if (i == "L")
                        {
                            icm.EnterInput(-1);
                        }
                        else if (i == "R")
                        {
                            icm.EnterInput(1);
                        }
                        else
                        {
                            icm.EnterInput(0);
                        }
                    }


                }

                while (icm.Output.Count > 0)
                {
                    long x = icm.GetNextOutput();
                    long y = icm.GetNextOutput();
                    long id = icm.GetNextOutput();

                    if (x == -1 && y == 0)
                    {
                        CurrentScore = id;
                    }
                    else
                    {
                        var t = Tiles.FirstOrDefault(t => t.X == x && t.Y == y);

                        if (t == null)
                        {
                            Tiles.Add(new Tile(x, y, id));
                        }
                        else
                        {
                            t.TileId = id;
                        }
                    }
                }

                PrintTiles();
            }

            public void PrintTiles()
            {
                for (int y = 0; y <= 21; y++)
                {
                    string s = "";
                    for (int x = 0; x <= 37; x++)
                    {
                        long tid = Tiles.FirstOrDefault(t => t.X == x && t.Y == y).TileId;

                        switch (tid)
                        {
                            case 0:
                                s += " ";
                                break;
                            case 1:
                                s += "|";
                                break;
                            case 2:
                                s += "=";
                                break;
                            case 3:
                                s += "-";
                                break;
                            case 4:
                                s += "O";
                                break;
                        }
                    }

                    Logger.LogMessage(LogLevel.ANSWER, s);
                }

                Logger.LogMessage(LogLevel.ANSWER, "CURRENT SCORE: " + CurrentScore.ToString());
            }
        }
    }
}

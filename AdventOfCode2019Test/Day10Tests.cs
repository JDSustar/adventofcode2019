using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode2019;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2019Test
{
    [TestClass]
    public class Day10Tests
    {
        [TestMethod]
        public void Test1()
        {
            string inputString =
                @"......#.#.
#..#.#....
..#######.
.#.#.###..
.#..#.....
..#....#.#
#..#....#.
.##.#..###
##...#..#.
.#....####";

            List<string> inputStrings = inputString.Split('\n').ToList();

            AsteroidBelt ab = new AsteroidBelt(inputStrings);

            Assert.AreEqual(40, ab.Asteroids.Count);
        }

        [TestMethod]
        public void Test2()
        {
            string inputString =
                @".#..#
.....
#####
....#
...##";

            List<string> inputStrings = inputString.Split('\n').ToList();

            AsteroidBelt ab = new AsteroidBelt(inputStrings);

            Assert.AreEqual(8, ab.GetVisibleAsteroidsFromPoint(new Point(3, 4)).Count);
        }

        [TestMethod]
        public void Test3()
        {
            string inputString =
                @".#..#
.....
#####
....#
...##";

            List<string> inputStrings = inputString.Split('\n').ToList();

            AsteroidBelt ab = new AsteroidBelt(inputStrings);

            Assert.AreEqual(7, ab.GetVisibleAsteroidsFromPoint(new Point(4,3)).Count);
        }

        [TestMethod]
        public void Test4()
        {
            string inputString =
                @"......#.#.
#..#.#....
..#######.
.#.#.###..
.#..#.....
..#....#.#
#..#....#.
.##.#..###
##...#..#.
.#....####";

            List<string> inputStrings = inputString.Split('\n').ToList();

            AsteroidBelt ab = new AsteroidBelt(inputStrings);

            Assert.AreEqual(33, ab.GetVisibleAsteroidsFromPoint(new Point(5, 8)).Count);
        }


        [TestMethod]
        public void Test5()
        {
            string inputString =
                @"#.#...#.#.
.###....#.
.#....#...
##.#.#.#.#
....#.#.#.
.##..###.#
..#...##..
..##....##
......#...
.####.###.";

            List<string> inputStrings = inputString.Split('\n').ToList();

            AsteroidBelt ab = new AsteroidBelt(inputStrings);

            Assert.AreEqual(35, ab.GetVisibleAsteroidsFromPoint(new Point(1,2)).Count);
        }

        [TestMethod]
        public void Test6()
        {
            string inputString =
                @"......#.#.
#..#.#....
..#######.
.#.#.###..
.#..#.....
..#....#.#
#..#....#.
.##.#..###
##...#..#.
.#....####";

            List<string> inputStrings = inputString.Split('\n').ToList();

            AsteroidBelt ab = new AsteroidBelt(inputStrings);

            Assert.AreEqual(33, ab.GetVisibleAsteroidsFromPoint(new Point(5, 8)).Count);
        }


        [TestMethod]
        public void Test7()
        {
            string inputString =
                @".#..#..###
####.###.#
....###.#.
..###.##.#
##.##.#.#.
....###..#
..#.#..#.#
#..#.#.###
.##...##.#
.....#.#..";

            List<string> inputStrings = inputString.Split('\n').ToList();

            AsteroidBelt ab = new AsteroidBelt(inputStrings);

            Assert.AreEqual(41, ab.GetVisibleAsteroidsFromPoint(new Point(6,3)).Count);
        }

        [TestMethod]
        public void Test8()
        {
            string inputString =
                @".#..##.###...#######
##.############..##.
.#.######.########.#
.###.#######.####.#.
#####.##.#.##.###.##
..#####..#.#########
####################
#.####....###.#.#.##
##.#################
#####.##.###..####..
..######..##.#######
####.##.####...##..#
.#####..#.######.###
##...#.##########...
#.##########.#######
.####.#.###.###.#.##
....##.##.###..#####
.#.#.###########.###
#.#.#.#####.####.###
###.##.####.##.#..##";

            List<string> inputStrings = inputString.Split('\n').ToList();

            AsteroidBelt ab = new AsteroidBelt(inputStrings);

            Assert.AreEqual(210, ab.GetVisibleAsteroidsFromPoint(new Point(11,13)).Count);
        }

        [TestMethod]
        public void Test9()
        {
            string inputString = @".#....#####...#..
##...##.#####..##
##...#...#.#####.
..#.....X...###..
..#.#.....#....##";

            List<string> inputStrings = inputString.Split('\n').ToList();

            AsteroidBeltWithLaser ab = new AsteroidBeltWithLaser(inputStrings);

            Assert.IsTrue(ab.Asteroids.Contains(new Point(8,1)));
            Assert.IsTrue(ab.Asteroids.Contains(new Point(5, 1)));


            ab.VaporizeNext();

            Assert.IsFalse(ab.Asteroids.Contains(new Point(5, 1)));
        }

        [TestMethod]
        public void Test10()
        {
            string inputString =
                @".#..##.###...#######
##.############..##.
.#.######.########.#
.###.#######.####.#.
#####.##.#.##.###.##
..#####..#.#########
####################
#.####....###.#.#.##
##.#################
#####.##.###..####..
..######..##.#######
####.##.####...##..#
.#####..#.######.###
##...#.##########...
#.##########.#######
.####.#.###.###.#.##
....##.##.###..#####
.#.#.###########.###
#.#.#.#####.####.###
###.##.####.##.#..##";

            List<string> inputStrings = inputString.Split('\n').ToList();

            AsteroidBeltWithLaser ab = new AsteroidBeltWithLaser(inputStrings);

            ab.VaporizeNext();

            Assert.AreEqual(new Point(11,12), ab.LastVaporizedAsteroid);
        }

        [TestMethod]
        public void Test11()
        {
            string inputString =
                @".#..##.###...#######
##.############..##.
.#.######.########.#
.###.#######.####.#.
#####.##.#.##.###.##
..#####..#.#########
####################
#.####....###.#.#.##
##.#################
#####.##.###..####..
..######..##.#######
####.##.####...##..#
.#####..#.######.###
##...#.##########...
#.##########.#######
.####.#.###.###.#.##
....##.##.###..#####
.#.#.###########.###
#.#.#.#####.####.###
###.##.####.##.#..##";

            List<string> inputStrings = inputString.Split('\n').ToList();

            AsteroidBeltWithLaser ab = new AsteroidBeltWithLaser(inputStrings);

            ab.VaporizeNext();
            ab.VaporizeNext();


            Assert.AreEqual(new Point(12,1), ab.LastVaporizedAsteroid);
        }

        [TestMethod]
        public void Test12()
        {
            string inputString =
                @".#..##.###...#######
##.############..##.
.#.######.########.#
.###.#######.####.#.
#####.##.#.##.###.##
..#####..#.#########
####################
#.####....###.#.#.##
##.#################
#####.##.###..####..
..######..##.#######
####.##.####...##..#
.#####..#.######.###
##...#.##########...
#.##########.#######
.####.#.###.###.#.##
....##.##.###..#####
.#.#.###########.###
#.#.#.#####.####.###
###.##.####.##.#..##";

            List<string> inputStrings = inputString.Split('\n').ToList();

            AsteroidBeltWithLaser ab = new AsteroidBeltWithLaser(inputStrings);

            ab.VaporizeNext();
            ab.VaporizeNext();
            ab.VaporizeNext();



            Assert.AreEqual(new Point(12,2), ab.LastVaporizedAsteroid);
        }

        [TestMethod]
        public void Test13()
        {
            string inputString =
                @".#..##.###...#######
##.############..##.
.#.######.########.#
.###.#######.####.#.
#####.##.#.##.###.##
..#####..#.#########
####################
#.####....###.#.#.##
##.#################
#####.##.###..####..
..######..##.#######
####.##.####...##..#
.#####..#.######.###
##...#.##########...
#.##########.#######
.####.#.###.###.#.##
....##.##.###..#####
.#.#.###########.###
#.#.#.#####.####.###
###.##.####.##.#..##";

            List<string> inputStrings = inputString.Split('\n').ToList();

            AsteroidBeltWithLaser ab = new AsteroidBeltWithLaser(inputStrings);

            for (int i = 0; i < 10; i++)
            {
                ab.VaporizeNext();
            }

            Assert.AreEqual(new Point(12,8), ab.LastVaporizedAsteroid);
        }

        [TestMethod]
        public void Test14()
        {
            string inputString =
                @".#..##.###...#######
##.############..##.
.#.######.########.#
.###.#######.####.#.
#####.##.#.##.###.##
..#####..#.#########
####################
#.####....###.#.#.##
##.#################
#####.##.###..####..
..######..##.#######
####.##.####...##..#
.#####..#.######.###
##...#.##########...
#.##########.#######
.####.#.###.###.#.##
....##.##.###..#####
.#.#.###########.###
#.#.#.#####.####.###
###.##.####.##.#..##";

            List<string> inputStrings = inputString.Split('\n').ToList();

            AsteroidBeltWithLaser ab = new AsteroidBeltWithLaser(inputStrings);

            for (int i = 0; i < 20; i++)
            {
                ab.VaporizeNext();
            }

            Assert.AreEqual(new Point(16,0), ab.LastVaporizedAsteroid);
        }

        [TestMethod]
        public void Test15()
        {
            string inputString =
                @".#..##.###...#######
##.############..##.
.#.######.########.#
.###.#######.####.#.
#####.##.#.##.###.##
..#####..#.#########
####################
#.####....###.#.#.##
##.#################
#####.##.###..####..
..######..##.#######
####.##.####...##..#
.#####..#.######.###
##...#.##########...
#.##########.#######
.####.#.###.###.#.##
....##.##.###..#####
.#.#.###########.###
#.#.#.#####.####.###
###.##.####.##.#..##";

            List<string> inputStrings = inputString.Split('\n').ToList();

            AsteroidBeltWithLaser ab = new AsteroidBeltWithLaser(inputStrings);

            for (int i = 0; i < 50; i++)
            {
                ab.VaporizeNext();
            }

            Assert.AreEqual(new Point(16,9), ab.LastVaporizedAsteroid);
        }

        [TestMethod]
        public void Test16()
        {
            string inputString =
                @".#..##.###...#######
##.############..##.
.#.######.########.#
.###.#######.####.#.
#####.##.#.##.###.##
..#####..#.#########
####################
#.####....###.#.#.##
##.#################
#####.##.###..####..
..######..##.#######
####.##.####...##..#
.#####..#.######.###
##...#.##########...
#.##########.#######
.####.#.###.###.#.##
....##.##.###..#####
.#.#.###########.###
#.#.#.#####.####.###
###.##.####.##.#..##";

            List<string> inputStrings = inputString.Split('\n').ToList();

            AsteroidBeltWithLaser ab = new AsteroidBeltWithLaser(inputStrings);

            for (int i = 0; i < 100; i++)
            {
                ab.VaporizeNext();
            }

            Assert.AreEqual(new Point(10,16), ab.LastVaporizedAsteroid);
        }

        [TestMethod]
        public void Test17()
        {
            string inputString =
                @".#..##.###...#######
##.############..##.
.#.######.########.#
.###.#######.####.#.
#####.##.#.##.###.##
..#####..#.#########
####################
#.####....###.#.#.##
##.#################
#####.##.###..####..
..######..##.#######
####.##.####...##..#
.#####..#.######.###
##...#.##########...
#.##########.#######
.####.#.###.###.#.##
....##.##.###..#####
.#.#.###########.###
#.#.#.#####.####.###
###.##.####.##.#..##";

            List<string> inputStrings = inputString.Split('\n').ToList();

            AsteroidBeltWithLaser ab = new AsteroidBeltWithLaser(inputStrings);

            for (int i = 0; i < 200; i++)
            {
                ab.VaporizeNext();
            }

            Assert.AreEqual(new Point(8,2), ab.LastVaporizedAsteroid);
        }

        [TestMethod]
        public void Test18()
        {
            string inputString =
                @".#..##.###...#######
##.############..##.
.#.######.########.#
.###.#######.####.#.
#####.##.#.##.###.##
..#####..#.#########
####################
#.####....###.#.#.##
##.#################
#####.##.###..####..
..######..##.#######
####.##.####...##..#
.#####..#.######.###
##...#.##########...
#.##########.#######
.####.#.###.###.#.##
....##.##.###..#####
.#.#.###########.###
#.#.#.#####.####.###
###.##.####.##.#..##";

            List<string> inputStrings = inputString.Split('\n').ToList();

            AsteroidBeltWithLaser ab = new AsteroidBeltWithLaser(inputStrings);

            for (int i = 0; i < 201; i++)
            {
                ab.VaporizeNext();
            }

            Assert.AreEqual(new Point(10,9), ab.LastVaporizedAsteroid);
        }
    }
}

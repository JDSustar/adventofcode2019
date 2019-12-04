using System;
using System.Collections.Generic;
using System.Text;
using AdventOfCode2019;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2019Test
{
    [TestClass]
    public class Day3Tests
    {
        [TestMethod]
        public void Day3BuildWireExample1()
        {
            List<Tuple<int, int>> expectedPoints = new List<Tuple<int, int>>()
            {
                new Tuple<int, int>(1, 0),
                new Tuple<int, int>(2,0),
                new Tuple<int, int>(3,0),
                new Tuple<int, int>(4,0),
                new Tuple<int, int>(5,0),
                new Tuple<int, int>(6,0),
                new Tuple<int, int>(7,0),
                new Tuple<int, int>(8,0),
                new Tuple<int, int>(8,1),
                new Tuple<int, int>(8,2),
                new Tuple<int, int>(8,3),
                new Tuple<int, int>(8,4),
                new Tuple<int, int>(8,5),
                new Tuple<int, int>(7,5),
                new Tuple<int, int>(6,5),
                new Tuple<int, int>(5,5),
                new Tuple<int, int>(4,5),
                new Tuple<int, int>(3,5),
                new Tuple<int, int>(3,4),
                new Tuple<int, int>(3,3),
                new Tuple<int, int>(3,2)
            };

            Wire testWire = new Wire("R8,U5,L5,D3");

            CollectionAssert.AreEqual(expectedPoints, testWire.WirePoints);
        }

        [TestMethod]
        public void Day3BuildWireExample2()
        {
            List<Tuple<int, int>> expectedPoints = new List<Tuple<int, int>>()
            {
                new Tuple<int, int>(0,1),
                new Tuple<int, int>(0,2),
                new Tuple<int, int>(0,3),
                new Tuple<int, int>(0,4),
                new Tuple<int, int>(0,5),
                new Tuple<int, int>(0,6),
                new Tuple<int, int>(0,7),
                new Tuple<int, int>(1,7),
                new Tuple<int, int>(2,7),
                new Tuple<int, int>(3,7),
                new Tuple<int, int>(4,7),
                new Tuple<int, int>(5,7),
                new Tuple<int, int>(6,7),
                new Tuple<int, int>(6,6),
                new Tuple<int, int>(6,5),
                new Tuple<int, int>(6,4),
                new Tuple<int, int>(6,3),
                new Tuple<int, int>(5,3),
                new Tuple<int, int>(4,3),
                new Tuple<int, int>(3,3),
                new Tuple<int, int>(2,3)
            };

            Wire testWire = new Wire("U7,R6,D4,L4");

            CollectionAssert.AreEqual(expectedPoints, testWire.WirePoints);
        }

        [TestMethod]
        public void Day3IntersectionExample1()
        {
            string wire1 = "R8,U5,L5,D3";
            string wire2 = "U7,R6,D4,L4";

            WirePanel wp = new WirePanel(wire1, wire2);

            List<Tuple<int, int>> expectedIntersections = new List<Tuple<int, int>>()
            {
                new Tuple<int, int>(3, 3),
                new Tuple<int, int>(6, 5)
            };

            foreach (var expectedIntersection in expectedIntersections)
            {
                CollectionAssert.Contains(wp.WireIntersections, expectedIntersection);
            }
        }

        [TestMethod]
        public void Day3DistanceExample1()
        {
            string wire1 = "R8,U5,L5,D3";
            string wire2 = "U7,R6,D4,L4";

            WirePanel wp = new WirePanel(wire1, wire2);

            Assert.AreEqual(6, wp.GetClosestIntersectionDistance());
        }

        [TestMethod]
        public void Day3DistanceExample2()
        {
            string wire1 = "R75,D30,R83,U83,L12,D49,R71,U7,L72";
            string wire2 = "U62,R66,U55,R34,D71,R55,D58,R83";

            WirePanel wp = new WirePanel(wire1, wire2);

            Assert.AreEqual(159, wp.GetClosestIntersectionDistance());
        }

        [TestMethod]
        public void Day3DistanceExample3()
        {
            string wire1 = "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51";
            string wire2 = "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7";

            WirePanel wp = new WirePanel(wire1, wire2);

            Assert.AreEqual(135, wp.GetClosestIntersectionDistance());
        }

        [TestMethod]
        public void Day3FewestStepsExample1()
        {
            string wire1 = "R75,D30,R83,U83,L12,D49,R71,U7,L72";
            string wire2 = "U62,R66,U55,R34,D71,R55,D58,R83";

            WirePanel wp = new WirePanel(wire1, wire2);

            Assert.AreEqual(610, wp.GetFewestStepsIntersection());
        }

        [TestMethod]
        public void Day3FewestStepsExample2()
        {
            string wire1 = "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51";
            string wire2 = "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7";

            WirePanel wp = new WirePanel(wire1, wire2);

            Assert.AreEqual(410, wp.GetFewestStepsIntersection());
        }
    }
}

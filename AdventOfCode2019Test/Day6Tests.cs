using System;
using System.Collections.Generic;
using System.Text;
using AdventOfCode2019;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2019Test
{
    [TestClass]
    public class Day6Tests
    {
        [TestMethod]
        public void Test1()
        {
            string input = "COM)B,B)C,C)D,D)E,E)F,B)G,G)H,D)I,E)J,J)K,K)L";
            string[] inputs = input.Split(',');

            UniversalOrbitMap orbitMap = new UniversalOrbitMap(inputs);

            Assert.AreEqual(3, orbitMap.ObjectsInSpace["D"].GetOrbits());
            Assert.AreEqual(7, orbitMap.ObjectsInSpace["L"].GetOrbits());
            Assert.AreEqual(0, orbitMap.ObjectsInSpace["COM"].GetOrbits());

            Assert.AreEqual(42, orbitMap.GetTotalOrbits());
        }

        [TestMethod]
        public void Test2()
        {
            string input = "COM)B,B)C,C)D,D)E,E)F,B)G,G)H,D)I,E)J,J)K,K)L,K)YOU,I)SAN";
            string[] inputs = input.Split(',');

            UniversalOrbitMap orbitMap = new UniversalOrbitMap(inputs);

            Assert.AreEqual(4, orbitMap.OrbitTransfersRequired("YOU", "SAN"));
        }
    }
}
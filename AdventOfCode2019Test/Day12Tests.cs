using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode2019;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2019Test
{
    [TestClass]
    public class Day12Tests
    {
        [TestMethod]
        public void Test1()
        {
            string input = @"<x=-1, y=0, z=2>
<x=2, y=-10, z=-7>
<x=4, y=-8, z=8>
<x=3, y=5, z=-1>";

            List<string> moonStringList = input.Split("\r\n").ToList();

            var jm = new JupiterMoons(moonStringList);

            Assert.AreEqual(4, jm.Moons.Count);

            Assert.AreEqual(-1, jm.Moons[0].X);
            Assert.AreEqual(0, jm.Moons[0].Y);
            Assert.AreEqual(2, jm.Moons[0].Z);

            Assert.AreEqual(2, jm.Moons[1].X);
            Assert.AreEqual(-10, jm.Moons[1].Y);
            Assert.AreEqual(-7, jm.Moons[1].Z);

            Assert.AreEqual(4, jm.Moons[2].X);
            Assert.AreEqual(-8, jm.Moons[2].Y);
            Assert.AreEqual(8, jm.Moons[2].Z);

            Assert.AreEqual(3, jm.Moons[3].X);
            Assert.AreEqual(5, jm.Moons[3].Y);
            Assert.AreEqual(-1, jm.Moons[3].Z);
        }

        [TestMethod]
        public void Test2()
        {
            string input = @"<x=-1, y=0, z=2>
<x=2, y=-10, z=-7>
<x=4, y=-8, z=8>
<x=3, y=5, z=-1>";

            List<string> moonStringList = input.Split("\r\n").ToList();

            var jm = new JupiterMoons(moonStringList);

            for (int i = 0; i < 10; i++)
            {
                jm.Tick();
            }

            Assert.AreEqual(2, jm.Moons[0].X);
            Assert.AreEqual(1, jm.Moons[0].Y);
            Assert.AreEqual(-3, jm.Moons[0].Z);

            Assert.AreEqual(1, jm.Moons[1].X);
            Assert.AreEqual(-8, jm.Moons[1].Y);
            Assert.AreEqual(0, jm.Moons[1].Z);

            Assert.AreEqual(3, jm.Moons[2].X);
            Assert.AreEqual(-6, jm.Moons[2].Y);
            Assert.AreEqual(1, jm.Moons[2].Z);

            Assert.AreEqual(2, jm.Moons[3].X);
            Assert.AreEqual(0, jm.Moons[3].Y);
            Assert.AreEqual(4, jm.Moons[3].Z);

            Assert.AreEqual(-3, jm.Moons[0].XVelocity);
            Assert.AreEqual(-2, jm.Moons[0].YVelocity);
            Assert.AreEqual(1, jm.Moons[0].ZVelocity);

            Assert.AreEqual(-1, jm.Moons[1].XVelocity);
            Assert.AreEqual(1, jm.Moons[1].YVelocity);
            Assert.AreEqual(3, jm.Moons[1].ZVelocity);

            Assert.AreEqual(3, jm.Moons[2].XVelocity);
            Assert.AreEqual(2, jm.Moons[2].YVelocity);
            Assert.AreEqual(-3, jm.Moons[2].ZVelocity);

            Assert.AreEqual(1, jm.Moons[3].XVelocity);
            Assert.AreEqual(-1, jm.Moons[3].YVelocity);
            Assert.AreEqual(-1, jm.Moons[3].ZVelocity);
        }

        [TestMethod]
        public void Test3()
        {
            string input = @"<x=-1, y=0, z=2>
<x=2, y=-10, z=-7>
<x=4, y=-8, z=8>
<x=3, y=5, z=-1>";

            List<string> moonStringList = input.Split("\r\n").ToList();

            var jm = new JupiterMoons(moonStringList);

            for (int i = 0; i < 10; i++)
            {
                jm.Tick();
            }

            Assert.AreEqual(6, jm.Moons[0].GetPotentialEnergy());
            Assert.AreEqual(9, jm.Moons[1].GetPotentialEnergy());
            Assert.AreEqual(10, jm.Moons[2].GetPotentialEnergy());
            Assert.AreEqual(6, jm.Moons[3].GetPotentialEnergy());

            Assert.AreEqual(6, jm.Moons[0].GetKineticEnergy());
            Assert.AreEqual(5, jm.Moons[1].GetKineticEnergy());
            Assert.AreEqual(8, jm.Moons[2].GetKineticEnergy());
            Assert.AreEqual(3, jm.Moons[3].GetKineticEnergy());


        }

        [TestMethod]
        public void Test4()
        {
            string input = @"<x=-1, y=0, z=2>
<x=2, y=-10, z=-7>
<x=4, y=-8, z=8>
<x=3, y=5, z=-1>";

            List<string> moonStringList = input.Split("\r\n").ToList();

            var jm = new JupiterMoons(moonStringList);

            for (int i = 0; i < 10; i++)
            {
                jm.Tick();
            }

            Assert.AreEqual(179, jm.GetSystemEnergy());
        }

        [TestMethod]
        public void Test5()
        {
            string input = @"<x=-1, y=0, z=2>
<x=2, y=-10, z=-7>
<x=4, y=-8, z=8>
<x=3, y=5, z=-1>";

            List<string> moonStringList = input.Split("\r\n").ToList();

            var jm = new JupiterMoons(moonStringList);

            var jm2 = (jm.Clone() as JupiterMoons);

            bool m = jm.Equals(jm2);

            Assert.AreEqual(jm, jm2);
        }

        [TestMethod]
        public void Test10()
        {
            string input = @"<x=-1, y=0, z=2>
<x=2, y=-10, z=-7>
<x=4, y=-8, z=8>
<x=3, y=5, z=-1>";

            List<string> moonStringList = input.Split("\r\n").ToList();

            var jm = new JupiterMoons(moonStringList);

            var states = new HashSet<JupiterMoons>();

            long steps = 0;

            bool repeatFound = false;

            JupiterMoons jm2 = null;

            while (!repeatFound)
            {
                jm.Tick();

                if (states.TryGetValue(jm, out jm2))
                {
                    repeatFound = true;
                }
                else
                {
                    states.Add((jm.Clone() as JupiterMoons));
                    steps++;
                }
            }

            Assert.AreEqual(jm, jm2);
            Assert.AreEqual(2772, steps);
        }
    }
}
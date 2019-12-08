using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode2019;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2019Test
{
    [TestClass]
    public class Day7Tests
    {
        [TestMethod]
        public void Test1()
        {
            var programMemory = "3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0".Split(',').Select(int.Parse).ToArray();
            var phases = "4,3,2,1,0".Split(',').Select(int.Parse).ToArray();

            ThrusterAmplifiers ta = new ThrusterAmplifiers(programMemory, phases);
            ta.Run();

            Assert.AreEqual(43210, ta.Output);
        }

        [TestMethod]
        public void Test2()
        {
            var programMemory = "3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0".Split(',').Select(int.Parse).ToArray();
            var phases = "0,1,2,3,4".Split(',').Select(int.Parse).ToArray();

            ThrusterAmplifiers ta = new ThrusterAmplifiers(programMemory, phases);
            ta.Run();

            Assert.AreEqual(54321, ta.Output);
        }

        [TestMethod]
        public void Test3()
        {
            var programMemory = "3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0".Split(',').Select(int.Parse).ToArray();
            var phases = "1,0,4,3,2".Split(',').Select(int.Parse).ToArray();

            ThrusterAmplifiers ta = new ThrusterAmplifiers(programMemory, phases);
            ta.Run();

            Assert.AreEqual(65210, ta.Output);
        }

        [TestMethod]
        public void Test4()
        {
            var programMemory = "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5".Split(',').Select(int.Parse).ToArray();
            var phases = "9,8,7,6,5".Split(',').Select(int.Parse).ToArray();

            ThrusterAmplifiers ta = new ThrusterAmplifiers(programMemory, phases);
            ta.Run();

            Assert.AreEqual(139629729, ta.Output);
        }

        [TestMethod]
        public void Test5()
        {
            var programMemory = "3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10".Split(',').Select(int.Parse).ToArray();
            var phases = "9,7,8,5,6".Split(',').Select(int.Parse).ToArray();

            ThrusterAmplifiers ta = new ThrusterAmplifiers(programMemory, phases);
            ta.Run();

            Assert.AreEqual(18216, ta.Output);
        }
    }
}

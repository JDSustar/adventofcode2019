using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode2019;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2019Test
{
    [TestClass]
    public class Day9Tests
    {
        [TestMethod]
        public void Test1()
        {
            long[] program = "109,2000,109,19,99".Split(',').Select(long.Parse).ToArray();
            IntCodeMachine icm = new IntCodeMachine(program);
            icm.Run();

            Assert.AreEqual(2019, icm.CurrentRelativeBase);
        }

        [TestMethod]
        public void Test2()
        {
            long[] program = "109,6,109,-4,204,-2,99".Split(',').Select(long.Parse).ToArray();
            IntCodeMachine icm = new IntCodeMachine(program);
            icm.Run();

            Assert.AreEqual("109", icm.Output);
        }

        [TestMethod]
        public void Test3()
        {
            long[] program = "109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99".Split(',').Select(long.Parse).ToArray();
            IntCodeMachine icm = new IntCodeMachine(program);
            icm.Run();

            Assert.AreEqual("1091204-1100110011001008100161011006101099", icm.Output);
        }

        [TestMethod]
        public void Test4()
        {
            long[] program = "1102,34915192,34915192,7,4,7,99,0".Split(',').Select(long.Parse).ToArray();
            IntCodeMachine icm = new IntCodeMachine(program);
            icm.Run();

            Assert.AreEqual(16, icm.Output.Length);
        }

        [TestMethod]
        public void Test5()
        {
            long[] program = "104,1125899906842624,99".Split(',').Select(long.Parse).ToArray();
            IntCodeMachine icm = new IntCodeMachine(program);
            icm.Run();

            Assert.AreEqual("1125899906842624", icm.Output);
        }
    }
}

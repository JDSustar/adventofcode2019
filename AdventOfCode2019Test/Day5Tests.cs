using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode2019;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2019Test
{
    [TestClass]
    public class Day5Tests
    {
        [TestMethod]
        public void Day5BExample1()
        {
            string memory =
                "3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99";

            var input = new long[1] { 1 };

            IntCodeMachine machine = new IntCodeMachine(Array.ConvertAll(memory.Split(','), long.Parse), input);
            machine.Run();

            Assert.AreEqual(999, machine.GetNextOutput());
        }

        [TestMethod]
        public void Day5BExample2()
        {
            string memory =
                "3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99";

            var input = new long[1] { 8 };

            IntCodeMachine machine = new IntCodeMachine(Array.ConvertAll(memory.Split(','), long.Parse), input);
            machine.Run();

            Assert.AreEqual(1000, machine.GetNextOutput());
        }

        [TestMethod]
        public void Day5BExample3()
        {
            string memory =
                "3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99";

            var input = new long[1] { 9 };

            IntCodeMachine machine = new IntCodeMachine(Array.ConvertAll(memory.Split(','), long.Parse), input);
            machine.Run();

            Assert.AreEqual(1001, machine.GetNextOutput());
        }

        [TestMethod]
        public void Day5BExample4()
        {
            string memory = "3,9,8,9,10,9,4,9,99,-1,8";

            var input = new long[1] { 8 };

            IntCodeMachine machine = new IntCodeMachine(Array.ConvertAll(memory.Split(','), long.Parse), input);
            machine.Run();

            Assert.AreEqual(1, machine.GetNextOutput());
        }
    }
}

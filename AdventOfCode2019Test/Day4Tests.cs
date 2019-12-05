using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using AdventOfCode2019;

namespace AdventOfCode2019Test
{
    [TestClass]
    public class Day4Tests
    {
        [TestMethod]
        public void Day4AValidPasswordExample1()
        {
            Assert.IsTrue(Day4.IsValidPassword("111111"));
            Assert.IsTrue(Day4.IsValidPassword(111111));
        }

        [TestMethod]
        public void Day4AValidPasswordExample2()
        {
            Assert.IsFalse(Day4.IsValidPassword("223450"));
            Assert.IsFalse(Day4.IsValidPassword(223450));
        }

        [TestMethod]
        public void Day4AValidPasswordExample3()
        {
            Assert.IsFalse(Day4.IsValidPassword("123789"));
            Assert.IsFalse(Day4.IsValidPassword(123789));
        }

        [TestMethod]
        public void Day4AValidPasswordExample4()
        {
            Assert.IsTrue(Day4.IsValidPassword("122345"));
            Assert.IsTrue(Day4.IsValidPassword(122345));
        }

        [TestMethod]
        public void Day4AValidPasswordExample5()
        {
            Assert.IsTrue(Day4.IsValidPassword("111123"));
            Assert.IsTrue(Day4.IsValidPassword(111123));
        }

        [TestMethod]
        public void Day4BValidPasswordExample1()
        {
            Assert.IsTrue(Day4.IsValidPassword("112233", false));
            Assert.IsTrue(Day4.IsValidPassword(112233, false));
        }

        [TestMethod]
        public void Day4BValidPasswordExample2()
        {
            Assert.IsFalse(Day4.IsValidPassword("123444", false));
            Assert.IsFalse(Day4.IsValidPassword(123444, false));
        }

        [TestMethod]
        public void Day4BValidPasswordExample3()
        {
            Assert.IsTrue(Day4.IsValidPassword("111122", false));
            Assert.IsTrue(Day4.IsValidPassword(111122, false));
        }

        [TestMethod]
        public void Day4BValidPasswordMyTest1()
        {
            Assert.IsFalse(Day4.IsValidPassword("555666", false));
            Assert.IsFalse(Day4.IsValidPassword(555666, false));
        }
    }
}

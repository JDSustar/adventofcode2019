using System;
using System.Collections.Generic;
using System.Text;
using AdventOfCode2019;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2019Test
{
    [TestClass]
    public class Day8Tests
    {
        [TestMethod]
        public void Test1()
        {
            string data = "123456221012";
            SpaceImage si = new SpaceImage(data, 3, 2);

            Assert.AreEqual(6, si.CalculateCheckDigit());
        }
    }
}

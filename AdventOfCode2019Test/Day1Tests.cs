using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode2019;

namespace AdventOfCode2019Test
{
    [TestClass]
    public class Day1Tests
    {
        [TestMethod]
        public void Day1AGetFuelRequiredForMassExamples()
        {
            Assert.AreEqual(Day1.GetFuelRequiredForMass(12), 2);
            Assert.AreEqual(Day1.GetFuelRequiredForMass(14), 2);
            Assert.AreEqual(Day1.GetFuelRequiredForMass(1969), 654);
            Assert.AreEqual(Day1.GetFuelRequiredForMass(100756), 33583);
        }

        [TestMethod]
        public void Day1GetFuelRequiredForFuelExamples()
        {
            Assert.AreEqual(0, Day1.GetFuelRequiredForFuel(2));
            Assert.AreEqual(312, Day1.GetFuelRequiredForFuel(654));
            Assert.AreEqual(966, Day1.GetFuelRequiredForFuel(Day1.GetFuelRequiredForMass(1969)) + Day1.GetFuelRequiredForMass(1969));
            Assert.AreEqual(50346, Day1.GetFuelRequiredForFuel(Day1.GetFuelRequiredForMass(100756)) + Day1.GetFuelRequiredForMass(100756));
        }
    }
}

using AdventOfCode2019;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2019Test
{
    [TestClass]
    public class Day2Tests
    {
        [TestMethod]
        public void Day2EvaluateIntCodeExample1()
        {
            int[] intCodeArray = { 1, 9, 10, 3, 2, 3, 11, 0, 99, 30, 40, 50 };
            IntCode testIntCode = new IntCode(intCodeArray);

            testIntCode.EvaluateCodes();
            CollectionAssert.AreEqual(new int[] {3500,9,10,70,2,3,11,0,99,30,40,50}, testIntCode.Memory);
        }

        [TestMethod]
        public void Day2EvaluateIntCodeExample2()
        {
            int[] intCodeArray = { 1,0,0,0, 99 };
            IntCode testIntCode = new IntCode(intCodeArray);

            testIntCode.EvaluateCodes();
            CollectionAssert.AreEqual(new int[] { 2,0,0,0,99 }, testIntCode.Memory);
        }

        [TestMethod]
        public void Day2EvaluateIntCodeExample3()
        {
            int[] intCodeArray = { 2,3,0,3,99 };
            IntCode testIntCode = new IntCode(intCodeArray);

            testIntCode.EvaluateCodes();
            CollectionAssert.AreEqual(new int[] { 2,3,0,6,99 }, testIntCode.Memory);
        }

        [TestMethod]
        public void Day2EvaluateIntCodeExample4()
        {
            int[] intCodeArray = { 2,4,4,5,99,0 };
            IntCode testIntCode = new IntCode(intCodeArray);

            testIntCode.EvaluateCodes();
            CollectionAssert.AreEqual(new int[] { 2,4,4,5,99,9801 }, testIntCode.Memory);
        }
        
        [TestMethod]
        public void Day2EvaluateIntCodeExample5()
        {
            int[] intCodeArray = { 1,1,1,4,99,5,6,0,99 };
            IntCode testIntCode = new IntCode(intCodeArray);

            testIntCode.EvaluateCodes();
            CollectionAssert.AreEqual(new int[] { 30,1,1,4,2,5,6,0,99 }, testIntCode.Memory);
        }
    }
}

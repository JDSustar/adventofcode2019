using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019
{
    public static class Helper
    {
        public static int GetManhattenDistance(int a, int b)
        {
            return Math.Abs(a) + Math.Abs(b);
        }
    }
}

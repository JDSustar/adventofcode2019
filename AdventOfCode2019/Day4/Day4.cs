using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019
{
    public static class Day4
    {
        public static void ExecuteStarOne(string input = "108457-562041")
        {
            string[] range = input.Split('-');

            int validPasswords = 0;

            int start = int.Parse(range[0]);
            int end = int.Parse(range[1]);

            foreach (int i in Enumerable.Range(start, end - start))
            {
                if (IsValidPassword(i))
                    validPasswords++;
            }

            Logger.LogMessage(LogLevel.ANSWER, "Day 4A: Valid Passwords in Range: " + validPasswords);
        }

        public static void ExecuteStarTwo(string input = "108457-562041")
        {
            string[] range = input.Split('-');

            int validPasswords = 0;

            int start = int.Parse(range[0]);
            int end = int.Parse(range[1]);

            foreach (int i in Enumerable.Range(start, end - start))
            {
                if (IsValidPassword(i, false))
                    validPasswords++;
            }

            Logger.LogMessage(LogLevel.ANSWER, "Day 4B: Valid Passwords in Range: " + validPasswords);
        }

        public static bool IsValidPassword(string password, bool largerGroupAllowed = true)
        {
            bool lengthValid = false;
            bool doubleDigitValid = false;
            bool decreasingValuesValid = true;

            if (password.Length == 6)
            {
                lengthValid = true;
            }



            for (int i = 0; i < password.Length - 1; i++)
            {
                if (!(int.Parse(password[i].ToString()) <= int.Parse(password[i + 1].ToString())))
                {
                    decreasingValuesValid = false;
                }
            }

            if (largerGroupAllowed)
            {
                for (int i = 0; i < password.Length - 1; i++)
                {
                    if (password[i] == password[i + 1])
                    {
                        doubleDigitValid = true;
                        break;
                    }
                }
            }
            else
            {
                // https://stackoverflow.com/questions/3244994/is-this-the-best-way-to-create-a-frequency-table-using-linq
                var groups = from c in password.ToCharArray()
                             group c by c into g
                             select g;
                var dic = groups.ToDictionary(g => g.Key, g => g.Count());

                doubleDigitValid = dic.Any(keyvalue => keyvalue.Value == 2);
            }

            if (lengthValid && doubleDigitValid && decreasingValuesValid)
            {
                Logger.LogMessage(LogLevel.DEBUG, "Valid Password: " + password);
            }

            return lengthValid && doubleDigitValid && decreasingValuesValid;
        }

        public static bool IsValidPassword(int password, bool largerGroupAllowed = true)
        {
            return IsValidPassword(password.ToString(), largerGroupAllowed);
        }
    }
}

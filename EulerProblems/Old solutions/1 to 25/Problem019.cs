using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Numerics;
using System.Reflection;

namespace fletcher.org
{
    [Export(typeof(IProblem))]
    [Solution("171")]
    class Problem019Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=19
        /// 
        /// You are given the following information, but you may prefer 
        /// to do some research for yourself.
        /// 
        /// * 1 Jan 1900 was a Monday.
        /// * Thirty days has September,
        ///   April, June and November.
        ///   All the rest have thirty-one,
        ///   Saving February alone,
        ///   Which has twenty-eight, rain or shine.
        ///   And on leap years, twenty-nine.
        /// * A leap year occurs on any year evenly divisible by 4, 
        ///   but not on a century unless it is divisible by 400.
        ///   
        /// How many Sundays fell on the first of the month during the 
        /// twentieth century (1 Jan 1901 to 31 Dec 2000)?
        /// 
        /// http://en.wikipedia.org/wiki/Doomsday_rule
        /// 
        /// Answer: 171
        /// </summary>
        public Problem019Generator() { }

        public enum Day
        {
            Sunday = 0,
            Monday,
            Tueday,
            Wednesday,
            Thursday,
            Friday,
            Saturday
        }

        protected override string InternalCalculateSolution()
        {
            return Enumerable.Range(1901, 100)
                .Select(y => MonthsWithSundayOn1(y).Count())
                .Sum().ToString();            
        }

        static int AnchorDay(int century)
        {
            switch (century % 4)
            {
                case 0: return 2;
                case 1: return 0;
                case 2: return 5;
                case 3: return 3;
                default: throw new ApplicationException("Oops! Could not determine anchor day");
            }        
        }

        static int DoomsDay(int year)
        {
            Verify.ArgIs<int>(year, "year", y => y.ToString().Length == 4, "Must be a four-digit year");
            
            int cent = year / 100;
            int yr = year - (cent * 100);

            int t1 = yr / 12;
            int t2 = yr % 12;
            int t3 = t2 / 4;
            return ((t1 + t2 + t3) % 7 + AnchorDay(cent)) % 7;
        }

        IEnumerable<int> MonthsWithSundayOn1(int year)
        {
            if (year.IsLeapYear())
            {
                if ((DoomsDay(year) + 4) % 7 == 0)
                    yield return 1;
                if (DoomsDay(year) == 0)
                    yield return 2;
            }
            else
            {
                if ((DoomsDay(year) + 5) % 7 == 0)
                    yield return 1;
                if ((DoomsDay(year) + 1) % 7 == 0)
                    yield return 2;
            }

            if ((DoomsDay(year) + 1) % 7 == 0)  // Mar
                yield return 3;
            if ((DoomsDay(year) + 4) % 7 == 0)  // Apr
                yield return 4;
            if ((DoomsDay(year) + 6) % 7 == 0)  // May
                yield return 5;
            if ((DoomsDay(year) + 2) % 7 == 0)  // Jun
                yield return 6;
            if ((DoomsDay(year) + 4) % 7 == 0)  // Jul
                yield return 7;
            if (DoomsDay(year) % 7 == 0)        // Aug
                yield return 8;
            if ((DoomsDay(year) + 3) % 7 == 0)  // Sep
                yield return 9;
            if ((DoomsDay(year) + 5) % 7 == 0)  // Oct
                yield return 10;
            if ((DoomsDay(year) + 1) % 7 == 0)  // Nov
                yield return 11;
            if ((DoomsDay(year) + 3) % 7 == 0)  // Dec
                yield return 12;
        }        
    }

    static class Problem019Ext
    {
        public static bool IsLeapYear(this int year)
        {
            return (year % 100 == 0)
                ? (year % 400 == 0)
                : (year % 4 == 0);            
        }    
    }
}

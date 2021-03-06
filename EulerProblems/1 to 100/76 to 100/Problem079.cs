﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
    Euler Problem 79

    Keith Fletcher
    Jul 2017

    This file is Unlicensed.
    See the foot of the file, or refer to <http://unlicense.org>
*/

namespace HisRoyalRedness.com
{
    [Solution("73162890")]
    public class Problem79 : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=79
        /// 
        /// A common security method used for online banking is to ask 
        /// the user for three random characters from a passcode. For 
        /// example, if the passcode was 531278, they may ask for the 
        /// 2nd, 3rd, and 5th characters; the expected reply would 
        /// be: 317.
        /// 
        /// The text file, keylog.txt, contains fifty successful login 
        /// attempts.
        /// 
        /// Given that the three characters are always asked for in 
        /// order, analyse the file so as to determine the shortest 
        /// possible secret passcode of unknown length.
        /// 
        /// Answer: 73162890
        /// </summary>

        protected override string InternalSolve()
        {
            var numStr = File.ReadAllLines(@"Resources\p079_keylog.txt").ToArray();
            var digits = numStr.SelectMany(n => n.Select(c => c)).Distinct().ToArray();
            var nums = numStr.Select(l => int.Parse(l)).Distinct().ToList();

            // digits is a dictionary of all digits present in the file. It's keyed by the digits, with
            // the value showing the relative order of each digit. We'll keep sorting based on the 
            // key file until no more order swaps have taken place

            var swapped = false;
            var pinDigits = digits.Select((digit, index) => new { digit, index }).ToDictionary(n => n.digit, n => n.index);

            do
            {
                swapped = false;
                foreach(var attempt in numStr)
                {
                    swapped |= CheckAndSwap(ref pinDigits, attempt[0], attempt[1]);
                    swapped |= CheckAndSwap(ref pinDigits, attempt[1], attempt[2]);
                }

            } while (swapped);

            return new string(pinDigits.OrderBy(p => p.Value).Select(p => p.Key).ToArray());
        }

        static bool CheckAndSwap(ref Dictionary<char, int> pinDigits, char digit1, char digit2)
        {
            if (pinDigits[digit1] > pinDigits[digit2])
            {
                var t = pinDigits[digit1];
                pinDigits[digit1] = pinDigits[digit2];
                pinDigits[digit2] = t;
                return true;
            }
            return false;
        }
    }
}

/*
This is free and unencumbered software released into the public domain.

Anyone is free to copy, modify, publish, use, compile, sell, or
distribute this software, either in source code form or as a compiled
binary, for any purpose, commercial or non-commercial, and by any
means.

In jurisdictions that recognize copyright laws, the author or authors
of this software dedicate any and all copyright interest in the
software to the public domain. We make this dedication for the benefit
of the public at large and to the detriment of our heirs and
successors. We intend this dedication to be an overt act of
relinquishment in perpetuity of all present and future rights to this
software under copyright law.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.

For more information, please refer to <http://unlicense.org>
*/

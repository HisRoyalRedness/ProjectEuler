using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    Routines to convert from and to roman numerals

    Keith Fletcher
    Jul 2017

    This file is Unlicensed.
    See the foot of the file, or refer to <http://unlicense.org>
*/

namespace HisRoyalRedness.com
{
    public static partial class RomanNumerals
    {
        // https://projecteuler.net/about=roman_numerals

        /*        
            I = 1
            V = 5
            X = 10
            L = 50
            C = 100
            D = 500
            M = 1000

            Basic rules:

            i.    Numerals must be arranged in descending order of size.
            ii.   M, C, and X cannot be equalled or exceeded by smaller denominations.
            iii.  D, L, and V can each only appear once.

            Subtractive rules:
            
            i.    Only one I, X, and C can be used as the leading numeral in part of a subtractive pair.
            ii.   I can only be placed before V and X.
            iii.  X can only be placed before L and C.
            iv.   C can only be placed before D and M.
        */

        public static ulong FromRomanNumerals(this string number)
        {
            return 0;
        }

        public static string ToRomanNumerals(this ulong number, bool subtractive = true)
        {
            var sb = new StringBuilder();
            foreach (var denom in _romanDenominations)
            {
                while (number >= denom.Item2)
                {
                    sb.Append(denom.Item1);
                    number -= denom.Item2;
                }
                if (subtractive && denom.Item4 != 0 && denom.Item2 - denom.Item4 == number)
                {
                    sb.Append(denom.Item3);
                    sb.Append(denom.Item1);
                    number -= denom.Item4;
                }
                if (number == 0)
                    break;
            }
            return sb.ToString();
        }

        static Tuple<char, ulong, char, ulong>[] _romanDenominations = new[]
        {
            new Tuple<char, ulong, char, ulong>('M', 1000, 'C', 100),
            new Tuple<char, ulong, char, ulong>('D', 500, 'C', 100),
            new Tuple<char, ulong, char, ulong>('C', 100, 'X', 10),
            new Tuple<char, ulong, char, ulong>('L', 50, 'X', 10),
            new Tuple<char, ulong, char, ulong>('X', 10, 'I', 1),
            new Tuple<char, ulong, char, ulong>('V', 5, 'I', 1),
            new Tuple<char, ulong, char, ulong>('I', 1, '\0', 0),
        };
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

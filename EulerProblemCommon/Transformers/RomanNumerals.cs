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

        public static ulong FromRomanNumerals(this string number, bool throwIfInvalid = true)
        {
            // M CM D CD C XC L XL X IX IV V I
            var tokens1 = "MCMDCDCXCLXLXIXIVVI".RomanScanner().ToList();
            var tokens2 = "M CM D CD C XC L XL X IX IV V I".RomanScanner(true).ToList();


            return 0;
        }


        static IEnumerable<RomanToken> RomanScanner(this string number, bool ignoreSpace = false)
        {
            if ((number?.Length ?? 0) == 0)
            {
                yield return RomanToken.EOS;
                yield break;
            }

            char currentChar = '\0';
            char nextChar = char.ToUpper(number[0]);
            var i = 1;
            var fetchNext = false;

            while(true)
            {
                while(true)
                {
                    currentChar = nextChar;
                    nextChar = (i >= number.Length) ? '\0' : char.ToUpper(number[i++]);
                    if (fetchNext)
                        fetchNext = false;
                    else
                        break;
                } 

                switch(currentChar)
                {
                    case 'M': yield return RomanToken.M; break;
                    case 'D': yield return RomanToken.D; break;
                    case 'C':
                        if (nextChar == 'M')
                        {
                            fetchNext = true;
                            yield return RomanToken.CM;
                        }
                        else if (nextChar == 'D')
                        {
                            fetchNext = true;
                            yield return RomanToken.CD;
                        }
                        else
                            yield return RomanToken.C;
                        break;
                    case 'L': yield return RomanToken.L; break;
                    case 'X':
                        if (nextChar == 'L')
                        {
                            fetchNext = true;
                            yield return RomanToken.XL;
                        }
                        else if (nextChar == 'C')
                        {
                            fetchNext = true;
                            yield return RomanToken.XC;
                        }
                        else
                            yield return RomanToken.X;
                        break;
                    case 'V': yield return RomanToken.V; break;
                    case 'I':
                        if (nextChar == 'V')
                        {
                            fetchNext = true;
                            yield return RomanToken.IV;
                        }
                        else if (nextChar == 'X')
                        {
                            fetchNext = true;
                            yield return RomanToken.IX;
                        }
                        else
                            yield return RomanToken.I;
                        break;
                    case '\0':
                        yield return RomanToken.EOS;
                        yield break;
                    default:
                        if (!ignoreSpace || currentChar != ' ')
                        {
                            yield return RomanToken.Invalid;
                            yield break;
                        }
                        break;
                }
            }
        }

        enum RomanToken { M, D, C, L, X, V, I, CD, CM, XL, XC, IV, IX, Invalid, EOS };


        static void ParseError(string number, int index)
        {
            throw new InvalidOperationException($"{number} is not a valid Roman numberal. The digit '{number[index - 1]}' at position {index} is unexpected.");
        }

        

        #region ToRomanNumerals
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
                if (subtractive && number + denom.Item4 >= denom.Item2)
                {
                    sb.Append(denom.Item3);
                    sb.Append(denom.Item1);
                    number -= (denom.Item2 - denom.Item4);
                }
                if (number == 0)
                    break;
            }
            return sb.ToString();
        }
        #endregion ToRomanNumerals

        const int ROMAN_M = 1000;
        const int ROMAN_D = 500;
        const int ROMAN_C = 100;
        const int ROMAN_L = 50;
        const int ROMAN_X = 10;
        const int ROMAN_V = 5;
        const int ROMAN_I = 1;
        const int ROMAN__ = 0;

        static Tuple<char, ulong, char, ulong>[] _romanDenominations = new[]
        {
            new Tuple<char, ulong, char, ulong>('M', ROMAN_M, 'C', ROMAN_C),
            new Tuple<char, ulong, char, ulong>('D', ROMAN_D, 'C', ROMAN_C),
            new Tuple<char, ulong, char, ulong>('C', ROMAN_C, 'X', ROMAN_X),
            new Tuple<char, ulong, char, ulong>('L', ROMAN_L, 'X', ROMAN_X),
            new Tuple<char, ulong, char, ulong>('X', ROMAN_X, 'I', ROMAN_I),
            new Tuple<char, ulong, char, ulong>('V', ROMAN_V, 'I', ROMAN_I),
            new Tuple<char, ulong, char, ulong>('I', ROMAN_I, '\0', ROMAN__),
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

using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        #region FromRomanNumerals
        public static ulong FromRomanNumerals(this string number, bool throwIfInvalid = true, bool ignoreSpace = false)
        {
            var state = RomanTokenType.None;
            var sum = (ulong)0;
            var mSum = (ulong)0;
            var cSum = (ulong)0;
            var xSum = (ulong)0;
            foreach (var token in RomanScanner(number, ignoreSpace))
            {
                switch(token.TokenType)
                {
                    #region M                   1000
                    case RomanTokenType.M:
                        if (state <= RomanTokenType.M)
                            state = token.TokenType;
                        else
                            { ParseError(number, token, throwIfInvalid); return 0; }
                        break;
                    #endregion M
                    #region CM                  900
                    case RomanTokenType.CM:
                        if (state <= RomanTokenType.M)
                            state = RomanTokenType.D;
                        else
                            { ParseError(number, token, throwIfInvalid); return 0; }
                        break;
                    #endregion CM
                    #region D                   500
                    case RomanTokenType.D:
                        if (state <= RomanTokenType.D)
                            state = RomanTokenType.C; // D can only appear once
                        else
                            { ParseError(number, token, throwIfInvalid); return 0; }
                        break;
                    #endregion D
                    #region CD                  400
                    case RomanTokenType.CD:
                        if (state <= RomanTokenType.D)
                            state = RomanTokenType.C; // D can only appear once
                        else
                            { ParseError(number, token, throwIfInvalid); return 0; }
                        break;
                    #endregion CD
                    #region C                   100
                    case RomanTokenType.C:
                        if (state <= RomanTokenType.C)
                            state = token.TokenType;
                        else
                            { ParseError(number, token, throwIfInvalid); return 0; }
                        break;
                    #endregion C
                    #region XC                  90
                    case RomanTokenType.XC:
                        if (state <= RomanTokenType.C)
                            state = RomanTokenType.L;
                        else
                            { ParseError(number, token, throwIfInvalid); return 0; }
                        break;
                    #endregion XC
                    #region L                   50
                    case RomanTokenType.L:
                        if (state <= RomanTokenType.L)
                            state = RomanTokenType.X; // D can only appear once
                        else
                            { ParseError(number, token, throwIfInvalid); return 0; }
                        break;
                    #endregion L
                    #region XL                  40
                    case RomanTokenType.XL:
                        if (state <= RomanTokenType.L)
                            state = RomanTokenType.X; // L can only appear once
                        else
                            { ParseError(number, token, throwIfInvalid); return 0; }
                        break;
                    #endregion XL
                    #region X                   10
                    case RomanTokenType.X:
                        if (state <= RomanTokenType.X)
                            state = token.TokenType;
                        else
                            { ParseError(number, token, throwIfInvalid); return 0; }
                        break;
                    #endregion X
                    #region IX                  9
                    case RomanTokenType.IX:
                        if (state <= RomanTokenType.X)
                            state = RomanTokenType.V;
                        else
                            { ParseError(number, token, throwIfInvalid); return 0; }
                        break;
                    #endregion IX
                    #region V                   5
                    case RomanTokenType.V:
                        if (state <= RomanTokenType.V)
                            state = RomanTokenType.I; // V can only appear once
                        else
                            { ParseError(number, token, throwIfInvalid); return 0; }
                        break;
                    #endregion V
                    #region IV                  4
                    case RomanTokenType.IV:
                        if (state <= RomanTokenType.V)
                            state = RomanTokenType.I; // V can only appear once
                        else
                            { ParseError(number, token, throwIfInvalid); return 0; }
                        break;
                    #endregion IV
                    #region I                   1
                    case RomanTokenType.I:
                        if (state <= RomanTokenType.I)
                            state = token.TokenType;
                        else
                            { ParseError(number, token, throwIfInvalid); return 0; }
                        break;
                    #endregion I
                    case RomanTokenType.EOS:
                        return sum;
                    case RomanTokenType.Invalid:
                    default:
                        ParseError(number, token, throwIfInvalid);
                        return 0;
                }

                if (state > RomanTokenType.M && !CheckDenominationSumLimits(number, ref mSum, RomanTokenType.M, token, throwIfInvalid))
                    return 0;
                if (state > RomanTokenType.C && !CheckDenominationSumLimits(number, ref cSum, RomanTokenType.C, token, throwIfInvalid))
                    return 0;
                if (state > RomanTokenType.X && !CheckDenominationSumLimits(number, ref xSum, RomanTokenType.X, token, throwIfInvalid))
                    return 0;

                sum += _tokenValues[token.TokenType];
            }

            return sum;
        }

        static bool CheckDenominationSumLimits(string number, ref ulong denomSum, RomanTokenType denomType, RomanToken token, bool throwIfInvalid = true)
        {
            denomSum += _tokenValues[token.TokenType];
            if (denomSum >= _tokenValues[denomType])
            {
                if (throwIfInvalid)
                    throw new InvalidOperationException($"{number} is not a valid Roman numberal. Smaller denominations exceed the value of {denomType}.");
                else
                    return false;
            }
            return true;
        }

        static void ParseError(string number, RomanToken token, bool throwIfInvalid = true)
        {
            if (throwIfInvalid)
                throw new InvalidOperationException($"{number} is not a valid Roman numberal. The token '{token.TokenType}' at position {token.Index} is unexpected.");
        }

        static IEnumerable<RomanToken> RomanScanner(this string number, bool ignoreSpace = false)
        {
            if ((number?.Length ?? 0) == 0)
            {
                yield return new RomanToken(RomanTokenType.EOS, 0);
                yield break;
            }

            char currentChar = '\0';
            char nextChar = char.ToUpper(number[0]);
            var i = 1;
            var fetchNext = false;

            while (true)
            {
                while (true)
                {
                    currentChar = nextChar;
                    nextChar = (i >= number.Length) ? '\0' : char.ToUpper(number[i]);
                    i++;
                    if (fetchNext)
                        fetchNext = false;
                    else
                        break;
                }

                switch (currentChar)
                {
                    case 'M': yield return new RomanToken(RomanTokenType.M, i); break;
                    case 'D': yield return new RomanToken(RomanTokenType.D, i); break;
                    case 'C':
                        if (nextChar == 'M')
                        {
                            fetchNext = true;
                            yield return new RomanToken(RomanTokenType.CM, i);
                        }
                        else if (nextChar == 'D')
                        {
                            fetchNext = true;
                            yield return new RomanToken(RomanTokenType.CD, i);
                        }
                        else
                            yield return new RomanToken(RomanTokenType.C, i);
                        break;
                    case 'L': yield return new RomanToken(RomanTokenType.L, i); break;
                    case 'X':
                        if (nextChar == 'L')
                        {
                            fetchNext = true;
                            yield return new RomanToken(RomanTokenType.XL, i);
                        }
                        else if (nextChar == 'C')
                        {
                            fetchNext = true;
                            yield return new RomanToken(RomanTokenType.XC, i);
                        }
                        else
                            yield return new RomanToken(RomanTokenType.X, i);
                        break;
                    case 'V': yield return new RomanToken(RomanTokenType.V, i); break;
                    case 'I':
                        if (nextChar == 'V')
                        {
                            fetchNext = true;
                            yield return new RomanToken(RomanTokenType.IV, i);
                        }
                        else if (nextChar == 'X')
                        {
                            fetchNext = true;
                            yield return new RomanToken(RomanTokenType.IX, i);
                        }
                        else
                            yield return new RomanToken(RomanTokenType.I, i);
                        break;
                    case '\0':
                        yield return new RomanToken(RomanTokenType.EOS, i);
                        yield break;
                    default:
                        if (!ignoreSpace || currentChar != ' ')
                        {
                            yield return new RomanToken(RomanTokenType.Invalid, i);
                            yield break;
                        }
                        break;
                }
            }
        }

        [DebuggerDisplay("{_type}: {_index}")]
        struct RomanToken
        {
            internal RomanToken(RomanTokenType tokenType, int index)
            {
                _type = tokenType;
                _index = index - 1;
            }

            public RomanTokenType TokenType => _type;
            public int Index => _index;

            readonly RomanTokenType _type;
            readonly int _index;
        }

        enum RomanTokenType { None, M, CM, D, CD, C, XC, L, XL, X, IX, V, IV, I, Invalid, EOS };
        #endregion FromRomanNumerals

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

        const int ROMAN_CM = ROMAN_M - ROMAN_C;
        const int ROMAN_CD = ROMAN_D - ROMAN_C;
        const int ROMAN_XC = ROMAN_C - ROMAN_X;
        const int ROMAN_XL = ROMAN_L - ROMAN_X;
        const int ROMAN_IX = ROMAN_X - ROMAN_I;
        const int ROMAN_IV = ROMAN_V - ROMAN_I;

        // Tuple of roman digit chars and the value ascribed to them.
        // Column 3 is the subtractive digit that is allowed, 
        // and the value of that subtractive digit
        readonly static Tuple<char, ulong, char, ulong>[] _romanDenominations = new[]
        {
            new Tuple<char, ulong, char, ulong>('M', ROMAN_M, 'C', ROMAN_C),
            new Tuple<char, ulong, char, ulong>('D', ROMAN_D, 'C', ROMAN_C),
            new Tuple<char, ulong, char, ulong>('C', ROMAN_C, 'X', ROMAN_X),
            new Tuple<char, ulong, char, ulong>('L', ROMAN_L, 'X', ROMAN_X),
            new Tuple<char, ulong, char, ulong>('X', ROMAN_X, 'I', ROMAN_I),
            new Tuple<char, ulong, char, ulong>('V', ROMAN_V, 'I', ROMAN_I),
            new Tuple<char, ulong, char, ulong>('I', ROMAN_I, '\0', ROMAN__),
        };

        readonly static Dictionary<RomanTokenType, ulong> _tokenValues = new Dictionary<RomanTokenType, ulong>()
        {
            { RomanTokenType.M, ROMAN_M },
            { RomanTokenType.CM, ROMAN_CM },
            { RomanTokenType.D, ROMAN_D },
            { RomanTokenType.CD, ROMAN_CD },
            { RomanTokenType.C, ROMAN_C },
            { RomanTokenType.XC, ROMAN_XC },
            { RomanTokenType.L, ROMAN_L },
            { RomanTokenType.XL, ROMAN_XL },
            { RomanTokenType.X, ROMAN_X },
            { RomanTokenType.IX, ROMAN_IX },
            { RomanTokenType.V, ROMAN_V },
            { RomanTokenType.IV, ROMAN_IV },
            { RomanTokenType.I, ROMAN_I },
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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
    Euler Problem 89

    Keith Fletcher
    Jul 2017

    This file is Unlicensed.
    See the foot of the file, or refer to <http://unlicense.org>
*/

namespace HisRoyalRedness.com
{
    [Solution("")]
    public class Problem89 : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=89
        /// 
        /// For a number written in Roman numerals to be considered valid there 
        /// are basic rules which must be followed.Even though the rules allow 
        /// some numbers to be expressed in more than one way there is always a 
        /// "best" way of writing a particular number.
        /// 
        /// For example, it would appear that there are at least six ways of 
        /// writing the number sixteen:
        /// 
        ///     IIIIIIIIIIIIIIII
        ///     VIIIIIIIIIII
        ///     VVIIIIII
        ///     XIIIIII
        ///     VVVI
        ///     XVI
        ///     
        /// However, according to the rules only XIIIIII and XVI are valid, and 
        /// the last example is considered to be the most efficient, as it uses 
        /// the least number of numerals.
        /// 
        /// The 11K text file, roman.txt,  contains one thousand numbers written 
        /// in valid, but not necessarily minimal, Roman numerals; 
        /// see About...Roman Numerals for the definitive rules for this problem.
        /// 
        /// Find the number of characters saved by writing each of these in their 
        /// minimal form.
        /// 
        /// Note: You can assume that all the Roman numerals in the file contain 
        /// no more than four consecutive identical units.
        /// 
        /// Answer: 
        /// </summary>

        protected override string InternalSolve()
        {
            return "";
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

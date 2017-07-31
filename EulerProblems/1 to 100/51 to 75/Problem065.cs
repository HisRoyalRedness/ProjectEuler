using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
    Euler Problem 65

    Keith Fletcher
    Jul 2017

    This file is Unlicensed.
    See the foot of the file, or refer to <http://unlicense.org>
*/

namespace HisRoyalRedness.com
{
    [Title("Convergents of e")]
    [Solution("272")]
    #region Summary and analysis
    [Summary(@"
The square root of 2 can be written as an infinite continued fraction.
$$\sqrt{2}=1+\frac{1}{2 + \frac{1}{2 + \frac{1}{2 + \frac{1}{2 + ...}}}}$$

The infinite continued fraction can be written, $\sqrt{2} = [1;(2)]$, 
(2) indicates that 2 repeats ad infinitum. In a similar way, 
$\sqrt{23} = [4;(1,3,1,8)]$.

It turns out that the sequence of partial values of continued fractions for 
square roots provide the best rational approximations. Let us consider the 
convergents for $\sqrt{2}$.
$$1+\frac{1}{2}=\frac{3}{2}$$
$$1+\frac{1}{2+\frac{1}{2}}=\frac{7}{5}$$
$$1+\frac{1}{2+\frac{1}{2 + \frac{1}{2}}}=\frac{17}{12}$$
$$1+\frac{1}{2+\frac{1}{2 + \frac{1}{2 + \frac{1}{2}}}}=\frac{41}{29}$$

Hence the sequence of the first ten convergents for $\sqrt{2}$ are:
$1, \frac{3}{2}, \frac{7}{5}, \frac{17}{12}, \frac{41}{29}, \frac{99}{70}, 
\frac{239}{169}, \frac{577}{408}, \frac{1393}{985}, \frac{3363}{2378}, ...$

What is most surprising is that the important mathematical constant,
$e = [2; 1,2,1,\ 1,4,1,\ 1,6,1,\ ...\ ,\ 1,2k,1,\ ...]$.

The first ten terms in the sequence of convergents for $e$ are:
$2, 3, \frac{8}{3}, \frac{11}{4}, \frac{19}{7}, \frac{87}{32}, \frac{106}{39}, 
\frac{193}{71}, \frac{1264}{465}, \frac{1457}{536}, ...$

The sum of digits in the numerator of the 10^th^ convergent is $1+4+5+7=17$.

Find the sum of digits in the numerator of the 100^th^ convergent of the continued 
fraction for $e$.
")]
    [Analysis(@"
The summary gives us the sequence of coefficients as $e = [2; 1,2,1,\ 1,4,1,
\ 1,6,1,\ ...\ ,\ 1,2k,1,\ ...]$.

With the coefficients, we can calculate the continued fractions 
([Wikipedia](https://en.wikipedia.org/wiki/Continued_fraction#Infinite_continued_fractions_and_convergents))
using the formula:

$$\frac{h_n}{k_n} = \frac{a_nh_{n-1}+h_{n-2}}{a_nk_{n-1}+k_{n-2}}$$

where $h_n$ and $k_n$ are the numerator and denominator of the continued
fraction for the coefficient $a_n$. The numerators and denomitors of the continued
fraction that correspond to the two prior coefficients would be $h_{n-1}$ and $h_{n-2}$,
and $k_{n-1}$ and $k_{n-2}$.

Thus, we can easily calculate the first 100 continued fractions. The 100^th^
fraction is 

$$\frac{6963524437876961749120273824619538346438023188214475670667}{2561737478789858711161539537921323010415623148113041714756}$$


We simply sum the digits of the numerator to give 272.

")]
    #endregion Summary and analysis
    public class Problem65 : ProblemBase
    {
        protected override string InternalSolve()
        {
            return ContinuedFraction
                .Sequence(eContinuedFractionCoefficients().Select(n => (BigInteger)n))
                .Skip(99).First()
                .Numerator
                .ToString()
                .Select(c => c - 0x30)
                .Sum()
                .ToString();
        }

        static IEnumerable<ulong> eContinuedFractionCoefficients()
        {
            yield return 2;
            ulong i = 2;
            while(true)
            {
                yield return 1;
                yield return i;
                yield return 1;
                i += 2;
            }
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

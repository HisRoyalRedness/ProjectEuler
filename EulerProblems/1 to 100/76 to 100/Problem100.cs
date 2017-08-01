using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
    Euler Problem 100

    Keith Fletcher
    Jul 2017

    This file is Unlicensed.
    See the foot of the file, or refer to <http://unlicense.org>
*/

namespace HisRoyalRedness.com
{
    [Title("Arranged probability")]
    [Solution("756872327473")]
    #region Summary and analysis
    [Summary(@"
If a box contains twenty-one coloured discs, composed of 
fifteen blue discs and six red discs, and two discs were
taken at random, it can be seen that the probability of 
taking two blue discs is 

$$P(BB) = \left(\frac{15}{21}\right)
\times\left(\frac{14}{20}\right) = \frac{1}{2}$$
   
The next such arrangement, for which there is exactly 50% 
chance of taking two blue discs at random, is a box 
containing eighty-five blue discs and thirty-five red 
discs.

By finding the first arrangement to contain over 
$10^12$ = 1,000,000,000,000 discs in total, determine the 
number of blue discs that the box would contain.
")]
    [Analysis(@"
The probabity equation rewritten is 

$$P(BB) = \left(\frac{B}{B+R}\right)
\times\left(\frac{B-1}{B+R-1}\right) = \frac{1}{2}$$

where $B$ is the number of blue tokens, and $R$ is the number of red tokens.

This can be rearranged to $B^2-B-2BR+R-R^2=0$. 

The [Wolfram equation solver](https://www.wolframalpha.com/input/?i=b%5E2-b-2br%2Br-r%5E2%3D0)
manages to determine the integer solutions for R as

$$R = -\frac{(3-2\sqrt{2})^n-(3+2\sqrt{2})^n}{4\sqrt{2}}$$

where $n\in Z, n \geq 0$

Now that we have $R$, we can rearrange to calculate $B$, namely $B^2+B(-1-2R)+(R-R^2)=0$,
and then using the quadratic equation, we select the positive solutions from:

$$B=\frac{(1+2R)+\sqrt{(-1-2R)^2-4(R-R^2)}}{2}$$

It's now quite simple to put these formulae into Excel and generate the first
few integer solutions for red and blue tokens.

$n$                  $R$                  $B$                 $R+B$
------  ----------------  -------------------  --------------------
1                      1                    3                     4
2                      6                   15                    21
3                     35                   85                   120
4                    204                  493                   697
5                   1189                 2871                  4060
6                   6930                16731                 23661
7                  40391                97513                137904
8                 235416               568345                803761
9                1372105              3312555               4684660
10               7997214             19306983              27304197
11              46611179            112529341             159140520
12             271669860            655869061             927538921
13            1583407981           3822685023            5406093004
14            9228778026          22280241075           31509019101
15           53789260175         129858761425          183648021600
**16**  **313506783024**     **756872327473**     **1070379110497**
17         1827251437969        4411375203411         6238626641380
18        10650001844790       25711378892991        36361380737781
19        62072759630771      149856898154533       211929657785304
20       361786555939835      873430010034203      1235216565974040

From the table, we can see that the total number of tokens exceed $10^12$
for the first time where $n=16$. 

The number of blue tokens for this solution is then 756,872,327,473
")]
    #endregion Summary and analysis

    public class Problem100 : ProblemBase
    {
        protected override string InternalSolve()
        {
            return "756872327473";
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

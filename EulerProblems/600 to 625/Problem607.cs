using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
    Euler Problem 607

    Keith Fletcher
    Jul 2017

    This file is Unlicensed.
    See the foot of the file, or refer to <http://unlicense.org>
*/

namespace HisRoyalRedness.com
{
    [Solution("")]
    public class Problem607 : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=607
        /// 
        /// 
        /// Answer: 
        /// </summary>

        protected override string InternalSolve()
        {
            var limit = 10000;

            return Enumerable.Range(1, (int)limit - 1).Select(n => new BigInteger(n))
                .AsParallel()
                .Where(n => n.IsLychrel())
                .Count()
                .ToString();
        } 
    }

    static class Program
    {
        static double DEG_TO_RAD = Math.PI / 180.0;
        static double DEG_45 = 45.0 * DEG_TO_RAD;
        static double DegreeToRadians(this double degree) => degree * DEG_TO_RAD;

        [DebuggerDisplay("mt={mt}, mx={mx}, my={my}")]
        struct MarshTravel
        {
            public double mt; // Time taken to travel the marsh
            public double mx; // Horizontal distance travelled
            public double my; // Vertical distance travelled
        }

        static void Main(string[] args)
        {
            const double ml = 50.0;             // Width of marsh
            const double ms = 9.0;              // Speed in marsh
            const double ol = 100.0;            // Width of open
            const double os = 10.0;             // Speed in open


            using (var writer = File.CreateText("coords.csv"))
            {
                writer.WriteLine("alpha,mt,mx,my,oh,ot,ox,oy,tt");

                var t = AngleSteps(-15, 105, 1)
                    .Select(a => a.DegreeToRadians())
                    .Select(a => CalcMarshTime(a, 10, 9))
                    .ToList();


                for (var alpha = -15.0; alpha < 105; alpha += 1.0)
                {
                    var alphaRad = alpha.DegreeToRadians();
                    var cos45alpha = Math.Cos(DEG_45 - alphaRad);

                    var m = CalcMarshTime(alphaRad, ml, ms);
                    //var mh = ml / cos45alpha;
                    //var mt = ml_ms / cos45alpha;
                    //var mx = mh * Math.Cos(alphaRad);
                    //var my = mh * Math.Sin(alphaRad);

                    var ox = ol - m.mx;
                    var oy = m.my;
                    var oh = Math.Sqrt((ox * ox) + (oy * oy));
                    var ot = oh / os;

                    var tt = ot + m.mt;

                    writer.WriteLine($"{alpha},{m.mt:#0.000},{m.mx:#0.000},{m.my:#0.000},{oh:#0.000},{ot:#0.000},{ox:#0.000},{oy:#0.000},{tt:#0.000}");
                }
            }
        }

        static IEnumerable<double> AngleSteps(double start, double end, double step) => Enumerable.Range(0, (int)(((end - start) / step) + 1.0)).Select(a => (double)a * step + start);

        static MarshTravel CalcMarshTime(double entryAngleRad, double marshWidth, double marshSpeed)
        {
            var cos45alpha = Math.Cos(DEG_45 - entryAngleRad);
            var cosalpha = Math.Cos(entryAngleRad);
            var sinalpha = Math.Sin(entryAngleRad);
            var mh = marshWidth / cos45alpha;
            var mt = mh / marshSpeed;
            var mx = mh * cosalpha;
            var my = mh * sinalpha;
            return new MarshTravel()
            {
                mt = mt,
                mx = mx,
                my = my
            };
        }

        static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
                action(item);
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

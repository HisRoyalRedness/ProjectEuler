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
            using (var writer = File.CreateText("coords.csv"))
            {
                writer.WriteLine(
                    "\"a1\",\"a2\",\"a3\",\"a4\",\"a5\"," + 
                    "\"m1t\",\"m1x\",\"m1y\",\"m1h\"," + 
                    "\"m2t\",\"m2x\",\"m2y\",\"m2h\"," +
                    "\"m3t\",\"m3x\",\"m3y\",\"m3h\"," +
                    "\"m4t\",\"m4x\",\"m4y\",\"m4h\"," +
                    "\"m5t\",\"m5x\",\"m5y\",\"m5h\"," +
                    "\"ot\",\"ox\",\"oy\",\"oh\"," + 
                    "\"at\",\"ax\",\"ay\",\"ah\"");

                var origin = new TravelStats();
                var step = 0.5;

                var f1 = 0.0; var t1 = 10.0;
                var f2 = 40.0; var t2 = 50.0;
                var f3 = 40.0; var t3 = 50.0;
                var f4 = 40.0; var t4 = 50.0;
                var f5 = 40.0; var t5 = 50.0;


                AngleSteps(f1, t1, step)
                    .Select(a => new Context() { a1Deg = a })
                    .AsParallel()
                    .Select(ctx => ctx.Tap(c => c.m1 = CalcMarshTravel(origin, ctx.a1Rad, M1l, M1s)))
                    .SelectMany(ctx1 => AngleSteps(f2, t2, step)
                            .Select(a => new Context(ctx1) { a2Deg = a })
                            .Select(ctx2 => ctx2.Tap(c => c.m2 = CalcMarshTravel(ctx2.m1, ctx2.a2Rad, M2l, M2s)))
                            .SelectMany(ctx2 => AngleSteps(f3, t3, step)
                                    .Select(a => new Context(ctx2) { a3Deg = a })
                                    .Select(ctx3 => ctx3.Tap(c => c.m3 = CalcMarshTravel(ctx3.m2, ctx3.a3Rad, M3l, M3s)))
                                    .SelectMany(ctx3 => AngleSteps(f4, t4, step)
                                            .Select(a => new Context(ctx3) { a4Deg = a })
                                            .Select(ctx4 => ctx4.Tap(c => c.m4 = CalcMarshTravel(ctx4.m3, ctx4.a4Rad, M4l, M4s)))
                                            .SelectMany(ctx4 => AngleSteps(f5, t5, step)
                                                    .Select(a => new Context(ctx4) { a5Deg = a })
                                                    .Select(ctx5 => ctx5.Tap(c => c.m5 = CalcMarshTravel(ctx5.m4, ctx5.a5Rad, M5l, M5s)))
                                            )
                                    )
                            )
                    )
                    .Select(ctx => ctx.Tap(c => c.o = CalcOpenTravel(c.m1, Ol, Os)))
                    .OrderBy(ctx => ctx.Total.st)
                    .ForEach(ctx =>
                    {
                        writer.WriteLine(
                            $"{ctx.a1Deg},{ctx.a2Deg},{ctx.a3Deg},{ctx.a4Deg},{ctx.a5Deg}," + 
                            $"{ctx.m1.ToCSV()},{ctx.m2.ToCSV()},{ctx.m3.ToCSV()},{ctx.m4.ToCSV()},{ctx.m5.ToCSV()}," + 
                            $"{ctx.o.ToCSV()},{ctx.Total.ToCSV()}");
                    });
            }

            return "";
        }

        static TravelStats CalcOpenTravel(TravelStats origin, double totalWidth, double openSpeed, StreamWriter writer = null)
        {
            var ox = totalWidth - origin.sx;
            var oh = Math.Sqrt((ox * ox) + (origin.sy * origin.sy));
            var ot = oh / Os;
            var stats = new TravelStats()
            {
                st = ot,
                sx = -origin.sx,
                sy = -origin.sy,
                sh = oh
            };
            if (writer != null)
                writer.Write(stats.ToCSV());
            return stats;
        }

        static TravelStats CalcMarshTravel(TravelStats origin, double entryAngleRad, double marshWidth, double marshSpeed, StreamWriter writer = null)
        {
            var cos45alpha = Math.Cos(DEG_45 - entryAngleRad);
            var cosalpha = Math.Cos(entryAngleRad);
            var sinalpha = Math.Sin(entryAngleRad);
            var mh = marshWidth / cos45alpha;
            var mt = mh / marshSpeed;
            var mx = mh * cosalpha;
            var my = mh * sinalpha;
            var stats = new TravelStats()
            {
                st = mt,
                sx = mx,
                sy = my,
                sh = mh
            };
            if (writer != null)
                writer.Write(stats.ToCSV());
            return stats;
        }

        class Context
        {
            public Context()
            { }

            public Context(Context other)
            {
                m1 = other.m1;
                m2 = other.m2;
                m3 = other.m3;
                m4 = other.m4;
                m5 = other.m5;
                a1Deg = other.a1Deg;
                a2Deg = other.a2Deg;
                a3Deg = other.a3Deg;
                a4Deg = other.a4Deg;
                a5Deg = other.a5Deg;
            }

            public double a1Deg { get { return _a1Deg; } set { _a1Deg = value; _a1Rad = value.DegreesToRadians(); } }
            public double a1Rad => _a1Rad;
            public double a2Deg { get { return _a2Deg; } set { _a2Deg = value; _a2Rad = value.DegreesToRadians(); } }
            public double a2Rad => _a2Rad;
            public double a3Deg { get { return _a3Deg; } set { _a3Deg = value; _a3Rad = value.DegreesToRadians(); } }
            public double a3Rad => _a3Rad;
            public double a4Deg { get { return _a4Deg; } set { _a4Deg = value; _a4Rad = value.DegreesToRadians(); } }
            public double a4Rad => _a4Rad;
            public double a5Deg { get { return _a5Deg; } set { _a5Deg = value; _a5Rad = value.DegreesToRadians(); } }
            public double a5Rad => _a5Rad;
            double _a1Deg = 0;
            double _a1Rad = 0;
            double _a2Deg = 0;
            double _a2Rad = 0;
            double _a3Deg = 0;
            double _a3Rad = 0;
            double _a4Deg = 0;
            double _a4Rad = 0;
            double _a5Deg = 0;
            double _a5Rad = 0;

            public TravelStats m1;
            public TravelStats m2;
            public TravelStats m3;
            public TravelStats m4;
            public TravelStats m5;
            public TravelStats o;

            public TravelStats Total => m1 + m2 + m3 + m4 + m5 +o;
        }

        [DebuggerDisplay("st={st}, sx={sx}, sy={sy}, sh={sh}")]
        struct TravelStats
        {
            public double sh; // Distance travelled
            public double st; // Time taken to travel the distance
            public double sx; // Horizontal distance travelled
            public double sy; // Vertical distance travelled

            public string ToCSV() => $"{st:#0.000},{sx:#0.000},{sy:#0.000},{sh:#0.000}";
            //public string ToCSV() => $"{st},{sx},{sy},{sh}";

            public static TravelStats operator+(TravelStats t1, TravelStats t2)
                => new TravelStats()
                {
                    st = t1.st + t2.st,
                    sx = t1.sx + t2.sx,
                    sy = t1.sy + t2.sy,
                    sh = t1.sh + t2.sh,
                };
        }

        const double M1l = 10.0;             // Width of marsh
        const double M1s = 9.0;              // Speed in marsh
        const double M2l = 10.0;             // Width of marsh
        const double M2s = 8.0;              // Speed in marsh
        const double M3l = 10.0;             // Width of marsh
        const double M3s = 7.0;              // Speed in marsh
        const double M4l = 10.0;             // Width of marsh
        const double M4s = 6.0;              // Speed in marsh
        const double M5l = 10.0;             // Width of marsh
        const double M5s = 5.0;              // Speed in marsh
        const double Ol = 100.0;            // Width of open
        const double Os = 10.0;             // Speed in open

        const double DEG_45 = 45.0 * AngleConversions.DEG_TO_RAD;

        static IEnumerable<double> AngleSteps(double start, double end, double step) => Enumerable.Range(0, (int)(((end - start) / step) + 1.0)).Select(a => (double)a * step + start);
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

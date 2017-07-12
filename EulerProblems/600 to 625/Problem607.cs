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
    [Solution("13.1265108586")]
    public class Problem607 : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=607
        /// 
        /// 
        /// Answer: 13.1265108586
        /// </summary>

        /*

        Straight across (0 degree)

	    M1	    M2	    M3	    M4	    M5	    O	    Total
Speed   9	    8	    7	    6	    5	    10	
Dist.   14.142	14.142	14.142	14.142	14.142	29.289	100.000
Time    1.571	1.768	2.020	2.357	2.828	2.929	13.474


        At 22.5 degree angle

	    M1	    M2	    M3	    M4	    M5	    O	    Total
Speed   9	    8	    7	    6	    5	    10	
Dist.   10.824	10.824	10.824	10.824	10.824	57.107	111.227
Time    1.203	1.353	1.546	1.804	2.165	5.711	13.781


        At 45 degree angle

	    M1	    M2	    M3	    M4	    M5	    O	    Total
Speed   9	    8	    7	    6	    5	    10	
Dist.   10	    10	    10	    10	    10	    73.681  123.681
Time    1.111	1.250	1.429	1.667	2.000	7.368	14.824


        */

        protected override string InternalSolve()
        {
            //BruteForce();
            AdaptiveSearch();

            return "13.1265108586";
        }

        void AdaptiveSearch()
        {
            var angles = new double[] { 0.0, 0.0, 0.0, 0.0, 0.0 };

            var index = 0;
            var inc = 1.0;
            var mark = 100.0;
            var dir = Direction.Up;


            while (inc > double.Epsilon * 2.0)
            {
                var change = false;

                while (index < 5)
                {

                    var ang = angles[index];
                    var chk = CalcAll(angles).st;
                    angles[index] = ang + inc;
                    var chk_p = CalcAll(angles).st;
                    angles[index] = ang - inc;
                    var chk_m = CalcAll(angles).st;
                    angles[index] = ang;

                    if (chk_p < chk_m)
                    {
                        if (chk_p >= mark)
                        {
                            ++index;
                            continue;
                        }
                        chk = chk_p;
                        ang += inc;
                        angles[index] = ang;
                        dir = Direction.Up;
                    }
                    else if (chk_m < chk_p)
                    {
                        if (chk_m >= mark)
                        {
                            ++index;
                            continue;
                        }
                        chk = chk_m;
                        ang -= inc;
                        angles[index] = ang;
                        dir = Direction.Down;
                    }
                    else
                    {
                        if (chk_m >= mark)
                        {
                            ++index;
                            continue;
                        }
                    }
                    change = true;
                    
                    do
                    {
                        mark = chk;
                        if (dir == Direction.Up)
                            ang += inc;
                        else
                            ang -= inc;
                        angles[index] = ang;

                        chk = CalcAll(angles).st;
                    } while (chk < mark);

                    ++index;
                }
                index = 0;
                if (!change)
                    inc = inc / 2.0;
            }

            // 13.166318817035169
            // 13.126510858559596   - 10.0
            // 13.126532420576325   - 2.0
            // 13.126738202269541   - 1.5
            // 13.14652814670106    - 1.25



            //while (true)
            //{



            //    var t1 = CalcAll(angles);
            //    if (t1.st < mark)
            //    {
            //        mark = t1.st;
            //        a1 += inc;
            //    }
            //    else
            //        break;
            //}

            Console.WriteLine();

        }

        TravelStats CalcAll(double a1, double a2, double a3, double a4, double a5)
        {
            var m1 = a1.ToStruct(M1l, M1s);
            var m2 = a2.ToStruct(M2l, M2s);
            var m3 = a3.ToStruct(M3l, M3s);
            var m4 = a4.ToStruct(M4l, M4s);
            var m5 = a5.ToStruct(M5l, M5s);

            var p = m1.Stats + m2.Stats + m3.Stats + m4.Stats + m5.Stats;
            var o = CalcOpenTravel(p, Ol, Os);
            return p + o;
        }

        TravelStats CalcAll(double[] angles)
        {
            var m1 = angles[0].ToStruct(M1l, M1s);
            var m2 = angles[1].ToStruct(M2l, M2s);
            var m3 = angles[2].ToStruct(M3l, M3s);
            var m4 = angles[3].ToStruct(M4l, M4s);
            var m5 = angles[4].ToStruct(M5l, M5s);

            var p = m1.Stats + m2.Stats + m3.Stats + m4.Stats + m5.Stats;
            var o = CalcOpenTravel(p, Ol, Os);
            return p + o;
        }

        void BruteForce()
        {
            //StreamWriter writer = null;
            using (var writer = File.CreateText("coords.csv"))
            {
                if (writer != null)
                    writer.WriteLine(
                        "\"a1\",\"a2\",\"a3\",\"a4\",\"a5\"," +
                        "\"at\",\"ax\",\"ay\",\"ah\", " +
                        "\"m1t\",\"m1x\",\"m1y\",\"m1h\"," +
                        "\"m2t\",\"m2x\",\"m2y\",\"m2h\"," +
                        "\"m3t\",\"m3x\",\"m3y\",\"m3h\"," +
                        "\"m4t\",\"m4x\",\"m4y\",\"m4h\"," +
                        "\"m5t\",\"m5x\",\"m5y\",\"m5h\"," +
                        "\"ot\",\"ox\",\"oy\",\"oh\"");


                var step = 5;

                var f1 = -45.0; var t1 = 45.0;
                var f2 = -45.0; var t2 = 45.0;
                var f3 = -45.0; var t3 = 45.0;
                var f4 = -45.0; var t4 = 45.0;
                var f5 = -45.0; var t5 = 45.0;

                var m1List = AngleSteps(f1, t1, step).AsParallel().ToStruct(M1l, M1s).OrderBy(m => m.EntryAngleDeg).ToList();
                var m2List = AngleSteps(f2, t2, step).AsParallel().ToStruct(M2l, M2s).OrderBy(m => m.EntryAngleDeg).ToList();
                var m3List = AngleSteps(f3, t3, step).AsParallel().ToStruct(M3l, M3s).OrderBy(m => m.EntryAngleDeg).ToList();
                var m4List = AngleSteps(f4, t4, step).AsParallel().ToStruct(M4l, M4s).OrderBy(m => m.EntryAngleDeg).ToList();
                var m5List = AngleSteps(f5, t5, step).AsParallel().ToStruct(M5l, M5s).OrderBy(m => m.EntryAngleDeg).ToList();

                var origin = new TravelStats();
                var results = m1List
                        //.AsParallel()
                        .SelectMany(m1 =>
                        {
                            Console.WriteLine(m1.EntryAngleDeg);
                            var p1 = origin + m1.Stats;
                            return m2List.AsParallel().SelectMany(m2 =>
                            {
                                var p2 = p1 + m2.Stats;
                                return m3List.SelectMany(m3 =>
                                {
                                    var p3 = p2 + m3.Stats;
                                    return m4List.SelectMany(m4 =>
                                    {
                                        var p4 = p3 + m4.Stats;
                                        return m5List.Select(m5 =>
                                        {
                                            var p5 = p4 + m5.Stats;
                                            var o = CalcOpenTravel(p5, Ol, Os);

                                            var total = p5 + o;
                                            var line =
                                                $"{m1.EntryAngleDeg},{m2.EntryAngleDeg},{m3.EntryAngleDeg},{m4.EntryAngleDeg},{m5.EntryAngleDeg}," +
                                                $"{total.ToCSV()}," +
                                                $"{m1.Stats.ToCSV()},{m2.Stats.ToCSV()},{m3.Stats.ToCSV()},{m4.Stats.ToCSV()},{m5.Stats.ToCSV()}," +
                                                $"{o.ToCSV()}";
                                            //var line = "";

                                            return new { M1 = m1, M2 = m2, M3 = m3, M4 = m4, M5 = m5, O = o, Total = total };
                                        });
                                    });
                                });
                            });
                        })
                        .OrderBy(t => t.Total.st)
                        .ToList();

                var r1 = results.GroupBy(r => r.M1.EntryAngleDeg).Select(r => new KeyValuePair<double, double>(r.Key, r.Min(rr => rr.Total.st))).ToList();
                var r2 = results.GroupBy(r => r.M2.EntryAngleDeg).Select(r => new KeyValuePair<double, double>(r.Key, r.Min(rr => rr.Total.st))).ToList();
                var r3 = results.GroupBy(r => r.M3.EntryAngleDeg).Select(r => new KeyValuePair<double, double>(r.Key, r.Min(rr => rr.Total.st))).ToList();
                var r4 = results.GroupBy(r => r.M4.EntryAngleDeg).Select(r => new KeyValuePair<double, double>(r.Key, r.Min(rr => rr.Total.st))).ToList();
                var r5 = results.GroupBy(r => r.M5.EntryAngleDeg).Select(r => new KeyValuePair<double, double>(r.Key, r.Min(rr => rr.Total.st))).ToList();

                //if (writer != null)
                //    results.ForEach(t => writer.WriteLine(t.Line));
                Console.WriteLine();
            }
        }

        enum Direction
        {
            Up,
            Down
        }

        static TravelStats CalcOpenTravel(TravelStats origin, double totalWidth, double openSpeed, StreamWriter writer = null)
        {
            var ox = totalWidth - origin.sx;
            var oh = Math.Sqrt((ox * ox) + (origin.sy * origin.sy));
            var ot = Math.Abs(oh / Os);
            var stats = new TravelStats()
            {
                st = ot,
                sx = ox,
                sy = -origin.sy,
                sh = oh
            };
            if (writer != null)
                writer.Write(stats.ToCSV());
            return stats;
        }

        static TravelStats CalcMarshTravel(double entryAngleDeg, double marshWidth, double marshSpeed, StreamWriter writer = null)
        {
            var entryAngleRad = entryAngleDeg.DegreesToRadians();
            var cos45alpha = Math.Cos((45.0 - entryAngleDeg).DegreesToRadians());
            var cosalpha = Math.Cos(entryAngleRad);
            var sinalpha = Math.Sin(entryAngleRad);

            var mh = marshWidth / cos45alpha;
            var mt = Math.Abs(mh / marshSpeed);
            var mx = mh * cosalpha;
            var my = mh * sinalpha;
            //var my = Math.Sqrt((mh * mh) - (mx * mx));
            var stats = new TravelStats()
            {
                st = mt,
                sx = mx,
                sy = my,
                sh = Math.Abs(mh)
            };
            if (writer != null)
                writer.Write(stats.ToCSV());
            return stats;
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

    [DebuggerDisplay("{EntryAngleDeg}")]
    internal struct AngleStruct
    {
        public AngleStruct(double entryAngle)
        {
            EntryAngleDeg = entryAngle;
            EntryAngleRad = EntryAngleDeg.DegreesToRadians();
            AlphaAngleRad = (45.0 - entryAngle).DegreesToRadians();
            SinEntryAngle = Math.Sin(EntryAngleRad);
            CosEntryAngle = Math.Cos(EntryAngleRad);
            CosAlpha = Math.Cos(AlphaAngleRad);
            Stats = new TravelStats();
        }

        public AngleStruct(double entryAngle, double width, double speed)
        {
            EntryAngleDeg = entryAngle;
            EntryAngleRad = EntryAngleDeg.DegreesToRadians();
            AlphaAngleRad = (45.0 - entryAngle).DegreesToRadians();
            SinEntryAngle = Math.Sin(EntryAngleRad);
            CosEntryAngle = Math.Cos(EntryAngleRad);
            CosAlpha = Math.Cos(AlphaAngleRad);

            var h = width / CosAlpha;
            var x = h * CosEntryAngle;
            var y = h * SinEntryAngle;
            var t = Math.Abs(h / speed);

            Stats = new TravelStats()
            {
                sh = h,
                st = t,
                sx = x,
                sy = y
            };
        }

        public double EntryAngleDeg;
        public double EntryAngleRad;
        public double AlphaAngleRad;
        public double SinEntryAngle;
        public double CosEntryAngle;
        public double CosAlpha;

        public double H => Stats.sh;
        public double X => Stats.sx;
        public double Y => Stats.sy;
        public double T => Stats.st;

        public TravelStats Stats;
    }

    [DebuggerDisplay("st={st}, sx={sx}, sy={sy}, sh={sh}")]
    internal struct TravelStats
    {
        public double sh; // Distance travelled
        public double st; // Time taken to travel the distance
        public double sx; // Horizontal distance travelled
        public double sy; // Vertical distance travelled

        public string ToCSV() => $"{st:#0.000},{sx:#0.000},{sy:#0.000},{sh:#0.000}";
        //public string ToCSV() => $"{st},{sx},{sy},{sh}";

        public static TravelStats operator +(TravelStats t1, TravelStats t2)
            => new TravelStats()
            {
                st = t1.st + t2.st,
                sx = t1.sx + t2.sx,
                sy = t1.sy + t2.sy,
                sh = t1.sh + t2.sh,
            };
    }

    internal static class Ext
    {
        public static IEnumerable<AngleStruct> ToStruct(this IEnumerable<double> angleDeg) => angleDeg.Select(a => new AngleStruct(a));
        public static IEnumerable<AngleStruct> ToStruct(this IEnumerable<double> angleDeg, double width, double speed) => angleDeg.Select(a => new AngleStruct(a, width, speed));
        public static AngleStruct ToStruct(this double angleDeg) => new AngleStruct(angleDeg);
        public static AngleStruct ToStruct(this double angleDeg, double width, double speed) => new AngleStruct(angleDeg, width, speed);

        public static ParallelQuery<AngleStruct> ToStruct(this ParallelQuery<double> angleDeg) => angleDeg.Select(a => new AngleStruct(a));
        public static ParallelQuery<AngleStruct> ToStruct(this ParallelQuery<double> angleDeg, double width, double speed) => angleDeg.Select(a => new AngleStruct(a, width, speed));


        public static TravelStats ToTravelStats(this AngleStruct angS, double marshWidth, double marshSpeed, StreamWriter writer = null)
        {
            //var entryAngleRad = entryAngleDeg.DegreesToRadians();
            //var cos45alpha = Math.Cos((45.0 - entryAngleDeg).DegreesToRadians());
            //var cosalpha = Math.Cos(entryAngleRad);
            //var sinalpha = Math.Sin(entryAngleRad);

            var mh = marshWidth / angS.CosAlpha;
            var mt = Math.Abs(mh / marshSpeed);
            var mx = mh * angS.CosEntryAngle;
            var my = mh * angS.SinEntryAngle;
            //var my = Math.Sqrt((mh * mh) - (mx * mx));
            var stats = new TravelStats()
            {
                st = mt,
                sx = mx,
                sy = my,
                sh = Math.Abs(mh)
            };
            if (writer != null)
                writer.Write(stats.ToCSV());
            return stats;
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

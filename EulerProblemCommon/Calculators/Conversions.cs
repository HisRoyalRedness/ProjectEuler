using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HisRoyalRedness.com
{
    public static class AngleConversions
    {
        public const double DEG_TO_RAD = Math.PI / 180.0;
        public const double RAD_TO_DEG = 180.0 / Math.PI;

        public static double DegreesToRadians(this double degree) => degree * DEG_TO_RAD;
        public static double RadiansToDegrees(this double radians) => radians * RAD_TO_DEG;

    }
}

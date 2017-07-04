using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HisRoyalRedness.com
{
    public static class SequenceTransformers
    {
        public static IEnumerable<int> ToNumericSequence(this string data, int digits = 1)
            => (digits == 1) 
                ? data.Select(c => (int)c - 0x30) 
                : ToNumericSequenceInternal(data, digits);

        public static IEnumerable<int> ToNumericSequenceInternal(this string data, int digits)
        {
            if (digits < 2)
                throw new ArgumentException($"{nameof(digits)} must be 2 or greater.", nameof(digits));

            var len = data.Length;
            if (len % digits != 0)
                throw new ArgumentException($"Cannot split the data evenly into {digits} digit numbers.");

            for(var i = 0; i < len; i += digits)
                yield return int.Parse(data.Substring(i, digits));
        }
    }
}

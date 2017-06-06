using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HisRoyalRedness.com
{
    public static class AggregatorExtensions
    {
        #region Sum
        /// <summary>
        /// Calculate the sum of all the elements in the sequence
        /// </summary>
        public static ulong Sum(this IEnumerable<ulong> sequence)
        {
            if (sequence == null)
                throw new ArgumentNullException(nameof(sequence));
            ulong sum = 0;
            foreach (var num in sequence)
                sum += num;
            return sum;
        }

        /// <summary>
        /// Calculate the sum of all the elements in the sequence
        /// </summary>
        public static BigInteger Sum(this IEnumerable<BigInteger> sequence)
        {
            if (sequence == null)
                throw new ArgumentNullException(nameof(sequence));
            BigInteger sum = 0;
            foreach (var num in sequence)
                sum += num;
            return sum;
        }
        #endregion Sum

        #region Product
        /// <summary>
        /// Calculate the sum of all the elements in the sequence
        /// </summary>
        public static ulong Product(this IEnumerable<ulong> sequence)
        {
            if (sequence == null)
                throw new ArgumentNullException(nameof(sequence));
            ulong product = 1;
            foreach (var num in sequence)
                product *= num;
            return product;
        }

        /// <summary>
        /// Calculate the sum of all the elements in the sequence
        /// </summary>
        public static BigInteger Product(this IEnumerable<BigInteger> sequence)
        {
            if (sequence == null)
                throw new ArgumentNullException(nameof(sequence));
            BigInteger product = 1;
            foreach (var num in sequence)
                product *= num;
            return product;
        }
        #endregion Product
    }
}

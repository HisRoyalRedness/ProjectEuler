using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Numerics;
using System.Reflection;
using System.Diagnostics;

namespace fletcher.org.Solutions
{
    [Export(typeof(IProblem))]
    [Solution("997651")]
    class Problem050Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=50
        /// 
        /// The prime 41, can be written as the sum of six consecutive primes:
        /// 
        ///     41 = 2 + 3 + 5 + 7 + 11 + 13
        ///     
        /// This is the longest sum of consecutive primes that adds to a prime 
        /// below one-hundred.
        /// 
        /// The longest sum of consecutive primes below one-thousand that adds 
        /// to a prime, contains 21 terms, and is equal to 953.
        /// 
        /// Which prime, below one-million, can be written as the sum of the 
        /// most consecutive primes?
        /// 
        /// Answer: 997651
        /// </summary>
        public Problem050Generator() { }

        protected override string InternalCalculateSolution()
        {
            ulong limit = 1000000;
            var primes = Prime.FromFile().TakeWhile(p => p < limit).ToList();

            return Attempt3(primes);
        }

        

        string Attempt3(List<ulong> primes)
        {
            var maxCount = 0;
            ulong maxPrime = 0;
            var primeHash = new HashSet<ulong>(primes);
            ulong lastPrime = primes.Last();

            var limit = primes.Count / 2;
            Enumerable.Range(0, limit)
                .AsParallel()
                .ForEach(i =>
                {
                    if (i % 1000 == 0)
                        WriteLine((int)((double)i / (double)limit * 100));

                    var len = primes.Count - (i * 2);

                    if (len > maxCount)
                    {
                        var list = primes.Skip(i).Take(len).ToArray();
                        ulong sum = 0;

                        for (int j = 0; j < len; j++)
                        {
                            sum += list[j];
                            if (j + 1 > maxCount && sum < lastPrime && primeHash.Contains(sum))
                            {
                                maxCount = j + 1;
                                maxPrime = sum;
                            }
                        }
                        for (int j = 0; j < len - 1 && (len - j) > maxCount; j++)
                        {
                            sum -= list[j];
                            if ((len - j) > maxCount && sum < lastPrime && primeHash.Contains(sum))
                            {
                                maxCount = (len - j);
                                maxPrime = sum;
                            }
                        }
                    }
                });

            WriteLine("Prime: {0}   Terms: {1}", maxPrime, maxCount);
            return "";
        }

        string Attempt2(List<ulong> primes)
        {
            var maxCount = 0;
            ulong maxPrime = 0;
            var primeHash = new HashSet<ulong>(primes);

            var len = primes.Count;

            for (int i = 0; i < len && maxCount < len - i; i++)
            {
                if (i % 10000 == 0)
                    WriteLine(i);
                ulong sum = 0;
                for (int j = i; j < len; j++)
                {
                    sum += primes[j];
                    if (primeHash.Contains(sum) && (j - i + 1 > maxCount))
                    {
                        maxPrime = sum;
                        maxCount = j - i + 1;
                    }
                }
            }
            WriteLine(maxPrime);
            return maxPrime.ToString();
        }

        string Attempt1(List<ulong> primes)
        {
            var maxCount = 0;
            ulong maxPrime = 0;

            primes
                .AsParallel()
                .ForEach(prime =>
                {
                    var subList = primes.TakeWhile(p => p < prime).ToList();
                    for (int i = 1; i < subList.Count; i++)
                    {
                        if (subList.Count - i < maxCount)
                            break;

                        for (int j = 0; j < subList.Count - i; j++)
                        {
                            var sum = subList.Skip(i).Take(j).Sum();
                            if (sum > prime)
                                break;
                            if (sum == prime && j > maxCount)
                            {
                                maxCount = j;
                                maxPrime = prime;
                                WriteLine("Prime: {0}  Terms: {1}", maxPrime, maxCount);
                            }
                        }
                    }
                });


            return maxCount.ToString();
        }
        
    }
}

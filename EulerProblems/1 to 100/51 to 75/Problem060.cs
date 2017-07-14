using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
    Euler Problem 60

    Keith Fletcher
    Jul 2017

    This file is Unlicensed.
    See the foot of the file, or refer to <http://unlicense.org>
*/

namespace HisRoyalRedness.com
{
    [Solution("26033")]
    public class Problem60 : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=60
        /// 
        /// The primes 3, 7, 109, and 673, are quite remarkable. By taking any 
        /// two primes and concatenating them in any order the result will 
        /// always be prime. For example, taking 7 and 109, both 7109 and 1097 
        /// are prime. The sum of these four primes, 792, represents the 
        /// lowest sum for a set of four primes with this property.
        /// 
        /// Find the lowest sum for a set of five primes for which any two 
        /// primes concatenate to produce another prime.
        /// 
        /// Answer: 26033
        /// </summary>

        protected override string InternalSolve()
        {
            GetPrimeAt(0);

            var index = 1;
            while(true)
            {
                var pIndex = GetPrimeAt(index);
                var pIndexStr = pIndex.ToString();
                var primePairs = new HashSet<string>();

                for (var i = 0; i < index; ++i)
                {
                    var pI = GetPrimeAt(i);
                    var pIStr = pI.ToString();

                    if (IsPrimePair(pIndexStr, pIStr) && primePairs.All(p => IsPrimePair(p, pIStr)))
                    {
                        primePairs.Add(pIStr);
                        if (primePairs.Count == 4)
                        {
                            primePairs.Add(pIndexStr);
                            return primePairs.Select(p => ulong.Parse(p)).Sum().ToString();
                        }
                    }

                }
                ++index;
            }
        }

        ulong GetPrimeAt(int index)
        {
            while (_cacheIndex < index)
                CacheNextPrime();
            return _primesList[index];
        }


        ulong GetNextPrime()
        {
            while (_cacheIndex < _readIndex)
                CacheNextPrime();
            return _primesList[_readIndex++];
        }

        bool IsPrime(ulong number)
        {
            while (number > _max)
                CacheNextPrime();
            return _primes.Contains(number);
        }

        bool IsPrimePair(string num1S, string num2S) => IsPrime(ulong.Parse(num1S + num2S)) && IsPrime(ulong.Parse(num2S + num1S));

        void CacheNextPrime()
        {
            if (_primeEnum.MoveNext())
            {
                _max = _primeEnum.Current;
                _primes.Add(_max);
                _primesList.Add(_max);
                ++_cacheIndex;
            }
        }

        int _readIndex = 0;
        int _cacheIndex = -1;
        IEnumerator<ulong> _primeEnum = Primes.Sequence().GetEnumerator();
        ulong _max = 0;
        readonly HashSet<ulong> _primes = new HashSet<ulong>();
        readonly List<ulong> _primesList = new List<ulong>();
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

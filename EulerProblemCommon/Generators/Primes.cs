using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/*
    Common sequence generators

    This file is Unlicensed.
    See the foot of the file, or refer to <http://unlicense.org>
*/

namespace HisRoyalRedness.com
{
    public static class Primes
    {
        static Primes()
        {
            _maxStop = primesieve_get_max_stop();
        }

        #region Prime counts
        [DllImport("libprimesieve.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "primesieve_count_primes")]
        public static extern ulong Count(ulong start, ulong stop);
        [DllImport("libprimesieve.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "primesieve_count_twins")]
        public static extern ulong CountTwins(ulong start, ulong stop);
        [DllImport("libprimesieve.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "primesieve_count_triplets")]
        public static extern ulong CountTriplets(ulong start, ulong stop);
        [DllImport("libprimesieve.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "primesieve_count_quadruplets")]
        public static extern ulong CountQuadruplets(ulong start, ulong stop);
        [DllImport("libprimesieve.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "primesieve_count_quintuplets")]
        public static extern ulong CountQuintuplets(ulong start, ulong stop);
        [DllImport("libprimesieve.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "primesieve_count_sextuplets")]
        public static extern ulong CountSextuplets(ulong start, ulong stop);
        #endregion Prime counts

        #region Prime meta
        [DllImport("libprimesieve.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern UInt64 primesieve_get_max_stop();
        [DllImport("libprimesieve.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 primesieve_get_sieve_size();
        [DllImport("libprimesieve.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 primesieve_get_num_threads();
        [DllImport("libprimesieve.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr primesieve_version();

        public static string PrimesieveVersion => _version.Value;
        static Lazy<string> _version = new Lazy<string>(() => Marshal.PtrToStringAnsi(primesieve_version()));

        public static UInt64 MaxStop => _maxStop;
        public static int SieveSize => primesieve_get_sieve_size();
        public static int Threads => primesieve_get_num_threads();
        #endregion Prime meta

        #region Prime iteration
        [StructLayout(LayoutKind.Sequential)]
        public struct PrimesieveIterator
        {
          public UInt64 i_;
          public UInt64 last_idx_;
          public IntPtr primes_;
          public IntPtr primes_pimpl_;
          public UInt64 start_;
          public UInt64 stop_;
          public UInt64 stop_hint_;
          public UInt64 tiny_cache_size_;
          public Int32 is_error_;
        }

        [DllImport("libprimesieve.dll", CallingConvention = CallingConvention.Cdecl)]        
        private static extern void primesieve_init(ref PrimesieveIterator iterator);
        [DllImport("libprimesieve.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void primesieve_free_iterator(ref PrimesieveIterator iterator);
        [DllImport("libprimesieve.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern UInt64 primesieve_next_prime(ref PrimesieveIterator iterator);
        [DllImport("libprimesieve.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void primesieve_skipto(ref PrimesieveIterator iterator, UInt64 start, UInt64 stop_hint);
        [DllImport("libprimesieve.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "primesieve_nth_prime")]
        public static extern UInt64 NthPrime(UInt64 n, UInt64 start = 0);

        public static IEnumerable<UInt64> Sequence(UInt64 start = 0) => new InternalPrimeEnumerable(start, _maxStop);
        public static IEnumerable<UInt64> Sequence(UInt64 start, UInt64 stopHint) => new InternalPrimeEnumerable(start, stopHint);

        #region InternalPrimeEnumerable
        private class InternalPrimeEnumerable : IEnumerable<UInt64>
        {
            public InternalPrimeEnumerable(UInt64 start, UInt64 maxPrime)
            {
                _start = start;
                _maxPrime = maxPrime;
            }

            public IEnumerator<ulong> GetEnumerator() => new InternalPrimeEnumerator(_start, _maxPrime);
            IEnumerator IEnumerable.GetEnumerator() => new InternalPrimeEnumerator(_start, _maxPrime);

            #region InternalPrimeEnumerator
            private class InternalPrimeEnumerator : IEnumerator<UInt64>
            {
                public InternalPrimeEnumerator(UInt64 start, UInt64 maxPrime)
                {
                    _maxPrime = maxPrime;
                    primesieve_init(ref _iterator);
                    if (start != 0 || maxPrime != _maxStop)
                        primesieve_skipto(ref _iterator, start, maxPrime);
                }

                public ulong Current => _currentPrime;
                object IEnumerator.Current => _currentPrime;

                public bool MoveNext()
                {
                    _currentPrime = primesieve_next_prime(ref _iterator);
                    return _currentPrime < _maxPrime;
                }

                public void Reset()
                { throw new NotImplementedException(); }

                #region IDisposable Support
                protected virtual void Dispose(bool disposing)
                {
                    if (!_disposed)
                    {
                        if (disposing)
                        {
                            // TODO: dispose managed state (managed objects).
                        }
                        primesieve_free_iterator(ref _iterator);
                        _disposed = true;
                    }
                }
                bool _disposed = false;

                ~InternalPrimeEnumerator() => Dispose(false);

                public void Dispose()
                {
                    Dispose(true);
                    GC.SuppressFinalize(this);
                }
                #endregion

                UInt64 _currentPrime = 0;
                UInt64 _maxPrime;
                PrimesieveIterator _iterator = new PrimesieveIterator();
            }
            #endregion InternalPrimeEnumerator

            UInt64 _start;
            UInt64 _maxPrime;
        }
        #endregion InternalPrimeEnumerable

        #endregion Prime iteration

        readonly static UInt64 _maxStop;
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

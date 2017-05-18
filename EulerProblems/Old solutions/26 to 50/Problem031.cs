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
    [Solution("73682")]
    class Problem031Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=31
        ///
        /// In England the currency is made up of pound, £, and
        /// pence, p, and there are eight coins in general circulation:
        ///
        ///     1p, 2p, 5p, 10p, 20p, 50p, £1 (100p) and £2 (200p).
        ///
        /// It is possible to make £2 in the following way:
        ///
        ///     1×£1 + 1×50p + 2×20p + 1×5p + 1×2p + 3×1p
        ///
        /// How many different ways can £2 be made using any
        /// number of coins?
        ///
        /// Answer: 73682
        /// </summary>
        public Problem031Generator() { }

        protected override string InternalCalculateSolution()
        {
            return Calc(Currencies.First()).Count().ToString();
        }

        IEnumerable<string> Calc(Currency currency, List<string> runningSymbol = null, int runningValue = 0)
        {
            if (currency == null)
                yield break;

            var childCurrency = Currencies.FirstOrDefault(c => c.Id == currency.Id + 1);

            var value = runningValue;
            var list = new List<string>();
            if (runningSymbol != null)
                list.AddRange(runningSymbol);

            while (value <= 200)
            {
                if (value == 200)
                    yield return list.ToCSV();
                else
                {
                    foreach (var item in Calc(childCurrency, list, value))
                        yield return item;
                }

                value += currency.Value;
                list.Add(currency.Name);
            }
        }

        IEnumerable<Currency> Currencies
        {
            get
            {
                yield return new Currency(1, "£2", 200);
                yield return new Currency(2, "£1", 100);
                yield return new Currency(3, "50p", 50);
                yield return new Currency(4, "20p", 20);
                yield return new Currency(5, "10p", 10);
                yield return new Currency(6, "5p", 5);
                yield return new Currency(7, "2p", 2);
                yield return new Currency(8, "1p", 1);
            }
        }

        [DebuggerDisplay("{Name}")]
        class Currency
        {
            public Currency(int id, string name, int value)
            {
                Id = id;
                Name = name;
                Value = value;
            }

            public int Id { get; private set; }
            public string Name { get; private set; }
            public int Value { get; private set; }
        }
    }
}

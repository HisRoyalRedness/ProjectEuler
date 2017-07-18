using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HisRoyalRedness.com;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace HisRoyalRedness.com.Test
{
    [TestClass]
    public class FractionTests
    {
        [TestMethod]
        public void TestFractionCreation()
        {
            var f_int = Fraction.Create((int)1, (int)2);
            var f_uint = Fraction.Create((uint)3, (uint)4);
            var f_long = Fraction.Create((long)5, (long)6);
            var f_ulong = Fraction.Create((ulong)7, (ulong)8);
            var f_bigint = Fraction.Create((BigInteger)9, (BigInteger)10);

            f_int.Numerator.Should().BeOfType(typeof(int));
            f_uint.Numerator.Should().BeOfType(typeof(uint));
            f_long.Numerator.Should().BeOfType(typeof(long));
            f_ulong.Numerator.Should().BeOfType(typeof(ulong));
            f_bigint.Numerator.Should().BeOfType(typeof(BigInteger));

            f_int.Numerator.Should().Be(1);
            f_uint.Numerator.Should().Be(3);
            f_long.Numerator.Should().Be(5);
            f_ulong.Numerator.Should().Be(7);
            f_bigint.Numerator.Should().Be(9);

            f_int.Denominator.Should().Be(2);
            f_uint.Denominator.Should().Be(4);
            f_long.Denominator.Should().Be(6);
            f_ulong.Denominator.Should().Be(8);
            f_bigint.Denominator.Should().Be(10);
        }

        [TestMethod]
        public void TestFractionDivideByZero()
        {
            new Action(() => Fraction.Create(1, 0)).ShouldThrow<DivideByZeroException>();

            var f = Fraction.Create(0, 1);
            new Action(() => { var i = f.Reciprocal; }).ShouldThrow<DivideByZeroException>();
        }

        [TestMethod]
        public void TestFractionEvaluate()
        {
            Fraction.Create(1, 2).Evaluate().Should().Be(0.5);
            Fraction.Create(1, 3).Evaluate().Should().Be(1.0/3.0);
            Fraction.Create(3, 1).Evaluate().Should().Be(3.0);
            Fraction.Create(0, 3).Evaluate().Should().Be(0.0);
        }

        [TestMethod]
        public void TestFraction_Addition()
        {
            // Same denominator
            var f1 = Fraction.Create(1, 3);
            var f2 = Fraction.Create(2, 3);

            (f1 + f2).Evaluate().Should().Be(1.0);
            (f2 + f1).Evaluate().Should().Be(1.0);

            // Denominator of 1
            f1 = Fraction.Create(1, 3);
            f2 = Fraction.Create(1, 1);

            (f1 + f2).Evaluate().Should().Be(4.0 / 3.0);
            (f2 + f1).Evaluate().Should().Be(4.0 / 3.0);

            // Different denominators
            f1 = Fraction.Create(1, 3);
            f2 = Fraction.Create(1, 2);

            (f1 + f2).Evaluate().Should().Be(5.0 / 6.0);
            (f2 + f1).Evaluate().Should().Be(5.0 / 6.0);

            // Constants
            f1 = Fraction.Create(1, 3);
            var c1 = 1;
            var c2 = 0;

            (f1 + c1).Evaluate().Should().Be(4.0 / 3.0);
            (c1 + f1).Evaluate().Should().Be(4.0 / 3.0);
            (f1 + c2).Evaluate().Should().Be(1.0 / 3.0);
            (c2 + f1).Evaluate().Should().Be(1.0 / 3.0);
        }

        [TestMethod]
        public void TestFraction_Subtraction()
        {
            // Same denominator
            var f1 = Fraction.Create(1, 3);
            var f2 = Fraction.Create(2, 3);

            (f1 - f2).Evaluate().Should().Be(-1.0 / 3.0);
            (f2 - f1).Evaluate().Should().Be(1.0 / 3.0);

            // Denominator of 1
            f1 = Fraction.Create(1, 3);
            f2 = Fraction.Create(1, 1);

            (f1 - f2).Evaluate().Should().Be(-2.0 / 3.0);
            (f2 - f1).Evaluate().Should().Be(2.0 / 3.0);

            // Different denominators
            f1 = Fraction.Create(1, 3);
            f2 = Fraction.Create(1, 2);

            (f1 - f2).Evaluate().Should().Be(-1.0 / 6.0);
            (f2 - f1).Evaluate().Should().Be(1.0 / 6.0);

            // Constants
            f1 = Fraction.Create(1, 3);
            var c1 = 1;
            var c2 = 0;

            (f1 - c1).Evaluate().Should().Be(-2.0 / 3.0);
            (c1 - f1).Evaluate().Should().Be(2.0 / 3.0);
            (f1 - c2).Evaluate().Should().Be(1.0 / 3.0);
            (c2 - f1).Evaluate().Should().Be(-1.0 / 3.0);
        }

        [TestMethod]
        public void TestFraction_Multiplication()
        {
            // Fractions
            var f1 = Fraction.Create(1, 3);
            var f2 = Fraction.Create(2, 3);

            (f1 * f2).Evaluate().Should().Be(2.0 / 9.0);
            (f2 * f1).Evaluate().Should().Be(2.0 / 9.0);

            // Constants
            f1 = Fraction.Create(1, 3);
            var c1 = 1;
            var c2 = 0;

            (f1 * c1).Evaluate().Should().Be(1.0 / 3.0);
            (c1 * f1).Evaluate().Should().Be(1.0 / 3.0);
            (f1 * c2).Evaluate().Should().Be(0.0);
            (c2 * f1).Evaluate().Should().Be(0.0);
        }

        [TestMethod]
        public void TestFraction_Division()
        {
            // Fractions
            var f1 = Fraction.Create(1, 3);
            var f2 = Fraction.Create(2, 3);

            (f1 / f2).Evaluate().Should().Be(1.0 / 2.0);
            (f2 / f1).Evaluate().Should().Be(2.0 / 1.0);

            // Constants
            f1 = Fraction.Create(1, 3);
            var c1 = 1;
            var c2 = 0;

            (f1 / c1).Evaluate().Should().Be(1.0 / 3.0);
            (c1 / f1).Evaluate().Should().Be(3.0);
            // f1 / c2 =  Divide by zero
            (c2 / f1).Evaluate().Should().Be(0.0);
        }
    }
}
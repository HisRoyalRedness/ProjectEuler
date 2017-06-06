using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HisRoyalRedness.com;
using FluentAssertions;

namespace HisRoyalRedness.com.Test
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void TestGCDWithMultipleParameters()
        {
            Calculators.GCD(54, 24, 12).ShouldBeEquivalentTo(6);
        }
    }
}

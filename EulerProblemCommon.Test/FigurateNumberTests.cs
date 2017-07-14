using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HisRoyalRedness.com;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;

namespace HisRoyalRedness.com.Test
{
    [TestClass]
    public class FigurateNumberTests
    {
        [TestMethod]
        public void TestTriangleNumbersSequenceWithDefaultStart()
        {
            TriangleNumber.Sequence(0).Take(_testTriangleSequence.Count).Should().Equal(_testTriangleSequence);
        }

        [TestMethod]
        public void TestTriangleNumbersSequenceWithOffsetStart()
        {
            TriangleNumber.Sequence(10).Take(_testTriangleSequence.Count-10).Should().Equal(_testTriangleSequence.Skip(10));
        }

        [TestMethod]
        public void TestTriangleNumbersAtIndex()
        {
            for (var i = 0; i < _testTriangleSequence.Count; ++i)
                TriangleNumber.AtIndex(i).Should().Be(_testTriangleSequence[i]);
        }

        [TestMethod]
        public void TestSquareNumbersSequenceWithDefaultStart()
        {
            SquareNumber.Sequence(0).Take(_testSquareSequence.Count).Should().Equal(_testSquareSequence);
        }

        [TestMethod]
        public void TestSquareNumbersSequenceWithOffsetStart()
        {
            SquareNumber.Sequence(10).Take(_testSquareSequence.Count - 10).Should().Equal(_testSquareSequence.Skip(10));
        }

        [TestMethod]
        public void TestSquareNumbersAtIndex()
        {
            for (var i = 0; i < _testSquareSequence.Count; ++i)
                SquareNumber.AtIndex(i).Should().Be(_testSquareSequence[i]);
        }

        // https://oeis.org/A000217
        static List<ulong> _testTriangleSequence = new List<ulong>(new ulong[] 
        {
            0, 1, 3, 6, 10, 15, 21, 28, 36, 45, 55, 66, 78, 91, 105, 120, 136, 153, 171, 190, 210, 231, 253, 276, 300, 325, 351, 378, 406, 435, 465, 496, 528, 561, 595, 630, 666, 703, 741, 780, 820, 861, 903, 946, 990, 1035, 1081, 1128, 1176, 1225, 1275, 1326, 1378, 1431
        });

        // https://oeis.org/A000290
        static List<ulong> _testSquareSequence = new List<ulong>(new ulong[]
        {
            0, 1, 4, 9, 16, 25, 36, 49, 64, 81, 100, 121, 144, 169, 196, 225, 256, 289, 324, 361, 400, 441, 484, 529, 576, 625, 676, 729, 784, 841, 900, 961, 1024, 1089, 1156, 1225, 1296, 1369, 1444, 1521, 1600, 1681, 1764, 1849, 1936, 2025, 2116, 2209, 2304, 2401, 2500
        });
    }
}

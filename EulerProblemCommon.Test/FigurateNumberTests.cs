using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HisRoyalRedness.com;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;

namespace HisRoyalRedness.com.Test
{
    [TestCategory("Figurate Numbers")]
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

        [TestMethod]
        public void TestPentagonalNumbersSequenceWithDefaultStart()
        {
            PentagonalNumber.Sequence(0).Take(_testPentagonalSequence.Count).Should().Equal(_testPentagonalSequence);
        }

        [TestMethod]
        public void TestPentagonalNumbersSequenceWithOffsetStart()
        {
            PentagonalNumber.Sequence(10).Take(_testPentagonalSequence.Count - 10).Should().Equal(_testPentagonalSequence.Skip(10));
        }

        [TestMethod]
        public void TestPentagonalNumbersAtIndex()
        {
            for (var i = 0; i < _testPentagonalSequence.Count; ++i)
                PentagonalNumber.AtIndex(i).Should().Be(_testPentagonalSequence[i]);
        }

        [TestMethod]
        public void TestHexagonalNumbersSequenceWithDefaultStart()
        {
            HexagonalNumber.Sequence(0).Take(_testHexagonalSequence.Count).Should().Equal(_testHexagonalSequence);
        }

        [TestMethod]
        public void TestHexagonalNumbersSequenceWithOffsetStart()
        {
            HexagonalNumber.Sequence(10).Take(_testHexagonalSequence.Count - 10).Should().Equal(_testHexagonalSequence.Skip(10));
        }

        [TestMethod]
        public void TestHexagonalNumbersAtIndex()
        {
            for (var i = 0; i < _testHexagonalSequence.Count; ++i)
                HexagonalNumber.AtIndex(i).Should().Be(_testHexagonalSequence[i]);
        }

        [TestMethod]
        public void TestHeptagonalNumbersSequenceWithDefaultStart()
        {
            HeptagonalNumber.Sequence(0).Take(_testHeptagonalSequence.Count).Should().Equal(_testHeptagonalSequence);
        }

        [TestMethod]
        public void TestHeptagonalNumbersSequenceWithOffsetStart()
        {
            HeptagonalNumber.Sequence(10).Take(_testHeptagonalSequence.Count - 10).Should().Equal(_testHeptagonalSequence.Skip(10));
        }

        [TestMethod]
        public void TestHeptagonalNumbersAtIndex()
        {
            for (var i = 0; i < _testHeptagonalSequence.Count; ++i)
                HeptagonalNumber.AtIndex(i).Should().Be(_testHeptagonalSequence[i]);
        }

        [TestMethod]
        public void TestOctagonalNumbersSequenceWithDefaultStart()
        {
            OctagonalNumber.Sequence(0).Take(_testOctagonalSequence.Count).Should().Equal(_testOctagonalSequence);
        }

        [TestMethod]
        public void TestOctagonalNumbersSequenceWithOffsetStart()
        {
            OctagonalNumber.Sequence(10).Take(_testOctagonalSequence.Count - 10).Should().Equal(_testOctagonalSequence.Skip(10));
        }

        [TestMethod]
        public void TestOctagonalNumbersAtIndex()
        {
            for (var i = 0; i < _testOctagonalSequence.Count; ++i)
                OctagonalNumber.AtIndex(i).Should().Be(_testOctagonalSequence[i]);
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

        // https://oeis.org/A000326
        static List<ulong> _testPentagonalSequence = new List<ulong>(new ulong[]
        {
            0, 1, 5, 12, 22, 35, 51, 70, 92, 117, 145, 176, 210, 247, 287, 330, 376, 425, 477, 532, 590, 651, 715, 782, 852, 925, 1001, 1080, 1162, 1247, 1335, 1426, 1520, 1617, 1717, 1820, 1926, 2035, 2147, 2262, 2380, 2501, 2625, 2752, 2882, 3015, 3151
        });

        // https://oeis.org/A000384
        static List<ulong> _testHexagonalSequence = new List<ulong>(new ulong[]
        {
            0, 1, 6, 15, 28, 45, 66, 91, 120, 153, 190, 231, 276, 325, 378, 435, 496, 561, 630, 703, 780, 861, 946, 1035, 1128, 1225, 1326, 1431, 1540, 1653, 1770, 1891, 2016, 2145, 2278, 2415, 2556, 2701, 2850, 3003, 3160, 3321, 3486, 3655, 3828, 4005, 4186, 4371, 4560
        });

        // https://oeis.org/A000566
        static List<ulong> _testHeptagonalSequence = new List<ulong>(new ulong[]
        {
            0, 1, 7, 18, 34, 55, 81, 112, 148, 189, 235, 286, 342, 403, 469, 540, 616, 697, 783, 874, 970, 1071, 1177, 1288, 1404, 1525, 1651, 1782, 1918, 2059, 2205, 2356, 2512, 2673, 2839, 3010, 3186, 3367, 3553, 3744, 3940, 4141, 4347, 4558, 4774, 4995, 5221, 5452, 5688
        });

        // https://oeis.org/A000567
        static List<ulong> _testOctagonalSequence = new List<ulong>(new ulong[]
        {
            0, 1, 8, 21, 40, 65, 96, 133, 176, 225, 280, 341, 408, 481, 560, 645, 736, 833, 936, 1045, 1160, 1281, 1408, 1541, 1680, 1825, 1976, 2133, 2296, 2465, 2640, 2821, 3008, 3201, 3400, 3605, 3816, 4033, 4256, 4485, 4720, 4961, 5208, 5461
        });

    }
}

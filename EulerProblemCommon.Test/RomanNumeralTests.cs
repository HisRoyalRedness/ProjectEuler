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
    public class RomanNumeralTests
    {
        [TestMethod]
        public void RomanNumeralCreationNonSubtractive()
        {
            1.ToRomanNumerals(false).Should().Be("I");
            4.ToRomanNumerals(false).Should().Be("IIII");
            5.ToRomanNumerals(false).Should().Be("V");
            6.ToRomanNumerals(false).Should().Be("VI");
            9.ToRomanNumerals(false).Should().Be("VIIII");
            10.ToRomanNumerals(false).Should().Be("X");
            11.ToRomanNumerals(false).Should().Be("XI");
            14.ToRomanNumerals(false).Should().Be("XIIII");
            15.ToRomanNumerals(false).Should().Be("XV");
            16.ToRomanNumerals(false).Should().Be("XVI");
            19.ToRomanNumerals(false).Should().Be("XVIIII");
            20.ToRomanNumerals(false).Should().Be("XX");
            21.ToRomanNumerals(false).Should().Be("XXI");
            24.ToRomanNumerals(false).Should().Be("XXIIII");
            25.ToRomanNumerals(false).Should().Be("XXV");
            26.ToRomanNumerals(false).Should().Be("XXVI");
            49.ToRomanNumerals(false).Should().Be("XXXXVIIII");
            50.ToRomanNumerals(false).Should().Be("L");
            51.ToRomanNumerals(false).Should().Be("LI");
            56.ToRomanNumerals(false).Should().Be("LVI");
            61.ToRomanNumerals(false).Should().Be("LXI");
            66.ToRomanNumerals(false).Should().Be("LXVI");
            99.ToRomanNumerals(false).Should().Be("LXXXXVIIII");
            100.ToRomanNumerals(false).Should().Be("C");
            101.ToRomanNumerals(false).Should().Be("CI");
            106.ToRomanNumerals(false).Should().Be("CVI");
            111.ToRomanNumerals(false).Should().Be("CXI");
            116.ToRomanNumerals(false).Should().Be("CXVI");
            151.ToRomanNumerals(false).Should().Be("CLI");
            156.ToRomanNumerals(false).Should().Be("CLVI");
            161.ToRomanNumerals(false).Should().Be("CLXI");
            166.ToRomanNumerals(false).Should().Be("CLXVI");
            499.ToRomanNumerals(false).Should().Be("CCCCLXXXXVIIII");
            500.ToRomanNumerals(false).Should().Be("D");
            501.ToRomanNumerals(false).Should().Be("DI");
            506.ToRomanNumerals(false).Should().Be("DVI");
            511.ToRomanNumerals(false).Should().Be("DXI");
            516.ToRomanNumerals(false).Should().Be("DXVI");
            551.ToRomanNumerals(false).Should().Be("DLI");
            556.ToRomanNumerals(false).Should().Be("DLVI");
            561.ToRomanNumerals(false).Should().Be("DLXI");
            566.ToRomanNumerals(false).Should().Be("DLXVI");
            600.ToRomanNumerals(false).Should().Be("DC");
            601.ToRomanNumerals(false).Should().Be("DCI");
            606.ToRomanNumerals(false).Should().Be("DCVI");
            611.ToRomanNumerals(false).Should().Be("DCXI");
            616.ToRomanNumerals(false).Should().Be("DCXVI");
            651.ToRomanNumerals(false).Should().Be("DCLI");
            656.ToRomanNumerals(false).Should().Be("DCLVI");
            661.ToRomanNumerals(false).Should().Be("DCLXI");
            666.ToRomanNumerals(false).Should().Be("DCLXVI");
            999.ToRomanNumerals(false).Should().Be("DCCCCLXXXXVIIII");
            1000.ToRomanNumerals(false).Should().Be("M");
            1001.ToRomanNumerals(false).Should().Be("MI");
            1006.ToRomanNumerals(false).Should().Be("MVI");
            1011.ToRomanNumerals(false).Should().Be("MXI");
            1016.ToRomanNumerals(false).Should().Be("MXVI");
            1051.ToRomanNumerals(false).Should().Be("MLI");
            1056.ToRomanNumerals(false).Should().Be("MLVI");
            1061.ToRomanNumerals(false).Should().Be("MLXI");
            1066.ToRomanNumerals(false).Should().Be("MLXVI");
            1100.ToRomanNumerals(false).Should().Be("MC");
            1101.ToRomanNumerals(false).Should().Be("MCI");
            1106.ToRomanNumerals(false).Should().Be("MCVI");
            1111.ToRomanNumerals(false).Should().Be("MCXI");
            1116.ToRomanNumerals(false).Should().Be("MCXVI");
            1151.ToRomanNumerals(false).Should().Be("MCLI");
            1156.ToRomanNumerals(false).Should().Be("MCLVI");
            1161.ToRomanNumerals(false).Should().Be("MCLXI");
            1166.ToRomanNumerals(false).Should().Be("MCLXVI");
            1500.ToRomanNumerals(false).Should().Be("MD");
            1501.ToRomanNumerals(false).Should().Be("MDI");
            1506.ToRomanNumerals(false).Should().Be("MDVI");
            1511.ToRomanNumerals(false).Should().Be("MDXI");
            1516.ToRomanNumerals(false).Should().Be("MDXVI");
            1551.ToRomanNumerals(false).Should().Be("MDLI");
            1556.ToRomanNumerals(false).Should().Be("MDLVI");
            1561.ToRomanNumerals(false).Should().Be("MDLXI");
            1566.ToRomanNumerals(false).Should().Be("MDLXVI");
            1600.ToRomanNumerals(false).Should().Be("MDC");
            1601.ToRomanNumerals(false).Should().Be("MDCI");
            1606.ToRomanNumerals(false).Should().Be("MDCVI");
            1611.ToRomanNumerals(false).Should().Be("MDCXI");
            1616.ToRomanNumerals(false).Should().Be("MDCXVI");
            1651.ToRomanNumerals(false).Should().Be("MDCLI");
            1656.ToRomanNumerals(false).Should().Be("MDCLVI");
            1661.ToRomanNumerals(false).Should().Be("MDCLXI");
            1666.ToRomanNumerals(false).Should().Be("MDCLXVI");
        }
    }
}
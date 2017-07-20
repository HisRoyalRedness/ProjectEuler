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
        public void RomanNumeralParsing()
        {
            /*        
                I = 1
                V = 5
                X = 10
                L = 50
                C = 100
                D = 500
                M = 1000

                Basic rules:

                i.    Numerals must be arranged in descending order of size.
                ii.   M, C, and X cannot be equalled or exceeded by smaller denominations.
                iii.  D, L, and V can each only appear once.

                Subtractive rules:

                i.    Only one I, X, and C can be used as the leading numeral in part of a subtractive pair.
                ii.   I can only be placed before V and X.
                iii.  X can only be placed before L and C.
                iv.   C can only be placed before D and M.
            */
            "MCMDCDCXCLXLXIXVIVI".FromRomanNumerals().Should().Be(3109);
            "MCMXLIV".FromRomanNumerals().Should().Be(1944);
        }

        [TestMethod]
        public void RomanNumeralParsing_InvalidNumber()
        {
            "".FromRomanNumerals().Should().Be(0, "an empty string equates to 0");
            ((string)null).FromRomanNumerals().Should().Be(0, "an null string equates to 0");

            "W".FromRomanNumerals(false).Should().Be(0, "an invalid string equates to 0 if not throwing");
            var act = new Action(() => "W".FromRomanNumerals());
            act.ShouldThrow<InvalidOperationException>("an invalid string yields an InvalidOperationException when throwing");
        }


        [TestMethod]
        public void RomanNumeralCreationNonSubtractive()
        {
            // Basics
            1.ToRomanNumerals(false).Should().Be("I");
            2.ToRomanNumerals(false).Should().Be("II");
            3.ToRomanNumerals(false).Should().Be("III");
            4.ToRomanNumerals(false).Should().Be("IIII");
            5.ToRomanNumerals(false).Should().Be("V");
            6.ToRomanNumerals(false).Should().Be("VI");
            7.ToRomanNumerals(false).Should().Be("VII");
            8.ToRomanNumerals(false).Should().Be("VIII");
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

        [TestMethod]
        public void RomanNumeralCreationSubtractive()
        {
            // Basics
            1.ToRomanNumerals().Should().Be("I");
            2.ToRomanNumerals().Should().Be("II");
            3.ToRomanNumerals().Should().Be("III");
            4.ToRomanNumerals().Should().Be("IV");
            5.ToRomanNumerals().Should().Be("V");
            6.ToRomanNumerals().Should().Be("VI");
            7.ToRomanNumerals().Should().Be("VII");
            8.ToRomanNumerals().Should().Be("VIII");
            9.ToRomanNumerals().Should().Be("IX");
            10.ToRomanNumerals().Should().Be("X");

            14.ToRomanNumerals().Should().Be("XIV");
            49.ToRomanNumerals().Should().Be("XLIX");
            54.ToRomanNumerals().Should().Be("LIV");
            59.ToRomanNumerals().Should().Be("LIX");
            64.ToRomanNumerals().Should().Be("LXIV");
            99.ToRomanNumerals().Should().Be("XCIX");
        }
    }
}
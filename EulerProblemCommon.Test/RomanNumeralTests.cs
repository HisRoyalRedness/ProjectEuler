using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HisRoyalRedness.com;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace HisRoyalRedness.com.Test
{
    [TestCategory("Roman Numerals")]
    [TestClass]
    public class RomanNumeralTests
    {
        [TestMethod]
        public void RomanNumeralParsing_Basic()
        {
            "M".FromRomanNumerals().Should().Be(1000);
            "D".FromRomanNumerals().Should().Be(500);
            "C".FromRomanNumerals().Should().Be(100);
            "L".FromRomanNumerals().Should().Be(50);
            "X".FromRomanNumerals().Should().Be(10);
            "V".FromRomanNumerals().Should().Be(5);
            "I".FromRomanNumerals().Should().Be(1);

            "CM".FromRomanNumerals().Should().Be(900);
            "CD".FromRomanNumerals().Should().Be(400);
            "XC".FromRomanNumerals().Should().Be(90);
            "XL".FromRomanNumerals().Should().Be(40);
            "IX".FromRomanNumerals().Should().Be(9);
            "IV".FromRomanNumerals().Should().Be(4);


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
        }

        [TestMethod]
        public void RomanNumeralParsing_ArrangementRules()
        {
            // i.    Numerals must be arranged in descending order of size.

            TestDigitOrdering("M", 1000, "D", 500);
            TestDigitOrdering("M", 1000, "C", 100, true); // CM allowed - subtractive
            TestDigitOrdering("M", 1000, "L", 50);
            TestDigitOrdering("M", 1000, "X", 10);
            TestDigitOrdering("M", 1000, "V", 5);
            TestDigitOrdering("M", 1000, "I", 1);

            TestDigitOrdering("D", 500, "C", 100, true); // CD allowed - subtractive
            TestDigitOrdering("D", 500, "L", 50);
            TestDigitOrdering("D", 500, "X", 10);
            TestDigitOrdering("D", 500, "V", 5);
            TestDigitOrdering("D", 500, "I", 1);

            TestDigitOrdering("C", 100, "L", 50);
            TestDigitOrdering("C", 100, "X", 10, true); // XC allowed - subtractive
            TestDigitOrdering("C", 100, "V", 5);
            TestDigitOrdering("C", 100, "I", 1);

            TestDigitOrdering("L", 50, "X", 10, true); // XL allowed - subtractive
            TestDigitOrdering("L", 50, "V", 5);
            TestDigitOrdering("L", 50, "I", 1);

            TestDigitOrdering("X", 10, "V", 5);
            TestDigitOrdering("X", 10, "I", 1, true); // IX allowed  - subtractive

            TestDigitOrdering("V", 5, "I", 1, true); // IV allowed - subtractive
        }

        void TestDigitOrdering(string digit1, ulong digit1Value, string digit2, ulong digit2Value, bool subtractive = false)
        {
            $"{digit1}{digit2}".FromRomanNumerals().Should().Be(digit1Value + digit2Value, $"{digit1}{digit2} should equate to {digit1Value + digit2Value}");
            if (subtractive)
                $"{digit2}{digit1}".FromRomanNumerals().Should().Be(digit1Value - digit2Value, $"{digit2}{digit1} should equate to {digit2Value - digit1Value}");
            else
            {
                $"{digit2}{digit1}".FromRomanNumerals(false).Should().Be(0, $"{digit2} should not appear before {digit1}");
                new Action(() => $"{digit2}{digit1}".FromRomanNumerals()).ShouldThrow<InvalidOperationException>($"{digit2} should not appear before {digit1}");
            }
        }

        [TestMethod]
        public void RomanNumeralParsing_MCXSum()
        {
            // ii.   M, C, and X cannot be equalled or exceeded by smaller denominations.

            "VV".FromRomanNumerals(false).Should().Be(0, "X cannot be exceeded by smaller denominations");
            "VIIIII".FromRomanNumerals(false).Should().Be(0, "X cannot be exceeded by smaller denominations");
            new string('I', 10).FromRomanNumerals(false).Should().Be(0, "X cannot be exceeded by smaller denominations");
            "IX".FromRomanNumerals().Should().Be(9);

            "LL".FromRomanNumerals(false).Should().Be(0, "C cannot be exceeded by smaller denominations");
            "LXXXXX".FromRomanNumerals(false).Should().Be(0, "C cannot be exceeded by smaller denominations");
            new string('X', 10).FromRomanNumerals(false).Should().Be(0, "C cannot be exceeded by smaller denominations");
            "XCIX".FromRomanNumerals().Should().Be(99);

            "DD".FromRomanNumerals(false).Should().Be(0, "M cannot be exceeded by smaller denominations");
            "DCCCCC".FromRomanNumerals(false).Should().Be(0, "M cannot be exceeded by smaller denominations");
            new string('C', 10).FromRomanNumerals(false).Should().Be(0, "M cannot be exceeded by smaller denominations");
            "CMXCIX".FromRomanNumerals().Should().Be(999);
        }

        [TestMethod]
        public void RomanNumeralParsing_RepetitionRules()
        {
            // iii.  D, L, and V can each only appear once.
            TestDigitRepetition("D", 500, false);
            TestDigitRepetition("L", 50, false);
            TestDigitRepetition("V", 5, false);

            TestDigitRepetition("M", 1000, true);
            TestDigitRepetition("C", 100, true);
            TestDigitRepetition("X", 10, true);
            TestDigitRepetition("I", 1, true);

        }

        void TestDigitRepetition(string digit, ulong value, bool canRepeat)
        {
            $"{digit}".FromRomanNumerals().Should().Be(value, $"{digit} should equal {value}");
            if (canRepeat)
            {
                $"{digit}{digit}".FromRomanNumerals().Should().Be(value * 2, $"{digit}{digit} should equal {value * 2}");
                $"{digit}{digit}{digit}".FromRomanNumerals().Should().Be(value * 3, $"{digit}{digit}{digit} should equal {value * 3}");
            }
            else
            {
                $"{digit}{digit}".FromRomanNumerals(false).Should().Be(0, $"{digit} should not appear more than once.");
                new Action(() => $"{digit}{digit}".FromRomanNumerals()).ShouldThrow<InvalidOperationException>($"{digit} should not appear more than once.");
            }
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
        public void RomanNumeralCreation_BasicNonSubtractive()
        {
            1000.ToRomanNumerals(false).Should().Be("M");
            500.ToRomanNumerals(false).Should().Be("D");
            100.ToRomanNumerals(false).Should().Be("C");
            50.ToRomanNumerals(false).Should().Be("L");
            10.ToRomanNumerals(false).Should().Be("X");
            5.ToRomanNumerals(false).Should().Be("V");
            1.ToRomanNumerals(false).Should().Be("I");

            900.ToRomanNumerals(false).Should().Be("DCCCC");
            400.ToRomanNumerals(false).Should().Be("CCCC");
            90.ToRomanNumerals(false).Should().Be("LXXXX");
            40.ToRomanNumerals(false).Should().Be("XXXX");
            9.ToRomanNumerals(false).Should().Be("VIIII");
            4.ToRomanNumerals(false).Should().Be("IIII");
        }

        [TestMethod]
        public void RomanNumeralCreation_BasicSubtractive()
        {
            1000.ToRomanNumerals().Should().Be("M");
            500.ToRomanNumerals().Should().Be("D");
            100.ToRomanNumerals().Should().Be("C");
            50.ToRomanNumerals().Should().Be("L");
            10.ToRomanNumerals().Should().Be("X");
            5.ToRomanNumerals().Should().Be("V");
            1.ToRomanNumerals().Should().Be("I");


            900.ToRomanNumerals().Should().Be("CM");
            400.ToRomanNumerals().Should().Be("CD");
            90.ToRomanNumerals().Should().Be("XC");
            40.ToRomanNumerals().Should().Be("XL");
            9.ToRomanNumerals().Should().Be("IX");
            4.ToRomanNumerals().Should().Be("IV");
        }


        [TestMethod]
        public void RomanNumeralCreation_ExtensiveNonSubtractive()
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
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SROM
{
    [TestFixture]
    class ModCalcTest
    {
        /*
        [TestCase("", "", "")]
        */

        [Test]
        [TestCase("F", "A", "5")]
        [TestCase("4D0FFA257CCEA11EBAB1F01E65A77392D01F1", "48C1B463F2782F60D0", "1")]
        [TestCase("1DF9E9A", "4", "2")]
        public void GCDTest(string hex1, string hex2, string expectedResult)
        {
            var actualResult = ModCalc.GCD(hex1, hex2);
            Assert.AreEqual(expectedResult, actualResult);
        }



        [Test]
        [TestCase("D", "F", "D")]
        [TestCase("F", "B", "4")]
        [TestCase("87AF11BCD", "B11", "14E")]
        [TestCase("3AC9EC8A7D3A554C1E9094A1854ACB0E2B7CBDDE59C2ADA018173C98BF4DFB1BD8D5DCCFD5BDFA9E91C8839958684D7121B4DA8863925E77EA0A27A28867B6CE",
                   "A320855784D35118ABBDA9116A2D52B9CF76C5C69427AED4F3ADD63FC3B6CC36",
                   "5C42488F9D580BBA73B6AB5FAEAB251C023E016259A48D44B1947A3837BA0E28"
            )]
        [TestCase("101A3F04E4BB34DD41FE0893D0FCF341776CFF41016BB753DBF23B5D6FEBF156B5CE6C511D5E54876559CF6A0BC3CDB93CE883331CE1349F604D789C440E1FBA",
                   "62CFC9B03F18DEB0667A9F46DEA1D1D10AB9720ED7379F544B8C5DC67211EF89",
                   "AAA234924090000BAAAAA"
            )]
        public void ModTest(string hex1, string hex2, string expectedResult)
        {
            var actualResult = ModCalc.Mod(hex1, hex2);
            Assert.AreEqual(expectedResult, actualResult);
        }



        [Test]
        [TestCase("A320855784D35118ABBDA9116A2D52B9CF76C5C69427AED4F3ADD63FC3B6CC36",
                  "5C42488F9D580BBA73B6AB5FAEAB251C023E016259A48D44B1947A3837BA0E28",
                  "18ABBD9A48D", "10F4DC367F2")]
        [TestCase("AAA234924090000BAAAAA", "488F9D580BBA73B6A2D52", "58684D7121B4DA88", "4111121EF0CA7F04")]
        [TestCase("C8839958684D7121B4DA8863925E77EA0A27A28867B6CE", "4D0FFA257CCEA11EBAB1F01E65A77392D01F1", "48C1B463F2782F60D0", "160CE11B1B5DB077DF")]
        public void LongModAddTest(string hex1, string hex2, string hex3, string expectedResult)
        {
            var actualResult = ModCalc.LongModAdd(hex1, hex2, hex3);
            Assert.AreEqual(expectedResult, actualResult);
        }



        [Test]
        [TestCase("A320855784D35118ABBDA9116A2D52B9CF76C5C69427AED4F3ADD63FC3B6CC36", "4D0FFA257CCEA11EBAB1F01E65A77392D01F1", "AB5FAEAB251C023E016259A48D44B1947A3837BA0E28", "57FA104EFE4BC0ED5A3770B77FB0E6191D3D5E707B8D")]
        public void LongModSubTest(string hex1, string hex2, string hex3, string expectedResult)
        {
            var actualResult = ModCalc.LongModSub(hex1, hex2, hex3);
            Assert.AreEqual(expectedResult, actualResult);
        }



        [Test]
        [TestCase("4D0FFA257CCEA11EBAB1F01E65A77392D01F1", "18ABBD9A48D", "10F4DC367F2", "C06CFA7EB5")]
        [TestCase("4D0FFA257CCEA11EBAB1F01E65A77392D01F1", "18ABBD9A48D", "AAA234924090000BAAAAA", "83D4B299AA06BDA2E18B9")]
        public void LongModMulTest(string hex1, string hex2, string hex3, string expectedResult)
        {
            var actualResult = ModCalc.LongModMul(hex1, hex2, hex3);
            Assert.AreEqual(expectedResult, actualResult);
        }



        [Test]
        [TestCase("18ABBD9A48D", "12", "C06CFA7EB5", "51A45FDC02")]
        [TestCase("AAA234924090000BAAAAA", "FF", "83D4B299AA06BDA2E18B9", "CECF5FCD06C7E6E8D508")]
        public void LongModWPowTest(string hex1, string hex2, string hex3, string expectedResult)
        {
            var actualResult = ModCalc.LongModWPow(hex1, hex2, hex3);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}

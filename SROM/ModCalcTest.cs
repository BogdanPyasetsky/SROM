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

        [Test]
        [TestCase("F", "A", "5")]
        [TestCase("4D0FFA257CCEA11EBAB1F01E65A77392D01F1", "48C1B463F2782F60D0", "1")]
        [TestCase("1DF9E9A", "4", "2")]/*
        [TestCase("", "", "")]
        [TestCase("", "", "")]
        [TestCase("", "", "")]
        [TestCase("", "", "")]
        [TestCase("", "", "")]
        [TestCase("", "", "")]*/
        public void GCDTest(string hex1, string hex2, string expectedResult)
        {
            var actualResult = ModCalc.GCD(hex1, hex2);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}

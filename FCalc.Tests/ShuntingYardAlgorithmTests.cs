using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCalcLib.Tests
{
    [TestClass]
    public class ShuntingYardAlgorithmTests
    {
        [DataRow("1 + 1", "1 1 +")]
        [DataRow("1+1", "1 1 +")]
        [DataRow("1 - 1", "1 1 -")]
        [DataRow("10 + 10", "10 10 +")]
        [DataRow("10+10", "10 10 +")]
        [DataRow("3 + 4", "3 4 +")]
        [DataRow("( 1 + 2 ) * 3", "1 2 + 3 *")]
        [DataRow("(1+2) * 3", "1 2 + 3 *")]
        [DataRow("3 + 4 * 2 / ( 1 - 5 )", "3 4 2 * 1 5 - / +")]
        [TestMethod]
        public void WhenConvert_ValidExpression_Returns_CorrectResult(string input, string expectedOutput)
        {
            var output = ShuntingYardAlgorithm.Convert(input);

            Assert.AreEqual(expectedOutput, output);
        }

        [DataRow("hello world")]
        [DataRow("12 + abc")]
        [TestMethod]
        public void WhenConvert_InvalidExpression_ThrowsException(string input)
        {
            Assert.ThrowsException<FormatException>(() => ShuntingYardAlgorithm.Convert(input));
        }
    }
}

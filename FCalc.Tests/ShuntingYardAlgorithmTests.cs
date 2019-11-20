using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCalc.Tests
{
    [TestClass]
    public class ShuntingYardAlgorithmTests
    {
        [DataRow("1 + 1", "1 1 +")]
        [TestMethod]
        public void WhenConvert_ValidExpression_Returns_CorrectResult(string input, string expectedOutput)
        {
            var output = ShuntingYardAlgorithm.Convert(input);

            Assert.AreEqual(expectedOutput, output);
        }
    }
}

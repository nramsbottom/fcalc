using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FCalc.Tests
{
    [TestClass]
    public class ReversePolishNotationTests
    {
        [DataRow("3 4 +", 7D)]
        [DataRow("3 3 *", 9D)]
        [TestMethod]
        public void WhenEvalute_ValidExpression_Returns_CorrectResult(string input, double expectedOutput)
        {
            double output = ReversePolishNotation.Evaluate(input);

            Assert.AreEqual(expectedOutput, output);
        }
    }
}

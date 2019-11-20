using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FCalc.Tests
{
    [TestClass]
    public class ReversePolishNotationTests
    {
        [DataRow("1 1 +", 2D)]
        [DataRow("1 1 -", 0D)]
        [DataRow("3 3 /", 1D)]
        [DataRow("3 3 *", 9D)]
        [DataRow("10 10 +", 20D)]
        [DataRow("10 10 -", 0D)]
        [DataRow("10 10 /", 1D)]
        [DataRow("10 10 *", 100D)]
        [DataRow("15 7 1 1 + - / 3 * 2 1 1 + + -")]
        [TestMethod]
        public void WhenEvalute_ValidExpression_Returns_CorrectResult(string input, double expectedOutput)
        {
            double output = ReversePolishNotation.Evaluate(input);

            Assert.AreEqual(expectedOutput, output);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FCalc.Tests
{
    [TestClass]
    public class ReversePolishNotationTests
    {
        [DataRow("1 1 +", 2)]
        [DataRow("1 1 -", 0)]
        [DataRow("3 3 /", 1)]
        [DataRow("3 3 *", 9)]
        [DataRow("10 10 +", 20)]
        [DataRow("10 10 -", 0)]
        [DataRow("10 10 /", 1)]
        [DataRow("10 10 *", 100)]
        [DataRow("15 7 1 1 + - / 3 * 2 1 1 + + -", 5D)]
        [TestMethod]
        public void WhenEvaluate_ValidExpression_Returns_CorrectResult(string input, double expectedOutput)
        {
            double output = ReversePolishNotation.Evaluate(input);

            Assert.AreEqual(expectedOutput, output);
        }
    }
}

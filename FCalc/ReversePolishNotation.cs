using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCalc
{
    public class ReversePolishNotation
    {
        static readonly char[] SupportedOperators = new char[] { '+', '-', '*', '/' };

        public static double Evaluate(string expression)
        {
            int position = 0;
            char c;
            do
            {
                c = expression[position];
                if (IsOperator(c))
                {
                    // pop two operands from the stack 
                    // evaluate
                    // push result
                }
                else
                {
                    // read until whitespace and place on stack
                    var operand = ReadOperand(position, expression);
                    position += operand.Length;
                }
            } while (position < expression.Length);


            return 0;
        }

        private static bool IsOperator(char c)
        {
            return SupportedOperators.Contains(c);
        }

        /// <summary>
        /// Read until we detect a non-digit or end of string
        /// </summary>
        private static string ReadOperand(int startIndex, string text)
        {
            var index = startIndex;
            var output = string.Empty;
            do
            {
                if (char.IsDigit(text[index]))
                    output += text[index++];
                else
                    break;
            } while (index < text.Length);
            return output;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FCalc
{
    public class ReversePolishNotation
    {
        // TODO: Change this to a dictionary of functions instead. Reduces duplication in the evaluations step.
        static readonly char[] SupportedOperators = new char[] { '+', '-', '*', '/' };

        public static int Evaluate(string expression)
        {
            int position = 0;
            char c;
            var output = new Stack<int>();
            do
            {
                c = expression[position];
                if (IsOperator(c))
                {
                    var op = c;

                    // pop operands from the stack 
                    var operand2 = output.Pop();
                    var operand1 = output.Pop();

                    // evaluate
                    var result = op switch
                    {
                        '+' => operand1 + operand2,
                        '*' => operand1 * operand2,
                        '-' => operand1 - operand2,
                        '/' => operand1 / operand2,
                        _ => throw new NotImplementedException($"The specified operator '{op}' has not been implemented."),
                    };

                    // push result back to the stack
                    output.Push(result);
                }
                else if (c == ' ')
                {
                    // ignore whitespace
                }
                else
                {
                    // read until whitespace and place on stack
                    var operand = ReadOperand(position, expression);
                    output.Push(int.Parse(operand));
                    position += operand.Length;
                }
                position++;
            } while (position < expression.Length);


            return output.Pop();
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
            // TODO: This only handles whole numbers.
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

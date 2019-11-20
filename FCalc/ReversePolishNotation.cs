using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            var output = new Stack<string>();
            do
            {
                c = expression[position];
                if (IsOperator(c))
                {
                    // pop two operands from the stack 
                    var op = c;
                    var operand2 = int.Parse(output.Pop()); // TODO: improve this
                    var operand1 = int.Parse(output.Pop());
                    double result;

                    // evaluate
                    switch(op)
                    {
                        case '+':
                            result = operand1+ operand2;
                            break;
                        case '*':
                            result = operand1 * operand2;
                            break;

                        default:
                            throw new NotImplementedException($"The specified operator '{op}' has not been implemented.");
                    }

                    // push result
                    output.Push(result.ToString());
                }
                else
                {
                    // read until whitespace and place on stack
                    var operand = ReadOperand(position, expression);
                    output.Push(operand);
                    position += operand.Length;
                }
                position++;
            } while (position < expression.Length);


            return int.Parse(output.Pop());
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

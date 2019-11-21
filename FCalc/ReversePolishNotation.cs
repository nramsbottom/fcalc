using System;
using System.Collections.Generic;

namespace FCalcLib
{
    public static class ReversePolishNotation
    {
        static readonly Dictionary<char, Func<int, int, int>> Operators = new Dictionary<char, Func<int, int, int>>()
        {
            { '*', (operand1, operand2) => operand1 * operand2  },
            { '/', (operand1, operand2) => operand1 / operand2  },
            { '-', (operand1, operand2) => operand1 - operand2  },
            { '+', (operand1, operand2) => operand1 + operand2  }
        };

        public static int Evaluate(string expression)
        {
            int position = 0;
            char c;
            var output = new Stack<int>();

            do
            {
                c = expression[position];
                if (Operators.ContainsKey(c))
                {
                    var currentOperator = c;
                    var operand2 = output.Pop();
                    var operand1 = output.Pop();
                    var result = Operators[currentOperator](operand1, operand2);

                    // push result back to the stack
                    output.Push(result);
                }
                else if (char.IsDigit(c))
                {
                    var operand = ReadOperand(position, expression);
                    output.Push(int.Parse(operand));
                    position += operand.Length;
                    continue;
                }
                else if (c == ' ')
                {
                    // ignore whitespace
                }
                else
                {
                    throw new FormatException($"Invalid character '{c}' in expression.");
                }

                position++;
            } while (position < expression.Length);


            return output.Pop();
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

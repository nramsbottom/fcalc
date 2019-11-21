using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCalcLib
{
    public static class ShuntingYardAlgorithm
    {
        private enum Operator
        {
            RightParen = 5,
            LeftParen = 4,
            Multiply = 3,
            Divide = 2,
            Subtract = 1,
            Add = 0
        }

        static readonly Dictionary<char, Operator> Operators = new Dictionary<char, Operator>()
        {
            { ')', Operator.RightParen },
            { '(', Operator.LeftParen },
            { '*', Operator.Multiply },
            { '/', Operator.Divide},
            { '-', Operator.Subtract},
            { '+', Operator.Add },
        };

        /// <summary>
        /// Converts the given infix expression to postfix (Reverse Polish Notiation)
        /// </summary>
        public static string Convert(string infixExpresssion)
        {
            var outputQueue = new Queue<string>();
            var operatorStack = new Stack<char>();
            var expressionPosition = 0;

            do
            {
                char c = infixExpresssion[expressionPosition];

                if (char.IsDigit(c))
                {
                    var s = ReadOperand(expressionPosition, infixExpresssion);
                    outputQueue.Enqueue(s);
                    expressionPosition += s.Length;
                    continue;
                }
                else if (Operators.ContainsKey(c))
                {
                    var currentOperator = Operators[c];

                    if (operatorStack.Any() && Operators[operatorStack.Peek()] > currentOperator && operatorStack.Peek() != '(')
                    {
                        // pop operators all to output queue until we find an 
                        // operator with a higher precedence
                        while (operatorStack.Any() && Operators[operatorStack.Peek()] > currentOperator)
                        {
                            outputQueue.Enqueue(operatorStack.Pop().ToString());
                        }
                    }
                    else if (currentOperator == Operator.LeftParen)
                    {
                        operatorStack.Push(c);
                    }
                    else if (currentOperator == Operator.RightParen)
                    {
                        // remove all operators back to the last left paren then
                        // remove the left paren itself
                        while (operatorStack.Peek() != '(')
                        {
                            outputQueue.Enqueue(operatorStack.Pop().ToString());
                        }
                        operatorStack.Pop();
                    }
                    else
                    {
                        // all other operators go straight on the stack
                        operatorStack.Push(c);
                    }

                }
                else if (c == ' ')
                {
                    // ignore spaces
                }
                else
                {
                    throw new FormatException($"Invalid character '{c}' in expression.");
                }

                expressionPosition++;
            } while (expressionPosition < infixExpresssion.Length);

            // push all remaining operators to the output queue
            while (operatorStack.Any())
            {
                outputQueue.Enqueue(operatorStack.Pop().ToString());
            }

            // convert the output queue to a string
            var output = new StringBuilder();

            while (outputQueue.Any())
            {
                output.Append(outputQueue.Dequeue());
                if (outputQueue.Any())
                    output.Append(" ");
            }

            return output.ToString();
        }

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

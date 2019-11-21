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
                else if (c == ' ')
                {
                    // ignore
                }
                else
                {
                    if (Operators.ContainsKey(c))
                    {
                        var op = Operators[c];

                        if (operatorStack.Any() && Operators[operatorStack.Peek()] > op && operatorStack.Peek() != '(')
                        {
                            // pop operators all to output queue
                            // until we find an operator with a
                            // higher or equal precedence
                            do
                            {
                                outputQueue.Enqueue(operatorStack.Pop().ToString());
                            }
                            while (operatorStack.Any());
                        }
                        else if (op == Operator.LeftParen)
                            operatorStack.Push(c);
                        else if (op == Operator.RightParen)
                        {
                            while (operatorStack.Peek() != '(')
                            {
                                var y = operatorStack.Pop().ToString();
                                outputQueue.Enqueue(y);
                            }
                            operatorStack.Pop(); // and pop the left paren
                        }
                        else
                        {
                            operatorStack.Push(c);
                        }
                    }
                    // ignore
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

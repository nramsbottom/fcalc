using System.Collections.Generic;
using System.Linq;

namespace FCalcLib
{
    public class ShuntingYardAlgorithm
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
            int position = 0;

            var outQueue = new Queue<string>();
            var opStack = new Stack<char>();

            do
            {
                char c = infixExpresssion[position];

                if (char.IsDigit(c))
                {
                    var s = ReadOperand(position, infixExpresssion);
                    outQueue.Enqueue(s);
                    position += s.Length;
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

                        if (opStack.Any() && Operators[opStack.Peek()] > op && opStack.Peek() != '(')
                        {
                            // pop operators all to output queue
                            // until we find an operator with a
                            // higher or equal precedence
                            do
                            {
                                outQueue.Enqueue(opStack.Pop().ToString());
                            }
                            while (opStack.Any());
                        }
                        else if (op == Operator.LeftParen)
                            opStack.Push(c);
                        else if (op == Operator.RightParen)
                        {
                            while (opStack.Peek() != '(')
                            {
                                var y = opStack.Pop().ToString();
                                outQueue.Enqueue(y);
                            }
                            opStack.Pop(); // and pop the left paren
                        }
                        else
                        {
                            opStack.Push(c);
                        }
                    }
                    // ignore
                }

                position++;
            } while (position < infixExpresssion.Length);

            // push all remaining operators to the output queue
            while (opStack.Any())
            {
                outQueue.Enqueue(opStack.Pop().ToString());
            }

            var x = string.Empty;
            
            while (outQueue.Any())
            {
                x += outQueue.Dequeue();
                if (outQueue.Any())
                    x += " ";
            }

            return x;
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

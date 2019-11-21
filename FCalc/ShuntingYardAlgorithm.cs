using System.Collections.Generic;
using System.Linq;

namespace FCalc
{
    public class ShuntingYardAlgorithm
    {
        private enum Operator
        {
            Multiply = 3,
            Divide = 2,
            Subtract = 1,
            Add = 0
        }

        static readonly Dictionary<char, Operator> Operators = new Dictionary<char, Operator>()
        {
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

                        // is operator stack empty?
                        if (opStack.Any() && Operators[opStack.Peek()] > op)
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
            do
            {
                outQueue.Enqueue(opStack.Pop().ToString());
            } while (opStack.Any());

            var x = string.Empty;
            do
            {
                x += outQueue.Dequeue();
                if (outQueue.Any())
                    x += " ";
            } while (outQueue.Any());

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

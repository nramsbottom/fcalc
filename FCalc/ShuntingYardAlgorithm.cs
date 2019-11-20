using System;
using System.Collections.Generic;
using System.Text;

namespace FCalc
{
    public class ShuntingYardAlgorithm
    {
        public static string Convert(string infixExpresssion)
        {
            int position = 0;

            var output = new Stack<string>();
            var operators = new Stack<Operator>();

            do
            {
                char c = infixExpresssion[position];

                if (char.IsDigit(c))
                {
                    var s = ReadOperand(position, infixExpresssion);
                    output.Push(s);
                    position += s.Length;
                }
                else if (c == ' ')
                {
                    // ignore
                }
                else
                {
                    switch (c)
                    {
                        case '(':
                            operators.Push(Operator.LeftParen);
                            break;
                        case ')':
                            operators.Push(Operator.RightParen);
                            break;
                        case '+':
                            operators.Push(Operator.Addition);
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }

                position++;
            } while (position < infixExpresssion.Length);

            var x = string.Empty;
            do
            {
                x += output.Pop() + " ";
            } while (output.Count > 0);

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
        
        private enum Operator
        {
            LeftParen,
            RightParen,
            Addition
        }
    }
}

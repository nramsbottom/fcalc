using System;

namespace FCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string input = PromptGetResponse("Enter expression (or 'exit')");
                if (input.ToLower() == "exit")
                    break;

                var postfix = ShuntingYardAlgorithm.Convert(input);
                var result = ReversePolishNotation.Evaluate(postfix);

                Console.WriteLine($"The result is : {result}");
            }
        }

        static string PromptGetResponse(string prompt)
        {
            string input = null;
            Console.Out.Write($"{prompt}: ");
            do
            {
                input = Console.ReadLine();
            } while (input == null);
            return input;
        }
    }

}

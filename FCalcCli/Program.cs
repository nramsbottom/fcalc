using FCalcLib;
using System;

namespace FCalcCli
{
    class Program
    {
        const string QuitKeyword = "exit";

        static void Main(string[] args)
        {
            while (true)
            {
                string input = PromptGetResponse(Messages.Prompt_EnterExpression);
                if (input.ToLower() == QuitKeyword.ToLower())
                    break;

                string postfix;
                int result;

                try
                {
                    postfix = ShuntingYardAlgorithm.Convert(input);
                }
                catch (Exception)
                {
                    DisplayMessage(Messages.ErrorMessage_PostfixParseFailed, isError: true);
                    continue;
                }

                try
                {
                    result = ReversePolishNotation.Evaluate(postfix);
                }
                catch (Exception ex)
                {
                    // special case in that it's very clear what the problem is
                    if (ex is DivideByZeroException)
                    {
                        DisplayMessage(Messages.ErrorMessage_EvaluationGeneratesDivideByZero, isError: true);
                    }
                    else
                    {
                        DisplayMessage(Messages.ErrorMessage_EvaluationFailed, isError: true);
                    }

                    continue;
                }

                DisplayMessage($"{Messages.Message_Result} {result}");
            }
        }

        /// <summary>
        /// Writes a message to the current output stream.
        /// </summary>
        /// <param name="message">Text to display.</param>
        /// <param name="isError">Determines if the messages will be written to the error stream.</param>
        private static void DisplayMessage(string message, bool isError = false)
        {
            var output = isError ? Console.Out : Console.Error;
            output.WriteLine(message);
        }

        /// <summary>
        /// Prompts user for input and does not return until something is entered.
        /// </summary>
        static string PromptGetResponse(string prompt)
        {
            Console.Out.Write($"{prompt}: ");
            string input;
            do
            {
                input = Console.ReadLine();
            } while (input == null);
            return input;
        }
    }
}

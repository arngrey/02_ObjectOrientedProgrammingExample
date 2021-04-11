using System;

namespace _02_ObjectOrientedProgrammingExample
{
    class Program
    {
        static void Main(string[] args)
        {
            string expressionAsString = Console.ReadLine();

            try
            {
                var expression = new Expression(expressionAsString);
                var expressionInReversedpolishNotation = expression.GetReversedPolishNotation();
                var value = expression.GetValue();

                Console.WriteLine(expressionInReversedpolishNotation);
                Console.WriteLine(value);
            }
            catch
            {
                Console.WriteLine("В выражении допущена ошибка.");
            }

            Console.ReadKey();
        }
    }
}

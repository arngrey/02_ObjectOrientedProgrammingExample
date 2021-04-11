using System;
using System.Linq;

namespace _02_ObjectOrientedProgrammingExample
{
    class OperatorLexema : Lexema
    {
        static private string[] _binaryOperators = new string[4] { "+", "-", "*", "/" };
        static private string[] _highPriorityOperators = new string[2] { "*", "/" };
        static private string[] _lowPriorityOperators = new string[2] { "+", "-" };

        /// <summary>
        /// Имеет ли оператор высокий приоритет?
        /// </summary>
        public bool IsHighPriority { get => _highPriorityOperators.Contains(_value); }

        /// <summary>
        /// Имеет ли оператор низкий приоритет?
        /// </summary>
        public bool IsLowPriority { get => _lowPriorityOperators.Contains(_value); }

        public OperatorLexema(string inputLexema)
        {
            if (!_binaryOperators.Contains(inputLexema))
            {
                // Ошибка, неверная строка
                throw new Exception();
            }

            this._value = inputLexema;
        }

        /// <summary>
        /// Имеет ли оператор приоритет выше или равный по сравнению с другим оператором?
        /// </summary>
        /// <param name="otherOperatorLexema">Другой оператор, с которым сравнивают.</param>
        /// <returns>Имеет ли оператор приоритет выше или равный по сравнению с другим оператором?</returns>
        public bool HasGraterOrEqualPriorityThan(OperatorLexema otherOperatorLexema)
        {
            return !(this.IsLowPriority && otherOperatorLexema.IsHighPriority);
        }

        /// <summary>
        /// Вычислить значение примитивного выражения над переданными операндами.
        /// </summary>
        /// <param name="firstOperand">Первый операнд.</param>
        /// <param name="secondOperand">Второй операнд.</param>
        /// <returns>Значение выражения.</returns>
        public double Evaluate(double firstOperand, double secondOperand)
        {
            switch (this._value)
            {
                case "+":
                    return firstOperand + secondOperand;
                case "-":
                    return firstOperand - secondOperand;
                case "*":
                    return firstOperand * secondOperand;
                case "/":
                    return firstOperand / secondOperand;
                default:
                    // Ошибка в выражении
                    throw new Exception();
            }
        }
    }
}

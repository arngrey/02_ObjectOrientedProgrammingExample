using System;

namespace _02_ObjectOrientedProgrammingExample
{
    class NumericLexema : Lexema
    {
        /// <summary>
        /// Возвращает значение лексемы как число.
        /// </summary>
        public double Value { get; }

        public NumericLexema (string inputLexema)
        {
            if (!double.TryParse(inputLexema, out var parseResult))
            {
                // Ошибка, неверная строка
                throw new Exception();
            }

            this._value = inputLexema;
            this.Value = parseResult;
        }
    }
}

using System.Collections.Generic;
using System;

namespace _02_ObjectOrientedProgrammingExample
{
    class Lexemes
    {
        /// <summary>
        /// Возвращает коллекцию лексем.
        /// </summary>
        public List<Lexema> List { get; }

        public Lexemes()
        {
            this.List = new List<Lexema>();
        }

        public Lexemes(string inputString)
        {
            this.List = GetLexemes(new Symbols(inputString));
        }

        /// <summary>
        /// Получает список лексем из коллекции символов.
        /// </summary>
        /// <param name="symbols">Коллекция символов.</param>
        private List<Lexema> GetLexemes(Symbols symbols)
        {
            var result = new List<Lexema>();

            int i = 0;
            while (i < symbols.Count)
            {
                AbstractSymbol currentSymbol = symbols[i];
                AbstractSymbol nextSymbol = symbols[i + 1];
                AbstractSymbol nextNextSymbol = symbols[i + 2];

                if (currentSymbol.IsBracket)
                {
                    result.Add(new BracketLexema(currentSymbol.Value.ToString()));
                    i++;
                }
                else if (currentSymbol.IsWhiteSpace && nextSymbol.IsBinaryOperator && nextNextSymbol.IsWhiteSpace)
                {
                    result.Add(new OperatorLexema(nextSymbol.Value.ToString()));
                    i += 3;
                }
                else if (currentSymbol.IsDigit)
                {
                    result.Add(new NumericLexema(currentSymbol.Value.ToString()));
                    i++;
                }
                else if (currentSymbol.IsUnaryOperator && nextSymbol.IsDigit)
                {
                    result.Add(new NumericLexema(currentSymbol.Value.ToString() + nextSymbol.Value.ToString()));
                    i += 2;
                }
                else
                {
                    // Ошибка, неопознанная лексема
                    throw new Exception();
                }
            }

            return result;
        }
    }
}

using System;

namespace _02_ObjectOrientedProgrammingExample
{
    class BracketLexema : Lexema
    {
        static private string _openBracket = "(";
        static private string _closeBracket = ")";

        /// <summary>
        /// Является ли скобка открывающейся? (Или закрывающейся, третьего не дано)
        /// </summary>
        public bool IsOpen { get; }

        public BracketLexema (string inputLexema)
        {
            if (inputLexema != _openBracket && inputLexema != _closeBracket)
            {
                // Ошибка, неверная строка
                throw new Exception();
            }

            this._value = inputLexema;
            this.IsOpen = inputLexema == _openBracket;
        }
    }
}

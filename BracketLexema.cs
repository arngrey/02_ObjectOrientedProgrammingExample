using System;

namespace _02_ObjectOrientedProgrammingExample
{
    class BracketLexema : Lexema
    {
        static private string _openBracket = "(";
        static private string _closeBracket = ")";

        /// <summary>
        /// Является ли скобка открывающейся?
        /// </summary>
        public bool IsOpen { get; }

        /// <summary>
        /// Является ли скобка закрывающейся?
        /// </summary>
        public bool IsClose { get; }

        public BracketLexema (string inputLexema)
        {
            if (inputLexema != _openBracket && inputLexema != _closeBracket)
            {
                throw new Exception();
            }

            this._value = inputLexema;
            this.IsOpen = inputLexema == _openBracket;
            this.IsClose = inputLexema == _closeBracket;
        }
    }
}

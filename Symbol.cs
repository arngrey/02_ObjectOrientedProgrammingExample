using System.Linq;

namespace _02_ObjectOrientedProgrammingExample
{
    class Symbol : AbstractSymbol
    {
        static private char[] _binaryOperators = new char[4] { '+', '-', '*', '/' };
        static private char[] _unaryOperators = new char[2] { '+', '-' };
        static private char[] _brackets = new char[2] { '(', ')' };

        /// <inheritdoc />
        override public char Value { get; }

        /// <inheritdoc />
        override public bool IsDigit { get => char.IsDigit(Value); }

        /// <inheritdoc />
        override public bool IsBinaryOperator { get => _binaryOperators.Contains(Value); }

        /// <inheritdoc />
        override public bool IsUnaryOperator { get => _unaryOperators.Contains(Value); }

        /// <inheritdoc />
        override public bool IsBracket { get => _brackets.Contains(Value); }

        /// <inheritdoc />
        override public bool IsWhiteSpace { get => char.IsWhiteSpace(Value); }

        public Symbol(char value)
        {
            this.Value = value;
        }
    }
}

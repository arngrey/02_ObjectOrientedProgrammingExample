namespace _02_ObjectOrientedProgrammingExample
{
    class NullSymbol : AbstractSymbol
    {
        /// <inheritdoc />
        override public char Value { get; }

        /// <inheritdoc />
        override public bool IsDigit { get => false; }

        /// <inheritdoc />
        override public bool IsBinaryOperator { get => false; }

        /// <inheritdoc />
        override public bool IsUnaryOperator { get => false; }

        /// <inheritdoc />
        override public bool IsBracket { get => false; }

        /// <inheritdoc />
        override public bool IsWhiteSpace { get => false; }

        public NullSymbol()
        {
            this.Value = ' ';
        }
    }
}

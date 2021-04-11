using System;
using System.Collections.Generic;
using System.Text;

namespace _02_ObjectOrientedProgrammingExample
{
    abstract class AbstractSymbol
    {
        /// <summary>
        /// Возвращает значение символа.
        /// </summary>
        abstract public char Value { get; }

        /// <summary>
        /// Является ли символ цифрой?
        /// </summary>
        abstract public bool IsDigit { get; }

        /// <summary>
        /// Является ли символ бинарным оператором?
        /// </summary>
        abstract public bool IsBinaryOperator { get; }

        /// <summary>
        /// Является ли символ унарным оператором?
        /// </summary>
        abstract public bool IsUnaryOperator { get; }

        /// <summary>
        /// Является ли символ скобкой?
        /// </summary>
        abstract public bool IsBracket { get; }

        /// <summary>
        /// Является ли символ пробелом?
        /// </summary>
        abstract public bool IsWhiteSpace { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace _02_ObjectOrientedProgrammingExample
{
    static class LexemesParser
    {
        static public List<Lexema> Parse(string expressionInInfixNotation)
        {
            var result = new List<Lexema>();

            var symbols = new Symbols(expressionInInfixNotation);

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
                    throw new Exception();
                }
            }

            return result;
        }
    }
}

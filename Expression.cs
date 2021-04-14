using System;
using System.Linq;
using System.Collections.Generic;

namespace _02_ObjectOrientedProgrammingExample
{
    class Expression
    {
        private List<Lexema> _infixNotationLexemes;
        private List<Lexema> _reversedPolishNotationLexemes;

        public Expression(string expressionInInflixNotation)
        {
            this._infixNotationLexemes = GetInfixNotationLexemes(expressionInInflixNotation);
            this._reversedPolishNotationLexemes = GetReversedPolishNotationLexemes(this._infixNotationLexemes);
        }

        /// <summary>
        /// Возвращает выражение в обратной польской нотации.
        /// </summary>
        /// <returns>Выражение в обратной польской нотации.</returns>
        public string GetReversedPolishNotation()
        {
            return this._reversedPolishNotationLexemes
                .Aggregate("", (accumulator, lexema) => accumulator + " " + lexema.ToString());
        }

        /// <summary>
        /// Парсит математическое выражение в инфиксной нотации.
        /// Возвращает массив лексем.
        /// </summary>
        /// <param name="expressionInInflixNotation">Математическое выражение в инфиксной нотации.</param>
        /// <returns>Массив лексем в порядке инфиксной нотации.</returns>
        private List<Lexema> GetInfixNotationLexemes(string expressionInInflixNotation)
        {
            return LexemesParser.Parse(expressionInInflixNotation);
        }

        /// <summary>
        /// Возвращает массив лексем в порядке обратной польской нотации.
        /// Использует алгоритм "Сортировочная станция" Э. Дейкстры.
        /// </summary>
        /// <param name="infixNotationLexemes">Массив лексем в порядке инфиксной нотации.</param>
        /// <returns>Массив лексем в порядке обратной польской нотации</returns>
        private List<Lexema> GetReversedPolishNotationLexemes(List<Lexema> infixNotationLexemes)
        {
            var result = new List<Lexema>();
            var stack = new Stack<Lexema>();

            foreach (Lexema lexema in infixNotationLexemes)
            {
                if (lexema is NumericLexema)
                {
                    result.Add(lexema);
                }
                else if (lexema is BracketLexema && ((BracketLexema)lexema).IsOpen)
                {
                    stack.Push(lexema);
                }
                else if (lexema is OperatorLexema)
                {
                    PushOperatorsFromStackToListTillLowerPriorityOperator(stack, result, (OperatorLexema)lexema);
                    stack.Push(lexema);
                }
                else if (lexema is BracketLexema && ((BracketLexema)lexema).IsClose)
                {
                    PushLexemesFromStackToListTillAnOpenBracket(stack, result);
                    stack.Pop();
                }
                else
                {
                    throw new Exception();
                }
            }

            PushLexemesFromStackToList(stack, result);

            return result;
        }

        /// <summary>
        /// Возвращает значение выражения.
        /// </summary>
        /// <returns>Значение выражения.</returns>
        public double GetValue()
        {
            var stack = new Stack<Lexema>();

            foreach (Lexema lexema in _reversedPolishNotationLexemes)
            {
                if (lexema is NumericLexema)
                {
                    stack.Push(lexema);
                }
                else if (lexema is OperatorLexema)
                {
                    ApplyOperatorToNumbersFromStackAndPushResult(stack, (OperatorLexema)lexema);
                }
                else
                {
                    // Ошибка в выражении
                    throw new Exception();
                }
            }

            return ((NumericLexema)stack.Pop()).Value;
        }

        /// <summary>
        /// Перекладывает лексемы-операторы из стэка в список пока их приоритет не ниже, чем приоритет у оператора для сравнения.
        /// </summary>
        /// <param name="stack">Стэк.</param>
        /// <param name="list">Список.</param>
        /// <param name="operatorLexemaToCompare">Оператор, приоритет которого сравнивается с приоритетами операторов из стэка.</param>
        private static void PushOperatorsFromStackToListTillLowerPriorityOperator(Stack<Lexema> stack, List<Lexema> list, OperatorLexema operatorLexemaToCompare)
        {
            while (true)
            {
                if (stack.IsEmpty)
                {
                    break;
                }

                if (!(stack.Top() is OperatorLexema))
                {
                    break;
                }

                if (((OperatorLexema)stack.Top()).HasGraterOrEqualPriorityThan(operatorLexemaToCompare))
                {
                    list.Add(stack.Pop());
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Перекладывает все лексемы из стэка в список.
        /// Вызывает исключение, если встретилась скобка.
        /// </summary>
        /// <param name="stack">Стэк.</param>
        /// <param name="list">Список.</param>
        private static void PushLexemesFromStackToList(Stack<Lexema> stack, List<Lexema> list)
        {
            while (!stack.IsEmpty)
            {
                if (stack.Top() is BracketLexema)
                {
                    throw new Exception();
                }
                else
                {
                    list.Add(stack.Pop());
                }
            };
        }

        /// <summary>
        /// Перекладывает лексемы из стэка в список до встречи открывающейся скобки.
        /// Вызывает исключение, если стэк оказался пустым.
        /// </summary>
        /// <param name="stack">Стэк.</param>
        /// <param name="list">Список.</param>
        private static void PushLexemesFromStackToListTillAnOpenBracket(Stack<Lexema> stack, List<Lexema> list)
        {
            while (true)
            {
                if (stack.IsEmpty)
                {
                    throw new Exception();
                }

                if ((stack.Top() is BracketLexema) && ((BracketLexema)stack.Top()).IsOpen)
                {
                    break;
                }
                else
                {
                    list.Add(stack.Pop());
                }
            };
        }

        /// <summary>
        /// Применить оператор к двум операндам из стэка и записать результат обратно.
        /// </summary>
        /// <param name="stack">Стэк.</param>
        /// <param name="operatorLexema">Применяемый оператор.</param>
        private static void ApplyOperatorToNumbersFromStackAndPushResult(Stack<Lexema> stack, OperatorLexema operatorLexema)
        {
            NumericLexema secondOperand = (NumericLexema)stack.Pop();
            NumericLexema firstOperand = (NumericLexema)stack.Pop();
            double value = operatorLexema.Evaluate(firstOperand.Value, secondOperand.Value);
            stack.Push(new NumericLexema(value.ToString()));
        }
    }
}

using System;
using System.Linq;

namespace _02_ObjectOrientedProgrammingExample
{
    class Expression
    {
        private Lexemes _infixNotationLexemes;
        private Lexemes _reversedPolishNotationLexemes;

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
            return this._reversedPolishNotationLexemes.List
                .Aggregate("", (accumulator, lexema) => accumulator + " " + lexema.ToString());
        }

        /// <summary>
        /// Парсит математическое выражение в инфиксной нотации.
        /// Возвращает массив лексем.
        /// </summary>
        /// <param name="expressionInInflixNotation">Математическое выражение в инфиксной нотации.</param>
        /// <returns>Массив лексем в порядке инфиксной нотации.</returns>
        private Lexemes GetInfixNotationLexemes(string expressionInInflixNotation)
        {
            return new Lexemes(expressionInInflixNotation);
        }

        /// <summary>
        /// Возвращает массив лексем в порядке обратной польской нотации.
        /// Использует алгоритм "Сортировочная станция" Э. Дейкстры.
        /// </summary>
        /// <param name="infixNotationLexemes">Массив лексем в порядке инфиксной нотации.</param>
        /// <returns>Массив лексем в порядке обратной польской нотации</returns>
        private Lexemes GetReversedPolishNotationLexemes(Lexemes infixNotationLexemes)
        {
            bool stackIsEmpty;
            Lexema stackTopLexema;

            var result = new Lexemes();
            var stack = new Stack<Lexema>();

            foreach (Lexema lexema in infixNotationLexemes.List)
            {
                // Число - добавляем в выходную очередь
                if (lexema is NumericLexema)
                {
                    result.List.Add(lexema);
                }
                // Открывающаяся скобка - добавляем в стэк
                else if (lexema is BracketLexema && ((BracketLexema)lexema).IsOpen)
                {
                    stack.Push(lexema);
                }
                // Оператор (О1):
                else if (lexema is OperatorLexema)
                {
                    // Пока на вершине стэка присутствует оператор (О2), приоритет которого выше или равен приоритету текущего оператора, то
                    //     Перекладываем оператор О2 в выходную очередь
                    // Помещаем О1 в стэк
                    OperatorLexema lexemaAsOperator = lexema as OperatorLexema;
                    bool stackTopLexemaIsOperator = false;
                    OperatorLexema stackTopLexemaAsOperator;
                    bool stackTopOperatorHasGraterOrEqualPriorityThanCurrentOperator = false;

                    do
                    {
                        stackIsEmpty = stack.IsEmpty;
                        if (!stackIsEmpty)
                        {
                            stackTopLexema = stack.Top();
                            stackTopLexemaIsOperator = stackTopLexema is OperatorLexema;

                            if (stackTopLexemaIsOperator)
                            {
                                stackTopLexemaAsOperator = (OperatorLexema)stackTopLexema;
                                stackTopOperatorHasGraterOrEqualPriorityThanCurrentOperator = stackTopLexemaAsOperator.HasGraterOrEqualPriorityThan(lexemaAsOperator);

                                if (stackTopOperatorHasGraterOrEqualPriorityThanCurrentOperator)
                                {
                                    // Перекладываем оператор О2 в выходную очередь
                                    result.List.Add(stack.Pop());
                                }
                            }
                        }
                    } while (!stackIsEmpty && stackTopLexemaIsOperator && stackTopOperatorHasGraterOrEqualPriorityThanCurrentOperator);

                    // Помещаем О1 в стэк
                    stack.Push(lexema);
                }
                // Закрывающаяся скобка:
                else if (lexema is BracketLexema && !((BracketLexema)lexema).IsOpen)
                {
                    // Пока лексема на вершине стэка не станет открывающейся скобкой, то
                    //     Если стэк стал пустым - в выражении ошибка
                    //     Добавляем лексемы-операторы в выходную очередь
                    // Удаляем из стэка открывающуюся скобку
                    bool stackTopLexemaIsOpenBracket = false;

                    do
                    {
                        stackIsEmpty = stack.IsEmpty;
                        if (!stackIsEmpty)
                        {
                            stackTopLexema = stack.Top();
                            stackTopLexemaIsOpenBracket = (stackTopLexema is BracketLexema) && ((BracketLexema)stackTopLexema).IsOpen;

                            if (!stackTopLexemaIsOpenBracket)
                            {
                                result.List.Add(stack.Pop());
                            }
                            else
                            {
                                // Удаляем из стэка открывающуюся скобку
                                stack.Pop();
                            }
                        }
                        else
                        {
                            // Ошибка, не встретилась открывающаяся скобка
                            throw new Exception();
                        }
                    } while (!stackIsEmpty && !stackTopLexemaIsOpenBracket);
                }
                else
                {
                    // Ошибка, неверная лексема
                    throw new Exception();
                }
            }

            // Если во входной строке больше не осталось лексем:
            // Пока в стэке есть операторы:
            //     Если на вершине стэка скобка - в выражении допущена ошибка
            //     Добавляем операторы из стэка в выходную очередь

            bool stackTopLexemaIsBracket = false;

            do
            {
                stackIsEmpty = stack.IsEmpty;
                if (!stackIsEmpty)
                {
                    stackTopLexema = stack.Top();
                    stackTopLexemaIsBracket = stackTopLexema is BracketLexema;

                    if (stackTopLexemaIsBracket)
                    {
                        // Ошибка в выражении
                        throw new Exception();
                    } 
                    else
                    {
                        result.List.Add(stack.Pop());
                    }

                }
            } while (!stackIsEmpty);

            return result;
        }

        /// <summary>
        /// Возвращает значение выражения.
        /// </summary>
        /// <returns>Значение выражения.</returns>
        public double GetValue()
        {
            var stack = new Stack<Lexema>();

            foreach (Lexema lexema in _reversedPolishNotationLexemes.List)
            {
                // Если лексема - числовая, то помещаем её на вершину стэка.
                if (lexema is NumericLexema)
                {
                    stack.Push(lexema);
                }
                // Если лексема - оператор, то:
                //     Соответствующая операция выполняется над требуемым количеством значений, извлеченных из стэка, взятых в порядке добавления.
                //     Результат выполнения операции помещаяется на вершину стэка.
                else if (lexema is OperatorLexema)
                {
                    NumericLexema secondOperand = (NumericLexema)stack.Pop();
                    NumericLexema firstOperand = (NumericLexema)stack.Pop();
                    OperatorLexema lexemaAsOperator = ((OperatorLexema)lexema);
                    double value = lexemaAsOperator.Evaluate(firstOperand.Value, secondOperand.Value);
                    stack.Push(new NumericLexema(value.ToString()));
                }
                else
                {
                    // Ошибка в выражении
                    throw new Exception();
                }
            }

            // После полной обработки входного набора лексем, результат выражения лежит на вершине стэка.
            return ((NumericLexema)stack.Pop()).Value;
        }
    }
}

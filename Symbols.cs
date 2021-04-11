using System.Collections.Generic;

namespace _02_ObjectOrientedProgrammingExample
{
    class Symbols
    {
        private List<AbstractSymbol> _list { get; }

        /// <summary>
        /// Индексатор коллекции символов.
        /// Возвращает символ по индексу.
        /// Возвращает null-символ при передачи индекса за границей допустимых.
        /// </summary>
        /// <param name="index">Индекс.</param>
        /// <returns>Символ.</returns>
        public AbstractSymbol this[int index]
        {
            get
            {
                if (index < 0 || index >= _list.Count)
                {
                    return new NullSymbol();
                }
                else
                {
                    return _list[index];
                }
            }
        }

        /// <summary>
        /// Возвращает количество символов в коллекции.
        /// </summary>
        public int Count { get => _list.Count; }

        public Symbols(string inputString)
        {
            _list = new List<AbstractSymbol>();

            foreach (char character in inputString)
            {
                _list.Add(new Symbol(character));
            }
        }
    }
}

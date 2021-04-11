using System.Collections.Generic;
using System.Linq;

namespace _02_ObjectOrientedProgrammingExample
{
    class Stack<T>
    {
        private List<T> _list;

        /// <summary>
        /// Является ли стэк пустым.
        /// </summary>
        public bool IsEmpty { get => _list.Count == 0; }

        public Stack()
        {
            _list = new List<T>();
        }

        /// <summary>
        /// Вставка элемента в стэк.
        /// </summary>
        /// <param name="item">Вставляемый элемент.</param>
        public void Push(T item)
        {
            _list.Add(item);
        }

        /// <summary>
        /// Выталкивает верхний элемент из стэка.
        /// </summary>
        /// <returns>Верхний элемент.</returns>
        public T Pop()
        {
            var result = _list.Last();
            _list.Remove(result);
            return result;
        }

        /// <summary>
        /// Возвращает верхний элемент стэка.
        /// </summary>
        /// <returns>Верхний элемент.</returns>
        public T Top()
        {
            return _list.Last();
        }

    }
}

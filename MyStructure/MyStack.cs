using System.Collections;

namespace MyStructure
{
    public class MyStack<T> : IEnumerable<T>
    {
        private MySLinkedList<T> _list;

        public int Count
        {
            get { return _list.Count; }
        }

        public MyStack()
        {
            _list = new MySLinkedList<T>();
        }

        public void Push(T item)
        {
            _list.AddFirst(item);
        }

        public T Peek()
        {
            return _list.First.Data;
        }

        public T Pop()
        {
            return _list.RemoveFirst();
        }

        public T[] ToArray()
        {
            return _list.ToArray();
        }

        public void Clear()
        {
            _list.Clear();
        }

        public void ForEach(Action<T> action)
        {
            foreach (var item in this)
            {
                action.Invoke(item);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

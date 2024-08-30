using System.Collections;

namespace MyStructure
{
    public class MyQueue<T> : IEnumerable<T>
    {
        private MySLinkedList<T> _list;

        public MyQueue()
        {
            _list = new MySLinkedList<T>(false);
        }

        public int Count
        {
            get { return _list.Count; }
        }

        public void Enqueue(T item)
        {
            _list.AddFirst(item);
        }

        public T Dequeue()
        {
            return _list.RemoveLast();
        }

        public T Peek()
        {
            return _list.Last.Data;
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
            return GetEnumerator();
        }
    }
}


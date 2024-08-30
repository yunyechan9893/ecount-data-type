using System.Collections;

namespace MyStructure
{
    public class SLinkedNode<T>
    {
        public T Data;
        public SLinkedNode<T> Next;
        public SLinkedNode<T> Prev;

        public SLinkedNode(T data)
        {
            this.Data = data;
        }

        public SLinkedNode(T data, SLinkedNode<T>? next = default, SLinkedNode<T>? prev = default)
        {
            this.Data = data;
            this.Next = next;
            this.Prev = prev;
        }

        public override string ToString()
        {
            var nextData = Next != null ? Data : default;
            var prevData = Next != null ? Data : default;
            return $"Data : {Data}, Next Data: {nextData}, Prev: {prevData} ";
        }
    }

    public partial class MySLinkedList<T> : IEnumerable<T>
    {
        private SLinkedNode<T> _head;
        private SLinkedNode<T> _tail;
        private int _size; // 현재 저장된 원소 개수
        private bool _isDesc;
        private IEqualityComparer<T> _comparer;

        public MySLinkedList(IEqualityComparer<T> comparer) : this(comparer, true)
        {

        }

        public MySLinkedList(bool isDesc = true) : this(null, isDesc)
        {

        }

        public MySLinkedList(IEqualityComparer<T> comparer, bool isDesc)
        {
            _isDesc = isDesc;
            _comparer = comparer ?? EqualityComparer<T>.Default;
        }

        public int Count
        {
            get { return _size; }
        }

        public SLinkedNode<T> First
        {
            get { return _head; }
        }

        public SLinkedNode<T> Last
        {
            get { return _tail; }
        }


        // METHODS
        //_________________________________________________________________________________________
        // 정책 = 아이템을 빼낼 때 널이면 에러 발생
        // 아이템이 1개일 때 헤더와 테일이 동일


        public void AddFirst(T data)
        {
            SLinkedNode<T> newNode = new SLinkedNode<T>(data);

            _size++;

            var curNode = _head;
            _head = newNode;
            _head.Next = curNode;

            if (_tail == null)
            {
                _tail = newNode;
            }
            else
            {
                curNode.Prev = newNode;
            }
        }

        public void AddLast(T data)
        {
            SLinkedNode<T> newNode = new SLinkedNode<T>(data);

            _size++;

            var curNode = _tail;
            _tail = newNode;
            _tail.Prev = curNode;

            if (_head == null)
            {
                _head = newNode;
            }
            else
            {
                curNode.Next = newNode;
            }
        }


        public T[] ToArray()
        {
            T[] objArray = new T[_size];
            int i = 0;

            var curNode = _head;
            objArray[i++] = curNode.Data;

            while (true)
            {
                curNode = curNode.Next;
                objArray[i++] = curNode.Data;

                if (curNode.Next == null)
                {
                    break;
                }
            }

            return objArray;
        }

        public bool Contains(T data)
        {
            return Contains((node) => _comparer.Equals(node.Data, data));
        }

        public bool Contains(Predicate<SLinkedNode<T>> match)
        {
            var curNode = _head;

            while (curNode != null)
            {
                if (match(curNode))
                {
                    return true;
                }

                curNode = curNode.Next;

                if (curNode == null)
                {
                    return false;
                }
            }

            return false;
        }

        public void Clear()
        {
            _head = null;
            _tail = null;
            _size = 0;
        }
    }

    /*
     * Find
     */
    public partial class MySLinkedList<T> : IEnumerable<T>
    {
        public SLinkedNode<T>? Find(T data)
        {
            ValidNullChecking(data, "노드 탐색시 Null을 탐색할 수 없습니다");

            return FindNodeByCondition((curNode) => curNode.Data.Equals(data), (curNode) => curNode.Next, _head);
        }

        public SLinkedNode<T>? Find(Predicate<SLinkedNode<T>> match)
        {
            return FindNodeByCondition(match, (curNode) => curNode.Next, _head);
        }

        public SLinkedNode<T>? FindLast(T data)
        {
            ValidNullChecking(data, "노드 탐색시 Null을 탐색할 수 없습니다");

            return FindNodeByCondition((curNode) => curNode.Data.Equals(data), (curNode) => curNode.Prev, _tail);
        }

        public SLinkedNode<T>? FindLast(Predicate<SLinkedNode<T>> match)
        {
            return FindNodeByCondition(match, (curNode) => curNode.Prev, _tail);
        }

        private SLinkedNode<T>? FindNodeByCondition(Predicate<SLinkedNode<T>> pred, Func<SLinkedNode<T>, SLinkedNode<T>> nextFunc, SLinkedNode<T> curNode)
        {
            while (curNode != null)
            {
                if (pred.Invoke(curNode))
                {
                    return curNode;
                }

                curNode = nextFunc.Invoke(curNode);

                if (curNode == null)
                {
                    return default;
                }
            }

            return default;
        }
    }

    /*
     * Remove
     */
    public partial class MySLinkedList<T> : IEnumerable<T>
    {
            // Remove의 최상위 부모
        private void Remove(SLinkedNode<T> node)
        {
            ValidNullChecking(_tail, "삭제 시 Node가 Null일 수 없습니다.");

            var nextNode = node.Next;
            var prevNode = node.Prev;

            if (nextNode != null)
            {
                nextNode.Prev = prevNode;
            }

            if (prevNode != null)
            {
                prevNode.Next = nextNode;
            }

            if (_head == node)
            {
                _head = _head.Next;
            }

            if (_tail == node)
            {
                _tail = _tail.Prev;
            }

            _size--;
        }

        public bool Remove(T data)
        {
            var curNode = Find(data);

            if (curNode == null)
            {
                return false;
            }

            Remove(curNode);

            return true;
        }

        public bool Remove(Predicate<SLinkedNode<T>> match)
        {
            var curNode = _head;

            while (true)
            {
                if (match(curNode))
                {
                    Remove(curNode);

                    return true;
                }

                curNode = curNode.Next;

                if (curNode == null)
                {
                    return false;
                }
            }
        }

        public T RemoveFirst()
        {
            ValidNullChecking(_tail, "Linked List에 값이 하나도 없습니다.");

            var curNode = _head.Data;
            Remove(_head);

            return curNode;
        }

        public T RemoveLast()
        {
            ValidNullChecking(_tail, "Linked List에 값이 하나도 없습니다.");

            var curNode = _tail.Data;
            Remove(_tail);

            return curNode;
        }

    }

    /*
     * Enumerable
     */
    public partial class MySLinkedList<T> : IEnumerable<T>
    {
        public IEnumerator<T> GetEnumerator()
        {
            return new MySLinkedListEnumerator(this, _isDesc);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class MySLinkedListEnumerator : IEnumerator<T>
        {
            private SLinkedNode<T> _node;
            private T _current;
            private bool _isDesc;

            public MySLinkedListEnumerator(MySLinkedList<T> list, bool isDesc)
            {
                _isDesc = isDesc;
                _node = _isDesc ? list.First : list.Last;
                _current = default(T);
                _isDesc = isDesc;
            }

            public T Current
            {
                get { return _current; }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public bool MoveNext()
            {
                if (_node != null)
                {
                    _current = _node.Data;
                    _node = _isDesc ? _node.Next : _node.Prev;

                    return true;
                }

                return false;
            }

            public void Reset()
            {
                _current = default(T);
                _node = null;
            }

            public void Dispose()
            {

            }
        }
    }

    public partial class MySLinkedList<T> : IEnumerable<T>
    {
        private void ValidNullChecking<V>(V data, string message)
        {
            if (data == null)
            {
                throw new NullReferenceException(message);
            }
        }
    }
}


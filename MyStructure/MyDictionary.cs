using System.Collections;

namespace MyStructure
{

    public class KeyValuePairComparer<TKey, TValue> : IEqualityComparer<KeyValuePair<TKey, TValue>>
    {
        private IEqualityComparer<TKey> _keyComparer;

        public KeyValuePairComparer(IEqualityComparer<TKey> keyComparer)
        {
            _keyComparer = keyComparer ?? EqualityComparer<TKey>.Default;
        }

        public bool Equals(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
        {
            return _keyComparer.Equals(x.Key, y.Key);
        }

        public int GetHashCode(KeyValuePair<TKey, TValue> obj)
        {
            return _keyComparer.GetHashCode(obj.Key);
        }
    }

    public struct KeyValuePair<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public KeyValuePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }

    public class MyDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private MySLinkedList<KeyValuePair<TKey, TValue>>[] _bucket;
        private IEqualityComparer<TKey> _comparer;
        private int _count;


        public MyDictionary(IEqualityComparer<TKey> equalityComparer = null)
            : this(3, equalityComparer)
        {
        }

        public MyDictionary(int capacity, IEqualityComparer<TKey> equalityComparer = null)
        {
            int size = HashHelpers.GetPrime(capacity);
            this._bucket = new MySLinkedList<KeyValuePair<TKey, TValue>>[size];
            this._comparer = equalityComparer ?? EqualityComparer<TKey>.Default;
        }


        // PROPERTIES
        //_________________________________________________________________________________________

        public int Count
        {
            get { return _count; }
        }

        public TValue this[TKey key]
        {
            get { return GetValue(key, false); }
            set { SetValue(key, value, false); }
        }

        public MySLinkedList<KeyValuePair<TKey, TValue>>[] Storage
        {
            get { return _bucket; }
        }


        // METHODS
        //_________________________________________________________________________________________

        private void Resize(int capacity)
        {
                  // 새로운 크기로 배열 새로 할당
            var newSize = HashHelpers.GetPrime(capacity);
            var newBucket = new MySLinkedList<KeyValuePair<TKey, TValue>>[newSize];

                  // 기존 버킷배열에 저장되어 있는 연결리스트 항목을 순회한다.(루프)
            for (int i = 0; i < _bucket.Length; i++)
            {
                var list = _bucket[i];

                if (list == null)
                {
                    continue;
                }

                foreach (var item in list)
                {
                    int index = GetBucketIndex(item.Key, capacity);
                    var newList = newBucket[index];
                    if (newList == null)
                    {
                                    // MySLinkedList 생성할 때 _compara를 삭제했음 이따 어떻게 될 지 보자
                        newBucket[index] = new MySLinkedList<KeyValuePair<TKey, TValue>>();
                        newList = newBucket[index];
                    }

                    newBucket[index].AddFirst(item);
                }
            }

            _bucket = newBucket;
        }


        internal int GetBucketIndex(TKey key, int bucketSize)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key 값이 Null 입니다.");
            }

            int hash = Math.Abs(key.GetHashCode()); // TODO:EqualityComparer를 이용하여 item을 해싱한 해쉬코드와 버킷(배열)의 크기를 이용하여 해당 인덱스를 구한다.
            return hash % bucketSize;
        }

            // 같은 어셈블리(DLL 또는 EXE) 안에 있는 다른 클래스에서만 공개 메소드로 사용 할 수 있도록 접근 제어자를 internal로 정의한다.
            // HashMap 구현시 해당 메소드를 호출하여 사용 할 예정
        internal MySLinkedList<KeyValuePair<TKey, TValue>> FindBucketList(TKey key)
        {
            int index = GetBucketIndex(key, _bucket.Length);
            return _bucket[index];
        }

        internal SLinkedNode<KeyValuePair<TKey, TValue>>? FindEntry(TKey key)
        {
            var list = FindBucketList(key);
            if (list != null)
            {
                return list.Find((n) => _comparer.Equals(n.Data.Key, key));
            }
            return null;
        }

            // 같은 어셈블리(DLL 또는 EXE) 안에 있는 다른 클래스에서만 공개 메소드로 사용 할 수 있도록 접근 제어자를 internal로 정의한다.
            // HashMap 구현시 해당 메소드를 호출하여 사용 할 예정
        internal TValue GetValue(TKey key, bool raiseError)
        {
            var node = FindEntry(key);
            if (node == null)
            {
                if (raiseError)
                {
                    throw new ArgumentException("The key doesn't exist in the Dictionary.", key.ToString());
                }
                return default(TValue);
            }
            return node.Data.Value;
        }

             // 같은 어셈블리(DLL 또는 EXE) 안에 있는 다른 클래스에서만 공개 메소드로 사용 할 수 있도록 접근 제어자를 internal로 정의한다.
             // HashMap 구현시 해당 메소드를 호출하여 사용 할 예정
        internal bool SetValue(TKey key, TValue value, bool raiseError)
        {
                  // 현재 데이터 개수가 해시 버킷 개수의 125% 가 넘으면 리사이징한다.
            if (_count >= _bucket.Length * HashHelpers.RESIZE_FACTOR)
            {
                Resize(_bucket.Length + HashHelpers.PRIME_FACTOR);
            }

            int index = GetBucketIndex(key, _bucket.Length);
            var list = _bucket[index];

            if (list == null)
            {
                         // TODO: 해당 버킷에 이미 만들어진 연결리스트가 없다면 새로 만들고 버킷에 할당한다.
                _bucket[index] = new MySLinkedList<KeyValuePair<TKey, TValue>>();
                list = _bucket[index];
            }
            else
            {
                KeyValuePair<TKey, TValue> keyValue = new KeyValuePair<TKey, TValue>(key, value);
                var node = list.Find((curNode) => _comparer.Equals(curNode.Data.Key, keyValue.Key));// TODO: EqualityComparer를 이용하여 list에 key와 같은 중복된 항목이 있는지 찾는다.
                if (node != null)
                {   
                               // 중복된 값이 있는 경우
                    if (raiseError)
                    {
                        throw new ArgumentException("An element with the same key already exists in the Dictionary.", key.ToString());
                    }

                              // 기존에 저장되어 있던 값을 새로 설정되는 값으로 변경한다.
                    node.Data = new KeyValuePair<TKey, TValue>(key, value);
                    return false;
                }
            }

                   // TODO: 연결리스트의 마지막에 해당 항목을 추가하고 카운트값을 하나 늘린다.
            list.AddLast(new KeyValuePair<TKey, TValue>(key, value));
            _count++;

            return true;
        }

        public void Add(TKey key, TValue value)
        {
            try
            {
                SetValue(key, value, true);
            }
            catch (ArgumentException)
            {

            }
        }

        public bool Remove(TKey key)
        {
            var list = FindBucketList(key);
            if (list != null)
            {
                        // TODO: 연결리스트에서 해당 항목을 찾은 후 있다면
                        // 해당 노드를 연결리스트에서 삭제하고 카운트값을 하나 줄인 후 true 리턴.
                foreach (var item in list)
                {
                    if (item.Key.Equals(key))
                    {
                        list.Remove(item);
                        _count--;
                        return true;
                    }
                }
            }

            return false;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            var node = FindEntry(key);// TODO: 찾고자 하는 키가 저장된 LinkedNode를 찾는다
            if (node != null)
            {
                value = node.Data.Value;
                return true;
            }

            value = default(TValue);
            return false;
        }

        public bool Contains(TKey key)
        {
            var list = FindBucketList(key);
            if (list != null)
            {
                return list.Contains((n) => _comparer.Equals(n.Data.Key, key));
            }

            return false;
        }

        public void Foreach(Action<KeyValuePair<TKey, TValue>> action)
        {
            foreach (var item in this)
            {
                action(item);
            }
        }

        public void Clear()
        {
            _bucket = new MySLinkedList<KeyValuePair<TKey, TValue>>[_bucket.Length];
            _count = 0;
        }


        // IEnumerable 인터페이스 구현
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return new MyDictEnumerator(this);
        }

        public IEnumerable<TKey> Keys
        {
            get { return new MyDictKeyCollection(this); }
        }


        public IEnumerable<TValue> Values
        {
            get { return new MyDictValueCollection(this); }
        }

        // NESTED Helper Class
        //_________________________________________________________________________________________

        public class MyDictEnumerator : IEnumerator<KeyValuePair<TKey, TValue>>
        {
            protected MyDictionary<TKey, TValue> _dict;
            protected IEnumerator<KeyValuePair<TKey, TValue>> _iterator;
            protected int _index;
            public KeyValuePair<TKey, TValue> Current
            {
                get { return _iterator.Current; }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            KeyValuePair<TKey, TValue> IEnumerator<KeyValuePair<TKey, TValue>>.Current { get { return this.Current; } }

            public MyDictEnumerator(MyDictionary<TKey, TValue> dict)
            {
                this._dict = dict;
                this._index = 0;
                this._iterator = FindNextEnumerator();
            }

            public void Dispose()
            {

            }

            protected IEnumerator<KeyValuePair<TKey, TValue>> FindNextEnumerator()
            {
                // TODO: 현재 인덱스가 딕셔너리의 버킷배열의 크기보다 작을때까지 반복한다.
                // 버킷배열에 할당된 연결리스트를 가져온 후 현재 인덱스를 하나 증가시킨다.
                // 연결리스트가 존재하고 리스트에 추가되어 있는 항목의 갯수가 0보다 크다면
                // 연결리스트의 GetEnumerator() 결과를 리턴한다.

                while (_index < _dict._bucket.Length)
                {
                    var list = _dict._bucket[_index++];

                    if (list != null && list.Count > 0)
                    {
                        return list.GetEnumerator();
                    }
                }

                return null;
            }

            public bool MoveNext()
            {
                // _iterator가 null이 아니고 _iterator의 MoveNext() 결과값이 false 일때까지
                // FindNextEnumerator를 호출하여 다음 버킷에 있는 연결리스트를 찾는다.
                while (_iterator != null && !_iterator.MoveNext())
                {
                    _iterator = FindNextEnumerator();

                    if (_iterator == null)
                    {
                        return false;
                    }
                }

                return true;
            }

            public void Reset()
            {

            }
        }

        abstract class MyDictEnumeratorBase<T> : IEnumerator<T>
        {
            protected MyDictionary<TKey, TValue> _dict;
            protected IEnumerator<KeyValuePair<TKey, TValue>> _iterator;
            protected int _index;


            public MyDictEnumeratorBase(MyDictionary<TKey, TValue> dict)
            {
                this._dict = dict;
                this._index = 0;
                this._iterator = FindNextEnumerator();
            }

            public void Dispose()
            {

            }


            protected IEnumerator<KeyValuePair<TKey, TValue>> FindNextEnumerator()
            {
                while (_index < _dict.Storage.Length)
                {
                    var list = _dict.Storage[_index++];

                    if (list != null && list.Count > 0)
                    {

                        return list.GetEnumerator();
                    }
                }

                return null;
            }

            public abstract T Current { get; }

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                        // _iterator가 null이 아니고 _iterator의 MoveNext() 결과값이 false 일때까지
                        // FindNextEnumerator를 호출하여 다음 버킷에 있는 연결리스트를 찾는다.
                while (_iterator != null && !_iterator.MoveNext())
                {
                    _iterator = FindNextEnumerator();

                    if (_iterator == null)
                    {
                        return false;
                    }
                }

                return true;
            }

            public void Reset()
            {

            }

        }
        class MyDictKeyEnumerator : MyDictEnumeratorBase<TKey>
        {
            public MyDictKeyEnumerator(MyDictionary<TKey, TValue> dict) : base(dict)
            {
            }

            // TODO:
            public override TKey Current { get { return _iterator.Current.Key; } }
        }

        class MyDictValueEnumerator : MyDictEnumeratorBase<TValue>
        {
            public MyDictValueEnumerator(MyDictionary<TKey, TValue> dict) : base(dict)
            {
            }

            // TODO:
            public override TValue Current { get { return _iterator.Current.Value; } }
        }


        abstract class MyDictCollectionBase<T> : IEnumerable<T>
        {
            protected MyDictionary<TKey, TValue> _dict;

            protected MyDictCollectionBase(MyDictionary<TKey, TValue> dict)
            {
                this._dict = dict;
            }


            public abstract IEnumerator<T> GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }

            // 키 열거를 위한 객체를 MyDictCollectionBase 에서 상속하는 구조로 코드를 작성하시오
        class MyDictKeyCollection : MyDictCollectionBase<TKey>
        {
            public MyDictKeyCollection(MyDictionary<TKey, TValue> dict)
                : base(dict)
            {
            }

            public override IEnumerator<TKey> GetEnumerator()
            {
                return new MyDictKeyEnumerator(_dict);
            }
        }

        class MyDictValueCollection : MyDictCollectionBase<TValue>
        {
            public MyDictValueCollection(MyDictionary<TKey, TValue> dict)
                : base(dict)
            {
            }

            public override IEnumerator<TValue> GetEnumerator()
            {
                return new MyDictValueEnumerator(_dict);
            }
        }
    }
}

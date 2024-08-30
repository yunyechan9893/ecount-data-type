using System.Collections;

namespace MyStructure
{
    public class MyHashMap<TValue> : IEnumerable<KeyValuePair<string, MyList<TValue>>>
    {
        private MyDictionary<string, MyList<TValue>> _dict;
        private MyList<string> _keyList;


        public MyHashMap(IEqualityComparer<string> equalityComparer = null)
            : this(3, equalityComparer)
        {
        }

        public MyHashMap(int capacity, IEqualityComparer<string> equalityComparer = null)
        {
            var comparer = equalityComparer ?? StringComparer.OrdinalIgnoreCase;    // 대소문자 구분없이 비교하는 비교자를 기본으로 사용한다.
            this._dict = new MyDictionary<string, MyList<TValue>>(capacity, comparer);
            this._keyList = new MyList<string>(capacity);    // 추가되는 키를 순서대로 저장 할 용도의 리스트 객체
        }

        public int Count
        {
            get { return _keyList.Count; }
        }

        public TValue this[int index]
        {
            get {
                var key = _keyList[index];
                return GetValue(key);
            }
            set
            {
                var key = _keyList[index];
                SetValue(key, value);
            }
        }

        public TValue this[string key]
        {
            get { return GetValue(key); }
            set { SetValue(key, value); }
        }

        public IEnumerable<string> Keys
        {
            get { return new MyMapKeyCollection(_keyList); }
        }

        public void Add(string key, TValue value)
        {
            SetValue(key, value);
        }

        public TValue[] GetAllValues()
        {
            MyList<TValue> values = new MyList<TValue>(_keyList.Count);

            foreach (string key in this._keyList)
            {

                if (key == null)
                {
                    continue;
                }

                values.Add(GetValue(key));
            }
            return values.ToArray();
        }

        public TValue[] GetValues(string key)
        {
            var list = _dict.GetValue(key, false);
            if (list == null)
            {
                return Array.Empty<TValue>();
            }
            return list.ToArray();
        }

        protected TValue GetValue(string key)
        {
            var list = _dict.GetValue(key, false);
            if (list == null)
            {
                return default(TValue);
            }

            return list[0]; // 첫번째 요소를 리턴한다.
        }

        protected void SetValue(string key, TValue value)
        {
            var list = _dict[key];
            if (list == null)
            {
                    // 리스트 새로 생성
                    // 딕셔너리의 해당 key에 새로 생성한 리스트 설정
                    // 키 목록 리스트에 key 추가
                    _dict.Add(key, new MyList<TValue>() { value });
                    _keyList.Add(key);
            } else
            {
                // 리스트에 값 추가
                _dict[key].Add(value);
            }
        }

        public bool Remove(string key)
        {
            _dict[key].Remove();
            _keyList.Remove();

            return false;
        }

        public IEnumerator<KeyValuePair<string, MyList<TValue>>> GetEnumerator()
        {
            return new MyDictEnumerator(_dict, _keyList);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public class MyDictEnumerator : IEnumerator<KeyValuePair<string, MyList<TValue>>>
        {
            protected MyDictionary<string, MyList<TValue>> _dict;
            private MyList<string> _keys;
            protected int _index;
            private KeyValuePair<string, MyList<TValue>> _current { get; set; }
            public KeyValuePair<string, MyList<TValue>> Current
            {
                get { return _current; }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            KeyValuePair<string, MyList<TValue>> IEnumerator<KeyValuePair<string, MyList<TValue>>>.Current { get { return this.Current; } }

            public MyDictEnumerator(MyDictionary<string, MyList<TValue>> dict, MyList<string> keys)
            {
                this._dict = dict;
                this._keys = keys;
                this._index = 0;
            }

            public void Dispose()
            {

            }

            public bool MoveNext()
            {
                if (_index < _keys.Count)
                {
                    var key = _keys[_index++];

                    if (key == null)
                    {
                        return false;
                    }

                    var values = _dict[key];
                    _current = new KeyValuePair<string, MyList<TValue>>(key, values);

                    return true;
                }

                return false;
            }

            public void Reset()
            {

            }
        }

        public class MyMapKeyCollection : IEnumerable<string>
        {

            private MyList<string> _list;

            public MyMapKeyCollection(MyList<string> list)
            {
                _list = list;
            }

            public IEnumerator<string> GetEnumerator()
            {
                return new MyMapKeyEnumerator(_list);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        class MyMapKeyEnumerator : IEnumerator<string>
        {
            private int _size;
            private int _index;
            private string _current;
            private MyList<string> _list;
            public string Current { get { return _current; } }

            object IEnumerator.Current { get { return _current; } }

            public MyMapKeyEnumerator(MyList<string> list)
            {
                _list = list;
                _size = _list.Count;
                _index = 0;
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (_size > _index)
                {
                    _current = _list[_index++];
                    return true;
                }

                return false;
            }

            public void Reset()
            {
            }
        }
    }
}

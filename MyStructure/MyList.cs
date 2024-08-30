using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace MyStructure
{
    public partial class MyList<T> : IEnumerable<T>
    {
        private T[] _array;
        private int _size;
        private IEqualityComparer<T> _comparer;

        public MyList()
            : this(EqualityComparer<T>.Default, 4)
        {
        }

        public MyList(int capacity)
            : this(EqualityComparer<T>.Default, capacity)
        {
        }

        // 생성자
        public MyList(IEqualityComparer<T> comparer)
            : this(comparer, 4)
        {
        }

        public MyList(IEqualityComparer<T> comparer, int capacity)
        {
            this._size = 0;
            this._array = new T[capacity];
            this._comparer = comparer;
        }

        public int Count
        {
            get { return _size; }
        }

        public int Capacity
        {
            get { return _array.Length; }
            set
            {
                if (value <= Capacity)
                    throw new ArgumentOutOfRangeException();

                Array Ts = new T[value];
                CopyTo(Ts);
            }
        }

        // 외부에서 배열 요소에 접근을 위한 인덱서 프로퍼티
        public T this[int index]
        {
            get
            {
                if (index > _size)
                    throw new IndexOutOfRangeException();
                return _array[index];
            }
            set
            {
                if (index >= _size)
                    throw new IndexOutOfRangeException();
                _array[index] = value;
            }
        }

        private void EnsureCapacity()
        {
            int capacity = _array.Length;
            if (_size >= capacity)
            {
                this.Capacity = capacity == 0 ? 4 : capacity * 2;
            }
        }

        public void Add(T element)
        {
            // 배열 공간 체크, 부족할 시 resize
            EnsureCapacity();

            addElement(_size, element);
        }

        // 해당 위치에 원소 추가
        public void Insert(int index, T element)
        {
            EnsureCapacity();

            for (int i = _size; i > 0; i--)
            {
                _array[i] = _array[i - 1];
            }

            addElement(index, element);

            foreach (var item in _array)
            {
                Console.WriteLine(item);
            }
        }

        private void addElement(int index, T element)
        {
            _array[index] = element;
            _size++;
        }

        public T Find(Predicate<T> match)
        {
            foreach (var item in _array)
            {
                if (match(item))
                {
                    return item;
                }
            }

            throw new InvalidOperationException();
        }


        // 해당 위치의 원소 삭제
        public void RemoveAt(int index)
        {
            ValidOutOfIndex(index);

            RemoveRange(index, 1);

            if (_array.Length - 4 < _size)
            {
                T[] array = new T[_array.Length];
                CopyTo(array);
            }
        }

        public void RemoveRange(int index, int count)
        {
            ValidOutOfIndex(index);
            ValidOutOfIndex(index + count);

            for (int i = 0; count > i; i++)
            {
                for (int j = index; _size - 1 > j; j++)
                {
                    _array[j] = _array[j + 1];
                }

                //_array[_size] = Object.;
                _array[_size - 1] = default;
                _size--;
            }
        }

        public void CopyTo(Array array)
        {
            CopyTo(array, 0);
        }

        public void CopyTo(Array array, int arrayIndex)
        {
            ValidOutOfIndex(arrayIndex);

            for (int i = arrayIndex; i < _size; i++)
            {
                if (_array[i] == null)
                {
                    continue;
                }

                array.SetValue(_array[i], i);
            }

            _array = (T[])array;
        }

        T[] ValueCopy(T[] array, int arrayIndex, int count = -1)
        {
            ValidOutOfIndex(arrayIndex);

            count = count == -1 ? _size : count;

            for (int i = arrayIndex; i < count; i++)
            {
                if (_array[i] == null)
                {
                    continue;
                }

                array.SetValue(_array[i], i);
            }

            return array;
        }

        public void Swap(int index1, int index2)
        {
            ValidOutOfIndex(index1);
            ValidOutOfIndex(index2);

            if (index1 >= _size || index2 >= _size)
            {
                throw new ArgumentOutOfRangeException();
            }

            var temp = _array[index1];
            _array[index1] = _array[index2];
            _array[index2] = temp;
        }

        public T[] ToArray()
        {
            T[] newArray = new T[_size];
            Array.Copy(_array, 0, newArray, 0, _size);
            return newArray;
        }

        public void Clear()
        {
            _size = 0;
        }

        public bool Contains(T item)
        {
            foreach (var item2 in _array)
            {
                if (_comparer.Equals(item, item2))
                {
                    return true;
                }
            }

            return false;
        }

        public bool Remove()
        {
            if (_array.Length > 0)
            {
                RemoveRange(0, 1);
                return true;
            }

            return false;
        }

        public int BinarySearch(T item)
        {
            return BinarySearch(item, Comparer<T>.Default);
        }

        public int BinarySearch(T item, IComparer<T> comparer)
        {
            return Array.BinarySearch(_array, 0, _size, item, comparer);
        }

        public void Sort()
        {
            Sort(Comparer<T>.Default);
        }

        public void Sort(IComparer<T> comparer)
        {
            T[] newArray = new T[_size];

            newArray = ValueCopy(newArray, 0, _size);

            Array.Sort(newArray, comparer);

            for (int i = 0; newArray.Length > i; i++)
            {
                _array[i] = newArray[i];
            }
        }



        public void ForEach(Action<T> action)
        {
            foreach (var item in _array)
            {
                action(item);
            }
        }

        public bool Remove(Predicate<T> match)
        {
            for (int i = 0; i < _size; i++)
            {
                if (match(_array[i]))
                {
                    RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        public bool RemoveAll(Predicate<T> match)
        {
            bool isDeletedItem = false;
            for (int i = 0; i < _size; i++)
            {
                if (match(_array[i]))
                {
                    RemoveAt(i);
                    isDeletedItem = true;
                }
            }

            return isDeletedItem;
        }


        private void ValidOutOfIndex(int index)
        {
            if (index > _size || index < 0)
            {
                throw new ArgumentOutOfRangeException("인덱스 초과");
            }
        }

        private void ValidNonNull(T obj)
        {
            if (obj == null)
            {
                throw new NullReferenceException("값이 비었습니다");
            }
        }

        private void ValidNonNull(string str)
        {
            if (str == null)
            {
                throw new NullReferenceException("값이 비었습니다");
            }
        }
    }

    /*
     * indexOf
     */
    public partial class MyList<T> : IEnumerable<T>
    {
        public int FindLastIndex(Predicate<T> match, int startIndex = 0, int count = -1)
        {
            int index = _size - 1 - startIndex;
            int endIndex = count != -1 ? index - count : 0;

            ValidOutOfIndex(index);
            ValidOutOfIndex(endIndex);

            for (int i = index; i >= endIndex; i--)
            {
                if (match(_array[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        public int FindIndex(Predicate<T> match, int startIndex = 0, int count = -1)
        {
            var endIndex = count != -1 ? startIndex + count : _size;

            ValidOutOfIndex(endIndex);

            for (int i = startIndex; i < endIndex; i++)
            {
                if (match(_array[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        public int IndexOf(T item, int index = 0, int count = -1)
        {
            ValidNonNull(item);

            var startPosition = index;
            var countPosition = count != -1 ? index + count : _size - 1;

            ValidOutOfIndex(startPosition);
            ValidOutOfIndex(countPosition);

            for (int i = startPosition; i < countPosition; i++)
            {
                if (_comparer.Equals(item, _array[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        public int LastIndexOf(T item, int index = 0, int count = -1)
        {
            ValidNonNull(item);

            var startPosition = _size - 1 - index;
            var countPosition = count != -1 ? startPosition - count : 0;

            ValidOutOfIndex(startPosition);
            ValidOutOfIndex(countPosition);

            for (int i = startPosition; i > countPosition; i--)
            {
                if (_comparer.Equals(item, _array[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        public bool Contains(Predicate<T> match)
        {
            foreach (var item in _array)
            {
                if (match(item))
                {
                    return true;
                }
            }

            return false;
        }
    }


    /*
     * Enumerable
     */
    public partial class MyList<T> : IEnumerable<T>
    {
        public IEnumerator<T> GetEnumerator()
        {
            return new MyListEnumerator<T>(this);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        public class MyListEnumerator<T> : IEnumerator<T>
        {
            private MyList<T> _list;
            private T _current;
            private int _index;

            public MyListEnumerator(MyList<T> list)
            {
                _list = list;
                _index = 0;
                _current = default(T);
            }

            public T Current
            {
                get { return _current; }
            }

            object IEnumerator.Current
            {
                get { return _current; }
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }

            public bool MoveNext()
            {
                if (_list.Count > _index)
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

    public class StringIgnoreCaseComparer<T> : IEqualityComparer<T>
    {
        public bool Equals(T? x, T? y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            if (x is string xStr && y is string yStr)
            {
                if (xStr.Length != yStr.Length)
                {
                    return false;
                }

                for (int i = 0; xStr.Length > i; i++)
                {
                    char xChar = xStr[i];
                    char yChar = yStr[i];

                    if (xChar >= 65 && xChar <= 90) { xChar = (char)(xChar + 32); }
                    if (yChar >= 65 && yChar <= 90) { yChar = (char)(yChar + 32); }

                    if (xChar != yChar)
                    {
                        return false;
                    }
                }

                return true;
            }

            return x.Equals(y);
        }

        public int GetHashCode([DisallowNull] T obj)
        {
            if (obj == null)
            {
                throw new NullReferenceException();
            }

            if (obj is string str)
            {
                char[] chars = new char[str.Length];
                for (int i = 0; str.Length > i; i++)
                {
                    char strToChar = str[i];
                    if (strToChar >= 65 && strToChar <= 90) { chars[i] = (char)(strToChar + 32); }
                    else { chars[i] = strToChar; }
                }

                return new string(chars).GetHashCode();
            }

            return obj.GetHashCode();
        }
    }
}
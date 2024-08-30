using System;
using System.Reflection;
using System.Xml.Linq;

namespace MyStructure
{
    public partial class MyArrayList
    {
        private object[] _array;  // 할당된 배열을 가리키는 참조변수
        private int _size;         // 현재 저장된 원소 개수

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

                Array objects = new object[value];
                CopyTo(objects);
            }
        }

        public MyArrayList()
            : this(4)
        {
        }

        public MyArrayList(int capacity)
        {
            this._size = 0;
            this._array = new object[capacity];
        }


        public object this[int index]
        {
            get
            {
                if (index >= _size)
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

            // 배열의 마지막에 원소 추가
        public void Add(object element)
        {
                // 배열 공간 체크, 부족할 시 resize
            EnsureCapacity();

                // 원소 추가
            _array[_size] = element;
            _size++;
        }

            // 해당 위치에 원소 추가
        public void Insert(int index, object element)
        {
            EnsureCapacity();

            for (int i = _size; i > 0; i--)
            {
                _array[i] = _array[i - 1];
            }

                // 원소 추가
            _array[index] = element;
            _size++;

            foreach (var item in _array)
            {
                Console.WriteLine(item);
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

            _array = (object[])array;
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

        public object[] ToArray()
        {
            object[] newArray = new object[_size];
            Array.Copy(_array, 0, newArray, 0, _size);
            return newArray;
        }

        public void Clear()
        {
            _size = 0;
        }

        public bool Contains(object item)
        {
            ValidNonNull(item);

            for (int index = 0; index < this._size; index++)
            {
                if (this._array[index].Equals(item))
                    return true;
            }
            return false;
        }

        public int IndexOf(object item, int index = 0, int count = -1)
        {
            ValidNonNull(item);

            var startPosition = index;
            var countPosition = count != -1 ? index + count : _size - 1;

            ValidOutOfIndex(startPosition);
            ValidOutOfIndex(countPosition);

            for (int i = startPosition; i < countPosition; i++)
            {
                if (_array[i].Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }


        public int LastIndexOf(object item, int index = 0, int count = -1)
        {
            ValidNonNull(item);

            var startPosition = _size - 1 - index;
            var countPosition = count != -1 ? startPosition - count : 0;

            ValidOutOfIndex(startPosition);
            ValidOutOfIndex(countPosition);

            for (int i = startPosition; i > countPosition; i--)
            {
                if (_array[i].Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }
    }

    public partial class MyArrayList
    {
        public bool Remove()
        {
            if (_array.Length > 0)
            {
                RemoveRange(0, 1);
                return true;
            }

            return false;
        }
        // 해당 위치의 원소 삭제
        public void RemoveAt(int index)
        {
            RemoveRange(index, 1);

            if (_array.Length - 4 < _size)
            {
                object[] array = new Array[_array.Length - 4];
                CopyTo(array);
            }
        }

        public void RemoveRange(int index, int count)
        {
            ValidOutOfIndex(index);
            ValidOutOfIndex(index + count);

            for (int i = 0; count > i; i++)
            {
                for (int j = index; _size > j; j++)
                {
                    _array[j] = _array[i + 1];
                }

                _array[_size] = "";
                _size--;
            }
        }
    }


    /*
     * Validation
     */
    public partial class MyArrayList
    {
        private void ValidOutOfIndex(int index)
        {
            if (index > _size || index < 0)
            {
                throw new ArgumentOutOfRangeException("인덱스 초과");
            }
        }

        private void ValidNonNull(object obj)
        {
            if (obj == null)
            {
                throw new NullReferenceException("값이 비었습니다");
            }
        }
    }
}
using System.Collections;

namespace MyStructure
{
    internal static class HashHelpers
    {
             // 1000보다 작은 소수들 (실제로는 더 큰 수까지 사용되지만 예제이므로 1000 이하의 숫자만 사용한다)
        private static readonly int[] _primes = new int[] {
        3, 7, 11, 17, 23, 29, 37, 47, 59, 71, 89, 107, 131, 163, 197, 239, 293, 353, 431, 521, 631, 761, 919
        };

        public static int PRIME_FACTOR = 4;
        public static decimal RESIZE_FACTOR = 1.25M;

        public static int GetPrime(int min)
        {
            for (int index = 0; index < _primes.Length; ++index)
            {
                int prime = _primes[index];
                if (prime >= min)
                    return prime;
            }
            return min;
        }
    }

    public class MyHashSet<T> : IEnumerable<T>
    {
        private MySLinkedList<T>[] _bucket;
        private IEqualityComparer<T> _comparer;
        private int _count;

        public MyHashSet(IEqualityComparer<T> equalityComparer = null)
            : this(3, equalityComparer)
        {
        }

        public MyHashSet(int capacity, IEqualityComparer<T> equalityComparer = null)
        {
            int size = HashHelpers.GetPrime(capacity);
            this._bucket = new MySLinkedList<T>[size];
            this._comparer = equalityComparer ?? EqualityComparer<T>.Default;
        }

        public int Count { get { return _count; } }

        private int GetBucketIndex(T item, int bucketSize)
        {
                   // EqualityComparer를 이용하여 item을 해싱한 해쉬코드와 버킷(배열)의 크기를 이용하여 해당 인덱스를 구한다.
            int hash = Math.Abs(item.GetHashCode());

            return hash % bucketSize;
        }

        private MySLinkedList<T> FindBucketList(T item)
        {
            int index = GetBucketIndex(item, _bucket.Length);
            return this._bucket[index];
        }

        private void Resize(int capacity)
        {
                  // 새로운 크기로 배열 새로 할당
            var newSize = HashHelpers.GetPrime(capacity);
            var newBucket = new MySLinkedList<T>[newSize];

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
                    int index = GetBucketIndex(item, capacity);
                    var newList = newBucket[index];
                    if (newList == null)
                    {
                        newBucket[index] = new MySLinkedList<T>(_comparer);
                        newList = newBucket[index];
                    }

                    newBucket[index].AddFirst(item);
                }
            }

            _bucket = newBucket;
        }

        public bool Contains(T item)
        {
            var list = FindBucketList(item);
            if (list == null)
                return false;

            return list.Contains(item);
        }

        public bool Add(T item)
        {
                  // 현재 데이터 개수가 해시 버킷 개수의 125% 가 넘으면 리사이징한다.
            if (_count >= _bucket.Length * HashHelpers.RESIZE_FACTOR)
            {
                Resize(_bucket.Length + HashHelpers.PRIME_FACTOR);
            }

            int index = GetBucketIndex(item, _bucket.Length);
            var list = _bucket[index];

                   // TODO: 해당 버킷에 이미 만들어진 연결리스트가 없다면 새로 만들고 버킷에 할당한다.
                   // 그렇지 않으면 연결리스트에 해당 항목이 이미 포함되어 있는지 검사 후 이미 추가된 값이면 false를 리턴한다.
                   // 연결리스트의 마지막에 해당 항목을 추가하고 카운트값을 하나 늘린다.
            if (list == null)
            {
                _bucket[index] = new MySLinkedList<T>(_comparer);
                list = _bucket[index];
            }

                   // 리스트가 다른곳에 들어간 것 같음 클래스 넣을 때도 hash값 고려
            for (int i = 0; _bucket.Length > i; i++)
            {
                var checkList = _bucket[i];

                if (checkList != null && checkList.Contains(item))
                {
                    return false;
                }
            }

            list.AddFirst(item);
            _count++;

            return true;
        }

        public void Remove(T item)
        {
            MySLinkedList<T> list = FindBucketList(item);

            if (list != null)
            {
                        // TODO: 연결리스트에서 해당 항목을 찾은 후 있다면
                        // 해당 노드를 연결리스트에서 삭제하고 카운트값을 하나 줄인 후 true 리턴.
                list.Remove(item);
                _count--;
            }
        }

        public void Clear()
        {
            _bucket.ToList().Clear();
            _count = 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new MyHashSetEnumerator(this);
        }

            // IEnumerable 인터페이스 구현
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }


        private class MyHashSetEnumerator : IEnumerator<T>
        {
            private MyHashSet<T> _hset;
            private IEnumerator<T> _iterator;
            private int _index;

            public MyHashSetEnumerator(MyHashSet<T> hset)
            {
                this._hset = hset;
                this._index = 0;
                this._iterator = FindNextEnumerator();
            }


            // IEnumerator<T>
            //_________________________________________________________________________________________

            public T Current
            {
                get { return _iterator.Current; }
            }

            object IEnumerator.Current
            {
                get { return this.Current; }
            }

            public void Dispose()
            {
            }

            public void Reset()
            {
                _index = 0;
                _iterator = FindNextEnumerator();
            }

            private IEnumerator<T> FindNextEnumerator()
            {
                while (_index < _hset._bucket.Length)
                {
                    var bucket = _hset._bucket[_index];
                    _index++;

                    if (bucket != null && bucket.Count > 0)
                    {
                        return bucket.GetEnumerator();
                    }
                }

                return null;
            }

            public bool MoveNext()
            {
                while (_iterator == null || !_iterator.MoveNext())
                {
                    _iterator = FindNextEnumerator();

                    if (_iterator == null)
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}

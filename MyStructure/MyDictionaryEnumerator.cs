using MyStructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStructure
{

    abstract class MyDictEnumeratorBase<TKey, TValue> : IEnumerator<KeyValuePair<TKey, TValue>>
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

        // IDispose
        //_________________________________________________________________________________________
        public void Dispose()
        {
        }

        // IEnumerator
        //_________________________________________________________________________________________
        object IEnumerator.Current
        {
            get { return this.Current; }
        }

        // IEnumerator<T>
        //_________________________________________________________________________________________

        public abstract KeyValuePair<TKey, TValue> Current { get; }

        public void Reset()
        {
            
        }

        // TODO:...
    }

    private abstract class MyDictCollectionBase<TKey, TValue> : IEnumerable<TCurrent>
    {
        protected MyDictionary<TKey, TValue> _dict;

        protected MyDictCollectionBase(MyDictionary<TKey, TValue> dict)
        {
            this._dict = dict;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public abstract IEnumerator<TCurrent> GetEnumerator();

    }



    private class MyDictKeyCollection : MyDictCollectionBase<TKey, TValue>
    {
        public MyDictKeyCollection(MyDictionary<TKey, TValue> dict)
            : base(dict)
        {
        }

        public override IEnumerator<TKey> GetEnumerator()
        {
            // TODO: 
        }

        private class MyDictKeyEnumerator : MyDictEnumeratorBase<TODO: 리턴되는 CURRENT TYPE..>
        {
                public MyDictKeyEnumerator(MyDictionary<TKey, TValue> dict)
                    : base(dict)
                { }

        // TODO: Current
        }
    }


    private abstract class MyDictCollectionBase<TCurrent> : IEnumerable<TCurrent>
    {
        protected MyDictionary<TKey, TValue> _dict;

        protected MyDictCollectionBase(MyDictionary<TKey, TValue> dict)
        {
            this._dict = dict;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public abstract IEnumerator<TCurrent> GetEnumerator();

    }

    // 키 열거를 위한 객체를 MyDictCollectionBase 에서 상속하는 구조로 코드를 작성하시오
    private class MyDictKeyCollection : MyDictCollectionBase<TODO: 리턴되는 CURRENT TYPE..>
        {
            public MyDictKeyCollection(MyDictionary<TKey, TValue> dict)
                : base(dict)
            {
    }

    public override IEnumerator<TKey> GetEnumerator()
    {
        // TODO: 
    }

    private class MyDictKeyEnumerator : MyDictEnumeratorBase<TODO: 리턴되는 CURRENT TYPE..>
            {
                public MyDictKeyEnumerator(MyDictionary<TKey, TValue> dict)
                    : base(dict)
                {
    }

                // TODO: Current
            }
        }

        // 값 열거를 위한 객체를 MyDictCollectionBase 에서 상속하는 구조로 코드를 작성하시오
        private class MyDictValueCollection : MyDictCollectionBase<TODO: 리턴되는 CURRENT TYPE..>
        {
            // TODO: MyDictKeyCollection 참고하여 구현
        }

        private class MyDictEnumerator : MyDictEnumeratorBase<KeyValuePair<TKey, TValue>>
    {
        public override TCurrent Current
        {
            get { return ... }
        }
    }

    
}

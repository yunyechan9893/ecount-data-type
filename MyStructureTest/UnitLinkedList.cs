using MyStructure;

namespace MyStructureTest
{
    public class MyLinkedListTest
    {
        
        [SetUp]
        public void Setup()
        {
           
        }

        [Test]
        public void AddFirst()
        {
            MySLinkedList<int> sLinkedList = new MySLinkedList<int>();
            sLinkedList.AddFirst(1);
            sLinkedList.AddFirst(2);
            sLinkedList.AddFirst(3);
            sLinkedList.AddFirst(4);

            var cur_node = sLinkedList.First;
            Assert.AreEqual(4, cur_node.Data);
            cur_node = cur_node.Next;
            Assert.AreEqual(3, cur_node.Data);
            cur_node = cur_node.Next;
            Assert.AreEqual(2, cur_node.Data);
            cur_node = cur_node.Next;
            Assert.AreEqual(1, cur_node.Data);
        }

        [Test]
        public void AddLast()
        {
            MySLinkedList<int> sLinkedList = new MySLinkedList<int>();
            sLinkedList.AddLast(1);
            sLinkedList.AddLast(2);
            sLinkedList.AddLast(3);
            sLinkedList.AddLast(4);

            var cur_node = sLinkedList.First;
            Assert.AreEqual(1, cur_node.Data);
            cur_node = cur_node.Next;
            Assert.AreEqual(2, cur_node.Data);
            cur_node = cur_node.Next;
            Assert.AreEqual(3, cur_node.Data);
            cur_node = cur_node.Next;
            Assert.AreEqual(4, cur_node.Data);
        }

        [Test]
        public void Add()
        {
            MySLinkedList<int> sLinkedList = new MySLinkedList<int>();
            sLinkedList.AddFirst(1);
            sLinkedList.AddLast(2);
            sLinkedList.AddFirst(3);
            sLinkedList.AddLast(4);

            var cur_node = sLinkedList.First;
            Assert.AreEqual(3, cur_node.Data);
            cur_node = cur_node.Next;
            Assert.AreEqual(1, cur_node.Data);
            cur_node = cur_node.Next;
            Assert.AreEqual(2, cur_node.Data);
            cur_node = cur_node.Next;
            Assert.AreEqual(4, cur_node.Data);
        }

        [Test]
        public void RemoveFirst()
        {
            MySLinkedList<int> sLinkedList = new MySLinkedList<int>();
            sLinkedList.AddFirst(1);
            sLinkedList.AddLast(2);
            sLinkedList.AddFirst(3);
            sLinkedList.AddLast(4);
            var result = sLinkedList.RemoveFirst();
            Assert.That(result, Is.EqualTo(3));
           
            var cur_node = sLinkedList.First;
            Assert.AreEqual(1, cur_node.Data);
            cur_node = cur_node.Next;
            Assert.AreEqual(2, cur_node.Data);
            cur_node = cur_node.Next;
            Assert.AreEqual(4, cur_node.Data);
        }

        [Test]
        public void RemoveFirst2()
        {
            MySLinkedList<int> sLinkedList = new MySLinkedList<int>();
            sLinkedList.AddFirst(1);
            sLinkedList.AddLast(2);
            sLinkedList.AddFirst(3);
            sLinkedList.AddLast(4);
            sLinkedList.RemoveFirst();
            sLinkedList.RemoveFirst();

            var cur_node = sLinkedList.First;
            Assert.AreEqual(2, cur_node.Data);
            cur_node = cur_node.Next;
            Assert.AreEqual(4, cur_node.Data);
        }

        [Test]
        public void RemoveLast()
        {
            MySLinkedList<int> sLinkedList = new MySLinkedList<int>();
            sLinkedList.AddFirst(1);
            sLinkedList.AddLast(2);
            sLinkedList.AddFirst(3);
            sLinkedList.AddLast(4);
            var result1 = sLinkedList.RemoveLast();
            var result2 = sLinkedList.RemoveLast();

            Assert.That(result1, Is.EqualTo(4));
            Assert.That(result2, Is.EqualTo(2));
            Assert.That(sLinkedList.Count, Is.EqualTo(2));
        }

        [Test]
        public void ToArray()
        {
            MySLinkedList<int> sLinkedList = new MySLinkedList<int>();
            sLinkedList.AddFirst(1);
            sLinkedList.AddLast(2);
            sLinkedList.AddFirst(3);
            sLinkedList.AddLast(4);
            sLinkedList.RemoveFirst();
            sLinkedList.RemoveFirst();
            Array array = sLinkedList.ToArray();

            Assert.IsTrue(array is Array);
            Assert.That(array.GetValue(0), Is.EqualTo(2));
        }

        [Test]
        public void ForEach()
        {
            MySLinkedList<int> sLinkedList = new MySLinkedList<int>();
            sLinkedList.AddFirst(1);
            sLinkedList.AddLast(2);
            sLinkedList.AddFirst(3);
            sLinkedList.AddLast(4);

            foreach (var item in sLinkedList)
            {
                Console.WriteLine(item);
            }
        }

        [Test]
        public void Contains()
        {
            MySLinkedList<int> sLinkedList = new MySLinkedList<int>();
            sLinkedList.AddFirst(1);
            sLinkedList.AddLast(2);
            sLinkedList.AddFirst(3);
            sLinkedList.AddLast(4);

            var result1 = sLinkedList.Contains((node) => node.Data == 1);
            var result2 = sLinkedList.Contains((node) => node.Data == 5);

            Assert.That(result1, Is.True);
            Assert.That(result2, Is.False);
        }

        [Test]
        public void FindLast()
        {
            MySLinkedList<int> sLinkedList = new MySLinkedList<int>();
            sLinkedList.AddFirst(1);
            sLinkedList.AddFirst(2);
            sLinkedList.AddFirst(3);
            sLinkedList.AddFirst(4);

            var result1 = sLinkedList.FindLast((node) => node.Data == 1);
            var result2 = sLinkedList.FindLast((node) => node.Data == 5);

            Assert.That(result1.Data, Is.EqualTo(1));
            Assert.That(result2, Is.Null);
        }

        [Test]
        public void RemoveByNode()
        {
            MySLinkedList<int> sLinkedList = new MySLinkedList<int>();
            sLinkedList.AddFirst(1);
            sLinkedList.AddFirst(2);
            sLinkedList.AddFirst(3);
            sLinkedList.AddFirst(4);

            var result1 = sLinkedList.Remove((node) => node.Data == 1);

            Assert.That(result1, Is.True);
            Assert.That(sLinkedList.Count, Is.EqualTo(3));
            Assert.That(sLinkedList.Last.Data, Is.EqualTo(2));
        }

        [Test]
        public void Clear()
        {
            MySLinkedList<int> sLinkedList = new MySLinkedList<int>();
            sLinkedList.AddFirst(1);
            sLinkedList.AddFirst(2);
            sLinkedList.AddFirst(3);
            sLinkedList.AddFirst(4);

            sLinkedList.Clear();

            Assert.That(sLinkedList.First, Is.Null);
            Assert.That(sLinkedList.Last,  Is.Null);
        }

        [Test]
        public void testt()
        {
            MySLinkedList<int> list = new MySLinkedList<int>();

            list.AddFirst(10);
            list.AddFirst(20);
            list.AddFirst(30);
            list.AddFirst(40);
            list.AddFirst(50);
            list.Remove(30);
            list.RemoveFirst();

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }


            MySLinkedList<string> slist = new MySLinkedList<string>();

            slist.AddFirst("abc");
            slist.AddFirst("def");
            slist.AddFirst("ghi");

            slist.Remove("DEF");

            foreach (var item in slist)
            {
                Console.WriteLine(item);
            }
        }

    }
}
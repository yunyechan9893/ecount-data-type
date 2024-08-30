using MyStructure;

namespace MyStructureTest
{
    public class HsetTest
    {
        
        [SetUp]
        public void Setup()
        {
           
        }

        [Test]
        public void Add()
        {
            MyHashSet<int> hset = new MyHashSet<int>();
            hset.Add(1);
            hset.Add(2);
            hset.Add(3);
            hset.Add(4);

            Assert.That(hset.Count, Is.EqualTo(4));
        }

        [Test]
        public void Contains()
        {
            MyHashSet<int> hset = new MyHashSet<int>();
            hset.Add(1);
            hset.Add(2);
            hset.Add(3);
            hset.Add(4);

            var result = hset.Contains(2);

            Assert.That(result, Is.True);
        }

        [Test]
        public void Remove()
        {
            MyHashSet<int> hset = new MyHashSet<int>();
            hset.Add(1);
            hset.Add(2);
            hset.Add(3);
            hset.Add(4);

            hset.Remove(2);

            Assert.That(hset.Count, Is.EqualTo(3));
        }


        [Test]
        public void Clear()
        {
            MyHashSet<int> hset = new MyHashSet<int>();
            hset.Add(1);
            hset.Add(2);
            hset.Add(3);
            hset.Add(4);

            hset.Clear();

            Assert.That(hset.Count, Is.EqualTo(0));
        }

        [Test]
        public void Comparer()
        {
            var x = new MyHashSet<Car>(3, new MakeAscendingEqualityComparer());    // �ʱ� ũ�⸦ 3���� �����ؼ� �߰��� Resizing�� �ǵ��� �׽�Ʈ �Ѵ�.
            x.Add(new Car(1, "a"));
            x.Add(new Car(1, "a"));
            x.Add(new Car(2, "a"));
            x.Add(new Car(3, "a"));
            x.Add(new Car(4, "a"));
            x.Add(new Car(5, "a"));
            x.Add(new Car(6, "a"));
            x.Add(new Car(6, "b"));
            x.Add(new Car(7, "a"));

            foreach (var item in x)
            {

                Console.WriteLine(item);
            }
        } 

        [Test]
        public void Test()
        {
            var x = new MyHashSet<int>(3);    // �ʱ� ũ�⸦ 3���� �����ؼ� �߰��� Resizing�� �ǵ��� �׽�Ʈ �Ѵ�.
            x.Add(10);
            x.Add(2);
            x.Add(30);
            x.Add(4);
            x.Add(50);
            x.Add(4);
            x.Add(50);
            x.Add(4);
            x.Add(4);
            x.Add(50);
            x.Add(4);
            x.Add(50);
            x.Add(50);
            x.Add(30);    //=> false. �̹� �ߺ��� ���̹Ƿ� �߰����� �ʴ´�.

            foreach (var item in x)
            {
                Console.WriteLine(item);
            }
        }
    }
}
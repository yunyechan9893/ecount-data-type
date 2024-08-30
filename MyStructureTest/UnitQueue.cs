using MyStructure;

namespace MyStructureTest
{
    public class QueueTest
    {
        
        [SetUp]
        public void Setup()
        {
           
        }

        [Test]
        public void Enqueue()
        {
            MyQueue<int> queue = new MyQueue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);

            Assert.That(queue.Count, Is.EqualTo(4));
        }

        [Test]
        public void Peek()
        {
            MyQueue<int> queue = new MyQueue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);

            var result = queue.Peek();

            Assert.That(result, Is.EqualTo(1));
            Assert.That(queue.Count, Is.EqualTo(4));
        }

        [Test]
        public void Dequeue()
        {
            MyQueue<int> queue = new MyQueue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);

            var result = queue.Dequeue();

            Assert.That(result, Is.EqualTo(1));
            Assert.That(queue.Count, Is.EqualTo(3));
        }

        [Test]
        public void Dequeue2()
        {
            MyQueue<int> queue = new MyQueue<int>();

            try
            {
                queue.Dequeue();
                Assert.IsTrue(false);
            } catch (NullReferenceException)
            {
                Assert.IsTrue(true);
            }
        }

        [Test]
        public void ToArray()
        {
            MyQueue<int> queue = new MyQueue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);

            var result = queue.ToArray();
            var result2 = result[0];

            Assert.That(result is Array, Is.True);
            Assert.That(result2, Is.EqualTo(4));
            Assert.That(result.Count, Is.EqualTo(4));
        }

        [Test]
        public void Clear()
        {
            MyQueue<int> queue = new MyQueue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);

            queue.Clear();

            Assert.That(queue.Count, Is.EqualTo(0));
        }

        [Test]
        public void Foreach()
        {
            MyQueue<int> queue = new MyQueue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);
           
            queue.ForEach((item) =>
            {
                Console.WriteLine(item);
            });
        }

        [Test]
        public void Test()
        {
            MyQueue<int> queue = new MyQueue<int>();

            queue.Enqueue(10);
            queue.Enqueue(20);
            queue.Enqueue(30);

            Console.WriteLine(queue.Peek());
            Console.WriteLine(queue.Dequeue());


            foreach (var item in queue)
            {
                Console.WriteLine(item);
            }
        }
    }
}
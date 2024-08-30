using MyStructure;
using System.Collections;
using System.Collections.Generic;

namespace MyStructureTest
{
    public class StackTest
    {
        
        [SetUp]
        public void Setup()
        {
           
        }

        [Test]
        public void Push()
        {
            MyStack<int> stack = new MyStack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);

            Assert.That(stack.Count, Is.EqualTo(4));
        }

        [Test]
        public void Peek()
        {
            MyStack<int> stack = new MyStack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);

            var result = stack.Peek();

            Assert.That(result, Is.EqualTo(4));
            Assert.That(stack.Count, Is.EqualTo(4));
        }

        [Test]
        public void Pop()
        {
            MyStack<int> stack = new MyStack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);

            var result = stack.Pop();

            Assert.That(result, Is.EqualTo(4));
            Assert.That(stack.Count, Is.EqualTo(3));
        }

        [Test]
        public void ToArray()
        {
            MyStack<int> stack = new MyStack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);

            var result = stack.ToArray();
            var result2 = result[0];

            Assert.That(result is Array, Is.True);
            Assert.That(result2, Is.EqualTo(4));
            Assert.That(result.Count, Is.EqualTo(4));
        }

        [Test]
        public void Clear()
        {
            MyStack<int> stack = new MyStack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);

            stack.Clear();

            Assert.That(stack.Count, Is.EqualTo(0));
        }

        [Test]
        public void Foreach()
        {
            MyStack<int> stack = new MyStack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
           
            stack.ForEach((item) =>
            {
                Console.WriteLine(item);
            });
        }
    }
}
using MyStructure;
using System.Collections;
using System.Collections.Generic;

namespace MyStructureTest
{
    public class UnitMyArrayTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void MyArrayListTest()
        {
            // Creates and initializes a new MyArrayList.
            List<String> myAL = new List<String>();

            myAL.Add("Hello");
            myAL.Add("C#");
            myAL.Add("World");
            myAL.Add("!\r\n");

            myAL.Add("I'm not generic");

            for (int i = 0; i < myAL.Count; i++)
            {
                Console.WriteLine(myAL[i]);
            }

            string val = myAL[1];
            Console.WriteLine(val);

            myAL.RemoveAt(2);

            for (int i = 0; i < myAL.Count; i++)
            {
                Console.WriteLine(myAL[i]);
            }

        }



        [Test]
        public void Insert()
        {
            MyArrayList myAL = new MyArrayList();

            myAL.Add("1");
            myAL.Add("3");
            myAL.Add("4");

            myAL.Insert(1, "2");

            Assert.IsTrue(myAL.Count == 4);
        }


        [Test]
        public void RemoveRange()
        {
            MyArrayList myAL = new MyArrayList();

            myAL.Add("1");
            myAL.Add("2");
            myAL.Add("3");
            myAL.Add("4");
            myAL.Add("5");

            myAL.RemoveRange(0, 3);

            Assert.IsTrue(myAL.Count == 2);
        }

        [Test]
        public void GetItemByIndex()
        {
            MyArrayList myAL = new MyArrayList();

            myAL.Add("1");
            myAL.Add("2");
            myAL.Add("3");
            myAL.Add("4");
            myAL.Add("5");

            string result = (string)myAL[3];

            Assert.IsTrue(result == "4");
        }


        [Test]
        public void Swap()
        {
            MyArrayList myAL = new MyArrayList();

            myAL.Add("1");
            myAL.Add("2");
            myAL.Add("3");
            myAL.Add("4");
            myAL.Add("5");

            myAL.Swap(0, 1);

            string result = (string)myAL[0];

            Assert.IsTrue(result == "2");
        }

        [Test]
        public void ToArray()
        {
            // 얕은 복사로 판단
            MyArrayList myAL = new MyArrayList();

            myAL.Add("1");
            myAL.Add("2");
            myAL.Add("3");
            myAL.Add("4");
            myAL.Add("5");

            var myAL2 = myAL.ToArray();

            myAL.Swap(0, 1);

            string result1 = (string)myAL[0];
            string result2 = (string)myAL[0];


            Assert.IsTrue(result1 == "2");
            Assert.IsTrue(result2 == "2");
        }

        [Test]
        public void IndexOf()
        {
            // 얕은 복사로 판단
            MyArrayList myAL = new MyArrayList();

            myAL.Add("1");
            myAL.Add("2");
            myAL.Add("3");
            myAL.Add("4");
            myAL.Add("3");

            var result = myAL.IndexOf("3");
            Assert.IsTrue(result == 2);
        }

        [Test]
        public void IndexOfWithIndex()
        {
            // 얕은 복사로 판단
            MyArrayList myAL = new MyArrayList();

            myAL.Add("1");
            myAL.Add("2");
            myAL.Add("3");
            myAL.Add("4");
            myAL.Add("3");

            var result = myAL.IndexOf("3", 1);
            Assert.IsTrue(result == 2);
        }

        [Test]
        public void IndexOfWithIndexAndCount()
        {
            // 얕은 복사로 판단
            MyArrayList myAL = new MyArrayList();

            myAL.Add("1");
            myAL.Add("2");
            myAL.Add("3");
            myAL.Add("4");
            myAL.Add("3");

            var result = myAL.IndexOf("3", 1, 2);
            Assert.IsTrue(result == 2);
        }

        [Test]
        public void IndexOfWithIndexAndCountToFail()
        {
            // 얕은 복사로 판단
            MyArrayList myAL = new MyArrayList();

            myAL.Add("1");
            myAL.Add("2");
            myAL.Add("3");
            myAL.Add("4");
            myAL.Add("5");

            var result = myAL.IndexOf("3", 3, 2);
            Assert.IsTrue(result == -1);
        }

        [Test]
        public void LastIndexOf()
        {
            MyArrayList myAL = new MyArrayList();

            myAL.Add("1");
            myAL.Add("2");
            myAL.Add("3");
            myAL.Add("4");
            myAL.Add("3");

            var result = myAL.LastIndexOf("3");
            Assert.IsTrue(result == 4);
        }

        [Test]
        public void LastIndexOfWithIndex()
        {
            MyArrayList myAL = new MyArrayList();

            myAL.Add("1");
            myAL.Add("2");
            myAL.Add("3");
            myAL.Add("4");
            myAL.Add("3");

            var result = myAL.LastIndexOf("2", 2);
            Assert.IsTrue(result == 1);
        }

        [Test]
        public void LastIndexOfWithIndexAndCount()
        {
            MyArrayList myAL = new MyArrayList();

            myAL.Add("1");
            myAL.Add("2");
            myAL.Add("3");
            myAL.Add("4");
            myAL.Add("3");

            var result = myAL.LastIndexOf("2", 2, 1);
            Assert.IsTrue(result == -1);
        }

        [Test]
        public void Remove()
        {
            MyArrayList myAL = new MyArrayList();

            myAL.Add("1");
            myAL.Add("2");
            myAL.Add("3");
            myAL.Add("4");
            myAL.Add("3");

            var result = myAL.Remove();

            Assert.IsTrue(result);
            Assert.IsTrue(myAL.Count == 4);
        }
    }
}
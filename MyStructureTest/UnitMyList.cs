using MyStructure;

namespace MyStructureTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }


        [Test]
        public void CompararEqualsTest()
        {
            MyList<string> myList = new MyList<string>(new StringIgnoreCaseComparer<string>());
            myList.Add("Hello");
            myList.Add("Hi");
            myList.Add("My");

            Assert.IsTrue(myList.Contains("hello"));
            Assert.IsFalse(myList.Contains("hihi"));
        }

        [Test]
        public void CompararEqualsTest2()
        {
            var y = new MyList<String>(new StringIgnoreCaseComparer<string>());
            y.Add("samsung");
            y.Add("Hyundae");
            y.Add("LG");
            Console.WriteLine(y.Contains("hyundae"));    //=> true
            Console.WriteLine(y.Contains("Hyundae"));    //=> true
        }

        [Test]
        public void GetHashCode()
        {
            var y = new MyList<String>(new StringIgnoreCaseComparer<string>());
            y.Add("samsung");
            y.Add("Hyundae");
            y.Add("LG");
            Console.WriteLine(y.IndexOf("hyundae"));    //=> true
            Console.WriteLine(y.LastIndexOf("hyundae"));    //=> true
            Console.WriteLine(y.Contains("Hyundae"));    //=> true
        }

        [Test]
        public void Sort()
        {
            // 형식 비교자를 사용하는 경우와 그렇지 않는 경우의 사용예제
            var list = new MyList<Car>();

            list.Add(new Car(1992, "Ford"));
            list.Add(new Car(1999, "Buick"));
            list.Add(new Car(1997, "Honda"));

            list.Sort();   //=> Sorted by Year (Ascending - IComparable 사용)
            foreach (var item in list)
            {
                Console.WriteLine(item);  //=> Car 객체 내부의 값이 문자열로 출력F될 수 있도록 Car객체의 ToString() 메소드를 재정의해야 함.
            }

            Console.WriteLine("============");

            list.Sort(new YearDescendingComparer());  //=> Sorted by Year (Descending - IComparer 사용)
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("============");

            list.Sort(new MakeAscendingComparer());   //=> Sorted by Make (Ascending  - IComparer 사용)
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

            Assert.IsTrue(list.Count() == 3);
            Assert.IsTrue(list.Capacity == 4);
        }

        [Test]
        public void Find()
        {
            var list = new MyList<Car>();

            var targetCar = new Car(1992, "Ford");
            list.Add(targetCar);
            list.Add(new Car(1999, "Buick"));
            list.Add(new Car(1997, "Honda"));

            var finderCar = list.Find((car) => car.Year == 1992);
            Assert.IsTrue(targetCar == finderCar);
        }

        [Test]
        
        public void FindIndex()
        {
            var list = new MyList<Car>();

            list.Add(new Car(1992, "Ford"));
            list.Add(new Car(1999, "Buick"));
            list.Add(new Car(1997, "Honda"));

            var index1 = list.FindIndex((car) => car.Year == 1992);
            var index2 = list.FindIndex((car) => car.Year == 1999);

            Assert.IsTrue(index1 == 0);
            Assert.IsTrue(index2 == 1);

            var index3 = list.FindIndex((car) => car.Year == 1997, 1);

            Assert.IsTrue(index3 == 2);

            var result = list.FindIndex((car) => car.Year == 1997, 0, 2);
            Assert.That(result, Is.EqualTo(-1));
        
            var index4 = list.FindIndex((car) => car.Year == 1997, 1, 2);
            Assert.IsTrue(index4 == 2);
        }

        [Test]
        public void FindLast()
        {
            var list = new MyList<Car>();

            list.Add(new Car(1992, "Ford"));
            list.Add(new Car(1999, "Buick"));
            list.Add(new Car(1997, "Honda"));

            var index1 = list.FindLastIndex((car) => car.Year == 1992);
            var index2 = list.FindLastIndex((car) => car.Year == 1999);

            Assert.IsTrue(index1 == 0);
            Assert.IsTrue(index2 == 1);

            var result = list.FindLastIndex((car) => car.Year == 1997, 1);
            Assert.That(result, Is.EqualTo(-1));

            var index3 = list.FindLastIndex((car) => car.Year == 1997, 0);
            Assert.IsTrue(index3 == 2);


            var index4 = list.FindLastIndex((car) => car.Year == 1997, 0, 2);
            Assert.IsTrue(index4 == 2);
        }

        [Test]
        public void Contains()
        {
            var list = new MyList<Car>();

            list.Add(new Car(1992, "Ford"));
            list.Add(new Car(1999, "Buick"));
            list.Add(new Car(1997, "Honda"));
            list.Add(new Car(1992, "FordTwo"));

            var result = list.Contains((car) => car.Year == 1992);
            Assert.That(result, Is.True);

            result = list.Contains((car) => car.Year == 2000);
            Assert.That(result, Is.False);
        }


        [Test]
        public void PredRemove()
        {
            var list = new MyList<Car>();

            list.Add(new Car(1992, "Ford"));
            list.Add(new Car(1999, "Buick"));
            list.Add(new Car(1997, "Honda"));
            list.Add(new Car(1992, "FordTwo"));

            list.Remove((car) => car.Year == 1992);
            Assert.That(list.Count, Is.EqualTo(3));
        }

        [Test]
        public void RemoveAll()
        {
            var list = new MyList<Car>();

            list.Add(new Car(1992, "Ford"));
            list.Add(new Car(1999, "Buick"));
            list.Add(new Car(1997, "Honda"));
            list.Add(new Car(1992, "FordTwo"));

            list.RemoveAll((car) => car.Year == 1992);
            Assert.That(list.Count, Is.EqualTo(2));
        }
    }
}
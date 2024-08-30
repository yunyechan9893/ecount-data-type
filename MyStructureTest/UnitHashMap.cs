using MyStructure;

namespace MyStructureTest
{
    class HashMapTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void GetAllValues()
        {
            MyHashMap<int> keyValuePairs = new MyHashMap<int>();

            keyValuePairs.Add("1", 1);
            keyValuePairs.Add("2", 2);
            keyValuePairs.Add("3", 3);
            keyValuePairs.Add("4", 4);
            keyValuePairs.Add("5", 5);

            var result = keyValuePairs.GetAllValues();

            Assert.That(result.Length, Is.EqualTo(5));
        }

        [Test]
        public void GetValues()
        {
            MyHashMap<int> keyValuePairs = new MyHashMap<int>();

            keyValuePairs.Add("1", 1);
            keyValuePairs.Add("2", 2);
            keyValuePairs.Add("2", 3);
            keyValuePairs.Add("3", 3);
            keyValuePairs.Add("4", 4);
            keyValuePairs.Add("5", 5);

            var result1 = keyValuePairs.GetValues("2");
            var result2 = keyValuePairs.GetValues("6");

            Assert.That(result1, Is.EqualTo(new int[] { 2, 3 }));
            Assert.That(result2, Is.EqualTo(new int[] { }));
        }

        [Test]
        public void Remove()
        {
            MyHashMap<int> keyValuePairs = new MyHashMap<int>();

            keyValuePairs.Add("1", 1);
            keyValuePairs.Add("2", 2);
            keyValuePairs.Add("2", 3);
            keyValuePairs.Add("3", 3);
            keyValuePairs.Add("4", 4);
            keyValuePairs.Add("5", 5);

            keyValuePairs.Remove("2");
            keyValuePairs.Remove("3");

            Assert.That(keyValuePairs.Count, Is.EqualTo(3));
        }

        [Test]
        public void Foreach()
        {
            MyHashMap<int> keyValuePairs = new MyHashMap<int>();

            keyValuePairs.Add("1", 1);
            keyValuePairs.Add("2", 2);
            keyValuePairs.Add("2", 3);
            keyValuePairs.Add("3", 3);
            keyValuePairs.Add("4", 4);
            keyValuePairs.Add("5", 5);

            foreach (var key in keyValuePairs.Keys)
            {
                Console.WriteLine($"Key: {key}");
            }


            foreach (var item in keyValuePairs)
            {
                foreach (var value in item.Value)
                {
                    Console.WriteLine($"Key: {item.Key}, value: {value}");
                }
            }
        }

    }

}

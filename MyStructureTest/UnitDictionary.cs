using MyStructure;

namespace MyStructureTest
{
    class DictionaryTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Add()
        {
            MyDictionary<int, string> myDict = new MyDictionary<int, string>();

            myDict.Add(1, "예찬1");
            myDict.Add(2, "예찬2");
            myDict.Add(3, "예찬3");
            myDict.Add(4, "예찬4");
            myDict.Add(5, "예찬5");
            myDict.Add(6, "예찬6");

            Assert.That(myDict.Count, Is.EqualTo(6));
        }

        [Test]
        public void Remove()
        {
            MyDictionary<int, string> myDict = new MyDictionary<int, string>();

            myDict.Add(1, "예찬1");
            myDict.Add(2, "예찬2");
            myDict.Add(3, "예찬3");
            myDict.Add(4, "예찬4");
            myDict.Add(5, "예찬5");
            myDict.Add(6, "예찬6");

            myDict.Remove(6);

            Assert.That(myDict.Count, Is.EqualTo(5));
        }

        [Test]
        public void TryGetValue()
        {
            MyDictionary<int, string> myDict = new MyDictionary<int, string>();

            myDict.Add(1, "예찬1");
            myDict.Add(2, "예찬2");
            myDict.Add(3, "예찬3");
            myDict.Add(4, "예찬4");
            myDict.Add(5, "예찬5");
            myDict.Add(6, "예찬6");

            Assert.IsTrue(myDict.TryGetValue(6, out string value));
            Assert.That(value, Is.EqualTo("예찬6"));
            Assert.IsFalse(myDict.TryGetValue(7, out string val));
        }

        [Test]
        public void Clear()
        {
            MyDictionary<int, string> myDict = new MyDictionary<int, string>();

            myDict.Add(1, "예찬1");
            myDict.Add(2, "예찬2");
            myDict.Add(3, "예찬3");
            myDict.Add(4, "예찬4");
            myDict.Add(5, "예찬5");
            myDict.Add(6, "예찬6");

            myDict.Clear();

            Assert.That(myDict.Count, Is.EqualTo(0));
        }

        [Test]
        public void Contains()
        {
            MyDictionary<int, string> myDict = new MyDictionary<int, string>();

            myDict.Add(1, "예찬1");
            myDict.Add(2, "예찬2");
            myDict.Add(3, "예찬3");
            myDict.Add(4, "예찬4");
            myDict.Add(5, "예찬5");
            myDict.Add(6, "예찬6");

            var result = myDict.Contains(3);
            var failResult = myDict.Contains(7);

            Assert.That(result, Is.True);
            Assert.That(failResult, Is.False);
        }

        [Test]
        public void Foreach()
        {
            MyDictionary<int, string> myDict = new MyDictionary<int, string>();

            myDict.Add(1, "예찬1");
            myDict.Add(2, "예찬2");
            myDict.Add(3, "예찬3");
            myDict.Add(4, "예찬4");
            myDict.Add(5, "예찬5");
            myDict.Add(6, "예찬6");

            myDict.Foreach((item) =>
            {
                Console.WriteLine($"{item.Key}와 {item.Value}");
            });
        }


        [Test]
        public void a()
        {
            var x = new MyDictionary<string, string>(3);    // 초기 크기를 3으로 시작해서 중간에 Resizing이 되도록 테스트 한다.
            x.Add("10", "101010");
            x.Add("2", "222222");
            x.Add("30", "303030");
            x.Add("4", "444444");
            x.Add("50", "505050");
            x.Add("30", "808080");    //=> 예외발생. 이미 중복된 값이므로 오류가 발생한다.
            x["30"] = "808080";       //=> 추가가 아닌 해당 키에 대한 값을 설정하는 것이므로 오류없이 값을 변경한다.

            Console.WriteLine(x["80"]);    //=> 예외발생. 추가되지 않은 키로 검색했으로 오류가 발생한다.

            string result = null;
            if (x.TryGetValue("80", out result))
            {    //=> 추가되지 않은 키로 검색해도 오류가 발생하지 않는다.
                Console.WriteLine(result);
            }

            foreach (var item in x.Keys)
            { // 키만 출력
                Console.WriteLine(item);
            }

            foreach (var item in x.Values)
            { // 값만 출력
                Console.WriteLine(item);
            }

            foreach (var item in x)
            { // 키와 값 쌍을 출력
                Console.WriteLine(string.Format("{0} = {1}", item.Key, item.Value));
            }
        }

    }

}

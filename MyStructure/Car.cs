using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStructure
{
    public class Car : IComparable<Car>
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }


        private int _year { get; set; }
        public int Year
        {
            get
            {
                return _year;
            }
            set
            {
                _year = value;
            }
        }

        public Car(int year, string name)
        {
            _name = name;
            _year = year;
        }

        public int CompareTo(Car? other)
        {
            if (other == null)
            {
                throw new ArgumentNullException();
            }

            if (this._year > other._year)
            {
                return -1;
            }
            else if (this._year < other._year)
            {
                return 1;
            }

            return 0;
        }

        public override string ToString()
        {
            return $"자동차 이름은 {_name} 연식은 {_year}입니다.";
        }
    }

    public class YearDescendingComparer : IComparer<Car>
    {
        public int Compare(Car? x, Car? y)
        {
            if (x == null || y == null)
            {
                return 1;
            }

            if (x.Year > y.Year)
            {
                return -1;
            }
            else if (x.Year < y.Year)
            {
                return 1;
            }

            return 0;
        }
    }

    public class MakeAscendingComparer : IComparer<Car>
    {
        public int Compare(Car? x, Car? y)
        {
            if (x == null || y == null)
            {
                return -1;
            }

            if (x.Year > y.Year)
            {
                return 1;
            }
            else if (x.Year < y.Year)
            {
                return -1;
            }

            return 0;
        }
    }

    public class MakeAscendingEqualityComparer : IEqualityComparer<Car>
    {
        public bool Equals(Car? x, Car? y)
        {
            if (x == null || y == null)
            {
                throw new NullReferenceException();
            }

            if (x.Year == y.Year && x.Name == y.Name)
            {
                return true;
            }

            return false;
        }

        public int GetHashCode([DisallowNull] Car obj)
        {
            return (obj.Name + obj.Year).GetHashCode();
        }
    }
}

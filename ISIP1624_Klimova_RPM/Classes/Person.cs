using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP1624_Klimova_RPM.Classes
{
    public class Person
    {
        private string _name;
        private string _surname;
        public int Age;

        public Person(string _name, string _surname, int Age)
        {
            this._name = _name;
            this._surname = _surname;
            this.Age = Age;

        }

        public virtual string printInfo()
        {
            return ($"{_surname} {_name}");
        }

        public string getName()
        {
            return _name;
        }

        public string getSurname()
        {
            return _surname;
        }
    }
}

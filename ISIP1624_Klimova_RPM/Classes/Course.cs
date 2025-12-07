using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP1624_Klimova_RPM.Classes
{
    public class Course
    {
        private string _name;
        private List<Teacher> _teachers = new List<Teacher>();

        public Course(string _name, List<Teacher> _teachers)
        {
            this._name = _name;
            this._teachers = _teachers;
        }

        public void addTeacher(Teacher teacher)
        {
            _teachers.Add(teacher);
        }

        public string printInfo()
        {
            string result = "Название курса: " + _name + "\n" + "Преподаватели: ";

            foreach (Teacher teacher in _teachers)
            {
                result += $"{teacher.printInfo()} ";
            }

            result += "\n";

            return result;
        }

        public string getName()
        {
            return _name;
        }
    }
}

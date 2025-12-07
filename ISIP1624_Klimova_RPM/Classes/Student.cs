using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP1624_Klimova_RPM.Classes
{
    public class Student : Person
    {
        private List<Course> _courses = new List<Course>();
        public Student(string _name, string _surname, int Age)
            : base(_name, _surname, Age)
        {
        }

        public string parentPrintInfo()
        {
            return base.printInfo();
        }

        public override string printInfo()
        {

            string result = "СТУДЕНТ: ";
            result += base.printInfo() + "\n";

            foreach (Course course in _courses)
            {
                result += course.printInfo();
            }

            return result;
        }

        public string printStudent()
        {
            return base.printInfo();
        }

        public void addCourse(Course course)
        {
            _courses.Add(course);
        }

        public List<Course> getCourses()
        {
            return _courses;
        }
    }
}

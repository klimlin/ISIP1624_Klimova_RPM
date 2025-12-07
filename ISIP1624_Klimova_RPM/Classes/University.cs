using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP1624_Klimova_RPM.Classes
{
    public class University
    {
        private List<Student> _students = new List<Student>();
        private List<Course> _courses = new List<Course>();
        private List<Teacher> _teachers = new List<Teacher>();

        public void printStudents()
        {
            Console.WriteLine("СТУДЕНТЫ");
            foreach (Student student in _students)
            {
                Console.WriteLine(student.printStudent());
            }

            Console.WriteLine();
        }
        public void printStudentsCourses()
        {
            foreach (Student student in _students)
            {
                Console.WriteLine(student.printInfo());
            }

        }

        public void printTeachers()
        {
            Console.WriteLine("ПРЕПОДАВАТЕЛИ");
            foreach (Teacher teacher in _teachers)
            {
                Console.WriteLine(teacher.printInfo());
            }
            Console.WriteLine();
        }

        public void printCourses()
        {
            Console.WriteLine("КУРСЫ");
            foreach (Course course in _courses)
            {
                Console.WriteLine(course.printInfo());
            }
        }
        public void addStudent(Student student) { _students.Add(student); }
        public void addCourse(Course course) { _courses.Add(course); }
        public void addTeacher(Teacher teacher) { _teachers.Add(teacher); }

        private Person addNewPerson()
        {
            Person newPer = null;
            bool flag = false;
            while (!flag)
            {
                Console.WriteLine("Напишите фамилию");

                string surname = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(surname))
                {
                    Console.WriteLine("Попробуйте ещё раз");
                    break;
                }

                Console.WriteLine("Напишите имя");

                string name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Попробуйте ещё раз");
                    break;
                }

                bool resA = false;
                int age = 0;

                while (!resA)
                {
                    Console.WriteLine("Напишите возраст");
                    resA = int.TryParse(Console.ReadLine(), out age);

                    if (!resA)
                    {
                        Console.WriteLine("Неверный возраст. Попробуйте ещё раз");
                        resA = false;
                    }
                    else if (age < 0 || age > 100)
                    {
                        Console.WriteLine("Неверный возраст. Попробуйте ещё раз");
                        resA = false;
                    }

                }

                newPer = new Person(name, surname, age);
                flag = true;

            }

            return newPer;
        }
        public void addNewStudentYourself()
        {
            Console.WriteLine("НОВЫЙ СТУДЕНТ");
            Person newStud = addNewPerson();
            Student stud = new Student(newStud.getName(), newStud.getSurname(), newStud.Age);
            addStudent(stud);
        }

        public void addNewTeacherYourself()
        {
            Console.WriteLine("НОВЫЙ ПРЕПОДАВАТЕЛЬ");
            Person newTeach = addNewPerson();
            Teacher teach = new Teacher(newTeach.getName(), newTeach.getSurname(), newTeach.Age);
            addTeacher(teach);
        }

        public void findStudent()
        {
            Console.WriteLine("Напишите фамилию студента, которого хотите найти");
            string surname = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(surname))
            {
                Console.WriteLine("Попробуйте ещё раз");
                return;
            }

            var student = _students.FirstOrDefault(s => s.getSurname() == surname);
            if (student != null)
            {
                Console.WriteLine("Студент найден:  " + student.printInfo());
            }
            else
            {
                Console.WriteLine("Такого студента нет в базе");
            }
        }

        public void addCourseToStudent()
        {
            Console.WriteLine("Напишите фамилию студента, которого нужно записать на курс");
            string surname = Console.ReadLine();
            var student = _students.FirstOrDefault(s => s.getSurname() == surname);

            if (student == null)
            {
                Console.WriteLine("Такого студента нет в базе");
                return;
            }

            Console.WriteLine("На какой курс записываем студента? Напишите название курса");
            string courseName = Console.ReadLine();
            var course = _courses.FirstOrDefault(c => c.getName() == courseName);

            if (course == null)
            {
                Console.WriteLine("Курс не найден");
                return;
            }

            student.addCourse(course);
            Console.WriteLine("Студент успешно записан");
        }

        public void findTeacher()
        {
            Console.WriteLine("Напишите фамилию преподавателя, которого хотите найти");
            string surname = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(surname))
            {
                Console.WriteLine("Попробуйте ещё раз");
                return;
            }

            var teacher = _teachers.FirstOrDefault(t => t.getSurname() == surname);
            if (teacher != null)
            {
                Console.WriteLine("Преподаватель найден:  " + teacher.printInfo());
            }
            else
            {
                Console.WriteLine("Такого преподавателя нет в базе");
            }
        }

        public void addCourseToTeacher()
        {
            Console.WriteLine("Напишите фамилию преподавателя, который будет преподавать курс");
            string surname = Console.ReadLine();
            var teacher = _teachers.FirstOrDefault(t => t.getSurname() == surname);

            if (teacher == null)
            {
                Console.WriteLine("Такого преподавателя нет в базе");
                return;
            }

            Console.WriteLine("Какой курс будет вести преподаватель? Напишите название курса");
            string courseName = Console.ReadLine();
            var course = _courses.FirstOrDefault(c => c.getName() == courseName);

            if (course == null)
            {
                Console.WriteLine("Курс не найден");
                return;
            }

            course.addTeacher(teacher);
            Console.WriteLine("Преподаватель успешно записан");
        }

        public void addNewCourse()
        {
            Console.WriteLine("Напишите название курса, который хотите добавить");
            bool flag = false;
            string coursename = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(coursename))
            {
                Console.WriteLine("Попробуйте ещё раз");
            }
            else
            {
                Course newcourse = new Course(coursename, new List<Teacher>());
                _courses.Add(newcourse);
                Console.WriteLine("Новый курс успешно добавлен");
            }

            Console.WriteLine("Нового преподавателя можно добавить через пункт «ДОБАВИТЬ нового преподавателя»");
            Console.WriteLine("Назначить преподавателя на курс можно через пункт «НАЗНАЧИТЬ преподавателя на курс»");
        }

        public void findCourse()
        {
            Console.WriteLine("Напишите название курса, который хотите найти");
            string courseName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(courseName))
            {
                Console.WriteLine("Попробуйте ещё раз");
                return;
            }

            var course = _courses.FirstOrDefault(c => c.getName() == courseName);
            if (course != null)
            {
                Console.WriteLine("Курс найден:  " + course.printInfo());
            }
            else
            {
                Console.WriteLine("Такого курса нет в базе");
            }
        }

        public void printStudentsCourse()
        {
            Console.WriteLine("Напишите название курса");
            string courseName = Console.ReadLine();

            var course = _courses.FirstOrDefault(c => c.getName() == courseName);
            if (course == null)
            {
                Console.WriteLine("Курс не найден");
                return;
            }

            Console.WriteLine("Курс найден: " + course.printInfo());
            Console.WriteLine("Студенты:");
            var studentsInCourse = _students.Where(s => s.getCourses().Any(c => c.getName() == courseName));

            foreach (var student in studentsInCourse)
            {
                Console.WriteLine(student.parentPrintInfo());
            }
        }
    }
}

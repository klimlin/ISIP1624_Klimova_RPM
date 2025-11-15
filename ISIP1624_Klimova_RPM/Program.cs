

using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;


University university = new University();

Student Ivanov = new Student("Иван", "Иванов", 18);
Student Borisov = new Student("Максим", "Борисов", 20);
Student Novikov = new Student("Никита", "Новиков", 21);
Student Fyedorova = new Student("Катя", "Федорова", 19);
Student Krasilov = new Student("Герман", "Красилов", 22);

university.addStudent(Ivanov);
university.addStudent(Borisov);
university.addStudent(Novikov);
university.addStudent(Fyedorova);
university.addStudent(Krasilov);

Teacher Sabitov = new Teacher("Карим", "Сабитов", 36);
Teacher Haito = new Teacher("Оливия", "Хайто", 25);
Teacher Ovechkin = new Teacher("Глеб", "Овечкин", 55);
Teacher Vershinin = new Teacher("Мирон", "Вершинин", 44);
Teacher Malkina = new Teacher("Милана", "Малкина", 29);
Teacher Boyarkina = new Teacher("Настя", "Бояркина", 31);
Teacher Feigelman = new Teacher("Артём", "Фейгельман", 60);

List<Teacher> math_t = [Ovechkin, Vershinin];
Course math = new Course("Математика", math_t);

List<Teacher> russian_t = [Sabitov, Haito];
Course russian = new Course("Русский язык", russian_t);

List<Teacher> literature_t = [Malkina];
Course literature = new Course("Литература", literature_t);

List<Teacher> biology_t = [Boyarkina, Feigelman];
Course biology = new Course("Биология", biology_t);

List<Teacher> phisics_t = [Feigelman];
Course phisics = new Course("Физика", phisics_t);

Ivanov.addCourse(math);
Ivanov.addCourse(russian);

Borisov.addCourse(literature);
Borisov.addCourse(math);
Borisov.addCourse(russian);

Novikov.addCourse(math);
Novikov.addCourse(russian);
Novikov.addCourse(biology);

Fyedorova.addCourse(russian);
Fyedorova.addCourse(math);
Fyedorova.addCourse(literature);

Krasilov.addCourse(phisics);

university.addTeacher(Sabitov);
university.addTeacher(Haito);
university.addTeacher(Ovechkin);
university.addTeacher(Vershinin);
university.addTeacher(Malkina);
university.addTeacher(Boyarkina);
university.addTeacher(Feigelman);

university.addCourse(math);
university.addCourse(russian);
university.addCourse(biology);
university.addCourse(literature);
university.addCourse(phisics);

university.printCourses();
university.printStudents();
university.printTeachers();
university.printStudentsCourses();

class Person
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
}

class Student : Person
{
    private List<Course> _courses = new List<Course>();
    public Student(string _name, string _surname, int Age) 
        : base(_name, _surname, Age)
    {
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
}

class Teacher : Person
{

    public Teacher(string _name, string _surname, int Age) 
        : base(_name, _surname, Age)
    {
    }
}

class Course
{
    private string _name;
    private List<Teacher> _teachers = new List<Teacher>();

    public Course(string _name, List<Teacher> _teachers)
    {
        this._name= _name;
        this._teachers = _teachers;
    }

    public string printInfo()
    {
        string result = "Название курса: " + _name + "\n" + "Преподаватели: ";
        
        foreach (Teacher teacher in _teachers) { 
            result += $"{teacher.printInfo()} ";
        }

        result += "\n";

        return result;
    }


}

class University
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
            Console.WriteLine( student.printInfo());
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
    public void addStudent(Student student) { _students.Add(student);}

    public void addCourse(Course course){ _courses.Add(course);}

    public void addTeacher(Teacher teacher){ _teachers.Add(teacher);}

    public void deleteStudent()
    {

    }
    public void deleteCourse() { }
    public void deleteTeacher() { }

}









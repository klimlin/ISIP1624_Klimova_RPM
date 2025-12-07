

//using System.ComponentModel;
//using System.Globalization;
//using System.Xml.Linq;

using ISIP1624_Klimova_RPM.Classes;


University university = new University();

Student ivanov = new Student("Иван", "Иванов", 18);
Student borisov = new Student("Максим", "Борисов", 20);
Student novikov = new Student("Никита", "Новиков", 21);
Student fyedorova = new Student("Катя", "Федорова", 19);
Student krasilov = new Student("Герман", "Красилов", 22);

university.addStudent(ivanov);
university.addStudent(borisov);
university.addStudent(novikov);
university.addStudent(fyedorova);
university.addStudent(krasilov);

Teacher sabitov = new Teacher("Карим", "Сабитов", 36);
Teacher haito = new Teacher("Оливия", "Хайто", 25);
Teacher ovechkin = new Teacher("Глеб", "Овечкин", 55);
Teacher vershinin = new Teacher("Мирон", "Вершинин", 44);
Teacher malkina = new Teacher("Милана", "Малкина", 29);
Teacher boyarkina = new Teacher("Настя", "Бояркина", 31);
Teacher feigelman = new Teacher("Артём", "Фейгельман", 60);

List<Teacher> matht = [ovechkin, vershinin];
Course math = new Course("Математика", matht);

List<Teacher> russiant = [sabitov, haito];
Course russian = new Course("Русский язык", russiant);

List<Teacher> literaturet = [malkina];
Course literature = new Course("Литература", literaturet);

List<Teacher> biologyt = [boyarkina, feigelman];
Course biology = new Course("Биология", biologyt);

List<Teacher> phisicst = [feigelman];
Course phisics = new Course("Физика", phisicst);

ivanov.addCourse(math);
ivanov.addCourse(russian);

borisov.addCourse(literature);
borisov.addCourse(math);
borisov.addCourse(russian);

novikov.addCourse(math);
novikov.addCourse(russian);
novikov.addCourse(biology);

fyedorova.addCourse(russian);
fyedorova.addCourse(math);
fyedorova.addCourse(literature);

krasilov.addCourse(phisics);

university.addTeacher(sabitov);
university.addTeacher(haito);
university.addTeacher(ovechkin);
university.addTeacher(vershinin);
university.addTeacher(malkina);
university.addTeacher(boyarkina);
university.addTeacher(feigelman);

university.addCourse(math);
university.addCourse(russian);
university.addCourse(biology);
university.addCourse(literature);
university.addCourse(phisics);

//university.printCourses();
//university.printStudents();
//university.printTeachers();
//university.printStudentsCourses();

bool cond = true;

while (cond)
{
    PrintMainMenu();

    int var = 0;
    bool result = false;
    while (!result)
    {
        Console.WriteLine("Введите число выбранного пункта");
        result = int.TryParse(Console.ReadLine(), out var);
    }

    switch (var)
    {
        case 0: cond = false; break;
        case 1:
            university.printCourses();
            university.printStudents();
            university.printTeachers();
            university.printStudentsCourses();
            break;
        case 2: university.printStudents(); break;
        case 3: university.printTeachers(); break;
        case 4: university.printCourses(); break;
        case 5: university.printStudentsCourses(); break;

        case 6: university.addNewStudentYourself(); break;
        case 7: university.findStudent(); break;
        case 8: university.addCourseToStudent(); break;

        case 9: university.addNewTeacherYourself(); break;
        case 10: university.findTeacher(); break;
        case 11: university.addCourseToTeacher(); break;

        case 12: university.addNewCourse(); break;
        case 13: university.findCourse(); break;
        case 14: university.printStudentsCourse(); break;

        default: Console.WriteLine("Некорректный ввод. Введите число."); break;
    }
}

void PrintMainMenu()
{
    Console.WriteLine("УПРАВЛЕНИЕ УЧЕБНЫМ ПРОЦЕССОМ В УНИВЕРСИТЕТЕ");
    Console.WriteLine();
    Console.WriteLine("1. ВЫВЕСТИ всю информацию об университете");
    Console.WriteLine("2. ВЫВЕСТИ список студентов");
    Console.WriteLine("3. ВЫВЕСТИ список преподавателей");
    Console.WriteLine("4. ВЫВЕСТИ список курсов");
    Console.WriteLine("5. ВЫВЕСТИ список студентов и их курсов");
    Console.WriteLine();
    Console.WriteLine("6. ДОБАВИТЬ нового студента");
    Console.WriteLine("7. ПОСМОТРЕТЬ информацию о конкретном студента (поиск по фамилии)");
    Console.WriteLine("8. ЗАПИСАТЬ студента на курс");
    Console.WriteLine();
    Console.WriteLine("9. ДОБАВИТЬ нового преподавателя");
    Console.WriteLine("10. ПОСМОТРЕТЬ информацию о конкретном преподавателе (поиск по фамилии)");
    Console.WriteLine("11. НАЗНАЧИТЬ преподавателя на курс");
    Console.WriteLine();
    Console.WriteLine("12. ДОБАВИТЬ новый курс");
    Console.WriteLine("13. ПОСМОТРЕТЬ информацию о курсе");
    Console.WriteLine("14. ВЫВЕСТИ всех студентов, записанных на курс");
    Console.WriteLine("0. Закончить работу");
}












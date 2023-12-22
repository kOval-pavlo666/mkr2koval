using System;
using System.Collections.Generic;

// Інтерфейс для спостерігача (студента)
public interface IObserver
{
    void Update(Task task); // Метод оновлення, який буде викликаний при отриманні нового завдання
}

// Інтерфейс для предмету, до якого можна підписатись
public interface ISubject
{
    void Attach(IObserver observer); // Метод підписки на спостереження за завданнями
    void Detach(IObserver observer); // Метод відписки від спостереження
    void Notify(); // Метод сповіщення про зміни (нове завдання)
}

// Реалізація конкретного завдання
public class Task
{
    public string Title { get; set; }
    public string Text { get; set; }
    public string Subject { get; set; }
    public DateTime DueDate { get; set; }
}

// Реалізація предмету, до якого можна підписатись на отримання сповіщень
public class Subject : ISubject
{
    private List<IObserver> observers = new List<IObserver>();
    private List<Task> tasks = new List<Task>();

    public void Attach(IObserver observer)
    {
        observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (var observer in observers)
        {
            observer.Update(tasks[tasks.Count - 1]); // Повідомлення про останнє завдання
        }
    }

    // Додавання нового завдання до предмету
    public void AddTask(Task task)
    {
        tasks.Add(task);
        Notify(); // При додаванні нового завдання сповіщаємо спостерігачів
    }
}

// Реалізація студента-спостерігача
public class Student : IObserver
{
    public string Name { get; set; }

    public Student(string name)
    {
        Name = name;
    }

    public void Update(Task task)
    {
        Console.WriteLine($"Студент {Name} отримав нове завдання з предмету '{task.Subject}': {task.Title}. Дата виконання: {task.DueDate}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Створення предмету
        Subject mathSubject = new Subject();

        // Створення студентів-спостерігачів
        Student student1 = new Student("Іван");
        Student student2 = new Student("Марія");

        // Підписка студентів на отримання сповіщень з предмету
        mathSubject.Attach(student1);
        mathSubject.Attach(student2);

        // Додавання нового завдання з математики
        mathSubject.AddTask(new Task
        {
            Title = "Розв'язати рівняння",
            Text = "Розв'язати рівняння 2x + 5 = 15",
            Subject = "Математика",
            DueDate = DateTime.Now.AddDays(7)
        });

        // Відписка одного студента від отримання сповіщень
        mathSubject.Detach(student2);

        // Додавання ще одного завдання з математики
        mathSubject.AddTask(new Task
        {
            Title = "Побудувати графік",
            Text = "Побудувати графік функції y = 2x - 3",
            Subject = "Математика",
            DueDate = DateTime.Now.AddDays(5)
        });
    }
}

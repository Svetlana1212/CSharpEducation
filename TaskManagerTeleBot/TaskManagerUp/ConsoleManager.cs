using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    internal class ConsoleManager
    {
        public int ReturnInt(string message = "")
        {
            int userId;
            do
            {
                Console.WriteLine(message);
            }
            while (!Int32.TryParse(Console.ReadLine(), out userId));
            return userId;
        }
        public  WorkTask CreateTask(int id)
        {
            Console.Write("Введите название задачи: ");
            string name = Console.ReadLine();

            Console.Write("Введите описание задачи: ");
            string description = Console.ReadLine();

            Console.Write("Введите срок выполнения задачи (ДД.ММ.ГГГГ): ");
            DateTime deadline = Convert.ToDateTime(Console.ReadLine());

            Console.Write("Укажите приоритет ('Обычный' / 'Срочный'): ");
            string priority = Console.ReadLine();

            WorkTask newWorkTask = new WorkTask(id, name, description, deadline);
            newWorkTask.Priority = priority;
            newWorkTask.Status = "Свободная";
            return newWorkTask;
        }

        public  Dictionary<string, string> UpdateTask()
        {
            Dictionary<string, string> paramerts = new Dictionary<string, string>();
            Console.Write("Введите название задачи: ");
            string name = Console.ReadLine();
            paramerts.Add("Name", name);

            Console.Write("Введите описание задачи: ");
            string description = Console.ReadLine();
            paramerts.Add("Description", description);

            Console.Write("Введите срок выполнения задачи (ДД.ММ.ГГГГ): ");
            DateTime deadline = Convert.ToDateTime(Console.ReadLine());
            paramerts.Add("Deadline", deadline.ToString());

            Console.Write("Укажите приоритет ('Обычный' / 'Срочный'): ");
            string priority = Console.ReadLine();
            paramerts.Add("Priority", priority);

            Console.WriteLine("Укажите статус задачи: ");
            string status = Console.ReadLine();
            paramerts.Add("Status", status);

            return paramerts;
        }
        public void ListOutput(List<WorkTask> workTasks)
        {
            foreach (WorkTask task in workTasks)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"Название:{task.Name} ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Описание: {task.Description}");
                Console.Write($"Срок выполнения: {task.Deadline} ");
                Console.Write($"Статус: {task.Status} ");
                Console.WriteLine($"Приоритет: {task.Priority}");
                if (task.Status != "Свободная")
                {
                    Console.Write("Ответственные: ");
                    foreach (User item in task.Responsible)
                    {
                        Console.Write($" {item.Name}");
                        Console.Write($" {item.Surname}  ");
                    }
                }
                Console.WriteLine();
            }
        }
        public void TaskInfo(WorkTask task)
        {
            Console.WriteLine($"Задача номер {task.Id}");
            Console.WriteLine(task.Name);
            Console.WriteLine(task.Description);
            Console.WriteLine($"Срок выолнения: {task.Deadline}");
            Console.WriteLine($"Дата создания: {task.СreationDate}");
            Console.WriteLine($"Статус: {task.Status}");
            Console.WriteLine($"Приоритет: {task.Priority}");
            Console.Write("Ответственные:");
            foreach (User item in task.Responsible)
            {
                Console.Write($" {item.Name}");
                Console.Write($" {item.Surname}");
            }
            
        }
        public User CreateUser()
        {
            Console.WriteLine("Введите логин: ");
            string login = Console.ReadLine();

            Console.WriteLine("Введите пароль: ");
            string password = Console.ReadLine();

            Console.Write("Введите имя: ");
            string name = Console.ReadLine();

            Console.Write("Введите фамилию: ");
            string surname = Console.ReadLine();

            Console.Write("Введите email: ");
            string email = Console.ReadLine();

            Console.Write("Введите должность: ");
            string jobTitle = Console.ReadLine();
            int id = UserManager.users.Count + 1;
            User newUser = new User(id, name, surname, email)
            {

                Login = login,
                Password = password,
                JobTitle = jobTitle,
                Role = "user"
            };
            return newUser;
        }

        public Dictionary<string, string> UpdateUser()
        {
            Dictionary<string, string> parametrs = new Dictionary<string, string>();
            Console.WriteLine("Введите новый логин: ");
            string login = Console.ReadLine();
            parametrs.Add("Login", login);

            Console.WriteLine("Введите новый пароль: ");
            string password = Console.ReadLine();
            parametrs.Add("Password", password);

            Console.Write("Введите фамилию: ");
            string surname = Console.ReadLine();
            parametrs.Add("Surname", surname);

            Console.Write("Введите новый email: ");
            string email = Console.ReadLine();
            parametrs.Add("Email", email);

            Console.Write("Введите  новую должность: ");
            string jobTitle = Console.ReadLine();
            parametrs.Add("JobTitle", jobTitle);

            return parametrs;
        }
    }
}

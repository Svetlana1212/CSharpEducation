using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using TaskManager;
using static System.Net.Mime.MediaTypeNames;

namespace TaskManagerUp
{
    internal class Program
    {
        #region Методы консоли

        #endregion
        static async Task Main(string[] args)
        {
            
            //var newTaskManager = new WorkTaskManager();
            //CommentsManager commentsManager = new CommentsManager();
            ConsoleManager consoleManager = new ConsoleManager();
            UserManager.LoadFromFile();
            string readerFile = "8228046850:AAGTNyQR94T50vxF8t4R1kkiPVIqxu8_9e4";
            try
            {
                var botService = new TelegramBot(readerFile);
                await botService.StartAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при чтении файла: " + ex.Message);
                Console.WriteLine("Вы ввели не правильный токен, приложение будет перезапущено, " +
                  "введите токен правильно");
            }
            /*

            do
            {
                Console.WriteLine(@"
                1. Авторизация
                2. Регистрация");                

                int number = consoleManager.ReturnInt();
                User CurrentUser = null;
                if (number == 1)
                {
                    Console.WriteLine("Введите логин:");
                    string login = Console.ReadLine();
                    Console.WriteLine("Введите пароь:");
                    string password = Console.ReadLine();
                    CurrentUser = UserManager.Auth(login,password);
                    
                }
                else if (number == 2)
                {
                    User newUser = consoleManager.CreateUser();
                    if(UserManager.Add(newUser))
                    {
                        Console.WriteLine("Регистрация завершена");
                        CurrentUser = newUser;
                    }
                    else
                    {
                        Console.WriteLine("Произошла ошибка");
                    }
                }
                if (CurrentUser == null)
                {
                    Console.WriteLine("Пользователь не найден.");
                }
                else
                {
                    auth = true;                    
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Добро пожаловать,{CurrentUser.Name}!\n\n");
                    Console.ForegroundColor = ConsoleColor.White;

                    do {                 
                        
                        if (CurrentUser.Role != "admin")
                        {
                            Console.WriteLine(@"
    1. Мои задачи
    2. Свободные задачи
    3. Посмотреть информацию о задаче
    4. Выйти");
                            
                            int num = consoleManager.ReturnInt();                              

                            switch (num)
                            {
                                case 1:
                                    List<WorkTask> workTasks = newTaskManager.ListCreate(CurrentUser);
                                    if (workTasks.Count > 0)
                                    {
                                        Console.Clear();
                                        consoleManager.ListOutput(workTasks);
                                        
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Нет задач в работе");
                                    }                                    
                                    break;
                                case 2:                                    
                                    List<WorkTask> freeTasks = newTaskManager.Filter (null, "status", "Свободная");
                                    if (freeTasks.Count > 0)
                                    {
                                        consoleManager.ListOutput(freeTasks);
                                    }
                                    else 
                                    {
                                        Console.WriteLine("Нет свободных задач");
                                    }
                                    break;
                                    
                                case 3:
                                    int currentTaskNumber = consoleManager.ReturnInt("Введите ID задачи: ");                                    
                                    WorkTask currentTask = newTaskManager.Search(currentTaskNumber);
                                    if (currentTask!=null)
                                    {
                                        Console.Clear();
                                        consoleManager.TaskInfo(currentTask);
                                        Console.WriteLine();
                                        List<Comment> comments = commentsManager.SearchTaskComment(currentTask.Id);
                                        foreach (var comment in comments)
                                        {
                                            Console.WriteLine(comment.Author);
                                            Console.WriteLine(comment.Description);
                                            Console.WriteLine();
                                        }
                                            
                                        Console.WriteLine();
                                        Console.WriteLine(@"
    1.Взять задачу в работу
    2.Изменить статус
    3.Добавить комментарий");
                                            int currenNumber = consoleManager.ReturnInt();

                                            if (currenNumber == 1)
                                            {

                                                if (newTaskManager.AddResponsible(currentTask, CurrentUser))
                                                {
                                                    Console.WriteLine($"Вы взяли в работу задачу {currentTask.Name}");
                                                }
                                            }
                                            else if (currenNumber == 2)
                                            {
                                                if (currentTask.Responsible.Find(item => item.Id == CurrentUser.Id) != null)
                                                {
                                                    Console.WriteLine("Выбирите статус: ");
                                                    Console.WriteLine(@"
1.Обсуждение
2.В работе
3.Выполнена");
                                                    if (int.TryParse(Console.ReadLine(), out int statusNum))
                                                    {
                                                        switch (statusNum)
                                                        {
                                                            case 1:
                                                                currentTask.Status = "Обсуждение";
                                                                break;
                                                            case 2:
                                                                currentTask.Status = "В работе";
                                                                break;
                                                            case 3:
                                                                currentTask.Status = "Выполнена";
                                                                break;
                                                        }
                                                        if (newTaskManager.WriteDown())
                                                        {
                                                            Console.WriteLine("Вы изменили статус задачи");
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Вы не можете менять статус данной задачи");
                                                }

                                            }

                                            else if (currenNumber == 3)
                                            {
                                                Console.WriteLine("Введите текст комментария: ");
                                                string Commenttext = Console.ReadLine();
                                                Comment userComment = new Comment(Commenttext, CurrentUser.Name, currentTask.Id);
                                                commentsManager.Add(userComment);
                                            }                                
                                                                                                     
                                    }

                                    break;
                                case 4:
                                    return;

                            }
                            
                        }
                        else // если админ
                        {
                            Console.WriteLine(@"
1. Список задач
2. Свободные задачи
3. Найти задачи пользователя
4. Добавить задачу
5. Назначить ответственного к задаче
6. Редактировать задачу
7. Добавить комментарий к задаче
8. Удалить задачу
9. Список пользователей
10. Добавить пользователя
11. Редактировать пользователя
12. Удалить пользователя
13. Выйти");

                            if (int.TryParse(Console.ReadLine(), out int adminChoice))
                            {
                                switch (adminChoice)
                                {
                                    case 1:
                                        List<WorkTask> AllTask = newTaskManager.ListCreate(CurrentUser);
                                        consoleManager.ListOutput(AllTask);
                                        Console.WriteLine(@" 
Сортировать:
1.По имени 
2.По описанию
3.По дедлайну
4.По статусу
5.По приоритету");
                                        if (int.TryParse(Console.ReadLine(), out int sortChoice))
                                        {
                                            if (sortChoice == 1) { newTaskManager.Sort(AllTask, "Name"); }
                                            else if (sortChoice == 2) { newTaskManager.Sort(AllTask, "Description"); }
                                            else if (sortChoice == 3) { newTaskManager.Sort(AllTask, "Deadline"); }
                                            else if (sortChoice == 4) { newTaskManager.Sort(AllTask, "Status"); }
                                            else if (sortChoice == 5) { newTaskManager.Sort(AllTask, "Priority"); }
                                            consoleManager.ListOutput(AllTask);
                                        }
                                        break;
                                    case 2:
                                        List<WorkTask> FreeTask = newTaskManager.Filter(null, "status", "Свободная"); // Без ответственных
                                        consoleManager.ListOutput(FreeTask);
                                        break;
                                    case 3:
                                        Console.Write("Введите ID пользователя: ");
                                        int.TryParse(Console.ReadLine(), out int uid);
                                        List<WorkTask> userTaskId = newTaskManager.Filter(null, "responsibleId", uid.ToString());
                                        consoleManager.ListOutput(userTaskId);
                                        break;
                                    case 4:
                                        int id = consoleManager.ReturnInt("Введите id для новой задачи");
                                        WorkTask task = consoleManager.CreateTask(id);
                                        if (newTaskManager.Add(task))
                                        {
                                            Console.WriteLine("Задача успешно создана");
                                        }
                                        
                                        break;
                                    case 5:
                                        int TaskId=consoleManager.ReturnInt("Введите id задачи: ");
                                        WorkTask newStatusTask = newTaskManager.Search(TaskId);
                                        int userId = consoleManager.ReturnInt("Введите id пользователя: ");
                                        User user = UserManager.Search(userId);
                                        if (newTaskManager.AddResponsible(newStatusTask, user)) { Console.WriteLine($"Вы назначили ответственного {user.Name} {user.Surname} для задачи {newStatusTask.Name} "); }                                                                        
                                        newTaskManager.SendMessage(newStatusTask, user);
                                        break;
                                    case 6:
                                        int updateTaskId = consoleManager.ReturnInt("Введите id задачи: ");
                                        WorkTask updateTask = newTaskManager.Search(updateTaskId);
                                        if (updateTask != null)
                                        {
                                            var param = consoleManager.UpdateTask();
                                            newTaskManager.Update(updateTask, param);
                                        }                                        
                                        
                                        break;
                                    case 7:
                                        int taskId = consoleManager.ReturnInt("Введите id задачи");
                                        Console.WriteLine("Введите комментарий");
                                        Comment comment = new Comment(Console.ReadLine(),CurrentUser.Name,taskId);
                                        comment.Author = CurrentUser.Name;                                        
                                        commentsManager.Add(comment);
                                        break;
                                    case 8:
                                        int deleteTaskId = consoleManager.ReturnInt("Введите id задачи: ");
                                        WorkTask deleteTask = newTaskManager.Search(deleteTaskId);
                                        newTaskManager.Delete(deleteTask);
                                        break;
                                    case 9:
                                        UserManager.List();
                                        break;
                                    case 10:
                                        User User = consoleManager.CreateUser();
                                        if (UserManager.Add(User))
                                        {
                                            Console.WriteLine("Пользователь успешно создан");
                                        }
                                        break;
                                    case 11:
                                        int updateUserId = consoleManager.ReturnInt("Введите id пользователя");
                                        User updateUser = UserManager.Search(updateUserId);
                                        Dictionary<string, string> parametrs= new Dictionary<string,string>();
                                        //UserManager.Update(updateUser);
                                        break;
                                    case 12:
                                        
                                        break;
                                    case 13:
                                        return;
                                }
                            }


                        }

                    } while (true);
                }                


            } while (auth == false);
           
        }*/

        }
    }
}


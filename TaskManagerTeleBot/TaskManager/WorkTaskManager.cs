using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using static TaskManager.UserException;

namespace TaskManager
{
    public class WorkTaskManager : IDataСollection<WorkTask>
    {
        private const string Path = "tasks.txt";
        public static string reportPath = "report.txt";

        private static List<WorkTask> myTasks = new List<WorkTask>();
        public static List<WorkTask> Tasks
        {
            get
            {
                return myTasks;
            }
            set
            {
                Tasks = new List<WorkTask>();
            }
        }
        public static int Count = Tasks.Count();
        public int GetCount()
        {
            return Tasks.Count();
        }
        public static void DeadlineСheck(WorkTask task)
        {
            int result = DateTime.Compare(DateTime.Now, task.Deadline);
            if (result > 0) 
            {
                task.Status = "Просрочена";
            }
        }

        public List<WorkTask> ListCreate(User user)
        {
            List<WorkTask> myTasks = new List<WorkTask>();
            foreach (WorkTask task in Tasks)
            {
                DeadlineСheck(task);
                if (user.Role == "admin")
                {      
                    myTasks.Add(task);
                }
                else
                {
                    foreach (User item in task.Responsible)
                    {
                        if (item.Id == user.Id)
                        {
                            myTasks.Add(task);
                        }
                    }
                    
                }
            }            
            return myTasks;
        }

        public List<WorkTask> FreeTaskList()
        {
            List<WorkTask> myTasks = new List<WorkTask>();
            foreach (WorkTask task in Tasks)
            {
                DeadlineСheck(task);
                if (task.Status == "Свободная")
                {
                    myTasks.Add(task);
                }
                
            }
            return myTasks;
        }

        public bool Add(WorkTask workTask)
        {
            if (Tasks.FirstOrDefault(u => u.Id == workTask.Id) != null)
            {
                throw new EmployeeAlreadyAdded("Пользователь с таким Id уже есть");
            }
            else
            {
                Tasks.Add(workTask);
                WriteDown();
                return true;
            }
        }

        public WorkTask Search(int id)
        {
            WorkTask searchTask = Tasks.Find(item => item.Id == id);
            if (searchTask == null)
            {
                throw new EmployeeNotFound("Задача с таким Id не найдена");
            }
            return searchTask;            
        }

        public bool Update(WorkTask task, Dictionary<string, string> parametrs)
        {
           
            task.Description = (parametrs["description"] != string.Empty) ? parametrs["description"] : task.Description; ; 
            task.Deadline = (parametrs["deadline"] != string.Empty) ? Convert.ToDateTime(parametrs["deadline"]) :task.Deadline;
            task.Priority = (parametrs["priority"] != string.Empty) ? parametrs["priority"] : task.Priority;
            task.Status = (parametrs["status"] != string.Empty) ? parametrs["status"]: task.Status;             
            WriteDown();            
            return true;
        }

        public bool Delete (WorkTask task)
        {
            Tasks.Remove(task);
            WriteDown();           
            return true;            
        }

        public bool Sort(List<WorkTask> works, string sort)
        {
            if(sort=="Name")
                works.Sort((task1, task2) => task1.Name.CompareTo(task2.Name));
            else if (sort == "Description")
                works.Sort((task1, task2) => task1.Description.CompareTo(task2.Description));
            else if(sort == "Deadline")
                works.Sort((task1, task2) => task1.Deadline.CompareTo(task2.Deadline));
            else if (sort == "СreationDate")
                works.Sort((task1, task2) => task1.СreationDate.CompareTo(task2.СreationDate));
            else if(sort == "Priority")
                works.Sort((task1, task2) => task1.Priority.CompareTo(task2.Priority));
            else if(sort == "Status")
                works.Sort((task1, task2) => task1.Status.CompareTo(task2.Status));            
            else
            {
                return false;
            }
            return true;
        }

        public List<WorkTask> Filter(List<WorkTask> works, string condition,string meaning)
        {
            List<WorkTask> filtrTask = new List<WorkTask>();
            if (works == null)
            {
                works = Tasks;
            }
           
            foreach (WorkTask task in works)
            {
                bool filter=false;
                if (condition == "status")
                {
                    filter = (task.Status == meaning) ? true : false;
                }
                else if (condition == "priority")
                {
                    filter = (task.Priority == meaning) ? true : false;
                }
                
                else if (condition == "responsibleId")
                {
                    
                    User found = task.Responsible.Find(item => item.Id == Int32.Parse(meaning));
                    filter = (found != null)  ? true : false;
                }
                else
                {
                    Console.WriteLine("Некорректные условия фильтрации");
                }
                if (filter)
                {
                    filtrTask.Add(task);
                }
            }         
            return filtrTask;
        }       

        public bool SendMessage(WorkTask task, User user)
        {             
            MailAddress from = new MailAddress("somemail@gmail.com", "Tom");
            MailAddress to = new MailAddress(user.Email);

            
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Вас назначили ответственным за задачу";
            m.Body = "<h2>Вас назначили ответственным на задачу</h2>"+task.Id+" "+task.Name;
            m.IsBodyHtml = true;

             
            SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 465);
            smtp.Credentials = new NetworkCredential("pl.swetik@yandex.ru", "Sos197sos");
            smtp.EnableSsl = true;
            

            try
            {
                smtp.Send(m);
                Console.WriteLine("Письмо отправлено!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при отправке письма: " + ex.Message);
            }
            
            return true;
        }
        
        public bool AddResponsible(WorkTask task, User user)
        {
            User found = task.Responsible.Find(item => item.Id == user.Id);
            bool res=false;
            if (found==null) 
            {
                task.Responsible.Add(user);
                if (task.Status == "Свободная") 
                {
                    task.Status = "назначен ответственный";
                }
                if (WriteDown())
                {
                    res=true;
                }
            }                
            else 
            {
                Console.WriteLine("Пользователь уже назначен ответственным для этой задачи");                
            }
            return res;                          
        }

        public bool DelAllResponsible(WorkTask task)
        {
            task.Responsible.Clear();
            task.Status = "Свободная";            
            return true;
        } 
        
        public static bool GenerateAReport()
        {
            using StreamWriter sw = File.CreateText(reportPath);
            foreach (var item in Tasks)
            {
                sw.Write($"Номер задачи: {item.Id}");
                sw.Write($" Название: {item.Name}");
                sw.Write($" Описание: {item.Description}");
                sw.Write($" Дедлайн: {item.Deadline}");
                sw.Write($" Дата создания: {item.СreationDate}");
                sw.Write($" Текущий статус: {item.Status}");
                sw.Write($" Приоритет: {item.Priority}");
                if (item.Responsible.Count != 0)
                {
                    foreach (User user in item.Responsible)
                    {
                        sw.Write($" Ответственные: {user.Id}, {user.Name},{user.Surname},{user.Email}\n");
                    }
                }
                else
                {
                    sw.WriteLine("ответственный не назначен");
                }

            }
            return true;
        }

        public bool WriteDown()
        {
            using StreamWriter sw = File.CreateText(Path);
            foreach (var item in Tasks)
            {
                sw.WriteLine($"id:{item.Id}, name:{item.Name}, description:{item.Description},deadline:{item.Deadline},creationDate:{item.СreationDate},status:{item.Status},priority:{item.Priority}");                
                if (item.Responsible.Count!= 0)
                {
                    sw.Write("responsible:");
                    foreach (User user in item.Responsible)
                    {
                        sw.Write($"userId_{user.Id}, userName_{user.Name},userSurname_{user.Surname},userEmail_{user.Email}|");
                    }
                    sw.Write("\n");
                }
                else
                {
                    sw.WriteLine($"responsible: ответственный не назначен");
                }
            }
            return true;
        }

        public static bool Read()
        {
           
            if (!File.Exists(Path)) 
            {               
                return false;
            }         
            string[] lines = File.ReadAllLines(Path);            
            if (lines.Length >= 1)
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');                    
                    for (int i = 0; i < parts.Length; i++)
                    {
                        string[] item = parts[i].Split(':');
                        dictionary.Add(item[0], item[1]);
                    }
                    WorkTask task = new WorkTask(Int32.Parse(dictionary["id"]), dictionary["name"], dictionary["description"], DateTime.Parse(dictionary["deadline"]));
                   
                    task.СreationDate = DateTime.Parse(dictionary["creationDate"]);
                    task.Status = dictionary["status"];
                    task.Priority = dictionary["priority"];
                    if (dictionary["status"] != "Свободная")
                    {
                        string[] Respons = dictionary["responsible"].Split("*");
                        foreach (string item in Respons)
                        {
                            string[] linUser = item.Split("|");
                           
                            if (linUser.Length > 1)
                            {
                                Dictionary<string, string> userDictonary = new Dictionary<string, string>();
                                
                                for (int n = 0; n < linUser.Length; n++)
                                {
                                    string[] lin = linUser[n].Split('_');
                                    userDictonary.Add(lin[0], lin[1]);
                                }
                                User user = new User(Int32.Parse(userDictonary["userId"]), userDictonary["userName"], userDictonary["userSurname"], userDictonary["userEmail"]);
                                task.Responsible.Add(user);
                                userDictonary.Clear();
                            }
                        }
                    }
                    Tasks.Add(task);
                    dictionary.Clear();
                }
            }
            return true;
        }   
        public WorkTaskManager()
        {
            Read();
        }
    }
}

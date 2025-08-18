using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TaskManager.UserException;

namespace TaskManager
{
    public class UserManager:IDataСollection <User>
    {
        public static string path = "users.txt";
        public static List<User> users = new List<User>();
        public static void List()
        {
            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.Id}, {user.Name} {user.Surname}, Email: {user.Email}, Role: {user.Role}");
            }
        }
        public static User Auth(string login,string password)
        {
            User authUser = users.Find(item => item.Login == login&&item.Password == password);            
            return authUser;
        }

        public static bool Add(User newUser)
        {           

            users.Add(newUser);
            SaveToFile();
            return true;
        }
        public static User Search(int Id)
        {
            User searchUser = users.Find(item => item.Id == Id);
            if (searchUser == null)
            {
                throw new EmployeeNotFound("Пользователь с таким Id не найден");
            }
            return searchUser;
        }

        public static bool Update(User user,Dictionary<string,string> parametrs)
        {
            user.Surname = parametrs["surname"];
            user.Description = parametrs["discription"];
            user.JobTitle = parametrs["jobTitle"];                
            user.Role = parametrs["role"];
            SaveToFile();
            return true;           
        }

        public static bool Delete(int Id)
        {
            User user = users.FirstOrDefault(u => u.Id == Id);
                if (user != null)
                {
                    users.Remove(user);
                    SaveToFile();
                    return true;
                }
            return false;
        }

        public static void LoadFromFile()
        {
            if (!File.Exists(path)) return;

            string[] lines = File.ReadAllLines(path);
            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length >= 8)
                {
                    User user = new User(int.Parse(parts[0]), parts[3], parts[4], parts[5])
                    {

                        Login = parts[1],
                        Password = parts[2],
                        JobTitle = parts[6],
                        Role = parts[7]
                    };
                    users.Add(user);
                }
            }
        }

        public static void SaveToFile()
        {
            using StreamWriter sw = new StreamWriter(path);
            foreach (var user in users)
            {
                sw.WriteLine($"{user.Id}|{user.Login}|{user.Password}|{user.Name}|{user.Surname}|{user.Email}|{user.JobTitle}|{user.Role}");
            }
        }

    }
}


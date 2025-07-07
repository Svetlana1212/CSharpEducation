using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace StaffManager
{
    /// <summary>
    /// Управляет списком сотрудников
    /// </summary>
    public class EmployeeManager: IEmployeeManager<Employee>
    {
        /// <summary>
        /// Путь к файлу, где записаны сотрудники
        /// </summary>
        private static string Path = "staff.txt";

        /// <summary>
        /// Коллекция для хранения списка сотрудиков
        /// </summary>
        public static List<Employee> Staffs = new List<Employee>();        

        /// <summary>
        /// Добавляет сотрудика в список
        /// </summary>
        /// <param name="employee">Объект класса Employee(сотрудик) </param>
        /// <exception cref="AddIdException"></exception>
        public static bool Add(Employee employee) 
        {
            if (Staffs.FirstOrDefault(u => u.Id == employee.Id) != null)
            {
                throw new AddIdException("Пользователь с таким Id уже есть");
                return false;
            } 
            else 
            {
                Staffs.Add(employee);
                Write();
                return true;                
            }                            
        }

        /// <summary>
        /// Находит сотрудика в списке
        /// </summary>
        /// <param name="id">Id сотрудника</param>
        /// <returns>Объект сотрудика</returns>
        /// <exception cref="SreachNullException">Если объект пустой</exception>
        public static Employee Get(int id)
        {
            Employee user = Staffs.Find(item => item.Id == id);
            if (user == null)
            {
                throw new SreachNullException("Пользователь с таким Id не найден");              
            }
            return user;
        }

        /// <summary>
        /// Редактирует информацию о сотруднике
        /// </summary>
        /// <param name="employee">Объект сотрудника</param>
        /// <param name="parametr">Параметр для редактирования</param>
        public static bool Update(Employee employee, string parametr, string parameterValue) 
        {
            switch (parametr)
            {
                case "salary":                    
                    if(Decimal.TryParse(parameterValue, out decimal salary))
                    {
                        employee.BaseSalary = salary;
                    }
                    else
                    {
                        return false;                                             
                    }
                    break;
                case "post":                    
                    employee.Post = parameterValue;
                    break;
                case "type":                   
                    employee.Type = (parameterValue == "1") ? "FullTime" : "PartTime";     
                    break;
                default:
                    return false;
                    break;
            }
            Write();
            return true;
        }

        /// <summary>
        /// Удаляет сотрудика из списка
        /// </summary>
        /// <param name="id">Id сотрудника</param>
        /// <returns>true в случае успеха, иначе false </returns>
        /// <exception cref="DeliteIdException"></exception>
        public static bool Delete(Employee employee)
        {
            //Employee employee = Get(id);
            if (employee == null)
            {
                throw new DeliteIdException("Пользователь с таким Id не найден");
                return false;
            }            
            Staffs.Remove(employee);
            Write();                
            return true;         
            
        }
        /// <summary>
        /// Считывает данные из файла в список
        /// </summary>
        /// <returns>Список сотрудников</returns>
        public static bool LoadFromFile()
        {
            if (!File.Exists(Path)) return false;
            string[] lines = File.ReadAllLines(Path);
            if (lines.Length >= 1)
            {
                foreach (string line in lines)
                {
                    string[] parts = line.Split('|');
                    for (int i = 0; i < parts.Length; i = i + 5)
                    {
                        Employee employee;
                        if (parts [i+4] == "FullTime") 
                        {
                            employee = new FullTimeEmployee (Int32.Parse(parts[i]),parts[i + 1]);
                        }
                        else
                        {
                            employee = new PartTimeEmployee (Int32.Parse(parts[i]),parts[i + 1]);
                        }                    
                        employee.BaseSalary = Decimal.Parse(parts[i + 2]);
                        employee.Post = parts[i + 3];
                        employee.Type = parts[i + 4];
                        Staffs.Add(employee);
                    }

                }

            }
            return true;
        }

        /// <summary>
        /// Записывет данные в файл
        /// </summary>
        /// <returns>true в случае успеха, иначе false</returns>
        public static bool Write()
        {
            using StreamWriter sw = File.CreateText(Path);
            foreach (var item in Staffs)
            {
                sw.WriteLine($"{item.Id}|{item.Name}|{item.BaseSalary}|{item.Post}|{item.Type}");
            }
            return true;
        }
    }
}

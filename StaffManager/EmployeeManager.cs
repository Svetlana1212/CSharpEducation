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
    /// Менеджер сотрудников.
    /// </summary>
    public class IEmployeeManager : IEmployeeManager<Employee>
    {
        #region Поля и свойства

        /// <summary>
        /// Путь к файлу, где записаны сотрудники.
        /// </summary>
        private const string Path = "staff.txt";

        /// <summary>
        /// Коллекция для хранения списка сотрудиков.
        /// </summary>
        private static List <Employee> Employees = new List <Employee> ();

        /// <summary>
        /// Список сотрудников.
        /// </summary>
        private static List<Employee> Staffs
        {
            get
            {
                return Employees;
            }                   
            set
            {
                Staffs = new List<Employee>();
            }
        }

        #endregion

        #region IEmployeeManager

        public void Add(Employee employee)
        {
            if (Staffs.FirstOrDefault(u => u.Id == employee.Id) != null)
            {
                throw new EmployeeAlreadyAdded("Пользователь с таким Id уже есть");
            }
            else
            {
                Staffs.Add(employee);
                Write();
            }
        }

        public Employee Get(int id)
        {
            Employee user = Staffs.Find(item => item.Id == id);
            if (user == null)
            {
                throw new EmployeeNotFound ("Пользователь с таким Id не найден");
            }
            return user;
        }

        public void Update(Employee employee, string parametr, string parameterValue)
        {
            switch (parametr)
            {
                case "salary":
                    if (Decimal.TryParse(parameterValue, out decimal salary))
                    {
                        employee.BaseSalary = salary;
                    }
                    break;
                case "post":
                    employee.Post = parameterValue;
                    break;
                case "type":
                    employee.Type = (parameterValue == "1") ? "MonthlyRate" : "HourlyRate";
                    break;
                default:
                    break;
            }
            Write();
        }

        #endregion

        #region Методы

        /// <summary>
        /// Удалить сотрудика из списка.
        /// </summary>
        /// <param name="id">Id сотрудника.</param>
        /// <returns>true в случае успеха, иначе false. </returns>
        /// <exception cref="DeliteIdException"></exception>
        public bool Delete(Employee employee)
        {
            
            if (employee == null)
            {
                throw new EmployeeNotFound ("Пользователь с таким Id не найден");
                return false;
            }            
            Staffs.Remove(employee);
            Write();                
            return true;         
            
        }

        /// <summary>
        /// Считать данные из файла в список.
        /// </summary>
        /// <returns>Список сотрудников</returns>
        private bool LoadFromFile()
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
                        if (parts [i+4] == "MonthlyRate") 
                        {
                            employee = new FullTimeEmployee (Int32.Parse(parts[i]),parts[i + 1]);                            
                        }
                        else
                        {
                            employee = new PartTimeEmployee (Int32.Parse(parts[i]),parts[i + 1]);
                           
                        }                    
                        employee.BaseSalary = Decimal.Parse(parts[i + 2]);
                        employee.Post = parts[i + 3];
                        
                        Staffs.Add(employee);
                    }

                }

            }
            return true;
        }

        /// <summary>
        /// Записать данные.
        /// </summary>
        /// <returns>true в случае успеха, иначе false</returns>
        public bool Write()
        {
            using StreamWriter sw = File.CreateText(Path);
            foreach (var item in Staffs)
            {
                
                sw.WriteLine($"{item.Id}|{item.Name}|{item.BaseSalary}|{item.Post}|{item.Type}");
            }
            return true;
        }

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор.
        /// </summary>
        public IEmployeeManager()
        {
            LoadFromFile();
        }

        #endregion
    }
}

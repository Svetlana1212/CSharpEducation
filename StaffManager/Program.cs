using System;

namespace StaffManager
{
    /// <summary>
    /// Консольное приложение для управления списком сотрудников. 
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Обрабатать ввод пользователя, преобразовать в целое число.
        /// </summary>
        /// <param name="message">Сообщение пользователю для ввода числа</param>
        /// <returns></returns>
        static int ReturnInt(string message = "")
        {
            int input;            
            Console.WriteLine(message);            
            while (!Int32.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("Введено некорректное значение");
                Console.WriteLine(message);
            }            
            return input;
        }

        /// <summary>
        /// Создать нового сотрудника.
        /// </summary>
        /// <param name="id">id сотрудника </param>
        /// <returns>Возвращает объект класса Employee</returns>
        static Employee CreateEmployee(int id)
        {
            Console.WriteLine("Введите имя сотрудника");
            string name = Console.ReadLine();
            Console.WriteLine("Введите ставку для оплаты");
            decimal salary;
            if (!Decimal.TryParse(Console.ReadLine(), out salary))
            {
                Console.WriteLine("Введена некорректная ставка для оплаты");
                salary = 0;
            }    
            Console.WriteLine("Введите должность сотрудника");
            string post = Console.ReadLine();
            Console.WriteLine("Введите тип оплаты сотрудника(оклад:1/ставка:2)");
            string typeSalary = Console.ReadLine();
            Employee newEmployee;
            if (typeSalary == "1")
            {
                newEmployee = new FullTimeEmployee(id, name);                
            }
            else if (typeSalary == "2")
            {
                newEmployee = new PartTimeEmployee(id, name);                
            }
            else
            {
                Console.WriteLine("Введен некорректный типа олаты для сотрудника. Сотрудник не создан");                
                return null;
            }

            newEmployee.BaseSalary = salary;
            newEmployee.Post = post;
            return newEmployee;
        }
        /// <summary>
        /// Точка входа программы.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            bool programm = true;
            IEmployeeManager newEmployeeManager = new IEmployeeManager();
            do
            {
                Console.WriteLine(@"
1. Добавить сотрудника c полным описанием
2. Добавить сотрудника с частичным описанием
3. Получить информацию о сотруднике
4. Обновить данные сотрудника
5. Удалить сотрудника
6. Очистить экран
7. Выйти
Выберите действие:");
                int input = ReturnInt();
                switch (input)
                {
                    case 1:
                        int newId = ReturnInt("Введите id сотрудника (целое число): ");
                        Employee newEmployee = CreateEmployee(newId);
                        if (newEmployee != null)
                        {
                            try
                            {
                                newEmployeeManager.Add(newEmployee);
                                Console.WriteLine("Пользователь успешно добавлен");
                            }                            
                            catch (EmployeeAlreadyAdded e)
                            {
                                Console.WriteLine(e.Message); 
                            }
                            catch (NullReferenceException)
                            {
                                Console.WriteLine("Невозможно добавить данного пользователя");
                            }
                        }                        
                        break;
                    case 2:
                        int userId = ReturnInt("Введите id сотрудника (целое число): ");
                        Console.WriteLine("Введите имя сотрудика: ");
                        string name = Console.ReadLine();                        
                        Console.WriteLine("Введите тип оплаты сотрудника(оклад:1/ставка:2)");
                        string typeSalary = Console.ReadLine();
                        Employee newBriefEmployee = null;
                        if ((typeSalary != "1")&&(typeSalary != "2"))
                        {
                            Console.WriteLine("Введен некорректный тип олаты для сотрудника");
                            break;
                        } 
                        else if (typeSalary == "1")
                        {
                            newBriefEmployee = new FullTimeEmployee(userId, name);
                        }
                        else if (typeSalary == "2")
                        {
                            newBriefEmployee = new PartTimeEmployee(userId, name);                            
                        }
                        try 
                        {
                            newEmployeeManager.Add(newBriefEmployee);
                            Console.WriteLine("Пользователь успешно добавлен");
                        }
                        catch (EmployeeAlreadyAdded e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        catch (NullReferenceException)
                        {
                            Console.WriteLine("Невозможно добавить данного пользователя");
                        }
                        break;
                    case 3:
                        int viewedId = ReturnInt("Введите id сотрудника (целое число): ");
                        try
                        {
                            Employee viewedEmployee = newEmployeeManager.Get(viewedId);
                            if (viewedEmployee != null)
                            {
                                string salaryType = (viewedEmployee.Type == "MonthlyRate") ? "оклад" : "часовая ставка";
                                Console.WriteLine($"Сотрудник - Имя:{viewedEmployee.Name}, должность:{viewedEmployee.Post}, {salaryType}: {viewedEmployee.BaseSalary}");
                            }
                        }
                        catch (EmployeeNotFound e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case 4:
                        int updateId = ReturnInt("Введите id сотрудника (целое число): ");
                        try
                        {                            
                            Employee updateEmployee = newEmployeeManager.Get(updateId);
                            string salaryType = (updateEmployee.Type == "MonthlyRate") ? "оклад" : "часовая ставка";
                            Console.WriteLine($"Сотрудник - Имя:{updateEmployee.Name}, должность:{updateEmployee.Post}, {salaryType}: {updateEmployee.BaseSalary}");
                            Console.WriteLine(@"Выберите параметр, который нужно изменить
1.тип оплаты 
2.оклад(часовую ставку)
3.должность
");
                            int parametr = ReturnInt("Выберите значение: ");
                            string parametrType = string.Empty;
                            string parametrValue = string.Empty;
                            switch (parametr)
                            {
                                case 1:
                                    Console.WriteLine("Введите новый тип оплаты пользователя(оклад-1/часовая ставка -2): ");
                                    string type = Console.ReadLine();
                                    if (type == "1" || type == "2")
                                    {
                                        parametrType = "type";
                                        parametrValue = type;                                        
                                    }
                                    else
                                    {
                                        Console.WriteLine("Введен некорректный формат типа оплаты");
                                    }
                                    break;
                                case 2:
                                    Console.Write("Введите новую ставку для расчета заработной платы:");
                                    parametrValue = Console.ReadLine();
                                    parametrType = "baseSalary";                                 
                                    break;
                                case 3:
                                    Console.WriteLine("Введите новую должность:");
                                    parametrValue = Console.ReadLine();
                                    parametrType = "post";
                                    break;
                                default:
                                    Console.WriteLine("Некорректный ввод");
                                    break;
                                
                            }
                             newEmployeeManager.Update(updateEmployee, parametrType, parametrValue);
                             Console.WriteLine("Информация обновлена");                            
                        }
                        catch (EmployeeNotFound)
                        {
                            Console.WriteLine("Пользователь с таким id не найден");
                        }
                        break;
                    case 5:
                        int delId = ReturnInt("Введите id сотрудника (целое число): ");                        
                        try
                        {                            
                            Employee employee = newEmployeeManager.Get(delId);
                            Console.WriteLine($"Пользователь - {employee.Name}, {employee.Post} будет удален. Продолжить? (да/нет)");
                            if (Console.ReadLine() == "да")
                            {
                                newEmployeeManager.Delete(employee);
                                Console.WriteLine("Пользователь успешно удален");
                            }
                            
                        }
                        catch(EmployeeNotFound e)
                        {
                            Console.WriteLine(e.Message);
                            
                        }                    

                        break;
                    case 6:
                        Console.Clear();
                        break;
                    case 7:
                        programm = false;
                        break;
                    default:
                        Console.WriteLine("Некорректный ввод");
                        break;

                }

            } while (programm == true);
        }
    }
}

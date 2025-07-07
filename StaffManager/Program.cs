using System;

namespace StaffManager
{
    internal class Program
    {
        static int ReturnInt(string message = "")
        {
            int userInt;
            do
            {
                Console.WriteLine(message);
            }
            while (!Int32.TryParse(Console.ReadLine(), out userInt));
            return userInt;
        }
        /// <summary>
        /// Создает нового сотрудника
        /// </summary>
        /// <param name="id">id сотрудника </param>
        /// <returns>Возвращает объект класса Employee</returns>
        static Employee CreateEmployee(int id)
        {
            Console.WriteLine("Введите имя сотрудника");
            string name = Console.ReadLine();
            Console.WriteLine("Введите ставку для оплаты");
            decimal salary;
            try
            {
                salary = Decimal.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Неправильный формат ставки для оплаты");
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
                newEmployee.Type = "FullTime";
            }
            else if (typeSalary == "2")
            {
                newEmployee = new PartTimeEmployee(id, name);
                newEmployee.Type = "PartTime";
            }
            else
            {
                Console.WriteLine("Введен некорректный тип олаты для сотрудника");
                return null;
            }

            newEmployee.BaseSalary = salary;
            newEmployee.Post = post;
            return newEmployee;
        }

        static void Main(string[] args)
        {
            bool programm = true;
            EmployeeManager.LoadFromFile();
            
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
                int choice = ReturnInt();
                switch (choice)
                {
                    case 1:
                        int newId = ReturnInt("Введите id сотрудника: ");
                        Employee newEmployee = CreateEmployee(newId);
                        if (newEmployee != null)
                        {
                            try
                            {
                                EmployeeManager.Add(newEmployee);
                            }                            
                            catch (AddIdException)
                            {
                                Console.WriteLine("Пользователь с таким Id уже существует");
                            }
                            catch (NullReferenceException)
                            {
                                Console.WriteLine("Невозможно добавить данного пользователя");
                            }
                        }                                                                       
                        break;
                    case 2:
                        int userId = ReturnInt("Введите id сотрудника: ");
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
                            EmployeeManager.Add(newBriefEmployee);
                        }
                        catch (AddIdException)
                        {
                            Console.WriteLine("Пользователь с таким Id уже существует");
                        }
                        catch (NullReferenceException)
                        {
                            Console.WriteLine("Невозможно добавить данного пользователя");
                        }
                        break;
                    case 3:
                        int viewedId = ReturnInt("Введите id сотрудника: ");
                        try
                        {
                            Employee viewedEmployee = EmployeeManager.Get(viewedId);
                            if (viewedEmployee != null)
                            {
                                string salaryType = (viewedEmployee.Type == "FullTime") ? "оклад" : "часовая ставка";
                                Console.WriteLine($"Сотрудник - Имя:{viewedEmployee.Name}, должность:{viewedEmployee.Post}, {salaryType}: {viewedEmployee.BaseSalary}");
                            }
                        }
                        catch (SreachNullException)
                        {
                            Console.WriteLine("Пользователь с таким id не найден");
                        }
                        break;
                    case 4:
                        int updateId = ReturnInt("Введите id сотрудника: ");
                        try
                        {                            
                            Employee updateEmployee = EmployeeManager.Get(updateId);
                            string salaryType = (updateEmployee.Type == "FullTime") ? "оклад" : "часовая ставка";
                            Console.WriteLine($"Сотрудник - Имя:{updateEmployee.Name}, должность:{updateEmployee.Post}, {salaryType}: {updateEmployee.BaseSalary}");
                            Console.WriteLine(@"Выберите параметр, который нужно изменить
1.тип оплаты 
2.оклад(часовую ставку)
3.должность
");
                            switch (Int32.Parse(Console.ReadLine()))
                            {
                                case 1:
                                    EmployeeManager.Update(updateEmployee, "type");
                                    break;
                                case 2:
                                    EmployeeManager.Update(updateEmployee, "salary");
                                    break;
                                case 3:
                                    EmployeeManager.Update(updateEmployee, "post");
                                    break;
                                default:
                                    Console.WriteLine("Некорректный ввод");
                                    break;
                            }
                        }
                        catch (SreachNullException)
                        {
                            Console.WriteLine("Пользователь с таким id не найден");
                        }
                        break;
                    case 5:
                        int delId = ReturnInt("Введите id сотрудника: ");
                        try
                        {
                            EmployeeManager.Delete(delId);
                            Console.WriteLine("Пользователь успешно удален");
                        }
                        catch(DeliteIdException)
                        {
                            Console.WriteLine("Пользователь с таким Id не найден");
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

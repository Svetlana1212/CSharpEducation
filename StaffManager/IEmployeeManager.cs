using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffManager
{
    /// <summary>
    /// Интерфейс для управления сотрудниками
    /// </summary>
    /// <typeparam name="T">Тип объекта.</typeparam>
    internal interface IEmployeeManager<T>
    {
        /// <summary>
        /// Добавляет объект в коллекцию
        /// </summary>
        /// <param name="employee"></param>
        void Add(T employee) { }

        /// <summary>
        /// Находит объект в коллекции по параметру
        /// </summary>
        /// <param name="name">Имя сотрудника(параметр)</param>
        T Get(string name) { return default(T); }

        /// <summary>
        /// Редактирует параметры объекта
        /// </summary>
        /// <param name="employee">Объект сотрудника</param>
        void Update(T employee) { }
    }
}

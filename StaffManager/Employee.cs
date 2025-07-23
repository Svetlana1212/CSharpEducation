using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffManager
{
    /// <summary>
    /// Тип оплаты (оклад или часовая ставка).
    /// </summary>
    public enum SalaryType
    {
        MonthlyRate,
        HourlyRate
    }

    /// <summary>
    /// Сотрудик.
    /// </summary>
    public abstract class Employee
    {
        #region Поля и свойства

        /// <summary>
        /// Id сотрудика.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя сотрудика.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Ставка для начислеия зарлаты.
        /// </summary>
        public decimal BaseSalary { get; set; }

        /// <summary>
        /// Должность.
        /// </summary>
        public string Post { get; set; }

        /// <summary>
        /// Тип пользователя по оплате.
        /// </summary>
        public virtual SalaryType Type { get; set; }     

        #endregion

        #region Методы
        /// <summary>
        /// Начислить заработную плату.
        /// </summary>
        /// <returns>Возвращает заработную плату сотрудика.</returns>
        public decimal CalculateSalary()
        {
            return  BaseSalary;
        }
        
        #endregion

        #region Конструктор
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="id">Id сотрудника.</param>
        /// <param name="name">Имя сотрудника.</param>
        public Employee(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
        
        #endregion
    }
}

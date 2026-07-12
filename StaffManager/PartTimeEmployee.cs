using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffManager
{
    /// <summary>
    /// Сотрудник с начислением оплаты по часовой ставке.
    /// </summary>
    public class PartTimeEmployee : Employee
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
        /// Тип оплаты по умолчанию.
        /// </summary>
        private SalaryType BaseType = SalaryType.HourlyRate;

        /// <summary>
        /// Тип оплаты - свойство
        /// </summary>
        public override SalaryType Type
        {
            get { return BaseType; }
            set { BaseType = value; }

        }

        #endregion

        #region Методы

        /// <summary>
        /// Начислить заработную плату сотрудника по часовой ставке.
        /// </summary>
        /// <param name="workCount"></param>
        /// <returns></returns>
        public decimal CalculateSalary(decimal workCount)
        {
            decimal salary = Math.Round(this.BaseSalary * workCount);
            return salary;
        }

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="id">Id сотрудника </param>
        /// <param name="name">Имя сотрудника</param>
        public PartTimeEmployee(int id, string name) : base(id, name)
        {            
        }

        #endregion
    }
}

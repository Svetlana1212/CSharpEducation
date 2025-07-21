using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffManager
{
    /// <summary>
    /// Сотрудник с начислением заработной платы по окладу.
    /// </summary>
    public class FullTimeEmployee : Employee
    {
        #region Поля и свойства

        /// <summary>
        /// Id сотрудика.
        /// </summary>
        public int Id {get; set;}

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
        public string Post {  get; set; }

        /// <summary>
        /// Тип олаты по умолчанию.
        /// </summary>
        private string salaryType = SalaryType.MonthlyRate.ToString();

        /// <summary>
        /// Тип оплаты - свойство.
        /// </summary>
        public string Type
        {
            get { return salaryType; }
            set { salaryType = value; }
        }

        #endregion

        #region Методы
        /// <summary>
        /// Начислить заработную плату сотрудника по окладу.
        /// </summary>
        /// <param name="monthDaysCount">Количество дней в месяце.</param>
        /// <param name="workDaysCount">Количество отработанных дней.</param>
        /// <returns>Возвращает заработную плату, рассчитанную по окладу.</returns>
        public decimal CalculateSalary(int monthDaysCount, int workDaysCount)
        {
            decimal salary = Math.Round((this.BaseSalary / monthDaysCount) * workDaysCount, 2);
            return salary;
        }
        #endregion

        #region Конструктор
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="id">Id сотрудика.</param>
        /// <param name="name">Имя сотрудника.</param>
        public FullTimeEmployee(int id, string name) : base(id, name)
        {
            
        }

        #endregion
    }
}

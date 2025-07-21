using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class Class1
    {
        private enum SalaryType
        {
            MonthlyRate,
            HourlyRate
        }
        private SalaryType salaryType;
        public static SalaryType SalaryType
        {
            get { return salaryType; }
            set { salaryType = value; }
        }
    }
}

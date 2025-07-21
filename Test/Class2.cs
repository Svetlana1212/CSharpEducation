using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class Class2:Class1
    {
       
        
        private static SalaryType salaryType = SalaryType.MonthlyRate;
        public static SalaryType SalaryType
        {
            get { return salaryType; }
            set { salaryType = value; }
        }
        
    }
}

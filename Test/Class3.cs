using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class Class3:Class1
    {
        

        private static SalaryType salaryType = SalaryType.HourlyRate;
        public static SalaryType SalaryType
        {
            get { return salaryType; }
            set { salaryType = value; }
        }
        
    }
}

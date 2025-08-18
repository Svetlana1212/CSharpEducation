using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    internal class UserException
    {
        /// <summary>
        /// Возникает в случае если пользователь с таким id уже есть.
        /// </summary>
        public class EmployeeAlreadyAdded : Exception
        {
            public EmployeeAlreadyAdded() { }

            public EmployeeAlreadyAdded(string message) : base(message) { }
        }

        /// <summary>
        /// Возникает в случае если пользователь не найден.
        /// </summary>
        public class EmployeeNotFound : Exception
        {
            public EmployeeNotFound() { }

            public EmployeeNotFound(string message) : base(message) { }
        }
    }
}

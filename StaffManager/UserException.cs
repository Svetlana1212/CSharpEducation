using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StaffManager
{
    /// <summary>
    /// Возникает в случае если пользователь с таким id уже есть
    /// </summary>
    public class AddIdException: Exception
    {
        public AddIdException() { }
        public AddIdException(string message) : base(message) { }        
    }

    /// <summary>
    /// Возникает в случае если удаляемый пользователь не найден
    /// </summary>
    public class DeliteIdException: Exception
    {
        public DeliteIdException() { }
        public DeliteIdException(string message) : base(message) { }
    }

    /// <summary>
    /// Возникает в случае если пользователь не найден
    /// </summary>
    public class SreachNullException : Exception
    {
        public SreachNullException() { }
        public SreachNullException(string message) : base(message) { }
    }

}

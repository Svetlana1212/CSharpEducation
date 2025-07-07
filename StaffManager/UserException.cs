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

        public AddIdException(string message, Exception inner) : base(message, inner) { }

        protected AddIdException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    /// <summary>
    /// Возникает в случае если удаляемый пользователь не найден
    /// </summary>
    public class DeliteIdException: Exception
    {

    }

    /// <summary>
    /// Возникает в случае если пользователь не найден
    /// </summary>
    public class SreachNullException : Exception
    {

    }

}

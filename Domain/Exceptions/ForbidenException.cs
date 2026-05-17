using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class ForbidenException : Exception
    {
        public ForbidenException(string message) : base(message) { }
        public ForbidenException(string message, Exception innerException) : base(message, innerException) { }

    }
}

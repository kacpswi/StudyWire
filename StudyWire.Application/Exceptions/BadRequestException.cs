using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyWire.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string exceptionMessage) : base(exceptionMessage)
        {
            
        }
    }
}

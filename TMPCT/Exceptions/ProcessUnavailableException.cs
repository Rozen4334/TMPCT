using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMPCT.Exceptions
{
    public class ProcessUnavailableException : Exception
    {
        public ProcessUnavailableException(string message)
            : base(message)
        {

        }
    }
}

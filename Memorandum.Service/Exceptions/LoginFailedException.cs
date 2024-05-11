using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memorandum.Service.Exceptions
{
    public class LoginFailedException:Exception
    {
        public LoginFailedException(string message): base(message)
        { 

        }
    }
}

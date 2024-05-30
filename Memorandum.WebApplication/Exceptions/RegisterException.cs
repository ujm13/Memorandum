using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memorandum.Repository.Exceptions
{
    /// <summary>
    /// RegisterException
    /// </summary>
    public class RegisterException:Exception
    {
        /// <summary>
        /// RegisterException
        /// </summary>
        /// <param name="message"></param>
        public RegisterException(string message):base(message)
        { 
        }
    }
}

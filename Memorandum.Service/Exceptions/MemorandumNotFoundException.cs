using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memorandum.Service.Exceptions
{
    public class MemorandumNotFoundException:Exception
    {
        public MemorandumNotFoundException(string message):base(message) 
        {
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memorandum.Repository.Exceptions
{
    public class RegisterException:Exception
    {
        public RegisterException(string message):base(message)
        { 
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memorandum.Repository.Interfaces
{
    public interface MemorandumRepository
    {
        Task<bool> InsertAsync();
    }
}
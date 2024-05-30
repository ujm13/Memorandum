using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memorandum.Service.infrastructure.Helpers
{
    public interface IEncryptHelper
    {
        /// <summary>
        ///加密方法
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        string HashPassword(string password);
    }
}

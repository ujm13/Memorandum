using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Memorandum.Service.infrastructure.Helpers
{

    public class Sha256EncryptHelper: IEncryptHelper
    {
        private const string HashKey = "gfdg43424fh";
        /// <summary>
        /// 加密方法
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string HashPassword(string password)
        {
            var inputBytes = Encoding.UTF8.GetBytes(password + HashKey);
            var inputHash = SHA256.HashData(inputBytes);
            return Convert.ToHexString(inputHash);
        }
    }
}

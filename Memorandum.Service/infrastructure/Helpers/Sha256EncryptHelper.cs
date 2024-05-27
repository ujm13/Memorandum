using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Memorandum.Service.infrastructure.Helpers
{
    public class Sha256EncryptHelper
    {
        private const string HashKey = "gfdg43424fh";
        public static string HashPasswordSha256(string password)
        {
            var inputBytes = Encoding.UTF8.GetBytes(password + HashKey);
            var inputHash = SHA256.HashData(inputBytes);
            return Convert.ToHexString(inputHash);
        }
    }
}

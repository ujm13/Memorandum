using System.Security.Cryptography;
using System.Text;

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

using FluentAssertions;
using Memorandum.Service.infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Memorandum.ServiceTest
{
    public class Sha256EncryptHelperTest
    {
        private readonly IEncryptHelper _encryptHelper;
        public Sha256EncryptHelperTest() 
        {
            _encryptHelper=new Sha256EncryptHelper();
        }

        /// <summary>
        /// 加密密碼測試
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task HashPasswordTest_輸入密碼_回傳加密後字串() 
        {
            //Arrange
            var password = "Afgfh5445";

            //Actual
            var actual=_encryptHelper.HashPassword(password);

            //Assert
            actual.Should().Be("B5BE5874ACD11F496BA12E5C4716681CB00C5C7462E2A38C837A01D56CD62BA9");
        }
    }
}

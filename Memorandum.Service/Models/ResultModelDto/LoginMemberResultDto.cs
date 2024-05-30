using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memorandum.Service.Models.ResultModelDto
{
    public class LoginMemberResultDto
    {
        /// <summary>
        /// 帳號
        /// </summary>
        public string Account { get; set; }


        /// <summary>
        /// 使用者名稱
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
    }
}

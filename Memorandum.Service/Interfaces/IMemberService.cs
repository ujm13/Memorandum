using Memorandum.Service.Models.ParamaterModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memorandum.Service.Interfaces
{
    public interface IMemberService
    {

        /// <summary>
        /// 註冊會員
        /// </summary>
        /// <param name="registerMemberParameterDto"></param>
        /// <returns></returns>
        Task<bool> RegisterAsync(RegisterMemberParameterDto registerMemberParameterDto);
    }
}

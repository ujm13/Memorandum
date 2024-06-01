using Memorandum.Service.Models.ParamaterModelDto;
using Memorandum.Service.Models.ParameterDto;
using Memorandum.Service.Models.ResultModelDto;

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

        /// <summary>
        /// 會員登入
        /// </summary>
        /// <param name="loginMemberParameterDto"></param>
        /// <returns></returns>
        Task<LoginMemberResultDto> LoginAsync(LoginMemberParameterDto loginMemberParameterDto);


    }
}

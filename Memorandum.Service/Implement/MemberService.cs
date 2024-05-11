using MapsterMapper;
using Memorandum.Repository.Interfaces;
using Memorandum.Repository.Models.ParamaterModels;
using Memorandum.Service.Exceptions;
using Memorandum.Service.Interfaces;
using Memorandum.Service.Models.ParamaterModelDto;
using Memorandum.Service.Models.ParameterDto;
using Memorandum.Service.Models.ResultModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memorandum.Service.Implement
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public MemberService(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }


        /// <summary>
        /// 註冊會員
        /// </summary>
        /// <param name="parameterDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> RegisterAsync(RegisterMemberParameterDto parameterDto)
        {
            var parameterModel= _mapper.Map<RegisterMemberParameterModel>(parameterDto);


            var success=await _memberRepository.InsterAsync(parameterModel);
            if (!success) 
            {
                throw new RegisterException("會員註冊資料插入失敗");
            }
            return success;
        }


        /// <summary>
        /// 會員登入
        /// </summary>
        /// <param name="loginMemberParameterDto"></param>
        /// <returns></returns>

        public async Task<LoginMemberResultDto> LoginAsync(LoginMemberParameterDto loginMemberParameterDto)
        {
            var parameterDto = _mapper.Map<LoginMemberParameterModel>(loginMemberParameterDto);
            var member=await _memberRepository.GetAsync(parameterDto);
            if (member is null) 
            {
                throw new MemberNotFoundException("查無此會員");
            }

            if (member.Password != loginMemberParameterDto.Password) 
            {
                throw new LoginFailedException("會員密碼錯誤");
            }

            return new LoginMemberResultDto 
            {
                Account= member.Account,
                UserName= member.UserName,
                Email= member.Email,
            };

        }
    }
}

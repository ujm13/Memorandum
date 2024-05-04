using MapsterMapper;
using Memorandum.Repository.Exceptions;
using Memorandum.Repository.Interfaces;
using Memorandum.Repository.Models.ParamaterModels;
using Memorandum.Service.Interfaces;
using Memorandum.Service.Models.ParamaterModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memorandum.Service.Implement
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepositor;
        private readonly IMapper _mapper;


       /// <summary>
       /// 註冊會員
       /// </summary>
       /// <param name="registerMemberParameterDto"></param>
       /// <returns></returns>
       /// <exception cref="NotImplementedException"></exception>
       public async Task<bool> RegisterAsync(RegisterMemberParameterDto registerMemberParameterDto)
        {
            var parameterModel= _mapper.Map<RegisterMemberParameter>(registerMemberParameterDto);
            var success=await _memberRepositor.RegisterAsync(parameterModel);
            if (!success) 
            {
                throw new RegisterException("會員註冊資料插入失敗");
            }
            return success;
        }
    }
}

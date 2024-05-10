﻿using MapsterMapper;
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
            var parameterModel= _mapper.Map<RegisterMemberParameter>(parameterDto);

            //Db操作就用Db的名稱不用用商業邏輯的名稱，或是Create
            var success=await _memberRepository.InsterAsync(parameterModel);
            if (!success) 
            {
                throw new RegisterException("會員註冊資料插入失敗");
            }
            return success;
        }
    }
}
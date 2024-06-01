using MapsterMapper;
using Memorandum.Repository.Interfaces;
using Memorandum.Repository.Models.ParamaterModels;
using Memorandum.Service.Exceptions;
using Memorandum.Service.infrastructure.Helpers;
using Memorandum.Service.Interfaces;
using Memorandum.Service.Models.ParamaterModelDto;
using Memorandum.Service.Models.ParameterDto;
using Memorandum.Service.Models.ResultModelDto;

namespace Memorandum.Service.Implement
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;
        private readonly IEncryptHelper _encryptHelper;

        public MemberService(IMemberRepository memberRepository,
                            IMapper mapper, 
                            IEncryptHelper encryptHelper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
            _encryptHelper = encryptHelper;
        }

        /// <summary>
        /// 註冊會員
        /// </summary>
        /// <param name="parameterDto"></param>
        /// <returns></returns>
        public async Task<bool> RegisterAsync(RegisterMemberParameterDto parameterDto)
        {
            var parameterModel= _mapper.Map<RegisterMemberParameterModel>(parameterDto);
            parameterModel.Id =await GenerateIdAsync();
            parameterModel.Password = _encryptHelper.HashPassword(parameterModel.Password);

            var success=await _memberRepository.InsertAsync(parameterModel);
            if (!success) 
            {
                throw new RegisterException("會員註冊失敗");
            }

            return success;
        }


        /// <summary>
        /// 產生id
        /// </summary>
        /// <returns></returns>
        private async Task<Guid> GenerateIdAsync()
        {
            var id = Guid.NewGuid();
            var isExistId = await _memberRepository.IsExistIdAsync(id);
            while (isExistId)
            {
                id = Guid.NewGuid();
                isExistId = await _memberRepository.IsExistIdAsync(id);
            }

            return id;
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

            var encryptPassword = _encryptHelper.HashPassword(loginMemberParameterDto.Password);
            if (member.Password != encryptPassword) 
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

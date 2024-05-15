using Asp.Versioning;
using MapsterMapper;
using Memorandum.Service.Interfaces;
using Memorandum.Service.Models.ParamaterModelDto;
using Memorandum.Service.Models.ParameterDto;
using Memorandum.WebApplication.infrastructure.ExceptionFilters;
using Memorandum.WebApplication.Models.Parameters;
using Memorandum.WebApplication.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Memorandum.WebApplication.Controllers
{
  
    [ApiController]
    [Route("api/v{version:apiversion}/[controller]")]
    [ApiVersion("1.0")]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;
        private readonly IMapper _mapper;
        public MemberController(IMemberService memberService, IMapper mapper)
        {
            _memberService = memberService;
            _mapper = mapper;
        }

        /// <summary>
        /// 註冊會員
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost]
        [RegisterExceptionFilter] 
        public async Task<IActionResult> RegisterAsync(RegisterMemberParameter parameter)
        {
            var parameterDto = _mapper.Map<RegisterMemberParameterDto>(parameter);
            var success = await _memberService.RegisterAsync(parameterDto);
            if (!success)
            {
                return BadRequest(
                    new ResultViewModel
                    {
                        StatuesCode = 400,
                        StatusMessage = "註冊會員失敗"
                    });
            }
            return Ok(new ResultViewModel
            {
                StatuesCode = 200,
                StatusMessage = "註冊會員成功"
            });
        }

        /// <summary>
        /// 會員登入
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpGet]
        [LoginFailedExceptionFilter]
        [MemberNotFoundExceptionFilter]
        public async Task<IActionResult> LoginAsync(LoginMemberParameter parameter) 
        {
            var parameterDto = _mapper.Map<LoginMemberParameterDto>(parameter);
            var resultDto = await _memberService.LoginAsync(parameterDto);
            if (resultDto is null)
            {
                return BadRequest(new ResultViewModel
                {
                    StatuesCode = 400,
                    StatusMessage = "會員登入失敗"
                });
            }

            return Ok(new ResultViewModel
            {
                StatuesCode = 200,
                StatusMessage = "會員登入成功"
            });
        }


    }
}

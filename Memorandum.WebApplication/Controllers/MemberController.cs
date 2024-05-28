using Asp.Versioning;
using MapsterMapper;
using Memorandum.Service.Interfaces;
using Memorandum.Service.Models.ParamaterModelDto;
using Memorandum.Service.Models.ParameterDto;
using Memorandum.WebApplication.infrastructure.ExceptionFilters;
using Memorandum.WebApplication.Models.Parameters;
using Memorandum.WebApplication.Models.Parameters.Validator;
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
        [ProducesResponseType<ResultViewModel<bool>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ResultViewModel<bool>>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterMemberParameter parameter)
        {
            var validator = new RegisterMemberParameterValidator();
            var validationResult = await validator.ValidateAsync(parameter);

            if (validationResult.IsValid is false)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage);
                var resultMessage = string.Join(",", errorMessages);
                return BadRequest(new ResultViewModel<string>
                {
                    StatuesCode = 400,
                    StatusMessage = resultMessage,
                    Data = string.Empty
                }); // 直接回傳 400 + 錯誤訊息
            }


            var parameterDto = _mapper.Map<RegisterMemberParameterDto>(parameter);
            var success = await _memberService.RegisterAsync(parameterDto);
            return Ok(new ResultViewModel<bool>
            {
                StatuesCode = 200,
                StatusMessage = "註冊會員成功",
                Data = success
            });
        }

        /// <summary>
        /// 會員登入
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost]
        [LoginFailedExceptionFilter]
        [MemberNotFoundExceptionFilter]
        [ProducesResponseType<ResultViewModel<bool>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ResultViewModel<bool>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ResultViewModel<bool>>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> LoginAsync([FromBody]LoginMemberParameter parameter) 
        {
            var parameterDto = _mapper.Map<LoginMemberParameterDto>(parameter);
            var resultDto = await _memberService.LoginAsync(parameterDto);
            var resultViewModel = _mapper.Map<LoginMemberResultViewModel>(resultDto);
            return Ok(new ResultViewModel<LoginMemberResultViewModel>
            {
                StatuesCode = 200,
                StatusMessage = "會員登入成功",
                Data = resultViewModel
            });
        }


    }
}

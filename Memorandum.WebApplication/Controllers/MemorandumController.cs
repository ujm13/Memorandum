﻿using Asp.Versioning;
using MapsterMapper;
using Memorandum.Service.Interfaces;
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
    public class MemorandumController : ControllerBase
    {
        private readonly IMemorandumService _memorandumService;
        private readonly IMapper _mapper;
        public MemorandumController(IMemorandumService memorandumService, IMapper mapper)
        {
            _memorandumService = memorandumService;
            _mapper = mapper;
        }

        /// <summary>
        /// 新增代辦事項
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        [MemorandumExceptionFilter]
        public async Task<IActionResult> CreateAsync(CreateMemorandumParameter parameter)
        {
            var parameterDto = _mapper.Map<CreateMemorandumParameterDto>(parameter);
            var success = await _memorandumService.CreateAsync(parameterDto);
            if (!success)
            {
                return BadRequest(
                    new ResultViewModel<bool>
                    {
                        StatuesCode = 400,
                        StatusMessage = "新增代辦事項失敗",
                        Data= success
                    });
            }

            return Ok(new ResultViewModel<bool>
            {
                StatuesCode = 200,
                StatusMessage = "新增代辦事項成功",
                Data= success
            });
        }

    }
}
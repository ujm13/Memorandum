using Asp.Versioning;
using MapsterMapper;
using Memorandum.Service.Interfaces;
using Memorandum.Service.Models.ParameterDto;
using Memorandum.WebApplication.infrastructure.ExceptionFilters;
using Memorandum.WebApplication.Models.Parameters;
using Memorandum.WebApplication.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

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
        [HttpPost]
        [MemorandumExceptionFilter]
        [ProducesResponseType<ResultViewModel<bool>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ResultViewModel<bool>>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody]CreateMemorandumParameter parameter)
        {
            var parameterDto = _mapper.Map<CreateMemorandumParameterDto>(parameter);
            var success = await _memorandumService.CreateAsync(parameterDto);  
            
            return Ok(new ResultViewModel<bool>
            {
                StatuesCode = 200,
                StatusMessage = "新增代辦事項成功",
                Data= success
            });
        }

        /// <summary>
        /// 修改代辦事項
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [MemorandumExceptionFilter]
        [MemorandumNotFountExceptionFilter]
        [ProducesResponseType<ResultViewModel<bool>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ResultViewModel<bool>>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync([FromRoute]Guid id,[FromBody] UpdateMemorandumParameter parameter)
        {
            var parameterDto = _mapper.Map<UpdateMemorandumParameterDto>(parameter);
            var success = await _memorandumService.UpdateAsync(id,parameterDto);

            return Ok(new ResultViewModel<bool>
            {
                StatuesCode = 200,
                StatusMessage = "修改代辦事項成功",
                Data = success
            });
        }

        /// <summary>
        /// 取得代辦事項名細
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [MemorandumNotFountExceptionFilter]
        [ProducesResponseType<ResultViewModel<bool>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ResultViewModel<bool>>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDetailAsync([FromRoute]Guid id)
        {
            var result = await _memorandumService.GetDetailAsync(id);
            var resultViewModel = _mapper.Map<MemorandumResultViewModel>(result);

            return Ok(new ResultViewModel<MemorandumResultViewModel>
            {
                StatuesCode = 200,
                StatusMessage = "取得代辦事項明細成功",
                Data = resultViewModel
            });
        }


        /// <summary>
        /// 取得所有代辦事項
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _memorandumService.GetAllAsync();
            var resultViewModel = _mapper.Map<IEnumerable<MemorandumResultViewModel>>(result);
            return Ok(new ResultViewModel<IEnumerable<MemorandumResultViewModel>>
            {
                StatuesCode = 200,
                StatusMessage = "OK",
                Data = resultViewModel
            });
        }

        /// <summary>
        /// 刪除代辦事項
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [MemorandumExceptionFilter]
        [MemorandumNotFountExceptionFilter]
        [ProducesResponseType<ResultViewModel<bool>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ResultViewModel<bool>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ResultViewModel<bool>>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var success = await _memorandumService.DeleteAsync(id);
            return Ok(new ResultViewModel<bool>
            {
                StatuesCode = 200,
                StatusMessage = "刪除代辦事項成功",
                Data = success
            });
        }
    }

}


using MapsterMapper;
using Memorandum.Repository.Implement;
using Memorandum.Repository.Interfaces;
using Memorandum.Repository.Models.ParamaterModels;
using Memorandum.Service.Exceptions;
using Memorandum.Service.Interfaces;
using Memorandum.Service.Models.ParameterDto;
using Memorandum.Service.Models.ResultModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memorandum.Service.Implement
{
    public class MemorandumService : IMemorandumService
    {
        private readonly IMemorandumRepository _memorandumRepository;
        private readonly IMapper _mapper;
        public MemorandumService(IMemorandumRepository memorandumRepository, IMapper mapper)
        {
            _memorandumRepository = memorandumRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// 建立代辦事項
        /// </summary>
        /// <param name="parameterDto"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(CreateMemorandumParameterDto parameterDto)
        {
            var parameterDtoModel = _mapper.Map<InsertMemorandumParameterModel>(parameterDto);
            var success=await _memorandumRepository.InsertAsync(parameterDtoModel);
            if (!success)
            {
                throw new MemorandumException("新增代辦事項失敗");
            }
            return success;
        }

        /// <summary>
        /// 修改代辦事項
        /// </summary>
        /// <param name="parameterModel">The parameter model.</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(UpdateMemorandumParameterDto parameterDto)
        {
            var parameterModel = _mapper.Map<UpdateMemorandumParameterModel>(parameterDto);
            var success = await _memorandumRepository.UpdateAsync(parameterModel);
            if (!success)  
            {
                throw new MemorandumException("更新代辦事項失敗");
            }

            return success;
        }

        /// <summary>
        /// 取得明細
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MemorandumResultModelDto> GetDetailAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new MemorandumNotFountException($"id {id} is empty");
            }

            var resultDataModel = await _memorandumRepository.GetDetailAsync(id);

            if (resultDataModel is null)
            {
                throw new MemorandumNotFountException($"id {id} not found");
            }

            var resultModelDto = _mapper.Map<MemorandumResultModelDto>(resultDataModel);

            return resultModelDto;

        }
    }
}

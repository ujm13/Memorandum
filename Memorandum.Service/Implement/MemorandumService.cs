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
            parameterDtoModel.Id = await GenerateIDAsync();
            var success=await _memorandumRepository.InsertAsync(parameterDtoModel);
            if (!success)
            {
                throw new MemorandumException("新增代辦事項失敗");
            }
            return success;
        }

        /// <summary>
        /// 產生id
        /// </summary>
        /// <returns></returns>
        private async Task<Guid> GenerateIDAsync()
        {
            var id = Guid.NewGuid();
            var isExistId = await _memorandumRepository.IsExistIdAsync(id);
            while (isExistId)
            {
                id = Guid.NewGuid();
                isExistId = await _memorandumRepository.IsExistIdAsync(id);
            }
            return id;
        }

        /// <summary>
        /// 修改代辦事項
        /// </summary>
        /// <param name="parameterModel">The parameter model.</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Guid id,UpdateMemorandumParameterDto parameterDto)
        {
            if (id == Guid.Empty)
            {
                throw new MemorandumNotFountException($"id {id} is empty");
            }
            var isExistId = await _memorandumRepository.IsExistIdAsync(id);
            if (!isExistId) 
            {
                throw new MemorandumNotFountException($"id {id} not found");
            }
            var parameterModel = _mapper.Map<UpdateMemorandumParameterModel>(parameterDto);
            parameterModel.Id = id;
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


        /// <summary>
        /// 取得所有資料
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<MemorandumResultModelDto>> GetAllAsync()
        {
            var resultDataModel = await _memorandumRepository.GetAllAsync();
            var resultModelDto = _mapper.Map<IEnumerable<MemorandumResultModelDto>>(resultDataModel);

            return resultModelDto;
        }


        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new MemorandumNotFountException($"id {id} is empty");
            }

            var isExistId = await _memorandumRepository.IsExistIdAsync(id);
            if (!isExistId)
            {
                throw new MemorandumNotFountException($"id {id} not found");
            }

            var success = await _memorandumRepository.DeleteAsync(id);

            if (!success)
            {
                throw new MemorandumException("刪除代辦事項失敗");
            }
            return success;
        }
    }
}

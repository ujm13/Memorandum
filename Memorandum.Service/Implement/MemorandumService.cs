using MapsterMapper;
using Memorandum.Repository.Interfaces;
using Memorandum.Repository.Models.ParamaterModels;
using Memorandum.Service.Exceptions;
using Memorandum.Service.Interfaces;
using Memorandum.Service.Models.ParameterDto;
using Memorandum.Service.Models.ResultModelDto;

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
            parameterDtoModel.Id = await GenerateIdAsync();

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
        private async Task<Guid> GenerateIdAsync()
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
        /// <param name="parameterDto"></param>
        /// <returns></returns>
        /// <exception cref="MemorandumNotFoundException"></exception>
        /// <exception cref="MemorandumException"></exception>
        public async Task<bool> UpdateAsync(UpdateMemorandumParameterDto parameterDto)
        {
            if (parameterDto.Id == Guid.Empty)
            {
                throw new MemorandumNotFoundException($"id {parameterDto.Id} is empty");
            }

            var isExistId = await _memorandumRepository.IsExistIdAsync(parameterDto.Id);
            if (!isExistId) 
            {
                throw new MemorandumNotFoundException($"id {parameterDto.Id} not found");
            }

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
                throw new MemorandumNotFoundException($"id {id} is empty");
            }

            var resultDataModel = await _memorandumRepository.GetDetailAsync(id);

            if (resultDataModel is null)
            {
                throw new MemorandumNotFoundException($"id {id} not found");
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
                throw new MemorandumNotFoundException($"id {id} is empty");
            }

            var isExistId = await _memorandumRepository.IsExistIdAsync(id);
            if (!isExistId)
            {
                throw new MemorandumNotFoundException($"id {id} not found");
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

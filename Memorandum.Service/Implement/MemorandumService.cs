using MapsterMapper;
using Memorandum.Repository.Implement;
using Memorandum.Repository.Interfaces;
using Memorandum.Repository.Models.ParamaterModels;
using Memorandum.Service.Exceptions;
using Memorandum.Service.Interfaces;
using Memorandum.Service.Models.ParameterDto;
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
    }
}

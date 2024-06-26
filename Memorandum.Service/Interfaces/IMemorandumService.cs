﻿using Memorandum.Service.Models.ParameterDto;
using Memorandum.Service.Models.ResultModelDto;

namespace Memorandum.Service.Interfaces
{
    public interface IMemorandumService
    {
        /// <summary>
        /// 建立代辦事項
        /// </summary>
        /// <returns></returns>
        Task<bool> CreateAsync(CreateMemorandumParameterDto parameterDto);

        /// <summary>
        /// 修改代辦事項
        /// </summary>
        /// <param name="parameterModel">The parameter model.</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(UpdateMemorandumParameterDto parameterDto);

        /// <summary>
        /// 取得明細
        /// </summary>
        /// <param name="parameterModel">The parameter model.</param>
        /// <returns></returns>
        Task<MemorandumResultModelDto> GetDetailAsync(Guid id);

        /// <summary>
        /// 取得所有資料
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<MemorandumResultModelDto>> GetAllAsync();

        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Guid id);
    }
}

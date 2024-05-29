using Memorandum.Repository.Models.DataModels;
using Memorandum.Repository.Models.ParamaterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memorandum.Repository.Interfaces
{
    public interface IMemorandumRepository
    {
        /// <summary>
        /// 新增代辦事項
        /// </summary>
        /// <param name="parameterModel"></param>
        /// <returns></returns>
        Task<bool> InsertAsync(InsertMemorandumParameterModel parameterModel);

        /// <summary>
        /// 修改代辦事項
        /// </summary>
        /// <param name="parameterModel">The parameter model.</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(UpdateMemorandumParameterModel parameterModel);

        /// <summary>
        /// 取得明細
        /// </summary>
        /// <param name="parameterModel">The parameter model.</param>
        /// <returns></returns>
        Task<MemorandumDataModel> GetDetailAsync(Guid id);

        /// <summary>
        /// 取得所有資料
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<MemorandumDataModel>> GetAllAsync();

        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Guid id);

        /// <summary>
        /// 查詢Id是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsExistIdAsync(Guid id);
    }
}

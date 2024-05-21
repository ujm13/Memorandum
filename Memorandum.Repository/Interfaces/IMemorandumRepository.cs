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
    }
}
